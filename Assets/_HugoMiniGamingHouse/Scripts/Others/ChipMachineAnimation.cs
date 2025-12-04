using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ChipMachineAnimation : MachineAnimation
{
    public List<Transform> circles = new List<Transform>();
    public Transform holder;

    private Vector3 rHolder;

    private void Start()
    {
        rHolder = holder.localEulerAngles;
    }

    public override void Trigger()
    {
        foreach (var item in circles)
        {
            item.DOLocalRotate(Vector3.right * 360, 1, RotateMode.LocalAxisAdd);
        }

        holder.DOLocalRotate(rHolder - Vector3.right * 45, 0.2f).onComplete += () => {
            holder.DOLocalRotate(rHolder, 0.2f);
        };
    }
}
