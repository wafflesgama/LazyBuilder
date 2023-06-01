﻿#if !WP7 && !WP8
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using DigitalRune.Game;
using DigitalRune.Game.Input;
using DigitalRune.Geometry;
using DigitalRune.Graphics;
using DigitalRune.Graphics.PostProcessing;
using DigitalRune.Graphics.Rendering;
using DigitalRune.Graphics.SceneGraph;
using DigitalRune.Mathematics;
using DigitalRune.Mathematics.Algebra;
using DigitalRune.Mathematics.Interpolation;
using DigitalRune.Storages;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Sceelix.Designer.GUI.Controls;
using Sceelix.Designer.Renderer3D.Settings;
using Sceelix.Designer.Services;
using Sceelix.Designer.Settings;
using Sceelix.Designer.Utils;
using DirectionalLight = DigitalRune.Graphics.DirectionalLight;
using MathHelper = Microsoft.Xna.Framework.MathHelper;


namespace Sceelix.Designer.Renderer3D.GameObjects
{
    // This game object creates a sky using
    // - a milky way skybox
    // - the 9110 stars which are visible from earth,
    // - sun and moon with simulated position and moon phase
    // - atmospheric scattering to create the sky colors
    // - cloud layers
    //
    // This game object also creates ambient and direct light for the scene using 
    // simulated sunlight and moonlight. 
    //
    // The sky nodes are either 
    // A) added to the scene directly, or
    // B) rendered into a skybox (cube map) and a SkyboxNode is added to the scene.
    // Option B is more efficient if the sky is not animated because the cube map can be rendered
    // once and be reused until the sky changes. The cube map can also be used for reflections.
    // However, the quality of B depends on the resolution of the cube map.
    //
    // If the scene contains a Fog, the fog color is also updated by the simulation.

    public class DynamicSkyObject : GameObject
    {
        private const float MilkyWayLightScale = 0.005f;
        private const float StarfieldLightScale = 0.4f;
        private const float ScatteringSkyLightScale = 0.000005f;
        private const float SunlightScale = 0.0000013f;
        private const float MoonlightScale = SunlightScale*100f;

        private static readonly Vector3F LightPollution = new Vector3F(0.004f);
        private readonly IServiceLocator _services;
        private readonly bool _enableClouds;
        private readonly bool _enableCloudAnimation;
        
        //private readonly Sky3DSettings _skySettings;
        private IGraphicsService _graphicsService;
        
        private IScene _scene;
        private CameraObject _cameraObject;
        private SkyboxNode _milkyWayNode;
        private StarfieldNode _starfieldNode;
        private SkyObjectNode _sunNode;
        private SkyObjectNode _moonNode;
        private ScatteringSkyNode _scatteringSkyNode;
        private CloudLayerNode _cloudLayerNode0;
        private CloudLayerNode _cloudLayerNode1;
        private SceneNode _skyGroupNode;
        private LightNode _ambientLightNode;
        private LightNode _sunlightNode;
        private LightNode _moonlightNode;
        private SceneNode _lightGroupNode;
        private Ephemeris _ephemeris;
        private bool _isDirty = true;
        private SceneCaptureNode _sceneCaptureNode;
        private CloudMapRenderer _cloudMapRenderer;
        private SkyRenderer _skyRenderer;
        private ColorEncoder _colorEncoder;
        private SceneCaptureRenderer _sceneCaptureRenderer;
        private RenderContext _context;
        private bool _enableCloudShadows;
        private bool _enableAmbientLight = true;
        private DateTimeOffset _time = new DateTimeOffset(2014, 06, 01, 10, 0, 0, 0, TimeSpan.Zero); //DateTimeOffset.Now
        private SkyboxNode _skyboxNode;
        private int _cloudSeed;

        
        private readonly bool _cacheSky;
        
        private bool _rotateSky = false;



        public DynamicSkyObject(IServiceLocator services, bool enableClouds = true, bool enableCloudShadows = true, int cloudSeed = 1000)
        {
            _services = services;           
            
            _graphicsService = _services.Get<IGraphicsService>();
            _scene = _services.Get<IScene>();

            Name = "DynamicSky";
            _enableClouds = enableClouds;
            _enableCloudAnimation = false;
            EnableCloudShadows = enableCloudShadows;

            _cloudSeed = cloudSeed;

            //both of these should be turned true if the sky is to be rotated again
            _cacheSky = false;
            _rotateSky = false;
        }



