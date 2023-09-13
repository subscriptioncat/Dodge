using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEditor.U2D;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    private MonsterPattern MonsterData4;

    [SerializeField]
    private MonsterPattern MonsterData5;

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
    private GameObject spawn4;
    public GameObject Spawn4
    {
        get { return spawn4; }
    }

    [SerializeField]
    private GameObject spawn5;
    public GameObject Spawn5
    {
        get { return spawn5; }
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
        InitPlayer();
    }

    private void InitPlayer()
    {
        switch (DataManager.Instance.playerCount)
        {
            case 1:
                player1.GetComponent<PlayerSprite>().SetSprite(DataManager.Instance.User1Image);
                player1.transform.position = new Vector2(0, -2);
                Destroy(player2);
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

    public void MonsterSpawn()
    {
        System.Random random = new System.Random();
        int n = random.Next(1, 8);
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
            case 4:
                MonsterInstantiate(spawn4, MonsterData4);
                break;
            case 5:
                MonsterInstantiate(spawn5, MonsterData5);
                break;
            case 6:
                MonsterInstantiate(spawn4, MonsterData2);
                break;
            case 7:
                MonsterInstantiate(spawn5, MonsterData2);
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
