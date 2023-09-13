using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : EnemyBullet
{
    [SerializeField]
    private BulletData bulletData;
    public BulletData BulletData
    {
        set { bulletData = value; }
    }

    private Vector2 direction;
    public Vector2 Direction
    {
        set { direction = value; }
    }
    public bool isPlayer;

    void Start()
    {
        isPlayer = bulletData.IsPlayer;
        count++;
        ChangeImage();
        Movement();
        DelayDestroy(bulletData.Delay);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && bulletData.IsPlayer == false)
        {
            //GameManager.TakeDamage(thisObjectData.collision.GetComponent<MonsterData>());
            NowDestroy();
        }
        else if (collision.tag == "Monster" && bulletData.IsPlayer == true)
        {
            //GameManager.TakeDamage(thisObjectData.collision.GetComponent<PlayerData>());
            NowDestroy();
        }
        if (collision.tag == "Wall" && bulletData.IsPlayer == false)
        {
            if (collision.transform.position.x != 0)
                direction = new Vector2(-direction.x, direction.y);
            else
                direction = new Vector2(direction.x, -direction.y);
            float posZ = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg) - 90;
            transform.rotation = Quaternion.Euler(0, 0, posZ);
            Movement();
        }
        else if (collision.tag == "Wall")
        {
            NowDestroy();
        }
    }

    private void NowDestroy()
    {
        Destroy(gameObject);
    }

    private void DelayDestroy(float delay)
    {
        Destroy(gameObject, delay);
    }

    private void Movement()
    {
        GetComponent<Rigidbody2D>().velocity = direction * bulletData.Speed;
    }

    private void ChangeImage()
    {
        transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(
            "Bullets/" + bulletData.ImageName
        );
    }

    private void OnDestroy()
    {
        count--;
    }
}
