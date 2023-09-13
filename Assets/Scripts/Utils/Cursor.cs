using UnityEngine;

public class Cursor : MonoBehaviour
{
    private int pastCursorLocation; // 이전 커서 위치
    private int currentCursorLocation; // 현재 커서 위치
    protected bool isSelect; // 플레이어 비행기 확정 선택 여부
    private int airplanesCount; // 등록된 비행기 개수
    private int airplanesRowCount; // 각 가로줄마다 위치하고 있는 비행기 개수

    [SerializeField]
    protected int player;

    [SerializeField]
    private GameObject unselectedCursor;

    [SerializeField]
    private GameObject selectedCursor;

    public AudioClip ClickClip;

    private void Start()
    {
        InitCursor();
    }

    private void InitCursor()
    {
        airplanesCount = SelectManager.Instance.airplanesCount; // SelectManager에 접근해서 비행기 개수 가져오기
        airplanesRowCount = SelectManager.Instance.airplanesRowCount; // 가로줄마다 위치하고 있는 비행기 개수
        pastCursorLocation = -1; // 이전 커서 위치 -1로 초기화
        isSelect = false; // 선택 여부 초기화
    }

    // 커서 이동
    protected void CursorDirection(Direction direction)
    {
        SoundManager.PlayAudio(eSoundType.Player, new SoundManager.SoundData(ClickClip, Vector2.zero));
        // 이전 커서 위치 값을 현재 커서 위치 값으로 초기화
        pastCursorLocation = currentCursorLocation;

        // 이전 커서가 위치하고 있던 비행기 상태를 'Unselected'로 변경
        SelectManager.Instance.airplanesStatus[pastCursorLocation] = SelectManager
            .Airplane
            .Unselected;

        switch (direction)
        {
            case Direction.Up:
                do
                {
                    currentCursorLocation -= airplanesRowCount;

                    if (currentCursorLocation < 0)
                    {
                        currentCursorLocation += airplanesCount;
                    }
                } while (
                    SelectManager.Instance.airplanesStatus[currentCursorLocation]
                    != SelectManager.Airplane.Unselected
                );
                break;

            case Direction.Down:
                do
                {
                    currentCursorLocation += airplanesRowCount;

                    if ((airplanesCount - 1) < currentCursorLocation)
                    {
                        currentCursorLocation -= airplanesCount;
                    }
                } while (
                    SelectManager.Instance.airplanesStatus[currentCursorLocation]
                    != SelectManager.Airplane.Unselected
                );
                break;

            case Direction.Left:
                do
                {
                    currentCursorLocation -= 1;

                    if (currentCursorLocation < 0)
                    {
                        currentCursorLocation += airplanesCount;
                    }
                } while (
                    SelectManager.Instance.airplanesStatus[currentCursorLocation]
                    != SelectManager.Airplane.Unselected
                );
                break;

            case Direction.Right:
                do
                {
                    currentCursorLocation += 1;

                    if (airplanesCount - 1 < currentCursorLocation)
                    {
                        currentCursorLocation -= airplanesCount;
                    }
                } while (
                    SelectManager.Instance.airplanesStatus[currentCursorLocation]
                    != SelectManager.Airplane.Unselected
                );
                break;
        }

        transform.position = SelectManager.Instance.airplanes[currentCursorLocation]
            .transform
            .position;
        SelectManager.Instance.airplanesStatus[currentCursorLocation] = SelectManager
            .Airplane
            .Standby;
    }

    // 비행기 선택 시
    public int SelectAirplane()
    {
        // 해당 커서의 isSelect = true
        isSelect = true;

        // 해당 커서가 위치하고 있는 비행기 정보를 'Selected'로 변경
        SelectManager.Instance.airplanesStatus[currentCursorLocation] = SelectManager
            .Airplane
            .Selected;

        // 커서 이미지 변경
        unselectedCursor.SetActive(false);
        selectedCursor.SetActive(true);

        return currentCursorLocation;
    }

    public void InitCursorLocation(int initIndex)
    {
        currentCursorLocation = initIndex;
    }

    // 콜라이더를 빠져나오면 비행기 스프라이트 변경
    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (other != null)
        {
            other.transform.parent.Find("AirplaneSelected").gameObject.SetActive(true);
            other.transform.parent.Find("AirplaneUnselected").gameObject.SetActive(false);
        }
    }

    // 콜라이더를 빠져나오면 비행기 스프라이트 변경
    protected void OnTriggerExit2D(Collider2D other)
    {
        if (other != null)
        {
            other.transform.parent.Find("AirplaneSelected").gameObject.SetActive(false);
            other.transform.parent.Find("AirplaneUnselected").gameObject.SetActive(true);
        }
    }

    protected enum Direction
    {
        Up,
        Left,
        Right,
        Down
    }
}
