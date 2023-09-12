using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCharacter : MonoBehaviour
{
    public string Name { get; }
    public int HP { get; set; }
    public int Atk { get; set; }
    public float AtkSpeed { get; set; }
    public bool IsDie { get; set; }
    
    public BaseCharacter()
    {
        Name = "Player";
        HP = 100;
        Atk = 10;
        AtkSpeed = 10f;
        IsDie = false;
    }

    public BaseCharacter(string name, int hp, int atk) {
        this.Name = name;
        this.HP = hp;
        this.Atk = atk;
    }

    public virtual void TakeDamage(int damage) {
        HP -= damage;
        if (HP <= 0) {
            IsDie = true;
        }
    }
}
