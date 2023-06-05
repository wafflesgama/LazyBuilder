using Sceelix.Core.Data;
using Sceelix.GameObjects;
using Sceelix.Loading;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Sceelix.Processors
{
    public class ProcessorManager
    {
        private Dictionary<Type, Type> processors = new Dictionary<Type, Type>();

        public bool destructiveProcess = true;

        public ProcessorManager()
        {
            SetProcessors();
        }

        private void SetProcessors()
        {
            var rawProcessors = SceelixDomain.Types.Where(type => type.GetCustomAttributes(typeof(ProcessorAttribute), true).Length > 0);
            processors = rawProcessors.ToDictionary(type => ((ProcessorAttribute)type.GetCustomAttributes(typeof(ProcessorAttribute), true)[0]).Type, type => type);
        }



        public void Populate(IEnumerable<IEntity> entities, GeoGraphComponent component)
        {
            List<RecycleObject> requiredObjects = Require(entities);

            var objecReuseInfos = component.GetObjectReuseInfo_Greedy(requiredObjects).ToList();

            List<RecycleObject> recycleObjectsAdded = new List<RecycleObject>();

            foreach (var objecReuseInfo in objecReuseInfos)
            {

                //If Recycled Object's GObj is null- it is marked as to create one
                if (objecReuseInfo.recycledObject.gameObject == null)
                {
                    GameObject gameObjectToAdd = new GameObject("Empty");
                    objecReuseInfo.recycledObject.gameObject = gameObjectToAdd;

                    //and mark the necessary components to add
                    recycleObjectsAdded.Add(objecReuseInfo.recycledObject);
                }

                var componentsToRemove = objecReuseInfo.ComponentsToRemove.ToList();
                foreach (var componentToRemove in componentsToRemove)
                {
                    if (componentToRemove == typeof(Transform)) continue;

                    DestroyComponent(((objecReuseInfo.recycledObject.gameObject.GetComponent(componentToRemove))));

                    objecReuseInfo.recycledObject.components.Remove(componentToRemove);
                }

                foreach (var componentToAdd in objecReuseInfo.ComponentsToAdd)
                {
                    if (componentToAdd == typeof(Transform)) continue;

                    objecReuseInfo.recycledObject.gameObject.AddComponent(componentToAdd);
                    objecReuseInfo.recycledObject.components.Add(componentToAdd);

                }

                //If no source instancing object is available check for unwanted children
                if (objecReuseInfo.recycledObject.sourceGameObject == null && objecReuseInfo.recycledObject.gameObject.transform.childCount > 0)
                {
                    foreach (Transform child in objecReuseInfo.recycledObject.gameObject.transform)
                    {
                        DestroyComponent(child.gameObject);
                    }
                }
            }

            //Reset all the parenting of the objects
            objecReuseInfos.ForEach(objecReuseInfo => objecReuseInfo.recycledObject.gameObject.transform.parent = component.transform);

            //Then process all the entitties in the respective RecycledObjects
            Process(objecReuseInfos.Select(x => x.recycledObject));

            //Destroy objects in excess
            if (destructiveProcess)
            {
                var surplus = component.GetRecycleObjectsSurplus(objecReuseInfos);
                component.RemoveRecycleObjects(surplus);
            }

            component.AddRecycleObjects(recycleObjectsAdded);
        }

        private List<RecycleObject> Require(IEnumerable<IEntity> entities)
        {
            List<RecycleObject> result = new List<RecycleObject>();
            foreach (IEntity entity in entities)
            {
                Type entityType = entity.GetType();
                bool hasProcessor = processors.ContainsKey(entityType);

                if (!hasProcessor)
                {
                    Debug.LogError($"{entityType.Name} Processor not implemented or is missing a ProcessorAttribute");
                    break;
                }

                IProcessor processor = (IProcessor)Activator.CreateInstance(processors[entityType]);
                var typesRequired = processor.Require(entity);

                GameObject sourceGameObject = null;
                if (entity.GetType() == typeof(GameObjectEntity))
                {
                    var gameobjectEntity = (GameObjectEntity)entity;
                    sourceGameObject = gameobjectEntity.gameObject;
                }

                RecycleObject recycleObject = new RecycleObject { components = typesRequired.ToList(), entity = entity, sourceGameObject = sourceGameObject };
                result.Add(recycleObject);
            }

            return result;
        }

        private void Process(IEnumerable<RecycleObject> recycledObjects)
        {
            foreach (RecycleObject recycledObject in recycledObjects)
            {
                Type entityType = recycledObject.entity.GetType();
                bool hasProcessor = processors.ContainsKey(entityType);

                if (!hasProcessor)
                {
                    Debug.LogError($"{entityType.Name} Processor not implemented or is missing a ProcessorAttribute");
                    break;
                }

                IProcessor processor = (IProcessor)Activator.CreateInstance(processors[entityType]);
                processor.Process(recycledObject);
            }

        }

        private void DestroyComponent(UnityEngine.Object obj)
        {
#if UNITY_EDITOR
            GameObject.DestroyImmediate(obj);
#else
            GameObject.Destroy(obj);
#endif
        }

    }
}
