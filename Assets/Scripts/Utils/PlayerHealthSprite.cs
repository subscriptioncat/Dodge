using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthSprite : MonoBehaviour
{
    [SerializeField]
    public GameObject[] health;

    public int startIndex;

    public void SetHealthSprite(int healthPoint)
    {
        health[startIndex].SetActive(false);

        health[healthPoint].SetActive(true);

        startIndex = healthPoint;
    }
}
