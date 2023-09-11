using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectManager : MonoBehaviour
{
    public static SelectManager instance = null;

    private GameObject player1Cursor;
    private GameObject player2Cursor;

    // 다음 씬으로 전달할 오브젝트
    private GameObject player1Airplane;
    private GameObject player2Airplane;

    public GameObject[] airplanes; // 하이어라이키에 위치하는 비행기 오브젝트
    public Airplane[] airplanesStatus; // 각 인덱스 별 비행기 상태를 담아둘 배열
    public int airplanesCount; // 비행기 총 개수

    public bool[] player; // player[1] : 플레이어 1 플레이 여부 | player[2] : 플레이어 2 플레이 여부

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
        player1Cursor.GetComponent<Player1Cursor>().InitCursorLocation(0);
    }

    // 비행기 오브젝트 초기화
    private void InitAirplane()
    {
        airplanes = GameObject.FindGameObjectsWithTag("Airplane");
        airplanesCount = airplanes.Length;
        airplanesStatus = new Airplane[airplanesCount];

        // 배열에 담은 모든 비행기 상태를 'Unselected'로 초기화
        for (int i = 0; i < airplanesCount; i++)
        {
            airplanesStatus[i] = Airplane.Unselected;
        }
    }

    private void GameControll()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // MainScene으로 씬 이동
        {
            SceneManager.LoadScene("MainScene");
        }
        else if (Input.GetKeyDown(KeyCode.F1)) // 플레이어 1 비행기 선택
        {
            player1Airplane = airplanes[
                player1Cursor.GetComponent<Player1Cursor>().SelectAirplane()
            ];
        }
        else if (Input.GetKeyDown(KeyCode.F2)) // 플레이어 2 커서 추가
        {
            if (player[2] == false)
            {
                player[2] = true;

                for (int i = 0; i < airplanesCount; i++)
                {
                    if (airplanesStatus[i] == Airplane.Unselected) // 비행기 배열 0번부터 미선택된 비행기를 찾아
                    {
                        player2Cursor.transform.position = airplanes[i].transform.position; // 해당 위치로 Player2 커서 이동
                        airplanesStatus[i] = Airplane.Standby; // 해당 인덱스의 비행기 상태를 'Standby'로 변경
                        player2Cursor.GetComponent<Player2Cursor>().InitCursorLocation(i); // 현재 인덱스 값을 Player2Cursor 스크립트로 넘겨줌
                        break;
                    }
                }
            }
            else // 플레이어 2 비행기 선택
            {
                player2Airplane = airplanes[
                    player2Cursor.GetComponent<Player2Cursor>().SelectAirplane()
                ];
            }
        }
    }

    public enum Airplane
    {
        Unselected, // 미선택한 비행기
        Standby, // 현재 커서가 위치하고 있음
        Selected // 선택된 비행기
    }
}
