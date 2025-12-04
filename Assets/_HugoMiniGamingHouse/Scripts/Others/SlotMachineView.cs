using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotMachineView : SlotView
{
    public override bool IsMaxLevel()
    {
        MachineResource machineResource = SlotResource as MachineResource;
        return slotData.level >= machineResource.levelDatas.Count - 1;
    }

    protected override SecuredDouble CostMax()
    {
        MachineResource machineResource = SlotResource as MachineResource;
        return machineResource.levelDatas[slotData.level].cost.Round();
    }
}
