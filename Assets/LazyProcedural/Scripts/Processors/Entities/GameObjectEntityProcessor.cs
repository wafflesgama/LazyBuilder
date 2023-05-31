using Sceelix.Core.Data;
using Sceelix.GameObjects;
using Sceelix.Processors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Sceelix.Processors
{
    [Processor(typeof(GameObjectEntity))]
    public class GameObjectEntityProcessor : IProcessor
    {
        public void Process(RecycleObject recycleObject)
        {
            GameObjectEntity goEntity = recycleObject.entity as GameObjectEntity;
            GameObject instance = null;

            if (recycleObject.sourceGameObject != null)
            {
                //Only append if this Recycled Gameobject came from instancing the same Source GameObject
                if (recycleObject.sourceGameObject == goEntity.gameObject)
                    instance = recycleObject.gameObject;
            }

            //In case of no source GameObject OR mismatched source GameObject
            if (instance == null)
            {

                //Create new instance
                instance = GameObject.Instantiate(goEntity.gameObject);

                //Apply the parenting applied to the original
                instance.transform.parent = recycleObject.gameObject.transform.parent;

                //Remove old object
#if UNITY_EDITOR
                GameObject.DestroyImmediate(recycleObject.gameObject);
#else
                GameObject.Destroy(recycleObject.gameObject);
#endif


                //And append the newly created
                recycleObject.gameObject = instance;

            }


            instance.transform.position = goEntity.BoxScope.Translation.FlipYZ().ToVector3();
        }

        public IEnumerable<Type> Require(IEntity input)
        {
            return new[] { typeof(Transform) };
        }
    }
}
