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
        CallLookEvent(lookInput);
    }

    public void OnFire(InputValue value)
    {
       Vector2 fireInput = value.Get<Vector2>().normalized;
       CallFireEvent(fireInput);
       
    }

    
}