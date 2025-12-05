using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotMachineMover : Mover
{
    public List<SlotMachineView> SlotMachineViews
    {
        get
        {
            List<SlotMachineView> slotMachineViews = new List<SlotMachineView>();
            List<SlotView> slotViews = Game.Instance.slotViews.FindAll((SlotView s) => s is SlotMachineView);
            foreach (var item in slotViews)
            {
                slotMachineViews.Add(item as SlotMachineView);
            }
            return slotMachineViews;
        }
    }

    public List<ChipMachine> ChipMachines
    {
        get
        {
            List<ChipMachine> cfs = new List<ChipMachine>();
            List<SlotMachineView> slotFactoryViews = SlotMachineViews;
            foreach (var item in slotFactoryViews)
            {
                cfs.Add(item.SlotBase as ChipMachine);
            }

            return cfs;
        }
    }

    public ChipMachine ChipMachineEmpty
    {
        get
        {
            List<ChipMachine> cm = ChipMachines;
            cm.Shuffle();
            return cm.Find((ChipMachine cf) => cf != null && cf.IngredientDatas.Count > 0);
        }
    }

    public ChipMachine ChipMachineEmptyWithType(IngredientType type)
    {
        List<ChipMachine> cm = ChipMachines;
        cm.Shuffle();
        return cm.Find((ChipMachine cf) => cf != null && cf.IngredientDatas.FindAll((MachineResource.IngredientData ida) => ida.ingredientType == type).Count > 0);
    }

    public List<SlotFactoryView> SlotFactoryViews
    {
        get
        {
            List<SlotFactoryView> slotFactoryView = new List<SlotFactoryView>();
            List<SlotView> slotViews = Game.Instance.slotViews.FindAll((SlotView s) => s is SlotFactoryView);
            foreach (var item in slotViews)
            {
                slotFactoryView.Add(item as SlotFactoryView);
            }
            return slotFactoryView;
        }
    }

    public Transform ChipPoint
    {
        get
        {
            SlotFactoryView slotFactoryView = SlotFactoryViews.Find((SlotFactoryView cfv) => cfv.slotData.type == SlotData.Type.ChipFactory);
            ChipFactory chipFactory = slotFactoryView.SlotBase as ChipFactory;
            return chipFactory.placeFactory.transform;
        }
    }

    public Transform CardPoint
    {
        get
        {
            SlotFactoryView slotFactoryView = SlotFactoryViews.Find((SlotFactoryView cfv) => cfv.slotData.type == SlotData.Type.CardFactory);
            ChipFactory chipFactory = slotFactoryView.SlotBase as ChipFactory;
            return chipFactory.placeFactory.transform;
        }
    }

    public int ChipCount
    {
        get
        {
            return character.sortSlot.SortObjects.FindAll((SortObject so) => so.GetComponent<ObjectType>() != null && so.GetComponent<ObjectType>().ingredientType == IngredientType.Chip).Count;
        }
    }

    public int CardCount
    {
        get
        {
            return character.sortSlot.SortObjects.FindAll((SortObject so) => so.GetComponent<ObjectType>() != null && so.GetComponent<ObjectType>().ingredientType == IngredientType.Card).Count;
        }
    }

    public int IngredientCount(IngredientType type)
    {
        return character.sortSlot.SortObjects.FindAll((SortObject so) => so.GetComponent<ObjectType>() != null && so.GetComponent<ObjectType>().ingredientType == type).Count;
    }

    public Transform IngredientTarget(IngredientType iDType)
    {
        SlotData.Type type = SlotType(iDType);
        SlotFactoryView slotFactoryView = SlotFactoryViews.Find((SlotFactoryView cfv) => cfv.slotData.type == type);
        ChipFactory chipFactory = slotFactoryView.SlotBase as ChipFactory;
        return chipFactory.placeFactory.transform;
    }

    private SlotData.Type SlotType(IngredientType type)
    {
        switch (type)
        {
            case IngredientType.Chip:
                return SlotData.Type.ChipFactory;
                break;
            case IngredientType.Card:
                return SlotData.Type.CardFactory;
                break;
        }
        return SlotData.Type.ChipFactory;
    }

    public bool HasEndMove()
    {
        if (!agent.enabled || !agent.isOnNavMesh)
        {
            return false;
        }
        float d = agent.GetPathRemainingDistance();
        return d < 1f && d >= 0;
    }

    public bool IsIdle()
    {
        if (!agent.enabled || !agent.isOnNavMesh)
        {
            return false;
        }
        float d = agent.GetPathRemainingDistance();
        return d <= agent.stoppingDistance && d >= 0;
    }

    private void Update()
    {
        OnUpdate();
    }

    public override void OnUpdate()
    {
        if (IsIdle())
        {
            characterAnimation.ActiveIdle();
        }
        else
        {
            Rotation();
            characterAnimation.ActiveMove();
        }
    }

    private IEnumerator Start()
    {
        yield return null;
        while (true)
        {
            yield return new WaitForSeconds(Random.RandomRange(0, 2f));
            yield return new WaitUntil(() => HasEndMove());
            yield return new WaitUntil(() => ChipMachineEmpty != null);

            ChipMachine chipMachine = ChipMachineEmpty;

            if (chipMachine == null || chipMachine.IngredientDatas.Count == 0)
            {
                continue;
            }

            MachineResource.IngredientData ingredientData = chipMachine.IngredientDatas[0];

            if (IngredientCount(ingredientData.ingredientType) < ingredientData.count)
            {
                MoveTo(IngredientTarget(ingredientData.ingredientType).position);
            }

            yield return new WaitUntil(() => (IngredientCount(ingredientData.ingredientType) >= ingredientData.count && IngredientCount(ingredientData.ingredientType) > 0) || character.IsObjectMax);
            if (chipMachine == null)
            {
                continue;
            }

            MoveTo(chipMachine.transform.position);
            yield return new WaitUntil(() => ingredientData.count == 0 || IngredientCount(ingredientData.ingredientType) == 0);

            yield return StartCoroutine(InventoryHandle());
        }
    }

    private IEnumerator InventoryHandle()
    {
        if (character.sortSlot.ObjectCount > 0)
        {
            foreach (IngredientType item in System.Enum.GetValues(typeof(IngredientType)))
            {
                if (IngredientCount(item) > 0 && ChipMachineEmptyWithType(item) != null)
                {
                    ChipMachine cm = ChipMachineEmptyWithType(item);

                    if (cm == null || cm.IngredientDatas.Count == 0)
                    {
                        continue;
                    }

                    MoveTo(cm.transform.position);

                    yield return new WaitUntil(() => IngredientCount(item) == 0 || cm.IngredientDatas.Count == 0
                    || cm.IngredientDatas.Find((MachineResource.IngredientData ida) => ida.ingredientType == item) == null);

                    yield return StartCoroutine(InventoryHandle());
                }
            }
        }
    }
}
