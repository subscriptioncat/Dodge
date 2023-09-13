using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSprite : MonoBehaviour
{
    [SerializeField]
    private GameObject playerSprite;

    [SerializeField]
    private GameObject playerShadow;

    public void SetSprite(Sprite sprite)
    {
        playerSprite.GetComponent<SpriteRenderer>().sprite = sprite;
        playerShadow.GetComponent<SpriteRenderer>().sprite = sprite;
    }
}
