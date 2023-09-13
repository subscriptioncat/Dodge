using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterHP : MonoBehaviour
{
    private GameObject player1;
    private GameObject player2;
    public int Player1Hp = 100;
    public int Player2Hp = 100;

    private RankingManager rankingManager;

    private void Start()
    {
        rankingManager = FindObjectOfType<RankingManager>();
    }
    void TakeDamage(int damageAmount)
    {
        Player1Hp -= damageAmount;
        Player2Hp -= damageAmount;
        if (Player1Hp <=0 && Player2Hp <=0)
        {
            int endTime = Mathf.FloorToInt(Time.time);
            rankingManager.AddRankingEntry("Player", endTime);
            LoadEndScene();
        }
    }

    void LoadEndScene()
    {
        SceneManager.LoadScene("EndScene");
    }
}
