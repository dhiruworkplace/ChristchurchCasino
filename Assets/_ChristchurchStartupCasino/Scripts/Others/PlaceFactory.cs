using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceFactory : MonoBehaviour
{
    public SortSlot sortSlot;

    public float timeRate = 0.1f;
    protected float timeCount;
    public int slotMax = 50;

    public bool IsMax
    {
        get
        {
            return sortSlot.ObjectCount >= slotMax;
        }
    }

    public void Add(SortObject so)
    {
        if (IsMax)
        {
            Destroy(so.gameObject);
            return;
        }

        sortSlot.AddObject(so);
    }

    public void Put(Character c)
    {
        SortObject sortObject = sortSlot.EndObject;

        if (sortObject != null)
        {
            if (c.AddObject(sortObject))
            {
                sortSlot.RemoveObject(sortObject);
            }
        }
    }

    protected void OnTriggerStay(Collider other)
    {
        timeCount += Time.deltaTime;
        if (timeCount >= timeRate)
        {
            Character character = other.GetComponent<Character>();
            if (character != null)
            {
                Put(character);
            }

            timeCount = 0;
        }
    }
}
