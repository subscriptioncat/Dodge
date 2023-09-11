using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : BaseCharacter
{
    public PlayerData(string name, int hp, int atk, int atkSpeed, bool isDie) : base(name , hp, atk)
    {
        AtkSpeed = atkSpeed;
        IsDie = isDie;
    }
}
