using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBackground : MonoBehaviour
{
    [SerializeField]
    [Range(1.0f, 20.0f)]
    float speed;

    private Vector2 startPos;
    private float repeatWidth;

    private void Awake()
    {
        startPos = transform.position;
        repeatWidth = GetComponent<BoxCollider2D>().size.y / 2;
    }

    private void Update()
    {
        if (transform.position.y < startPos.y - repeatWidth)
        {
            transform.position = startPos;
        }

        transform.position += Vector3.down * Time.deltaTime * speed;
    }
}
