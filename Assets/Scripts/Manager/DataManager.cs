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
    public int playerCount = 1;
    public GameObject Bullet;
    public Sprite User1Image;
    public Sprite User2Image;

    public float EffectVolume = 0.5f;
    public float BGMVolume = 0.5f;
}
