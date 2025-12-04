using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetObjectPlace : MonoBehaviour
{
    public SortSlot sortSlot;
    public int slotMax = 50;
    public bool IsMax
    {
        get
        {
            return sortSlot.ObjectCount >= slotMax;
        }
    }

    public float timeRate = 0.1f;
    protected float timeCount;

    public virtual void Get(SortSlot ss)
    {
        if (IsMax)
        {
            return;
        }

        SortObject sortObject = ss.EndObject;

        if (sortObject != null)
        {
            ss.RemoveObject(sortObject);
            sortSlot.AddObject(sortObject);
        }
    }

    protected virtual void OnTriggerStay(Collider other)
    {
        timeCount += Time.deltaTime;
        if (timeCount >= timeRate)
        {
            Character character = other.GetComponent<Character>();
            if (character != null)
            {
                Get(character.sortSlot);
            }

            timeCount = 0;
        }
    }
}