        public bool RotateSky
        {
            get { return _rotateSky; }
            set { _rotateSky = value; }
        }



        // The direction to the sun in world space.
        public Vector3F SunDirection
        {
            get;
            private set;
        }



        // The sun light intensity (RGB).
        public Vector3F SunLight
        {
            get;
            private set;
        }



        // The ambient light intensity (RGB).
        public Vector3F AmbientLight
        {
            get;
            private set;
        }




        // If true, the cloud map is applied to the directional light to create shadows.
        public bool EnableCloudShadows
        {
            get { return _enableCloudShadows; }
            set
            {
                if (_enableCloudShadows == value)
                    return;

                _enableCloudShadows = value;
                _isDirty = true;
            }
        }



        public bool EnableAmbientLight
        {
            get { return _enableAmbientLight; }
            set
            {
                _enableAmbientLight = value;
                if (_ambientLightNode != null)
                    _ambientLightNode.IsEnabled = value;
            }
        }



        // The current date, time and time zone.
        public DateTimeOffset Time
        {
            get { return _time; }
            set
            {
                if (_time == value)
                    return;

                _time = value;
                _isDirty = true;
            }
        }



        protected override void OnLoad()
        {

            var content = _services.Get<ContentManager>();
            var gameObjectService = _services.Get<IGameObjectService>();

            // Get camera game object.
            _cameraObject = (CameraObject) gameObjectService.Objects["Camera"];

            // Create SkyNodes.
            InitializeSky(content);

            // Create LightNodes
            InitializeLights();

            // Optionally, the sky is captured into a cube map and the cube map is added
            // to the scene instead of all the sky nodes.
            if (_cacheSky)
            {
                // We use a SceneCaptureNode to create the cube map.
                // The cube map uses RGBM encoding to store HDR values. 
                var renderToTexture = new RenderToTexture
                {
                    Texture = new RenderTargetCube(_graphicsService.GraphicsDevice, 1024, true, SurfaceFormat.Color, DepthFormat.None),
                };
                var projection = new PerspectiveProjection();
                
                projection.SetFieldOfView(ConstantsF.PiOver2, 1, 1, 10);
                _sceneCaptureNode = new SceneCaptureNode(renderToTexture)
                {
                    // Note: The scene is captured at the origin (0, 0, 0).
                    CameraNode = new CameraNode(new Camera(projection))
                    {
                        PoseLocal = _rotateSky ? new Pose(DigitalRuneUtils.ZUpToYUpRotationMatrix) : Pose.Identity
                    }
                };

                _skyboxNode = new SkyboxNode
                {
                    Encoding = ColorEncoding.Rgbm,
                    Texture = (TextureCube) renderToTexture.Texture
                };
                _scene.Children.Add(_skyboxNode);
            }

            // The Ephemeris class computes the positions of the sun and the moon as seen
            // from any place on the earth.
            _ephemeris = new Ephemeris
            {
                // Seattle, Washington
                //Latitude = 47,
                //Longitude = 122,
                //Altitude = 100

                // Vienna
                //Latitude = 48,
                //Longitude = -16,
                //Altitude = 0

                // Equator
                Latitude = 0,
                Longitude = 0,
                Altitude = 0,
            };

            // Update the positions of sky objects and the lights.
            UpdateSky();

            // Create cube map.
            if (_cacheSky)
                UpdateCubeMap(TimeSpan.Zero);
        }



