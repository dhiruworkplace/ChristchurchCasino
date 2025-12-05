using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotBase : MonoBehaviour
{
    protected SlotView slotView;

    public virtual void Initialize(SlotView slotView)
    {
        this.slotView = slotView;
        UpdateInfo();
    }

    public virtual void UpdateInfo()
    {

    }
}
