using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance = null;

    public UIManager()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public GameObject player1Panel;
    public GameObject player2Panel;

    [SerializeField]
    TextMeshProUGUI bulletCount;

    [SerializeField]
    TextMeshProUGUI timeCount;

    [SerializeField]
    GameObject player1AirplaneSprite;

    [SerializeField]
    GameObject player2AirplaneSprite;

    float time;

    void Awake()
    {
        InitUI();
    }

    void Update()
    {
        bulletCount.text = EnemyBullet.count.ToString();
        time += Time.deltaTime;
        timeCount.text = time.ToString("N2");
    }

    void InitUI()
    {
        time = 0;

        switch (DataManager.Instance.playerCount)
        {
            case 1:
                player1AirplaneSprite.GetComponent<Image>().sprite = DataManager
                    .Instance
                    .User1Image;
                break;

            case 2:
                player2Panel.SetActive(true);
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