        private void InitializeSky(ContentManager content)
        {
            // This scene node is used as the parent of all sky nodes created here.
            _skyGroupNode = new SceneNode
            {
                Name = "ScatteringSkyGroup",
                Children = new SceneNodeCollection()
            };

            // If we render the sky into a cube map, then we do not add the sky nodes to the scene.
            if (!_cacheSky)
                _scene.Children.Add(_skyGroupNode);

            // Add a skybox with milky way cube map.
            _milkyWayNode = new SkyboxNode(content.Load<TextureCube>("Deferred/Sky/MilkyWay"))
            {
                Color = new Vector3F(MilkyWayLightScale),
            };
            _skyGroupNode.Children.Add(_milkyWayNode);

            // Add a starfield with all visible stars from a star catalog.
            InitializeStarfield();
            _skyGroupNode.Children.Add(_starfieldNode);

            // Add a node which draws a sun disk.
            _sunNode = new SkyObjectNode
            {
                GlowExponent0 = 4000,
            };
            _skyGroupNode.Children.Add(_sunNode);

            // Add a node which draws a moon texture including moon phase.
            _moonNode = new SkyObjectNode
            {
                Texture = new PackedTexture(content.Load<Texture2D>("Deferred/Sky/Moon")),
                LightWrap = 0.1f,
                LightSmoothness = 1,
                AngularDiameter = new Vector2F(MathHelper.ToRadians(5)),

                // Disable glow.
                GlowColor0 = new Vector3F(0),
                GlowColor1 = new Vector3F(0),
            };
            _skyGroupNode.Children.Add(_moonNode);

            // Add a ScatteringSky which renders the sky colors using a physically-based model.
            _scatteringSkyNode = new ScatteringSkyNode
            {
                SunIntensity = 1f,

                // Set a base color to get a dark blue instead of a pitch black night.
                BaseHorizonColor = new Vector3F(0.043f, 0.090f, 0.149f)*0.01f,
                BaseZenithColor = new Vector3F(0.024f, 0.051f, 0.102f)*0.01f,
                BaseColorShift = 0.5f
            };
            _skyGroupNode.Children.Add(_scatteringSkyNode);

            if (_enableClouds)
                InitializeClouds();

            // The following scene nodes inherit from SkyNode and are rendered by a SkyRenderer
            // in the DeferredLightingScreen. We have to set the DrawOrder to render them from
            // back to front.
            _milkyWayNode.DrawOrder = 0;
            _starfieldNode.DrawOrder = 1;
            _sunNode.DrawOrder = 2;
            _moonNode.DrawOrder = 3;
            _scatteringSkyNode.DrawOrder = 4;
            if (_enableClouds)
            {
                _cloudLayerNode1.DrawOrder = 5;
                _cloudLayerNode0.DrawOrder = 6;
            }
        }



        private void InitializeStarfield()
        {
            // Load star positions and luminance from file with 9110 predefined stars.
            const int numberOfStars = 9110;
            var stars = new Star[numberOfStars];
            var storage = _services.Get<IStorage>();
            using (var reader = new BinaryReader(storage.OpenFile("Deferred/Sky/Stars.bin")))
            {
                for (int i = 0; i < numberOfStars; i++)
                {
                    stars[i].Position.X = reader.ReadSingle();
                    stars[i].Position.Y = reader.ReadSingle();
                    stars[i].Position.Z = reader.ReadSingle();

                    // To avoid flickering, the star size should be >= 2.8 pix.
                    stars[i].Size = 2.8f;

                    stars[i].Color.X = reader.ReadSingle()*StarfieldLightScale;
                    stars[i].Color.Y = reader.ReadSingle()*StarfieldLightScale;
                    stars[i].Color.Z = reader.ReadSingle()*StarfieldLightScale;
                }

                Debug.Assert(reader.PeekChar() == -1, "End of file should be reached.");
            }

            // Note: To improve performance, we could sort stars by brightness and 
            // remove less important stars.

            _starfieldNode = new StarfieldNode
            {
                Color = new Vector3F(1),
                Stars = stars
            };
        }



