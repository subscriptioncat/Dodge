using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField]
    private BulletData bulletData;
    public BulletData BulletData { set { bulletData = value; } }

    private Vector2 direction;
    public Vector2 Direction { set { direction = value; } }

    void Start()
    {
        GetComponentInChildren<SpriteRenderer>().sprite = Re
        GetComponent<Rigidbody2D>().velocity = direction * bulletData.Speed;
        Destroy(gameObject, 3f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" && bulletData.IsPlayer == false)
        {

            //GameManager.TakeDamage(thisObjectData.collision.GetComponent<MonsterData>());
            NowDestroy();
        }
        else if(collision.tag == "Monster" && bulletData.IsPlayer == true)
        {
            //GameManager.TakeDamage(thisObjectData.collision.GetComponent<PlayerData>());
            NowDestroy();
        }
    }
    private void NowDestroy()
    {
        Destroy(gameObject);
    }
}
