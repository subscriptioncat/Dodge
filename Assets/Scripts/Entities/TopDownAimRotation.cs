using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownAimRotation : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer characterRenderer;

    [SerializeField]
    private SpriteRenderer shadowRenderer;

    private TopDownCharacterController _controller;

    private void Awake()
    {
        _controller = GetComponent<TopDownCharacterController>();
    }

    void Start()
    {
        _controller.OnLookEvent += OnAim;
    }

    public void OnAim(Vector2 newAimDirection)
    {
        RotateCharacter(newAimDirection);
    }

    private void RotateCharacter(Vector2 direction)
    {
        float characterRotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        characterRenderer.transform.rotation = Quaternion.Euler(0, 0, characterRotZ);
        shadowRenderer.transform.rotation = Quaternion.Euler(0, 0, characterRotZ);
    }
}