        private void InitializeClouds()
        {
            // Add a CloudLayerNode which renders dynamic clouds.
            // This layer represents dense, lit clouds at low altitude.
            var cloudMap0 = new LayeredCloudMap
            {
                Density = 15,
                Coverage = 0.6f,
                Size = 2048,
                Seed = _cloudSeed,
            };

            _cloudLayerNode0 = new CloudLayerNode(cloudMap0)
            {
                SkyCurvature = 0.9f,
                TextureMatrix = CreateScale(0.5f),
                ForwardScatterExponent = 10,
                ForwardScatterScale = 10f,
                ForwardScatterOffset = 0.3f,
                NumberOfSamples = 16,
                HorizonFade = 0.03f
            };

            // Define the layers which are combined to a single CloudMap.Texture in the CloudMapRenderer.
            var scale = CreateScale(0.2f);
            float animationScale = _enableCloudAnimation ? 0.01f : 0.0f;
            cloudMap0.Layers[0] = new CloudMapLayer(null, scale*CreateScale(1), -0.5f, 1, 1*animationScale);
            cloudMap0.Layers[1] = new CloudMapLayer(null, scale*CreateScale(1.7f), -0.5f, 1f/2f, 5*animationScale);
            cloudMap0.Layers[2] = new CloudMapLayer(null, scale*CreateScale(3.97f), -0.5f, 1f/4f, 13*animationScale);
            cloudMap0.Layers[3] = new CloudMapLayer(null, scale*CreateScale(8.1f), -0.5f, 1f/8f, 27*animationScale);
            cloudMap0.Layers[4] = new CloudMapLayer(null, scale*CreateScale(16, 17), -0.5f, 1f/16f, 43*animationScale);
            cloudMap0.Layers[5] = new CloudMapLayer(null, scale*CreateScale(32, 31), -0.5f, 1f/32f, 77*animationScale);
            cloudMap0.Layers[6] = new CloudMapLayer(null, scale*CreateScale(64, 67), -0.5f, 1f/64f, 127*animationScale);
            cloudMap0.Layers[7] = new CloudMapLayer(null, scale*CreateScale(128, 127), -0.5f, 1f/64f, 200*animationScale);
            _skyGroupNode.Children.Add(_cloudLayerNode0);

            // Add a second CloudLayerNode which renders dynamic clouds.
            // This layer represents light clouds at high altitude.
            var cloudMap1 = new LayeredCloudMap
            {
                Density = 2,
                Coverage = 0.4f,
                Size = 1024,
                Seed = _cloudSeed + 1000
            };

            _cloudLayerNode1 = new CloudLayerNode(cloudMap1)
            {
                SkyCurvature = 0.9f,
                TextureMatrix = CreateScale(0.5f),
                ForwardScatterExponent = 10,
                ForwardScatterScale = 10f,
                ForwardScatterOffset = 0f,
                NumberOfSamples = 0, // No samples to disable lighting calculations.
                HorizonFade = 0.03f,
                Alpha = 0.5f, // Make these clouds more transparent.
            };

            scale = CreateScale(0.25f);
            cloudMap1.Layers[0] = new CloudMapLayer(null, scale*CreateScale(1), -0.5f, 1, 0);
            cloudMap1.Layers[1] = new CloudMapLayer(null, scale*CreateScale(1.7f), -0.5f, 1f/2f, 0);
            cloudMap1.Layers[2] = new CloudMapLayer(null, scale*CreateScale(3.97f), -0.5f, 1f/4f, 0);
            cloudMap1.Layers[3] = new CloudMapLayer(null, scale*CreateScale(8.1f), -0.5f, 1f/8f, 0);
            cloudMap1.Layers[4] = new CloudMapLayer(null, scale*CreateScale(16, 17), -0.5f, 1f/16f, 0);
            cloudMap1.Layers[5] = new CloudMapLayer(null, scale*CreateScale(32, 31), -0.5f, 1f/32f, 0);
            cloudMap1.Layers[6] = new CloudMapLayer(null, scale*CreateScale(64, 67), -0.5f, 1f/64f, 0);
            cloudMap1.Layers[7] = new CloudMapLayer(null, scale*CreateScale(128, 127), -0.5f, 1f/128f, 0);
            _skyGroupNode.Children.Add(_cloudLayerNode1);
        }



