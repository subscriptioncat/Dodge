using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class PlayerData : BaseCharacter
{
    public string Name { get { return name; } }
    public int HP { get { return hp; } }
    public int ATK { get { return atk; } }
    public float ATKSpeed { get { return atkSpeed; } }
    public bool IsDie { get { return isDie; } }

    [SerializeField]
    private float speed;
    public float Speed { get { return speed; } }

    public PlayerData()
    {

    }

    public PlayerData(string _name, int _hp, int _atk, float _atkSpeed, float _speed)
    {
        name = _name;
        hp = _hp;
        atk = _atk;
        atkSpeed = _atkSpeed;
        isDie = false;
        speed = _speed;
    }
}
