using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(menuName = "MachineResource", fileName = "MachineResource")]
public class MachineResource : SlotResource
{
    public List<LevelData> levelDatas = new List<LevelData>();

    [Serializable]
    public class LevelData
    {
        public List<IngredientData> ingredientDatas = new List<IngredientData>();
        public SecuredDouble cost;
        public SecuredDouble bonus;
    }

    [Serializable]
    public class IngredientData
    {
        public IngredientType ingredientType;
        public int count;

        public IngredientData(IngredientType ingredientType, int count)
        {
            this.ingredientType = ingredientType;
            this.count = count;
        }

        public IngredientData Clone()
        {
            return new IngredientData(ingredientType, count);
        }
    }
}