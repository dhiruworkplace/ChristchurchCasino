using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMover : Mover
{
    public float sensitivity = 0.5f;

    private Vector3 fistPos;
    private bool touch;

    public Joystick joystick;
    private Vector3 direction;

    private void OnEnable()
    {
        StartCoroutine(OnControlUpdate());
    }

    protected void MobileMoving()
    {
        if (agent.enabled == false)
        {
            return;
        }

        if (Input.touchCount == 0)
        {
            characterAnimation.ActiveIdle();
            return;
        }

        Vector3 direction = Vector3.zero;

        if (Input.GetTouch(0).phase == TouchPhase.Began)
        {
            fistPos = Input.GetTouch(0).position;
            touch = true;
        }

        if (touch)
        {
            direction = (Vector3)Input.GetTouch(0).position - fistPos;

            if (direction.sqrMagnitude > 70)
            {
                fistPos = (Vector3)Input.GetTouch(0).position - (direction.normalized * 50);
            }
        }

        if (Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            touch = false;
            direction = Vector3.zero;
        }

        moveDirection = new Vector3(direction.x, 0, direction.y);

        if (moveDirection.magnitude < 0.1f)
        {
            characterAnimation.ActiveIdle();
            return;
        }

        moveDirection.Normalize();
        moveDirection *= agent.speed;

        agent.Move(moveDirection * Time.deltaTime);

        if (moveDirection != Vector3.zero)
        {
            characterAnimation.ActiveMove();
        }
    }

    protected void EditorMoving()
    {
        if (agent.enabled == false)
        {
            return;
        }

        Vector3 direction = Vector3.zero;

        if (Input.GetMouseButtonDown(0))
        {
            fistPos = Input.mousePosition;
        }

        if (Input.GetMouseButton(0))
        {
            direction = (Vector3)Input.mousePosition - fistPos;

            if (direction.sqrMagnitude > 70)
            {
                fistPos = (Vector3)Input.mousePosition - (direction.normalized * 50);
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            direction = Vector3.zero;
        }

        moveDirection = new Vector3(direction.x, 0, direction.y);

        if (moveDirection.magnitude < 0.1f)
        {
            characterAnimation.ActiveIdle();
            return;
        }

        moveDirection.Normalize();
        moveDirection *= agent.speed;

        agent.Move(moveDirection * Time.deltaTime);

        if (moveDirection != Vector3.zero)
        {
            characterAnimation.ActiveMove();
        }
    }

    public void Rotation()
    {
        if (agent.enabled == false)
        {
            return;
        }

        if (!agent.isStopped)
        {
            Vector3 rotation = new Vector3(moveDirection.x, 0, moveDirection.z);
            if (rotation != Vector3.zero)
            {
                characterAnimation.RotationBody(rotation);
            }
        }
    }

    public IEnumerator OnControlUpdate()
    {
        agent.enabled = true;

        while (true)
        {
            yield return null;

            if (Input.GetMouseButtonDown(0))
            {
                fistPos = Input.mousePosition;
                touch = true;
            }

            if (!GameManager.IsState(GameState.Playing))
            {
                continue;
            }

#if UNITY_EDITOR
            EditorMoving();
#else
                        MobileMoving();
#endif
            Rotation();
        }
    }

    private void Update()
    {
        //OnUpdate();
    }

    public override void OnUpdate()
    {
        direction = new Vector3(joystick.Direction.x, 0, joystick.Direction.y);
        agent.Move(direction * agent.speed * Time.deltaTime);

        if (direction != Vector3.zero)
        {
            characterAnimation.RotationBody(direction);
        }

        if (direction != Vector3.zero)
        {
            characterAnimation.ActiveMove();
        }
        else
        {
            characterAnimation.ActiveIdle();
        }
    }
}
