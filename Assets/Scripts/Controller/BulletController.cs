using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField]
    private BulletData bulletData;
    public BulletData BulletData { set { bulletData = value; } }

    [SerializeField]
    private BaseCharacter thisObjectData;
    public BaseCharacter ThisObjectData { set { thisObjectData = value; } }

    private Vector2 direction;
    public Vector2 Direction { set { direction = value; } }
    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = direction * thisObjectData.AtkSpeed;
        Destroy(gameObject, bulletData.Duration);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {

            //GameManager.TakeDamage(thisObjectData.collision.GetComponent<MonsterData>());
            NowDestroy();
        }
        else if(collision.tag == "Monster")
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
