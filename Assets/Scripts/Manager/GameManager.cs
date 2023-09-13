using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEditor.U2D;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;

    public GameManager()
    {
        if (Instance = null)
        {
            Instance = this;
        }
    }

    [SerializeField]
    private GameObject player1;

    [SerializeField]
    private GameObject player2;

    private void Awake()
    {
        InitPlayer();
    }

    private void InitPlayer()
    {
        switch (DataManager.Instance.playerCount)
        {
            case 1:
                player1.GetComponent<PlayerSprite>().SetSprite(DataManager.Instance.User1Image);
                player1.transform.position = new Vector2(0, -2);
                player2.SetActive(false);
                break;

            case 2:
                player1.GetComponent<PlayerSprite>().SetSprite(DataManager.Instance.User1Image);
                player2.GetComponent<PlayerSprite>().SetSprite(DataManager.Instance.User2Image);
                player1.transform.position = new Vector2(-2, -2);
                player2.transform.position = new Vector2(2, -2);
                break;
        }
    }
}
