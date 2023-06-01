﻿using System;
using DigitalRune.Graphics;
using DigitalRune.Graphics.Effects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Sceelix.Designer.Actors.Annotations;
using Sceelix.Designer.Actors.Utils;
using Sceelix.Designer.Messaging;
using Sceelix.Designer.ProjectExplorer.Messages;
using Sceelix.Designer.Renderer3D.Loading;
using Sceelix.Designer.Services;
using Sceelix.Designer.Utils;
using Sceelix.Logging;
using Sceelix.Meshes.Data;
using Material = Sceelix.Actors.Data.Material;

namespace Sceelix.Designer.Meshes.Translators.Materials
{
    [MaterialTranslator(typeof(Material))]
    public class OtherMeshMaterialTranslator : MeshMaterialTranslator<VertexPositionNormalTexture>
    {
        private ContentManager _content;
        private IGraphicsService _graphicsService;
        private MessageManager _messageManager;


        public override void Initialize(IServiceLocator services)
        {
            base.Initialize(services);

            _content = services.Get<ContentManager>();
            _graphicsService = services.Get<IGraphicsService>();
            _messageManager = services.Get<MessageManager>();
        }


        protected override VertexPositionNormalTexture PrepareVertex(Material colorMaterial, Face face, Vertex vertex)
        {
            VertexPositionNormalTexture customVertex = new VertexPositionNormalTexture();

            customVertex.Position = vertex.Position.ToVector3();
            customVertex.TextureCoordinate = Vector2.Zero;
            customVertex.Normal = vertex[face].Normal.ToVector3();

            return customVertex;
        }



        protected override DigitalRune.Graphics.Material CreateMaterial(ContentLoader contentLoader, Material sceelixMaterial)
        {
            _messageManager.Publish(new LogMessageSent(String.Format("The visualization of material '{0}' for meshes is not (yet) supported.", sceelixMaterial.GetType()),LogType.Warning));

            //colorMaterial.DefaultColor.ToXnaColor()
            //Texture2D texture = TextureLoader.CreateColorTexture(graphicsService.GraphicsDevice, Color.White); //ContentLoader.LoadTexture(graphicsService.GraphicsDevice, textureMaterial.Texture);
            Texture2D texture = TextureLoader.CreateColorTexture(_graphicsService.GraphicsDevice, Color.Fuchsia); //ContentLoader.LoadTexture(graphicsService.GraphicsDevice, textureMaterial.Texture);

            //
            DigitalRune.Graphics.Material material = new DigitalRune.Graphics.Material();

            BasicEffectBinding basicEffectBinding = new BasicEffectBinding(_graphicsService, null) {LightingEnabled = true, TextureEnabled = true, VertexColorEnabled = false};
            basicEffectBinding.Set("Texture", texture);
            material.Add("Default", basicEffectBinding);

            if (BuildPlatform.IsWindows)
            {
                EffectBinding shadowMapEffectBinding = new EffectBinding(_graphicsService, _content.Load<Effect>("DigitalRune/Materials/ShadowMap"), null, EffectParameterHint.Material);
                //shadowMapEffectBinding.Set("DiffuseTexture", texture);
                //shadowMapEffectBinding.Set("ReferenceAlpha", ReferenceAlpha);
                material.Add("ShadowMap", shadowMapEffectBinding);

                // EffectBinding for the "GBuffer" pass.
                EffectBinding gBufferEffectBinding = new EffectBinding(_graphicsService, _content.Load<Effect>("DigitalRune/Materials/GBuffer"), null, EffectParameterHint.Material);
                //gBufferEffectBinding.Set("DiffuseTexture", texture);
                //gBufferEffectBinding.Set("DiffuseColor", colorMaterial.DefaultColor.ToXnaColor());
                //gBufferEffectBinding.Set("ReferenceAlpha", ReferenceAlpha);
                material.Add("GBuffer", gBufferEffectBinding);

                // EffectBinding for the "Material" pass.
                EffectBinding materialEffectBinding = new EffectBinding(_graphicsService, _content.Load<Effect>("DigitalRune/Materials/Material"), null, EffectParameterHint.Material);
                materialEffectBinding.Set("DiffuseTexture", texture);
                materialEffectBinding.Set("DiffuseColor", new Vector3(1, 1, 1)); //  * 2f
                //materialEffectBinding.Set("DiffuseColor", colorMaterial.DefaultColor.ToXnaVector());
                materialEffectBinding.Set("SpecularColor", new Vector3(0.2f, 0.2f, 0.2f));
                //materialEffectBinding.Set("ReferenceAlpha", ReferenceAlpha);
                material.Add("Material", materialEffectBinding);
            }


            return material;
        }
    }
}