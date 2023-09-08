using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class MonsterController// : TopDownCharacterController
{
    public List<MonsterPattern> _pattern = new List<MonsterPattern>();
    public void Pattern()
    {

    }

    /// <summary>
    /// 현재 위치에서 지정된 방향으로 이동
    /// </summary>
    public void Move()
    {

    }

    /// <summary>
    /// 현재 위치에서 Bullet을 지정된 방향으로 발사.
    /// </summary>
    public void Fire()
    {
        GameObject bulletModel = DataManager.Instance.Bullet;

    }

    /// <summary>
    /// 현재 위치에서 지정된 방향을 바라봄.
    /// </summary>
    public void Look()
    {

    }
}
public enum ePatternType
{
    Move,
    Aim,
    Fire,
    Look
}