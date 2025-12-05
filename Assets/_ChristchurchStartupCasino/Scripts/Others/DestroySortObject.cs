using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySortObject : MonoBehaviour
{
    public float timeRate = 0.1f;
    public Transform point;
    protected float timeCount;

    public virtual void Get(SortSlot ss)
    {
        SortObject sortObject = ss.EndObject;

        if (sortObject != null)
        {
            ss.RemoveObject(sortObject);
            float time = 0.3f;
            sortObject.MoveToWorldPosition(point, time);
            Destroy(sortObject.gameObject, time);
        }
    }

    protected virtual void OnTriggerStay(Collider other)
    {
        timeCount += Time.deltaTime;
        if (timeCount >= timeRate)
        {
            Character character = other.GetComponent<Character>();
            Mover mover = character.GetComponent<Mover>();
            if (character != null && mover.MoveDirection == Vector3.zero)
            {
                Get(character.sortSlot);
            }

            timeCount = 0;
        }
    }
}
