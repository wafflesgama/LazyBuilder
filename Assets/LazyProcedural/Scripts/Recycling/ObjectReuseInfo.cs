using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectReuseInfo
{
    public RecycleObject recycledObject { get; set; }
    public IEnumerable<Type> ComponentsToAdd { get; set; } = new List<Type>();
    public IEnumerable<Type> ComponentsToRemove { get; set; } = new List<Type>();

}

