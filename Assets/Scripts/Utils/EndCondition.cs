using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

//[CreateAssetMenu(fileName = "MonsterEndCondition", menuName = "Monster/EndCondition", order = 2)]
[Serializable]
public class EndCondition
{
    [SerializeField]
    public enum eEndType
    {
        Count,
        Time
    }
    public eEndType Type;
    public float floatPoint;
    public int intPoint;
    public virtual bool IsEnd(float time, int count)
    {
        if (Type == eEndType.Count)
        {
            if (intPoint < count)
                return true;
        }
        else if (Type == eEndType.Time)
        {
            if (floatPoint < time)
                return true;
        }
        return false;
    }
}
