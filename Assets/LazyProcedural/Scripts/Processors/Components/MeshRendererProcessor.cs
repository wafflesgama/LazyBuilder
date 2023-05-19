using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using Sceelix.Core.Data;
using Sceelix.Meshes.Data;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Sceelix.Processors
{
    //[Processor("MeshRenderer")]
    public class MeshRendererProcessor
    {
        //Dictionary<string, MaterialProcessor> _materialProcessors = ProcessorAttribute.GetClassesOfType<MaterialProcessor>();

        public void Process(IEntity entity, GameObject gameObject)
        {
            MeshEntity meshEntity = entity as MeshEntity;
            MaterialProcessor materialProcessor = new MaterialProcessor();

            MeshRenderer meshRenderer = gameObject.GetComponent<MeshRenderer>();

            //If a Mesh Renderer doesn't exist, create it
            if (meshRenderer == null)
                meshRenderer = gameObject.AddComponent<MeshRenderer>();

            UnityEngine.Material[] materials= meshEntity.Faces.Select(x => x.Material).Distinct().Select(x=> materialProcessor.Process(x)).ToArray();

            meshRenderer.materials = materials;

            ////GenericData[] genericMaterials = genericGameComponent.Get<GenericData[]>("Materials");
            //var materialTokens = jtoken["Materials"].Children().ToList();

            //Material[] sharedMaterials = new Material[materialTokens.Count];
            //for (int index = 0; index < materialTokens.Count; index++)
            //{
            //    var materialToken = materialTokens[index];
            //    var materialName = materialToken["Name"].ToObject<String>();

            //    var material = context.CreateOrGetAssetOrResource<Material>(materialName + ".mat", delegate ()
            //    {
            //        MaterialProcessor materialProcessorAttribute;

            //        if (materialToken["Type"] == null)
            //        {
            //            Debug.LogWarning("Could not load material. It was expected to have been loaded before. This could have been caused by a failure in a previous load.");
            //            return null;
            //        }

            //        //if there is a type field, use it to find its processor
            //        var materialType = materialToken["Type"].ToObject<String>();
            //        if (_materialProcessors.TryGetValue(materialType, out materialProcessorAttribute))
            //            return materialProcessorAttribute.Process(context, materialToken);


            //        Debug.LogWarning(String.Format("There is no defined processor for material type {0}.", materialType));
            //        return null;

            //    });

            //    sharedMaterials[index] = material;
            //}

            //renderer.sharedMaterials = sharedMaterials;

            //if (realGameObject != gameObject)
            //    Object.DestroyImmediate(gameObject);
        }
    }
}