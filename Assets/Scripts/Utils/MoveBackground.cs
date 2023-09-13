using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBackground : MonoBehaviour
{
    [SerializeField]
    [Range(1f, 20f)]
    float speed;

    [SerializeField]
    private float posValue;

    Vector2 startPos;
    float newPos;

    private void Start()
    {
        startPos = transform.position;
    }

    private void Update()
    {
        newPos = Mathf.Repeat(Time.deltaTime * speed, posValue);
        transform.position = startPos + Vector2.down * newPos;
    }
}
