using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance = null;

    public UIManager()
    {
        if (Instance = null)
        {
            Instance = this;
        }
    }

    [SerializeField]
    GameObject player2Panel;

    [SerializeField]
    GameObject player1IconSprite;

    [SerializeField]
    GameObject player2IconSprite;

    [SerializeField]
    GameObject player1AirplaneSprite;

    [SerializeField]
    GameObject player2AirplaneSprite;

    void Awake()
    {
        InitUI();
    }

    void InitUI()
    {
        switch (DataManager.Instance.playerCount)
        {
            case 1:
                player1AirplaneSprite.GetComponent<Image>().sprite = DataManager
                    .Instance
                    .User1Image;
                break;

            case 2:
                player1AirplaneSprite.GetComponent<Image>().sprite = DataManager
                    .Instance
                    .User1Image;
                player2AirplaneSprite.GetComponent<Image>().sprite = DataManager
                    .Instance
                    .User2Image;
                break;
        }
    }
}
