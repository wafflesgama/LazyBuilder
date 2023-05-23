using Sceelix.Core.Data;
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
            var requiredObjects = Require(entities);

            var objecReuseInfos = component.GetObjectReuseInfo_Greedy(requiredObjects).ToList();

            //var missingObjects = objecReuseInfo.Where(x => x.ExistingObject == null);


            List<RecycleObject> recycleObjectsAdded = new List<RecycleObject>();

            foreach (var objecReuseInfo in objecReuseInfos)
            {

                if (objecReuseInfo.recycledObject.gameObject == null)
                {
                    GameObject gameObjectToAdd = new GameObject("Empty");
                    objecReuseInfo.recycledObject.gameObject = gameObjectToAdd;
                    recycleObjectsAdded.Add(objecReuseInfo.recycledObject);
                }

                var componentsToRemove = objecReuseInfo.ComponentsToRemove.ToList();
                foreach (var componentToRemove in componentsToRemove)
                {
                    if (componentToRemove == typeof(Transform)) continue;

#if UNITY_EDITOR
                    GameObject.DestroyImmediate(((objecReuseInfo.recycledObject.gameObject.GetComponent(componentToRemove))));
#else
                    component.DestroyComponent((objecReuseInfo.recycledObject.gameObject.GetComponent(componentToRemove)));
#endif
                    objecReuseInfo.recycledObject.components.Remove(componentToRemove);
                }

                foreach (var componentToAdd in objecReuseInfo.ComponentsToAdd)
                {
                    if (componentToAdd == typeof(Transform)) continue;

                    objecReuseInfo.recycledObject.gameObject.AddComponent(componentToAdd);
                    objecReuseInfo.recycledObject.components.Add(componentToAdd);

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

                RecycleObject recycleObject = new RecycleObject { components = typesRequired.ToList(), entity = entity };
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

    }
}
