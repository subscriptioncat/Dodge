using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using UnityEngine;

public class PlayerData : BaseCharacter
{
    [SerializeField]
    private int player;

    public float AtkSpeed
    {
        get => m_AttackSpeed;
        set
        {
            GetComponent<PlayerInputController>()
                .SetAttackSpeed(value <= 0 ? float.MinValue : value);
            m_AttackSpeed = value <= 0 ? float.MinValue : value;
        }
    }
    private float m_AttackSpeed;
    public int Score { get; set; }

    //BulletData bullet;
    private bool isUnHitTime;
    private bool isHit;

    private void Awake()
    {
        Type = "player";
        Hp = 5;
        Atk = 10;
        Speed = 5;

        spriteRenderer = sprite.GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (
            collision.gameObject.CompareTag("Monster")
            || collision.gameObject.GetComponent<BulletController>().isPlayer == false
        )
        {
            // 무적시간
            if (isUnHitTime)
                return;
            // 총알 겹쳐있을때 연속으로 히트 방지
            if (isHit)
                return;

            isHit = true;
            TakeDamage(player, 1);
            Invoke("OffDamage", 3f);
            isHit = false;
        }
        else if (collision.gameObject.tag == "Item")
        {
            GameObject item = collision.gameObject;
            switch (item.GetComponent<Item>().type)
            {
                case "bulletTime":
                    UnityEngine.Debug.Log("bulletTime");
                    //bullet.Delay /= 2;
                    break;
                case "timeSlow":
                    StartCoroutine(EnemySlow());
                    break;
                case "superPower":
                    UnityEngine.Debug.Log("superPower");
                    isUnHitTime = true;
                    spriteRenderer.color = new Color(1, 1, 1, 0.4f);
                    Invoke("OffDamage", 5f);
                    isUnHitTime = false;
                    break;
            }
            Destroy(item);
        }
    }

    IEnumerator EnemySlow()
    {
        for (int i = 0; i < 15; i++)
        {
            // 적 태그 다르면 수정해주세요 ***
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            // 적 총알 태그 다르면 수정해주세요 ***
            GameObject[] bullet = GameObject.FindGameObjectsWithTag("EnemyBullet");
            for (int index = 0; index < enemies.Length; index++)
            {
                // 적 속도 감소
                //enemies[index].GetComponent<EnemyData>().Speed = 0.3f;
            }
            for (int index = 0; index < bullet.Length; index++)
            {
                // 적 탄환 속도 감소
                bullet[index].GetComponent<BulletData>().Speed = 0.6f;
            }
            yield return new WaitForSeconds(0.2f);
        }
    }
}
