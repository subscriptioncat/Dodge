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

    private CharacterAudio m_Audio;

    private void Awake()
    {
        Type = "player";
        Hp = 5;
        Atk = 10;
        Speed = 5;

        spriteRenderer = sprite.GetComponent<SpriteRenderer>();
        m_Audio = gameObject.GetComponent<CharacterAudio>();
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
            isUnHitTime = true;
            spriteRenderer.color = new Color(1, 1, 1, 0.4f);
            Invoke("OffDamage", 5f);
            isUnHitTime = false;
            Destroy(collision.gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Wall"))
        {
            UnityEngine.Debug.Log("Wall!!");
        }
    }

    public override void TakeDamage(int player, int damage)
    {
        base.TakeDamage(player, damage);
        m_Audio.PlayHit();
    }
}
