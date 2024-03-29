using Sceelix.Core.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RecycleObject
{
    public GameObject gameObject { get; set; }
    public GameObject sourceGameObject { get; set; }    
    public IEntity entity { get; set; }
    public List<Type> components { get; set; }


    public RecycleObject()
    {
        components = new List<Type>();
    }
    public RecycleObject(GameObject gameObject)
    {
        this.gameObject = gameObject;
        components = gameObject.GetComponents(typeof(Component)).Select(x => x.GetType()).ToList();
    }


    public override bool Equals(object obj)
    {
        if (obj == null || obj.GetType() != typeof(RecycleObject)) return false;

        RecycleObject other = obj as RecycleObject;


        return this.gameObject.GetInstanceID() == other.gameObject.GetInstanceID();
    }

    public bool HasExactComponents(IEnumerable<Type> componentsRequired)
    {
        return components.Intersect(componentsRequired).Count() == componentsRequired.Count();
    }

    public override int GetHashCode()
    {
        int hashCode = 1735283341;
        hashCode = hashCode * -1521134295 + EqualityComparer<GameObject>.Default.GetHashCode(gameObject);
        hashCode = hashCode * -1521134295 + EqualityComparer<IEnumerable<Type>>.Default.GetHashCode(components);
        return hashCode;
    }
}

