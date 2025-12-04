using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class SlotResource : ScriptableObject
{
    public SlotData.Type type;
    public GameObject prefab;
}
