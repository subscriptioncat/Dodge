using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.Android;

public class DataManager
{
    public static DataManager Instance = null;
    public DataManager()
    {
        if (Instance == null)
            Instance = this;
    }

    [SerializeField] public GameObject Bullet { get; private set; }
}
