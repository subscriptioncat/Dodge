using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCharacter : MonoBehaviour
{
    protected string name;
    [SerializeField] protected int hp;
    [SerializeField] protected int atk;
    [SerializeField] protected float atkSpeed;
    protected bool isDie;
    
    public virtual void TakeDamage(int damage) {
        hp -= damage;
        if (hp <= 0) {
            isDie = true;
        }
    }
}
