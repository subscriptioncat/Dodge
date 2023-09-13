using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthSprite : MonoBehaviour
{
    [SerializeField]
    GameObject[] health;

    int pastIndex = -1;

    public void SetHealthSprite(int healthPoint)
    {
        if (pastIndex != healthPoint)
        {
            if (pastIndex != -1)
            {
                health[healthPoint].SetActive(false);
            }

            health[healthPoint].SetActive(true);

            pastIndex = healthPoint;
        }
    }
}
