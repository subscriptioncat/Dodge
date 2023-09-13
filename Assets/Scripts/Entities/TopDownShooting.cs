using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownShooting : MonoBehaviour
{
    private TopDownCharacterController _contoller;
    private Vector2 aimDirection = Vector2.right;

    [SerializeField]
    private Transform projectileSpawnPosition;

    [SerializeField]
    private GameObject bulletPrefab;

    [SerializeField]
    private BulletData bulletData;

    private void Awake()
    {
        _contoller = GetComponent<TopDownCharacterController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _contoller.OnAttackEvent += OnShoot;
        _contoller.OnLookEvent += OnAim;
        _contoller.OnFireEvent += OnShoot;
    }

    private void OnAim(Vector2 newAimDirection)
    {
        aimDirection = newAimDirection;
    }

    private void CreateProjectile(Vector2 direction)
    {
        var newBullet = Instantiate(
                bulletPrefab,
                projectileSpawnPosition.position,
                projectileSpawnPosition.rotation
            )
            .GetComponent<BulletController>();
        newBullet.BulletData = bulletData;
        newBullet.Direction = direction;
    }

    public Vector2 Foward(float z)
    {
        z += 90f;
        return new Vector2(Mathf.Cos(z * Mathf.Deg2Rad), Mathf.Sin(z * Mathf.Deg2Rad));
    }

    private void OnShoot()
    {
        int n = bulletData.Count;
        int pos = -(n / 2) - 1;
        float nowPosZ = transform.rotation.eulerAngles.z;
        nowPosZ += n % 2 == 0 ? 0.5f : 0f;
        for (int i = 0; i < n; i++)
        {
            pos += 1;
            CreateProjectile(Foward(nowPosZ + pos));
        }
    }

    private void OnShoot(Vector2 direction)
    {
        int n = bulletData.Count;
        int pos = -(n / 2) - 1;
        float nowPosZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        nowPosZ += n % 2 == 0 ? 0.5f : 0f;
        for (int i = 0; i < n; i++)
        {
            pos += 1;
            CreateProjectile(Foward(nowPosZ + pos));
        }
    }
}
