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
    public int playerCount { get; set; } = 1;
    public GameObject Bullet { get; set; }
    public Sprite User1Image { get; set; }
    public Sprite User2Image { get; set; }

    public float EffectVolume { get; set; }  = 0.5f;
    public float BGMVolume { get; set; }  = 0.5f;
}
