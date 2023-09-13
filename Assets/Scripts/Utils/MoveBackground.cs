using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBackground : MonoBehaviour
{
    [SerializeField]
    public float speed;

    [SerializeField]
    private GameObject background1;

    [SerializeField]
    private GameObject background2;

    private Vector3 location;
    BoxCollider2D triggerY;

    private void Awake()
    {
        // triggerY
    }

    private void Update()
    {
        location = transform.position;

        transform.position += speed * Time.deltaTime * Vector3.down;

        // if ()
    }
}
