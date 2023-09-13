using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectManager : MonoBehaviour
{
    public static SelectManager Instance = null;

    public SelectManager()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    [SerializeField]
    private GameObject player1Cursor;

    [SerializeField]
    private GameObject player2Cursor;

    [SerializeField]
    public int airplanesRowCount; // �� �࿡ ��ġ�� ����� ����

    // ���� ������ ������ ������Ʈ
    private GameObject player1Airplane;
    private GameObject player2Airplane;

    private PlayerCursor player1CursorComponent;
    private PlayerCursor player2CursorComponent;

    public GameObject[] airplanes; // ���̾����Ű�� ��ġ�ϴ� ����� ������Ʈ
    public Airplane[] airplanesStatus; // �� �ε��� �� ����� ���¸� ��Ƶ� �迭
    public int airplanesCount; // ����� �� ����
    public bool[] player; // player[1] : �÷��̾� 1 �÷��� ���� | player[2] : �÷��̾� 2 �÷��� ����

    private void Awake()
    {
        InitPlayer();
        InitAirplane();
        InitCursor();
        DataManager dataManager = new DataManager();
    }

    private void Update()
    {
        GameControll();
    }

    public void InitPlayer()
    {
        // �÷��̾� �� �ʱ�ȭ
        player1Airplane = null;
        player2Airplane = null;
        player = new bool[3];
        player[2] = false; // Player2�� ��Ȱ��ȭ
    }

    // Ŀ�� �ʱ�ȭ
    private void InitCursor()
    {
        player1CursorComponent = player1Cursor.GetComponent<PlayerCursor>();
        player2CursorComponent = player2Cursor.GetComponent<PlayerCursor>();

        // ù ��° ����� position�� Player1 Ŀ�� ��ġ
        player1Cursor.transform.position = airplanes[0].transform.position;
        airplanesStatus[0] = Airplane.Standby;
        player1CursorComponent.InitCursorLocation(0);
    }

    // ����� ������Ʈ �ʱ�ȭ
    private void InitAirplane()
    {
        airplanesCount = airplanes.Length;
        airplanesStatus = new Airplane[airplanesCount];

        // �迭�� ���� ��� ����� ���¸� 'Unselected'�� �ʱ�ȭ
        for (int i = 0; i < airplanesCount; i++)
        {
            airplanesStatus[i] = Airplane.Unselected;
        }
    }

    private void GameControll()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // MainScene���� �� �̵�
        {
            if (
                ((player[2] == false) && (player1Airplane != null))
                || ((player[2] == true) && (player1Airplane != null) && (player2Airplane != null))
            )
            {
                if (player[2] == false)
                {
                    DataManager.Instance.playerCount = 1;
                }
                else
                {
                    DataManager.Instance.playerCount = 2;
                }
                BGMPlayer.PlayBGM(2);
                SceneManager.LoadScene("MainScene");
            }
        }
        else if (Input.GetKeyDown(KeyCode.F1)) // �÷��̾� 1 ����� ����
        {
            player1Airplane = airplanes[player1CursorComponent.SelectAirplane()];

            DataManager.Instance.User1Image = player1Airplane
                .GetComponent<SelectAirplane>()
                .selectedAirplane.GetComponent<SpriteRenderer>()
                .sprite;
        }
        else if (Input.GetKeyDown(KeyCode.F2)) // �÷��̾� 2 Ŀ�� �߰�
        {
            if (player[2] == false)
            {
                player[2] = true;

                for (int i = 0; i < airplanesCount; i++)
                {
                    if (airplanesStatus[i] == Airplane.Unselected) // ����� �迭 0������ �̼��õ� ����⸦ ã��
                    {
                        player2Cursor.transform.position = airplanes[i].transform.position; // �ش� ��ġ�� Player2 Ŀ�� �̵�
                        airplanesStatus[i] = Airplane.Standby; // �ش� �ε����� ����� ���¸� 'Standby'�� ����
                        player2CursorComponent.InitCursorLocation(i); // ���� �ε��� ���� Player2Cursor ��ũ��Ʈ�� �Ѱ���
                        break;
                    }
                }
            }
            else // �÷��̾� 2 ����� ����
            {
                player2Airplane = airplanes[player2CursorComponent.SelectAirplane()];

                DataManager.Instance.User2Image = player2Airplane
                    .GetComponent<SelectAirplane>()
                    .selectedAirplane.GetComponent<SpriteRenderer>()
                    .sprite;
            }
        }
    }

    public enum Airplane
    {
        Unselected, // �̼����� �����
        Standby, // ���� Ŀ���� ��ġ�ϰ� ����
        Selected // ���õ� �����
    }
}
