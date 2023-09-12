using System.Collections;
using System.Collections.Generic;
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
    private GameObject Player1;

    [SerializeField]
    private GameObject Player2;

    private void Awake()
    {
        InitPlayer();
    }

    private void InitPlayer()
    {
        if (DataManager.Instance.User2Image == null)
        {
            Player1.GetComponentInChildren<SpriteRenderer>().sprite = DataManager
                .Instance
                .User1Image;
            Player2.SetActive(false);
            Player1.transform.position = Vector2.zero;
        }
        else
        {
            Player1.GetComponentInChildren<SpriteRenderer>().sprite = DataManager
                .Instance
                .User1Image;
            Player2.GetComponentInChildren<SpriteRenderer>().sprite = DataManager
                .Instance
                .User2Image;
            Player1.transform.position = new Vector2(-2, 0);
            Player2.transform.position = new Vector2(2, 0);
        }
    }
}