        private void InitializeLights()
        {
            // This scene node is used as the parent of all light nodes created here.
            _lightGroupNode = new SceneNode
            {
                Name = "SkyGroup",
                Children = new SceneNodeCollection()
            };
            _scene.Children.Add(_lightGroupNode);

            // Add an ambient light.
            var ambientLight = new AmbientLight
            {
                HemisphericAttenuation = 0.5f,
                Intensity = 0.5f,
            };
            _ambientLightNode = new LightNode(ambientLight)
            {
                Name = "Ambient",
                IsEnabled = _enableAmbientLight,
            };
            _lightGroupNode.Children.Add(_ambientLightNode);

            /*_lightGroupNode.Children.Add(new LightNode(new AmbientLight()
            {
                Intensity = 0.01f,
            }));*/

            // Add a directional light for the sun.
            _sunlightNode = new LightNode(new DirectionalLight()
            {
                /*Color = new Vector3F(1, 0.9607844f, 0.9078432f),
                HdrScale = 0.35f,*/
                /*SpecularIntensity = 1,*/
                DiffuseIntensity = 1.3f,
            })
            {
                Name = "Sunlight",
                Priority = 10, // This is the most important light.

                // This light uses Cascaded Shadow Mapping.
                Shadow = new CascadedShadow
                {
                    PreferredSize = 1024,
                    Prefer16Bit = true,
                    MinLightDistance = 1000,
                    Distances = new Vector4F(4, 50, 200, 1000),
                }
            };
            _lightGroupNode.Children.Add(_sunlightNode);

            // Add a directional light for the moonlight.
            _moonlightNode = new LightNode(new DirectionalLight())
            {
                Name = "Moonlight",
                Priority = 5
            };
            _lightGroupNode.Children.Add(_moonlightNode);
        }



        protected override void OnUnload()
        {
            // Detach the scene nodes from the scene.
            if (_skyGroupNode.Parent != null)
                _skyGroupNode.Parent.Children.Remove(_skyGroupNode);

            _lightGroupNode.Parent.Children.Remove(_lightGroupNode);

            // Dispose allocated resources.
            _skyGroupNode.Dispose(false);
            _lightGroupNode.Dispose(false);

            if (_enableClouds)
            {
                _cloudLayerNode0.CloudMap.Dispose();
                _cloudLayerNode1.CloudMap.Dispose();
            }

            if (_cacheSky)
            {
                _skyboxNode.Parent.Children.Remove(_skyboxNode);
                _sceneCaptureNode.RenderToTexture.Texture.Dispose();
                _sceneCaptureNode.Dispose(false);
                _cloudMapRenderer.Dispose();
                _skyRenderer.Dispose();
                _colorEncoder.Dispose();
                _sceneCaptureRenderer.Dispose();
            }

            // Set references to null.
            _cameraObject = null;
            _skyGroupNode = null;
            _lightGroupNode = null;
            _milkyWayNode = null;
            _starfieldNode = null;
            _sunNode = null;
            _moonNode = null;
            _scatteringSkyNode = null;
            _ambientLightNode = null;
            _sunlightNode = null;
            _moonlightNode = null;
            _cloudLayerNode0 = null;
            _cloudLayerNode1 = null;
            _sceneCaptureNode = null;
            _skyboxNode = null;
            _cloudMapRenderer = null;
            _skyRenderer = null;
            _colorEncoder = null;
            _sceneCaptureRenderer = null;
        }



        public void UpdateTime(int hour, int minutes)
        {
            _time = new DateTimeOffset(_time.Year, _time.Month, _time.Day, hour, minutes, 0, TimeSpan.Zero);
            _isDirty = true;
            _services.Get<RenderTargetControl>().ShouldRender = true;
        }



