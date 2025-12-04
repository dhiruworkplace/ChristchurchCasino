using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(menuName = "FactoryResource", fileName = "FactoryResource")]
public class FactoryResource : SlotResource
{
    public List<LevelData> levelDatas = new List<LevelData>();

    [Serializable]
    public class LevelData
    {
        public int ingredientInCountMax;
        public int ingredientOutCountMax;
        public int ingredientCount;
        public SecuredDouble cost;
    }
}