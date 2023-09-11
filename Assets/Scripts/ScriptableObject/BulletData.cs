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
    private float duration;
    public float Duration { get { return duration; } }
    public Vector2 Foward(Quaternion quaternion)
    {
        float z = quaternion.eulerAngles.z +90f;
        return new Vector2(Mathf.Cos(z * Mathf.Deg2Rad), Mathf.Sin(z * Mathf.Deg2Rad));
    }
}
