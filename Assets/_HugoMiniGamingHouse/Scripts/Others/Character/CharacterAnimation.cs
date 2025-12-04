using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    [SerializeField] private Transform body;
    [SerializeField] private float rotationSpeed = 50;
    [SerializeField] private Animator animator;

    public void SetAnimator(Animator animator)
    {
        this.animator = animator;
    }

    public void SetBool(string name, bool value)
    {
        if (animator == null)
        {
            return;
        }

        animator.SetBool(name, value);
    }

    public void SetInt(string name, int value)
    {
        if (animator == null)
        {
            return;
        }

        animator.SetInteger(name, value);
    }

    public void SetFloat(string name, float value)
    {
        if (animator == null)
        {
            return;
        }

        animator.SetFloat(name, value);
    }

    public int GetInt(string name)
    {
        if (animator == null)
        {
            return -1;
        }

        return animator.GetInteger(name);
    }

    public void SetTrigger(string name)
    {
        if (animator == null)
        {
            return;
        }

        animator.SetTrigger(name);
    }

    public void SetRotationSpeed(float value)
    {
        rotationSpeed = value;
    }

    public void RotationBody(Vector3 direction)
    {
        Quaternion inputRotate = Quaternion.LookRotation(direction);
        body.rotation = Quaternion.Lerp(body.rotation, inputRotate, rotationSpeed);
    }

    public virtual void ActiveMove()
    {
        SetInt("State", 0);
    }

    public virtual void ActiveIdle()
    {
        SetInt("State", 1);
    }

    public void SetBlendIdle(float value)
    {
        SetFloat("Idle_Blend", value);
    }

    public void SetBlendMove(float value)
    {
        SetFloat("Move_Blend", value);
    }

}
