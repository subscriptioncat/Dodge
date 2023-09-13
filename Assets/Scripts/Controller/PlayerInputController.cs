using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : TopDownCharacterController
{
    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
    }

    public void OnMove(InputValue value)
    {
        Vector2 moveInput = value.Get<Vector2>().normalized;
        CallMoveEvent(moveInput);
    }

    public void OnLook(InputValue value)
    {
        Vector2 lookInput = value.Get<Vector2>().normalized;
        Vector2 modifiedLookInput = new Vector2(lookInput.y, -lookInput.x);
        CallLookEvent(modifiedLookInput);
    }

    public void OnFire(InputValue value)
    {
        IsAttacking = value.isPressed;
    }

    public void SetAttackSpeed(float speed)
    {
        base.m_Delay = 1.0f / speed;
    }
}
