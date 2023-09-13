using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEditor.U2D;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;

    public GameManager()
    {
        if (Instance = null)
        {
            Instance = this;
        }
    }

    [SerializeField]
    private GameObject player1;

    [SerializeField]
    private GameObject player2;

    [SerializeField]
    private GameObject MonsterPrefabs;

    [SerializeField]
    private MonsterPattern MonsterData1;

    [SerializeField]
    private MonsterPattern MonsterData2;

    [SerializeField]
    private MonsterPattern MonsterData3;

    [SerializeField]
    private GameObject spawn1;
    public GameObject Spawn1
    {
        get { return spawn1; }
    }

    [SerializeField]
    private GameObject spawn2;
    public GameObject Spawn2
    {
        get { return spawn2; }
    }

    [SerializeField]
    private GameObject spawn3;
    public GameObject Spawn3
    {
        get { return spawn3; }
    }

    [SerializeField]
    private float spawnCycle;
    public float SpawnCycle
    {
        get { return spawnCycle; }
    }

    private float time;

    [SerializeField]
    private bool test;

    private void Awake()
    {
        DataManager dataManager = new DataManager();
        if (test)
        {
            DataManager.Instance.User1Image = Resources.Load<Sprite>("Planes/ship_0000");
        }
        InitPlayer();
    }

    private void InitPlayer()
    {
        switch (DataManager.Instance.playerCount)
        {
            case 1:
                player1.GetComponent<PlayerSprite>().SetSprite(DataManager.Instance.User1Image);
                player1.transform.position = new Vector2(0, -2);
                player2.SetActive(false);
                break;

            case 2:
                player1.GetComponent<PlayerSprite>().SetSprite(DataManager.Instance.User1Image);
                player2.GetComponent<PlayerSprite>().SetSprite(DataManager.Instance.User2Image);
                player1.transform.position = new Vector2(-2, -2);
                player2.transform.position = new Vector2(2, -2);
                break;
        }
    }

    private void Update()
    {
        time += Time.deltaTime;
        if (time >= spawnCycle)
        {
            MonsterSpawn();
            time = 0;
        }
    }

    //public void HitBullet(BaseCharacter gameObject, bool isPlayer)
    //{
    //    if (isPlayer)
    //    {
    //        gameObject.TakeDamage(DataManager.Instance.PlayerATK);
    //    }
    //    else
    //    {
    //        gameObject.TakeDamage(DataManager.Instance.EnemyATK);
    //    }
    //}
    //public void GameOver()
    //{
    //    Time.timeScale = 0;
    //    // 버튼 및 점수등을 출력
    //}
    public void MonsterSpawn()
    {
        System.Random random = new System.Random();
        int n = random.Next(1, 4);
        switch (n)
        {
            case 1:
                MonsterInstantiate(spawn1, MonsterData1);
                break;
            case 2:
                MonsterInstantiate(spawn2, MonsterData2);
                break;
            case 3:
                MonsterInstantiate(spawn3, MonsterData3);
                break;
        }
    }

    public void MonsterInstantiate(GameObject spawn, MonsterPattern MonsterData)
    {
        var newMonster = Instantiate(
            MonsterPrefabs,
            spawn.transform.position,
            spawn.transform.rotation
        );
        newMonster.GetComponent<MonsterController>().Pattern = MonsterData;
    }
}
