using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class PlayerData : MonoBehaviour, BaseCharacter
{
    public string Name { get; set; }
    public string Type { get; set; } // boss, enemy, player
    public int Hp { get; set; }
    public int Atk { get; set; }
    public float AtkSpeed { get; set; }
    public float Speed { get; set; }
    public bool IsDead { get; set; }

    public PlayerData(string _name, string _type, int _hp, int _atk, float _atkSpeed, float _speed) {
        Name = _name;
        Type = _type;
        Hp = _hp;
        Atk = _atk;
        AtkSpeed = _atkSpeed;
        Speed = _speed;
        IsDead = false;
    }

    public void TakeDamage(int damage) {
        Hp -= damage;
        if (Hp <= 0) {
            IsDead = true;
        }
    }

    //public void Shooting();
}
