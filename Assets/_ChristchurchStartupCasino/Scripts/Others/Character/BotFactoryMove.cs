using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BotFactoryMove : Mover
{
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

    public List<ChipFactory> ChipFactories
    {
        get
        {
            List<ChipFactory> cfs = new List<ChipFactory>();
            List<SlotFactoryView> slotFactoryViews = SlotFactoryViews;
            foreach (var item in slotFactoryViews)
            {
                cfs.Add(item.SlotBase as ChipFactory);
            }

            return cfs;
        }
    }

    public ChipFactory ChipFactoryEmpty
    {
        get
        {
            return ChipFactories.Find((ChipFactory cf) => cf.getObjectPlace.sortSlot.ObjectCount == 0);
        }
    }

    public int BoxCount
    {
        get
        {
            return character.sortSlot.SortObjects.FindAll((SortObject so) => so.GetComponent<ObjectType>() != null && so.GetComponent<ObjectType>().ingredientType == IngredientType.Box).Count;
        }
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
        return d < 0.1f && d >= 0;
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
            yield return new WaitUntil(() => HasEndMove());
            yield return new WaitUntil(() => ChipFactoryEmpty != null);
            ChipFactory chipFactory = ChipFactoryEmpty;

            if (BoxCount == 0)
            {
                MoveTo(Game.Instance.getBoxPos.position);
            }

            yield return new WaitUntil(() => BoxCount > 0);
            if (chipFactory == null)
            {
                continue;
            }

            MoveTo(chipFactory.getObjectPlace.transform.position);
            yield return new WaitUntil(() => HasEndMove());
        }
    }
}
