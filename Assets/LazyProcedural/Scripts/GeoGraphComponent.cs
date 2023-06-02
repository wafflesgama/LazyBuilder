using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[ExecuteAlways]
public class GeoGraphComponent : MonoBehaviour
{
    public List<RecycleObject> existingObjects;


    private void OnEnable()
    {
        SetupExsitingObjects();
    }


    private void SetupExsitingObjects()
    {
        existingObjects = new List<RecycleObject>();

        foreach (Transform child in transform)
        {
            AddRecycleObject(new RecycleObject(child.gameObject));
        }
    }

    public void DestroyComponent(UnityEngine.Object objectToDestroy)
    {
        Destroy(objectToDestroy);
    }

    public List<ObjectReuseInfo> GetObjectReuseInfo_Greedy(List<RecycleObject> requiredObjects)
    {
        //If there is any object related 
        if (existingObjects.Where(x => x.gameObject == null).Any())
            SetupExsitingObjects();


        List<ObjectReuseInfo> objectsToReuseInfo = new List<ObjectReuseInfo>();
        List<RecycleObject> existingObjectsCopy = existingObjects.ToList();

        foreach (RecycleObject requiredObject in requiredObjects)
        {
            ObjectReuseInfo objectReuseInfo = new ObjectReuseInfo();

            //If this recycle object is intantiating from another 
            if (requiredObject.sourceGameObject != null)
            {
                RecycleObject sameSourceObject = existingObjectsCopy.FirstOrDefault(o => o.sourceGameObject == requiredObject.sourceGameObject);

                if (sameSourceObject != null)
                {
                    objectReuseInfo.recycledObject = sameSourceObject;
                    objectReuseInfo.recycledObject.entity = requiredObject.entity;
                }
            }
            else
            {
                // First try to find an exact match in the existing objects
                RecycleObject matchingExistingObject = existingObjectsCopy.FirstOrDefault(o => o.HasExactComponents(requiredObject.components));
                if (matchingExistingObject != null)
                {
                    objectReuseInfo.recycledObject = matchingExistingObject;
                    objectReuseInfo.recycledObject.entity = requiredObject.entity;
                }
                else
                {
                    if (existingObjectsCopy.Count > 0)
                    {
                        // If no exact match is found, try to find an existing object that has the most components in common
                        int maxCommonComponents = -1;
                        foreach (RecycleObject existingObject in existingObjectsCopy)
                        {
                            int commonComponentsCount = existingObject.components.Intersect(requiredObject.components).Count();
                            if (commonComponentsCount > maxCommonComponents)
                            {
                                maxCommonComponents = commonComponentsCount;
                                objectReuseInfo.recycledObject = existingObject;
                                objectReuseInfo.recycledObject.entity = requiredObject.entity;
                            }
                        }

                    }

                }

                if (objectReuseInfo.recycledObject != null)
                    objectReuseInfo.recycledObject.sourceGameObject = null;
            }


            //If there are no more object to recycle create one entry with all the necessary components
            if (objectReuseInfo.recycledObject == null)
            {
                objectReuseInfo.recycledObject = new RecycleObject { gameObject = null, entity = requiredObject.entity };
                objectReuseInfo.ComponentsToAdd = requiredObject.components;
            }
            else
            {
                // Determine which components need to be added and removed to reuse the existing object
                objectReuseInfo.ComponentsToRemove = objectReuseInfo.recycledObject.components.Except(requiredObject.components);
                objectReuseInfo.ComponentsToAdd = requiredObject.components.Except(objectReuseInfo.recycledObject.components);
            }

            // Remove the existing object from the list of available existing objects (If it was recycled)
            if (objectReuseInfo.recycledObject.gameObject != null)
                existingObjectsCopy.Remove(objectReuseInfo.recycledObject);

            //Finally adds to the list
            objectsToReuseInfo.Add(objectReuseInfo);
        }

        return objectsToReuseInfo;
    }



    public List<RecycleObject> GetRecycleObjectsSurplus(List<ObjectReuseInfo> objectsReused)
    {
        return existingObjects.Except(objectsReused.Select(x => x.recycledObject)).ToList();
    }

