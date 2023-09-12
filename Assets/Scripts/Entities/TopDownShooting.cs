using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownShooting : MonoBehaviour
{
    private TopDownCharacterController _contoller;
    private Vector2 _aimDirection = Vector2.right;

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
        _aimDirection = newAimDirection;
    }
    private void OnShoot()
    {
        CreateProjectile();
    }

    private void CreateProjectile()
    {
        var newBullet = Instantiate(bulletPrefab, projectileSpawnPosition.position, projectileSpawnPosition.rotation).GetComponent<BulletController>();
        newBullet.BulletData = bulletData;
        newBullet.Direction = Foward(transform.rotation);
        
    }
    private void OnShoot(Vector2 Direction)
    {
        CreateProjectile(Direction);
    }
    private void CreateProjectile(Vector2 Direction)
    {
        var newBullet = Instantiate(bulletPrefab, projectileSpawnPosition.position, projectileSpawnPosition.rotation).GetComponent<BulletController>();
        newBullet.BulletData = bulletData;
        newBullet.Direction = Foward(transform.rotation);

    }
    public Vector2 Foward(Quaternion quaternion)
    {
        float z = quaternion.eulerAngles.z + 90f;
        return new Vector2(Mathf.Cos(z * Mathf.Deg2Rad), Mathf.Sin(z * Mathf.Deg2Rad));
    }

}