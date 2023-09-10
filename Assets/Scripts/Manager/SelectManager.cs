using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectManager : MonoBehaviour
{
    public static SelectManager instance = null;

    private GameObject player1Cursor;
    private GameObject player2Cursor;
    private readonly GameObject player1Airplane;
    private readonly GameObject player2Airplane;

    public GameObject[] airplanes;
    public Airplane[] airplanesStatus;
    public int airplanesCount;

    public bool[] player;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        // else
        // {
        //     if (instance != this)
        //     {
        //         Destroy(this.gameObject);
        //     }
        // }

        instance = this;

        InitPlayer();
        InitAirplane();
        InitCursor();
    }

    private void Update()
    {
        GameControll();
    }

    public void InitPlayer()
    {
        // 플레이어 수 초기화
        player = new bool[3];
        player[2] = false; // Player2는 비활성화
    }

    // 커서 초기화
    private void InitCursor()
    {
        // 커서 오브젝트 맵핑
        player1Cursor = GameObject.Find("Player1Cursor");
        player2Cursor = GameObject.Find("Player2Cursor");

        // 첫 번째 비행기 position에 Player1 커서 배치
        player1Cursor.transform.position = airplanes[0].transform.position;
        airplanesStatus[0] = Airplane.Standby;
        player1Cursor.GetComponent<Player1Cursor>().currentCursorLocation = 0;
    }

    private void InitAirplane()
    {
        airplanes = GameObject.FindGameObjectsWithTag("Airplane");
        airplanesCount = airplanes.Length;
        airplanesStatus = new Airplane[airplanesCount];

        for (int i = 0; i < airplanesCount; i++)
        {
            airplanesStatus[i] = Airplane.Unselected;
        }
    }

    private void GameControll()
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
            if (player[2] == false)
            {
                player[2] = true;

                for (int i = 0; i < airplanesCount; i++)
                {
                    if (airplanesStatus[i] == Airplane.Unselected)
                    {
                        player2Cursor.transform.position = airplanes[i].transform.position;
                        airplanesStatus[i] = Airplane.Standby;
                        player2Cursor.GetComponent<Player2Cursor>().currentCursorLocation = i;
                        break;
                    }
                }
            }
            else
            {
                player2Cursor.GetComponent<Player2Cursor>().SelectAirplane();
            }
        }
    }

    public enum Airplane
    {
        Unselected,
        Standby,
        Selected
    }
}
