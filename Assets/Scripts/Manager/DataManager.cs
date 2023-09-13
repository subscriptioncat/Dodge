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

    public GameObject Bullet;
    public Sprite User1Image;
    public Sprite User2Image;

    public float EffectVolume;
    public float BGMVolume;
}
