using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class Mover : MonoBehaviour
{
    public float rotationDamping = 0.18f;
    protected Vector3 moveDirection = Vector3.zero;
    protected private NavMeshAgent agent;
    protected Character character;
    protected CharacterAnimation characterAnimation;

    public Vector3 MoveDirection {
        get {
            return moveDirection;
        }

        set
        {
            moveDirection = value;
        }
    }

    public abstract void OnUpdate();

    public virtual bool IsMoving
    {
        get
        {
            return !agent.isStopped;
        }
    }

    protected virtual void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        character = GetComponent<Character>();
        if (characterAnimation == null)
        {
            characterAnimation = GetComponent<CharacterAnimation>();
        }
    }

    public virtual void MoveTo(Vector3 position, float speed = 0)
    {
        if (!agent.enabled || !agent.isOnNavMesh)
        {
            return;
        }

        agent.isStopped = false;
        agent.SetDestination(position);
    }

    public virtual void Rotation()
    {
        if (!agent.enabled || !agent.isOnNavMesh)
        {
            return;
        }

        if (agent.enabled && !agent.isStopped && agent.remainingDistance >= 0.1f)
        {
            moveDirection = agent.steeringTarget - transform.position;
            Vector3 rotation = new Vector3(moveDirection.x, 0, moveDirection.z);
            if (rotation != Vector3.zero)
            {
                characterAnimation.RotationBody(rotation);
            }
        }
        else
        {
            Vector3 dir = transform.position;
            Vector3 left = Vector3.Cross(dir, Vector3.down).normalized;

            transform.forward = Vector3.Lerp(transform.forward, left, Time.deltaTime * 5);
        }
    }

    public void SetSpeed(float speed)
    {
        agent.speed = speed;
    }

}
