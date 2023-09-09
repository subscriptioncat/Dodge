using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectManager : MonoBehaviour
{
    private GameObject player1Cursor;
    private GameObject player2Cursor;
    private GameObject player1Airplane;
    private GameObject player2Airplane;

    private void Awake()
    {
        player1Cursor = GameObject.Find("Player1Cursor");
        player2Cursor = GameObject.Find("Player2Cursor");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("MainScene");
        }
        else if (Input.GetKeyDown(KeyCode.F1))
        {
            player1Cursor.GetComponent<Player1Cursor>().SelectAirplane();
        }
        else if (Input.GetKeyDown(KeyCode.F2))
        {
            player2Cursor.GetComponent<Player2Cursor>().SelectAirplane();
        }
    }

    protected enum Airplane
    {
        Unselected,
        Standby,
        Selected
    }
}
