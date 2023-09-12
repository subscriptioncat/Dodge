using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class PlayerData : BaseCharacter
{
    public float Speed { get; private set; }
    
    public PlayerData(string name, int hp, int atk, float atkSpeed, bool isDie, float speed) : base(name,hp,atk)
    {
        AtkSpeed = 10f;
        IsDie = false;
        Speed = speed;
    }
}
