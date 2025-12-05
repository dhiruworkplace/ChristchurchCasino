using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

public class CustomerMover : Mover
{
    private Vector3 firstPos;
    private ChipMachine chipMachine;

    public List<SlotMachineView> SlotMachineViews {
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

    public SlotMachineView ActiveSlot
    {
        get
        {
            List<SlotMachineView> smv = SlotMachineViews;
            smv.Shuffle();
            return smv.Find((SlotMachineView s) => s.SlotBase != null && !(s.SlotBase as ChipMachine).HasCharacter);
        }
    }

    public void Stopping()
    {
        agent.isStopped = true;
    }

    public void SetRandomMove()
    {
        RandomAgent();
    }

    private void RandomAgent()
    {
        MoveTo(GetRandomLocation());
    }

    private Vector3 GetRandomLocation()
    {
        NavMeshTriangulation navMeshData = NavMesh.CalculateTriangulation();

        int maxIndices = navMeshData.indices.Length - 3;

        int firstVertexSelected = Random.Range(0, maxIndices);
        int secondVertexSelected = Random.Range(0, maxIndices);

        Vector3 point = navMeshData.vertices[navMeshData.indices[firstVertexSelected]];

        Vector3 firstVertexPosition = navMeshData.vertices[navMeshData.indices[firstVertexSelected]];
        Vector3 secondVertexPosition = navMeshData.vertices[navMeshData.indices[secondVertexSelected]];

        if ((int)firstVertexPosition.x == (int)secondVertexPosition.x ||
            (int)firstVertexPosition.z == (int)secondVertexPosition.z
            )
        {
            point = GetRandomLocation();
        }
        else
        {
            point = Vector3.Lerp(
                                            firstVertexPosition,
                                            secondVertexPosition,
                                            Random.Range(0.05f, 0.95f)
                                        );
        }
        return point;
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
            if (chipMachine != null)
            {
                characterAnimation.RotationBody(chipMachine.playPoint.forward);
            }
        }
        else
        {
            Rotation();
            characterAnimation.ActiveMove();
        }
    }

    private IEnumerator Start()
    {
        characterAnimation.SetBool("IsWalk", true);
        firstPos = transform.position;
        while (true)
        {
            yield return new WaitUntil(() => HasEndMove());
            yield return new WaitUntil(() => ActiveSlot != null);
            chipMachine = ActiveSlot.SlotBase as ChipMachine;
            MoveTo(chipMachine.playPoint.position);
            yield return new WaitUntil(() => HasEndMove());
            if (chipMachine.HasCharacter)
            {
                continue;
            }
            transform.DORotate(chipMachine.playPoint.eulerAngles, 0.2f);
            chipMachine.AddCharacter(character);
            yield return new WaitUntil(() => chipMachine.IsDone);
            chipMachine.RemoveCharacter(character);
            yield return new WaitUntil(() => character.sortSlot.ObjectCount == 0);
            MoveTo(firstPos);
            Game.Instance.gameLoader.RemoveCustomer(gameObject);
            yield return new WaitUntil(() => HasEndMove());
            Destroy(gameObject);
        }
    }
}
