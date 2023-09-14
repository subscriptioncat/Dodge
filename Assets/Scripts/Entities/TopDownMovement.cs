using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownMovement : MonoBehaviour
{
    private TopDownCharacterController _controller;

    private Vector2 _movementDirection = Vector2.zero;
    private Rigidbody2D _rigidbody;
    private bool _isPlayer;
    private void Awake()
    {
        _controller = GetComponent<TopDownCharacterController>();
        _rigidbody = GetComponent<Rigidbody2D>();
        WhoIs();
    }

    private void Start()
    {
        _controller.OnMoveEvent += Move;
        
    }

    private void FixedUpdate()
    {
        ApplyMovment(_movementDirection);
    }

    private void Move(Vector2 direction)
    {
        _movementDirection = direction;
    }

    private void ApplyMovment(Vector2 direction)
    {
        direction = direction * 10;
        RestrictingPlayerMovment();
        _rigidbody.velocity = direction;
    }
    private void WhoIs()
    {
        var gameObject = GetComponent<PlayerData>();
        if (gameObject != null)
            _isPlayer = true;
    }
    private void RestrictingPlayerMovment()
    {
        if( _isPlayer )
        {
            if (_rigidbody.position.x > 5f)
            {
                _rigidbody.position = new Vector2(5f, _rigidbody.position.y);
            }
            else if(_rigidbody.position.x < -5f)
            {
                _rigidbody.position = new Vector2(-5f, _rigidbody.position.y);
            }
            if(_rigidbody.position.y > 4f)
            {
                _rigidbody.position = new Vector2(_rigidbody.position.x, 4f);
            }
            else if(_rigidbody.position.y < -3.5f)
            {
                _rigidbody.position = new Vector2(_rigidbody.position.x, -3.5f);
            }
        }
    }
}