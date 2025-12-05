using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotFactoryView : SlotView
{
    public override bool IsMaxLevel()
    {
        FactoryResource factoryResource = SlotResource as FactoryResource;
        return slotData.level >= factoryResource.levelDatas.Count - 1;
    }

    protected override SecuredDouble CostMax()
    {
        FactoryResource factoryResource = SlotResource as FactoryResource;
        return factoryResource.levelDatas[slotData.level].cost.Round();
    }

}
