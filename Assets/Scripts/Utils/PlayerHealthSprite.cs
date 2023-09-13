using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthSprite : MonoBehaviour
{
    [SerializeField]
    GameObject[] health;

    int startIndex = 5;

    public void SetHealthSprite(int healthPoint)
    {
        if (startIndex != healthPoint)
        {
            if (startIndex != -1)
            {
                health[startIndex].SetActive(false);
            }

            health[healthPoint].SetActive(true);

            startIndex = healthPoint;
        }
    }
}
