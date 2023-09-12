using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.Android;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance = null;

    public DataManager()
    {
        if (Instance == null)
            Instance = this;
    }

    public GameObject Bullet { get; set; }
    public Sprite User1Image { get; set; }
    public Sprite User2Image { get; set; }
}
