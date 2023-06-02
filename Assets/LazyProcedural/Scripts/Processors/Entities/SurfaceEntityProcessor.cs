using Sceelix.Core.Data;
using Sceelix.Extensions;
using Sceelix.Mathematics.Data;
using Sceelix.Surfaces.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Sceelix.Processors
{

    [Processor(typeof(SurfaceEntity))]
    public class SurfaceEntityProcessor : IProcessor
    {
        //unfortunately unity terrain maps have to be square and the sizes must be always powers of 2
        private static int[] UNITY_HEIGHTMAPS_SIZES = new int[] { 32, 64, 128, 256, 512, 1024, 2048, 4096 };

        public void Process(RecycleObject recycleObject)
        {
            SurfaceEntity surfaceEntity = recycleObject.entity as SurfaceEntity;
            GameObject gameObject = recycleObject.gameObject;


            gameObject.name = "Surface Entity";
            gameObject.transform.position = Vector3.zero;


            Terrain terrain = gameObject.GetComponent<Terrain>();

            //If a Terrain doesn't exist, create it
            if (terrain == null)
                terrain = gameObject.AddComponent<Terrain>();


            var maxResolution = Math.Max(surfaceEntity.Width, surfaceEntity.Length);

            int heightMapResolution = FindNextSmallestHeightmap(maxResolution);

            //This factor keeps the visual size of the 
            //float proportionFactor = heightMapResolution / 128;

            var size = new Vector3(heightMapResolution, Math.Max(1, surfaceEntity.MaximumZ), heightMapResolution);



            var surfaceHeights = GetSurfaceHeights(surfaceEntity);
            var heightsResolution = new Vector2D(surfaceHeights.GetLength(0), surfaceHeights.GetLength(1));

            //var heightsBytes = surfaceHeights.ToByteArray();
            //var heights = heightsBytes.ToTArray<float>((int)heightsResolution.X, (int)heightsResolution.Y);


            TerrainData terrainData = new TerrainData();



            terrainData.heightmapResolution = heightMapResolution;
            terrainData.alphamapResolution = heightMapResolution;
            terrainData.size = size;
            terrainData.SetHeights(0, 0, surfaceHeights);


            bool[,] terrainHoles = new bool[heightMapResolution, heightMapResolution];
            int holeMarginPx = 4;
            for (int i = 0; i < heightMapResolution; i++)
                for (int j = 0; j < heightMapResolution; j++)
                    terrainHoles[i, j] = i <= surfaceEntity.NumRows - holeMarginPx && j <= surfaceEntity.NumColumns - holeMarginPx;


            terrainData.SetHoles(0, 0, terrainHoles);


           


            //var materialToken = jtoken["Material"];
            //if (materialToken != null)
            //{
            //    var defaultTexture = Texture2D.whiteTexture.ToMipmappedTexture();
            //    List<TerrainLayer> terrainLayers = new List<TerrainLayer>();

            //    var splatMapSize = materialToken["SplatmapSize"].ToVector3();
            //    var splatMapBytes = materialToken["Splatmap"].ToObject<byte[]>();
            //    float[,,] splatMap = splatMapBytes.ToTArray<float>((int)splatMapSize.x, (int)splatMapSize.y, (int)splatMapSize.z);

            //    foreach (JToken layerToken in materialToken["Layers"].Children())
            //    {
            //        var tileSize = layerToken["TileSize"].ToVector2();
            //        var textureToken = layerToken["Texture"];

            //        //var name = textureToken["Name"].ToObject<String>();

            //        terrainLayers.Add(new TerrainLayer()
            //        {
            //            diffuseTexture = Texture2DExtensions.CreateOrGetTexture(context, layerToken["DiffuseMap"]) ?? defaultTexture,
            //            normalMapTexture = Texture2DExtensions.CreateOrGetTexture(context, layerToken["NormalMap"], true),
            //            tileSize = new Vector2(terrainData.size.x / tileSize.x, terrainData.size.z / tileSize.y)
            //        });
            //    }

            //    terrainData.terrainLayers = terrainLayers.ToArray();

            //    terrainData.SetAlphamaps(0, 0, splatMap);
            //}


            terrain.materialTemplate = surfaceEntity.Material ?? MaterialProcessor.CreateDefaultTerrainMaterial();

            terrain.terrainData = terrainData;



            TerrainCollider collider = gameObject.GetComponent<TerrainCollider>();

            //If a TerrainCollider doesn't exist, create it
            if (collider == null)
                collider = gameObject.AddComponent<TerrainCollider>();

            collider.terrainData = terrainData;


            //ReadTerrainParameters(context, terrain, jtoken);
            //ReadTreeInstances(context, terrain, jtoken);

            //Apply Translation
            gameObject.transform.position = surfaceEntity.BoxScope.Translation.FlipYZ().ToVector3();
        }


        private float[,] GetSurfaceHeights(SurfaceEntity surfaceEntity)
        {
            //unity has its y inverted, so...
            float[,] invertedHeights = new float[surfaceEntity.NumRows, surfaceEntity.NumColumns];

            var numRows = surfaceEntity.NumRows;
            var numColumns = surfaceEntity.NumColumns;
            var height = surfaceEntity.MaximumZ;

            var heightLayer = surfaceEntity.GetLayer<HeightLayer>();

            for (int i = 0; i < numColumns; i++)
            {
                for (int j = 0; j < numRows; j++)
                    invertedHeights[numRows - j - 1, i] = (heightLayer != null ?
                        heightLayer.GetGenericValue(new Coordinate(i, j)) :
                        0) / height; //numRows - 1 - j

            }

            return invertedHeights;
        }

        //private void ReadTerrainParameters(IGenerationContext context, Terrain terrain, JToken jtoken)
        //{
        //    var pixelErrorToken = jtoken["PixelError"];
        //    if (pixelErrorToken != null)
        //        terrain.heightmapPixelError = pixelErrorToken.ToTypeOrDefault<int>();

        //    var billboardStartToken = jtoken["BillboardStart"];
        //    if (billboardStartToken != null)
        //        terrain.treeBillboardDistance = billboardStartToken.ToTypeOrDefault<float>();
        //}

        public IEnumerable<Type> Require(IEntity input)
        {
            return new[] { typeof(Transform), typeof(Terrain), typeof(TerrainCollider) };
        }



        private static int FindNextSmallestHeightmap(float input)
        {
            return UNITY_HEIGHTMAPS_SIZES.FirstOrDefault(size => size > input);
        }

    }
}
