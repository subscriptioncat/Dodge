using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface BaseCharacter
{
    string Name { get; set; }
    string Type { get; set; } // boss, enemy, player
    int Hp { get; set; }
    int Atk { get; set; }
    float AtkSpeed { get; set; }
    float Speed { get; set; }
    bool IsDead { get; set; }

    

    public void TakeDamage(int damage);

    //public void Shooting();
}
