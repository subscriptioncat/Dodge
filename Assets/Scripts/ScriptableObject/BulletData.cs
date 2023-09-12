using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Bullet Data", menuName = "Scriptable Object/Bullet Data", order = int.MaxValue)]
public class BulletData : ScriptableObject
{
    [SerializeField]
    private float count;
    public float Count { get { return count; } }
    [SerializeField]
    private bool isPlayer;
    public bool IsPlayer { get { return isPlayer; } }
    [SerializeField]
    private float speed;
    public float Speed { get { return speed; } }
    
}
