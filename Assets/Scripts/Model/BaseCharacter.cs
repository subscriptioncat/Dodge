using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCharacter : MonoBehaviour
{
    protected string Name { get; set; }
    protected string Type { get; set; } // boss, enemy, player
    protected int Hp { get; set; }
    protected int Atk { get; set; }
    protected float Speed { get; set; }
    protected bool IsDead { get; set; }

    protected GameObject sprite;
    protected SpriteRenderer spriteRenderer;

    public void TakeDamage(int damage)
    {
        Hp -= damage;
        StartCoroutine(DamageEffect());

        if (Hp <= 0)
        {
            IsDead = true;
            Destroy(gameObject);
        }
    }

    IEnumerator DamageEffect()
    {
        for (int i = 0; i < 3; i++)
        {
            spriteRenderer.color = Color.red;
            yield return new WaitForSeconds(0.2f);
            spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(0.2f);
        }
    }

    private void OnDestroy()
    {
        
        //기체 폭팔 또는 사라지는 애니메이션 실행  -> 나중에 만들어야됨
    }

    public void OffDamage()
    {
        spriteRenderer.color = new Color(1, 1, 1, 1);
    }
}