        protected override void OnUpdate(TimeSpan deltaTime)
        {
            // <Up> / <Down> -> Change time of day.
            
            

            if (_enableClouds && _enableCloudAnimation)
            {
                _isDirty = true;

                // Scroll cloud textures to simulate wind.
                var cloudMap0 = (LayeredCloudMap) _cloudLayerNode0.CloudMap;
                for (int i = 0; i < 8; i++)
                {
                    var matrix = cloudMap0.Layers[i].TextureMatrix;

                    // Increase translation to scroll texture. Every second layers scrolls into the
                    // orthogonal direction to create a chaotic animation.
                    if (i%2 == 0)
                        matrix.M02 += (float) deltaTime.TotalSeconds*(1)*0.002f*matrix.M00;
                    else
                        matrix.M12 += (float) deltaTime.TotalSeconds*(1)*0.002f*matrix.M11;

                    cloudMap0.Layers[i].TextureMatrix = matrix;
                }

                var cloudMap1 = (LayeredCloudMap) _cloudLayerNode1.CloudMap;
                for (int i = 0; i < 8; i++)
                {
                    var matrix = cloudMap1.Layers[i].TextureMatrix;

                    // Increase translation to scroll texture. Every second layers scrolls into the
                    // orthogonal direction to create a chaotic animation.
                    if (i%2 == 0)
                        matrix.M02 += (float) deltaTime.TotalSeconds*(1)*0.001f*matrix.M00;
                    else
                        matrix.M12 += (float) deltaTime.TotalSeconds*(1)*0.001f*matrix.M11;

                    cloudMap1.Layers[i].TextureMatrix = matrix;
                }
            }

            if (_isDirty)
            {
                UpdateSky();

                if (_cacheSky)
                    UpdateCubeMap(deltaTime);

                _isDirty = false;
            }
        }



