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
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag  == "Player")
        {
            //피까는 처리
            Debug.Log("플레이어 피격");
        }
        else if(other.tag == "Monster")
        {
            //피까는처리
            Debug.Log("몬스터 피격");
        }
    }
}
