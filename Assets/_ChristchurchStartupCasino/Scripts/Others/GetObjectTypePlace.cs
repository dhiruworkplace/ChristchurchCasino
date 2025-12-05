using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetObjectTypePlace : GetObjectPlace
{
    public IngredientType type;

    public override void Get(SortSlot ss)
    {
        if (sortSlot.ObjectCount >= slotMax)
        {
            return;
        }

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
            sortSlot.AddObject(sortObject);
            SoundManager.Instance.PlayGetObject(transform.position);
        }
    }
}