        private void UpdateSky()
        {
            // Update ephemeris model.
            _ephemeris.Time = _time;
            _ephemeris.Update();

            // Update rotation of milky way. We also need to add an offset because the 
            // cube map texture is rotated.
            _milkyWayNode.PoseWorld = new Pose(
                (Matrix33F) _ephemeris.EquatorialToWorld.Minor
                *Matrix33F.CreateRotationZ(ConstantsF.PiOver2 + -0.004f)
                *Matrix33F.CreateRotationX(ConstantsF.PiOver2 + -0.002f));

            // Update rotation of stars.
            _starfieldNode.PoseWorld = new Pose((Matrix33F) _ephemeris.EquatorialToWorld.Minor);

            // Update direction of sun.
            SunDirection = (Vector3F) _ephemeris.SunDirectionRefracted;
            var sunUp = SunDirection.Orthonormal1;
            _sunNode.LookAt(SunDirection, sunUp);

            // Update direction of moon.
            var moonDirection = (Vector3F) _ephemeris.MoonPosition.Normalized;
            var moonUp = (Vector3F) _ephemeris.EquatorialToWorld.TransformDirection(Vector3D.Up);
            _moonNode.LookAt(moonDirection, moonUp);

            // The moon needs to know the sun position and brightness to compute the moon phase.
            _moonNode.SunDirection = (Vector3F) _ephemeris.SunPosition.Normalized;
            _moonNode.SunLight = Ephemeris.ExtraterrestrialSunlight*SunlightScale;

            // The ScatteringSky needs the sun direction and brightness to compute the sky colors.
            _scatteringSkyNode.SunDirection = SunDirection;
            _scatteringSkyNode.SunColor = Ephemeris.ExtraterrestrialSunlight*ScatteringSkyLightScale;

            // Update the light directions.
            _sunlightNode.LookAt(-SunDirection, sunUp);
            _moonlightNode.LookAt(-moonDirection, moonUp);

            // The ScatteringSkyNode can compute the actual sunlight.
            SunLight = _scatteringSkyNode.GetSunlight();

            // Undo the ScatteringSkyLightScale and apply a custom scale for the sunlight.
            SunLight = SunLight/ScatteringSkyLightScale*SunlightScale;

            // The ScatteringSkyNode can also compute the ambient light by sampling the 
            // sky hemisphere.
            AmbientLight = _scatteringSkyNode.GetAmbientLight(256);
            AmbientLight = AmbientLight/ScatteringSkyLightScale*SunlightScale;

            // Desaturate the ambient light to avoid very blue shadows.
            AmbientLight = InterpolationHelper.Lerp(
                new Vector3F(Vector3F.Dot(AmbientLight, GraphicsHelper.LuminanceWeights)),
                AmbientLight,
                0.5f);

            // The Ephemeris model can compute the actual moonlight.
            Vector3F moonlight, moonAmbient;
            Ephemeris.GetMoonlight(
                _scatteringSkyNode.ObserverAltitude,
                2.2f,
                _ephemeris.MoonPosition,
                (float) _ephemeris.MoonPhaseAngle,
                out moonlight,
                out moonAmbient);
            moonlight *= MoonlightScale;
            moonAmbient *= MoonlightScale;

            // Scale sun light to 0 at horizon.
            var directionalLightColor = SunLight;
            directionalLightColor *= MathHelper.Clamp(SunDirection.Y/0.1f, 0, 1);

            var directionalLight = ((DirectionalLight) _sunlightNode.Light);
            directionalLight.Color = directionalLightColor;
            ((DirectionalLight) _moonlightNode.Light).Color = moonlight;
            ((AmbientLight) _ambientLightNode.Light).Color = AmbientLight + moonAmbient + LightPollution;

            // Use the sunlight color to create a bright sun disk.
            var sunDiskColor = SunLight;
            sunDiskColor.TryNormalize();
            _sunNode.GlowColor0 = sunDiskColor*Ephemeris.ExtraterrestrialSunlight.Length*SunlightScale*10;

            if (_enableClouds)
            {
                // Update lighting info of cloud layer nodes.
                _cloudLayerNode0.SunDirection = SunDirection;
                _cloudLayerNode0.SunLight = SunLight;
                _cloudLayerNode0.AmbientLight = AmbientLight;

                // The second cloud layer uses simple unlit clouds (only ambient lighting).
                _cloudLayerNode1.SunDirection = SunDirection;
                _cloudLayerNode1.SunLight = Vector3F.Zero;
                _cloudLayerNode1.AmbientLight = AmbientLight + SunLight;

                // Use the cloud map as the texture for the directional light to create cloud shadows.
                if (EnableCloudShadows)
                    directionalLight.Texture = _cloudLayerNode0.CloudMap.Texture;
                else
                    directionalLight.Texture = null;

                // Compute a texture offset so that the current sun position and the projected cloud 
                // shadows match.
                // Since sky dome is always centered on the camera, position the sunlight node in 
                // line with the camera.
                var cameraPosition = _cameraObject.CameraNode.PoseWorld.Position;
                var upVector = Vector3F.AreNumericallyEqual(SunDirection, Vector3F.UnitZ) ? Vector3F.UnitY : Vector3F.UnitZ;
                _sunlightNode.LookAt(cameraPosition + SunDirection, cameraPosition, upVector);

                // Choose a scale for the cloud shadows.
                directionalLight.TextureScale = new Vector2F(-1000);

                // Get the scaled texture offset for the sun direction.
                directionalLight.TextureOffset = _cloudLayerNode0.GetTextureCoordinates(SunDirection)*
                                                 directionalLight.TextureScale;
            }

            /*_milkyWayNode.PoseLocal *= _rotationPose;
            _sunlightNode.PoseWorld *= _rotationPose;
            _starfieldNode.PoseLocal *= _rotationPose;
            _scatteringSkyNode.PoseLocal *= _rotationPose;
            _moonNode.PoseLocal *= _rotationPose;*/

            // The ScatteringSkyNode can also estimate a fog color by sampling the horizon colors.
            Vector3F fogColor = _scatteringSkyNode.GetFogColor(256);

            // Desaturate the fog color.
            fogColor = InterpolationHelper.Lerp(
                new Vector3F(Vector3F.Dot(fogColor, GraphicsHelper.LuminanceWeights)),
                fogColor,
                0.3f);

            // Find any FogNode in the scene and update its fog color.
            foreach (var fogNode in ((Scene) _scene).GetSubtree().OfType<FogNode>())
            {
                var fog = fogNode.Fog;
                fog.Color0 = fog.Color1 = new Vector4F(fogColor, 1);

                // We can use the ScatteringSymmetry to create a highlight in the sky direction.
                // To make the fog color more interesting we set higher forward scattering for
                // red and less for blue.
                fog.ScatteringSymmetry = new Vector3F(0.26f, 0.24f, 0.22f);
            }

            // TODO: If the fog is dense, reduce the direct light and increase the ambient light.
        }



