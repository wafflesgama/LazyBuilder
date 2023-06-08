using System;
using System.Collections.Generic;
using Sceelix.Actors.Data;
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


        public IEnumerable<Type> Require(IEntity input)
        {
            return new[] { typeof(Transform), typeof(MeshRenderer), typeof(MeshFilter) };
        }

        public void Process(RecycleObject recycleObject)
        {
            recycleObject.gameObject.name = "Mesh Entity";
            recycleObject.gameObject.transform.position = Vector3.zero;

            //var baseEntity = (IActor)recycleObject.entity;
            //recycleObject.gameObject.transform.position = baseEntity.BoxScope.Translation.FlipYZ().ToVector3();

            //recycleObject.gameObject.transform.rotation = baseEntity.

            //gameObject.isStatic = true;

            ////fill in the name, static, enabled, tag and layer fields
            //ProcessCommonUnityAttributes(entity, gameObject);

            ////use the processors already defined for the component
            meshFilterProcessor.Process(recycleObject.entity, recycleObject.gameObject);
            meshRendererProcessor.Process(recycleObject.entity, recycleObject.gameObject);
        }

        /// <summary>
        /// Processes optional fields that are common in the transformation of all entities to unity game objects (such as name, tag, layer, etc.)
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="gameObject">The game object .</param>
        /// <param name="entityToken">The entity token.</param>
        public static void ProcessCommonUnityAttributes(GameObject gameObject)
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