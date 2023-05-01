using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Sceelix.Core.Data;
using Sceelix.Meshes.Data;
using UnityEngine;

namespace Sceelix.Processors
{
    [Processor(typeof(MeshEntity))]
    public class MeshEntityProcessor : IProcessor
    {
        private MeshFilterProcessor meshFilterProcessor = new MeshFilterProcessor();
        private MeshRendererProcessor meshRendererProcessor = new MeshRendererProcessor();

        public GameObject Process(IEntity entity)
        {
            GameObject gameObject = new GameObject("Mesh Entity");
            //gameObject.isStatic = true;

            ////fill in the name, static, enabled, tag and layer fields
            //ProcessCommonUnityAttributes(entity, gameObject);

            ////use the processors already defined for the component
            meshFilterProcessor.Process(entity, gameObject);
            meshRendererProcessor.Process(entity, gameObject);

            return gameObject;
        }

        /// <summary>
        /// Processes optional fields that are common in the transformation of all entities to unity game objects (such as name, tag, layer, etc.)
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="gameObject">The game object .</param>
        /// <param name="entityToken">The entity token.</param>
        public static void ProcessCommonUnityAttributes(IEntity entity, GameObject gameObject)
        {
            //var name = entityToken["Name"].ToTypeOrDefault<String>();
            //if (!String.IsNullOrEmpty(name))
            //    gameObject.name = name;

            //var isStatic = entityToken["Static"].ToTypeOrDefault<String>();
            //if (isStatic != null)
            //    gameObject.isStatic = Convert.ToBoolean(isStatic);

            //var enabled = entityToken["Enabled"].ToTypeOrDefault<String>();
            //if (enabled != null)
            //    gameObject.SetActive(Convert.ToBoolean(enabled));

            //var tag = entityToken["Tag"].ToTypeOrDefault<String>();
            //if (!String.IsNullOrEmpty(tag))
            //    context.AddTag(gameObject, tag);

            //var layer = entityToken["Layer"].ToTypeOrDefault<String>();
            //if (!String.IsNullOrEmpty(layer))
            //{
            //    var layerValue = LayerMask.NameToLayer(layer);

            //    //unfortunately we can't create the layer programmatically, so
            //    if (layerValue < 0)
            //        throw new ArgumentException("Layer '" + layer + "' is not defined. It must be created manually in Unity first.");

            //    gameObject.layer = layerValue;
            //}
        }
    }
}