        private void UpdateCubeMap(TimeSpan deltaTime)
        {
            if (_cloudMapRenderer == null)
            {
                // This is the first call of UpdateCubeMap. Create the renderers which
                // we need to render the cube map.

                // The CloudMapRenderer creates and animates the LayeredCloudMaps.
                _cloudMapRenderer = new CloudMapRenderer(_graphicsService);

                // The SkyRenderer renders SkyNodes.
                _skyRenderer = new SkyRenderer(_graphicsService);

                // We use a ColorEncoder to encode a HDR image in a normal Color texture.
                _colorEncoder = new ColorEncoder(_graphicsService)
                {
                    SourceEncoding = ColorEncoding.Rgb,
                    TargetEncoding = _skyboxNode.Encoding,
                };

                // The SceneCaptureRenderer handles SceneCaptureNodes. We need to specify
                // a render callback which is called in SceneCaptureNode.Render().
                _sceneCaptureRenderer = new SceneCaptureRenderer(RenderSky);

                // Normally, the render context is managed by the graphics service. 
                // When we call renderer outside of a GraphicsScreen, we have to use our
                // own context instance.
                _context = new RenderContext(_graphicsService);
            }

            // Update render context. For off-screen rendering we must at least update 
            // the time properties (e.g. for cloud animations).
            _context.DeltaTime = deltaTime;
            _context.Time += deltaTime;
            _context.Frame++;

            // Create cloud maps.
            _cloudMapRenderer.Render(_skyGroupNode.Children, _context);

            // Capture sky in cube map.
            _sceneCaptureRenderer.Render(_sceneCaptureNode, _context);
        }



        // Renders the sky. This method is the RenderCallback of the SceneCaptureNode.
        private void RenderSky(RenderContext context)
        {
            var graphicsDevice = _graphicsService.GraphicsDevice;
            var renderTargetPool = _graphicsService.RenderTargetPool;

            // We have to render into this render target.
            var ldrTarget = context.RenderTarget;

            // Reset render states.
            graphicsDevice.BlendState = BlendState.Opaque;
            graphicsDevice.DepthStencilState = DepthStencilState.Default;
            graphicsDevice.RasterizerState = RasterizerState.CullCounterClockwise;

            // Use an intermediate HDR render target with the same resolution as the final target.
            var format = new RenderTargetFormat(ldrTarget)
            {
                SurfaceFormat = SurfaceFormat.HdrBlendable,
                DepthStencilFormat = DepthFormat.Depth24Stencil8
            };
            var hdrTarget = renderTargetPool.Obtain2D(format);

            graphicsDevice.SetRenderTarget(hdrTarget);
            context.RenderTarget = hdrTarget;

            graphicsDevice.Clear(Color.Black);

            // Render the sky.
            _skyRenderer.Render(_skyGroupNode.Children, context);

            // Convert the HDR image to RGBM image.
            context.SourceTexture = hdrTarget;
            context.RenderTarget = ldrTarget;
            _colorEncoder.Process(context);
            context.SourceTexture = null;

            // Clean up.
            renderTargetPool.Recycle(hdrTarget);
            context.RenderTarget = ldrTarget;
        }



        // Ambient light from light pollution.

        // These SkyNodes create the sky:

        // We group all of these scene nodes under one node. (Optional.)

        // 1 ambient light and 2 directional lights for sun and moon.

        // We group all scene nodes of these scene nodes under one scene node. (Optional.)

        // Optionally, the sky is rendered into a cube map.

        #region ----- Helper Methods -----

        /// <summary>
        /// Creates the texture matrix for scaling texture coordinates.
        /// </summary>
        /// <param name="s">The uniform scale factor.</param>
        /// <returns>The texture matrix.</returns>
        private static Matrix33F CreateScale(float s)
        {
            return CreateScale(s, s);
        }



        /// <summary>
        /// Creates the texture matrix for scaling the texture coordinates.
        /// </summary>
        /// <param name="su">The scale factor for u texture coordinates.</param>
        /// <param name="sv">The scale factor for v texture coordinates.</param>
        /// <returns>The texture matrix.</returns>
        private static Matrix33F CreateScale(float su, float sv)
        {
            return new Matrix33F(
                su, 0, 0,
                0, sv, 0,
                0, 0, 1);
        }

        #endregion

        // This game object computes physically based light intensities. Since our material
        // properties and all other lights in this application are not physically based, 
        // we scale the light intensities down.
    }
}

#endif