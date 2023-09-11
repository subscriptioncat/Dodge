using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MonsterEndCondition", menuName = "Monster/EndCondition", order = 2)]
public class MonsterEndCondition : ScriptableObject
{
    public bool IsEnd { get; protected set; }
}
