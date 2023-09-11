using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownShooting : MonoBehaviour
{
    private TopDownCharacterController _contoller;
    [SerializeField] private Transform projectileSpawnPosition;
    private Vector2 _aimDirection = Vector2.right;
    public GameObject bulletPrefab;
    [SerializeField]
    private BulletData bulletData;

    private BaseCharacter ObjectData;

    private void Awake()
    {
        _contoller = GetComponent<TopDownCharacterController>();
        //ObjectData = GetComponent<PlayerData>();
        ObjectData = new PlayerData("Palyer", 100, 10, 10, false);
    }
    // Start is called before the first frame update
    void Start()
    {
        _contoller.OnAttackEvent += OnShoot;
        _contoller.OnLookEvent += OnAim;
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
        GameObject newBullet = Instantiate(bulletPrefab, projectileSpawnPosition.position, projectileSpawnPosition.rotation);
        newBullet.GetComponent<BulletController>().BulletData = bulletData;
        newBullet.GetComponent<BulletController>().Direction = bulletData.Foward(transform.rotation);
        newBullet.GetComponent<BulletController>().ThisObjectData = ObjectData;
    }
}