    public void RemoveRecycleObjects(IEnumerable<RecycleObject> objectsToRemove)
    {
        foreach (var objectToRemove in objectsToRemove)
        {
#if UNITY_EDITOR
            DestroyImmediate(objectToRemove.gameObject);
#else
            Destroy(objectToRemove.gameObject);
#endif
        }
        existingObjects.RemoveAll(x => objectsToRemove.Contains(x));
    }

    public void AddRecycleObjects(IEnumerable<RecycleObject> objectsToAdd)
    {
        foreach (var objectToAdd in objectsToAdd)
            AddRecycleObject(objectToAdd);
    }

    public void AddRecycleObject(RecycleObject recycleObject)
    {
        // Find the index to insert the recycleObject based on the number of components
        int index = existingObjects.FindIndex(obj => obj.components.Count > recycleObject.components.Count);
        if (index == -1)
        {
            // If no object has more components, insert at the end
            existingObjects.Add(recycleObject);
        }
        else
        {
            // Insert at the found index
            existingObjects.Insert(index, recycleObject);
        }
    }


    //BEWARE of the performance on this one
    public List<ObjectReuseInfo> GetObjectReuseInfo_AllCombinations(List<RecycleObject> requiredObjects)
    {
        List<ObjectReuseInfo> objectReuseInfoList = new List<ObjectReuseInfo>();

        // Initialize a list of all possible combinations between existingObjects and requiredObjects
        List<List<RecycleObject>> combinations = new List<List<RecycleObject>>();
        for (int i = 0; i <= requiredObjects.Count; i++)
        {
            List<RecycleObject> combination = new List<RecycleObject>(existingObjects);
            combination.AddRange(requiredObjects.GetRange(0, i));
            combination.AddRange(requiredObjects.GetRange(i, requiredObjects.Count - i));
            combinations.Add(combination);
        }

        // Calculate the total number of additions and removals for each combination
        List<Tuple<List<RecycleObject>, int>> combinationAdditionRemovalCounts = new List<Tuple<List<RecycleObject>, int>>();
        foreach (var combination in combinations)
        {
            int additionCount = 0;
            int removalCount = 0;
            foreach (var requiredObject in requiredObjects)
            {
                int existingObjectIndex = combination.FindIndex(x => x != null && x.gameObject.GetInstanceID() == requiredObject.gameObject.GetInstanceID());
                if (existingObjectIndex >= 0)
                {
                    // If a matching existingObject is found, calculate the components that need to be added and removed
                    var existingObject = combination[existingObjectIndex];
                    var componentsToAdd = requiredObject.components.Except(existingObject.components);
                    var componentsToRemove = existingObject.components.Except(requiredObject.components);
                    additionCount += componentsToAdd.Count();
                    removalCount += componentsToRemove.Count();
                }
                else
                {
                    // If no matching existingObject is found, all components of the requiredObject need to be added
                    additionCount += requiredObject.components.Count();
                }
            }
            combinationAdditionRemovalCounts.Add(Tuple.Create(combination, additionCount + removalCount));
        }

        // Select the combination with the least total number of additions and removals
        var selectedCombination = combinationAdditionRemovalCounts.OrderBy(x => x.Item2).First().Item1;

        // Create the objectReuseInfoList based on the selected combination
        foreach (var requiredObject in requiredObjects)
        {
            int existingObjectIndex = selectedCombination.FindIndex(x => x != null && x.gameObject.GetInstanceID() == requiredObject.gameObject.GetInstanceID());
            if (existingObjectIndex >= 0)
            {
                var existingObject = selectedCombination[existingObjectIndex];
                var componentsToAdd = requiredObject.components.Except(existingObject.components);
                var componentsToRemove = existingObject.components.Except(requiredObject.components);
                objectReuseInfoList.Add(new ObjectReuseInfo { recycledObject = existingObject, ComponentsToAdd = componentsToAdd, ComponentsToRemove = componentsToRemove });
                selectedCombination[existingObjectIndex] = null;
            }
            else
            {
                objectReuseInfoList.Add(new ObjectReuseInfo { recycledObject = null, ComponentsToAdd = requiredObject.components, ComponentsToRemove = Enumerable.Empty<Type>() });
            }
        }

        return objectReuseInfoList;
    }
}
