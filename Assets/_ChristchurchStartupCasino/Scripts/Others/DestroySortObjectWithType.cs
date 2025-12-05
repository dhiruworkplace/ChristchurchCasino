using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySortObjectWithType : DestroySortObject
{
    public IngredientType type;

    public override void Get(SortSlot ss)
    {
        List<SortObject> sortObjects = ss.SortObjects;
        List<SortObject> sos = sortObjects.FindAll((SortObject a) => a.GetComponent<ObjectType>().ingredientType == type);

        if (sos == null || sos.Count == 0)
        {
            return;
        }

        sos.Sort((SortObject a, SortObject b) => b.Position.y.CompareTo(a.Position.y));

        SortObject sortObject = sos[0];
        if (sortObject != null)
        {
            ss.RemoveObject(sortObject);
            float time = 0.3f;
            sortObject.MoveToWorldPosition(point, time);
            Destroy(sortObject.gameObject, time);
        }
    }
}
