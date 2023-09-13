using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Bullet Data", menuName = "Scriptable Object/Bullet Data", order = int.MaxValue)]
public class BulletData : ScriptableObject
{
    [SerializeField]
    private int count;
    public int Count { get { return count; } }
    [SerializeField]
    private bool isPlayer;
    public bool IsPlayer { get { return isPlayer; } }
    [SerializeField]
    private float speed;
    public float Speed { get { return speed; } set { speed = value; } }
    [SerializeField]
    private string imageName;
    public string ImageName { get { return imageName; } }
    [SerializeField]
    private float delay;
    public float Delay { get { return delay; } }
}
