using UnityEngine;

public class SelectCursor : MonoBehaviour
{
    private int pastCursorLocation; // ���� Ŀ�� ��ġ
    private int currentCursorLocation; // ���� Ŀ�� ��ġ
    protected bool isSelect; // �÷��̾� ����� Ȯ�� ���� ����
    private int airplanesCount; // ��ϵ� ����� ����
    private int airplanesRowCount; // �� �����ٸ��� ��ġ�ϰ� �ִ� ����� ����

    [SerializeField]
    private GameObject unselectedCursor;

    [SerializeField]
    private GameObject selectedCursor;

    private void Start()
    {
        InitCursor();
    }

    private void InitCursor()
    {
        airplanesCount = SelectManager.instance.airplanesCount; // SelectManager�� �����ؼ� ����� ���� ��������
        airplanesRowCount = SelectManager.instance.airplanesRowCount; // �����ٸ��� ��ġ�ϰ� �ִ� ����� ����
        pastCursorLocation = -1; // ���� Ŀ�� ��ġ -1�� �ʱ�ȭ
        isSelect = false; // ���� ���� �ʱ�ȭ
    }

    // Ŀ�� �̵�
    protected void CursorDirection(Direction direction)
    {
        // ���� Ŀ�� ��ġ ���� ���� Ŀ�� ��ġ ������ �ʱ�ȭ
        pastCursorLocation = currentCursorLocation;

        // ���� Ŀ���� ��ġ�ϰ� �ִ� ����� ���¸� 'Unselected'�� ����
        SelectManager.instance.airplanesStatus[pastCursorLocation] = SelectManager
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
                    SelectManager.instance.airplanesStatus[currentCursorLocation]
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
                    SelectManager.instance.airplanesStatus[currentCursorLocation]
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
                    SelectManager.instance.airplanesStatus[currentCursorLocation]
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
                    SelectManager.instance.airplanesStatus[currentCursorLocation]
                    != SelectManager.Airplane.Unselected
                );
                break;
        }

        transform.position = SelectManager.instance.airplanes[currentCursorLocation]
            .transform
            .position;
        SelectManager.instance.airplanesStatus[currentCursorLocation] = SelectManager
            .Airplane
            .Standby;
    }

    // ����� ���� ��
    public int SelectAirplane()
    {
        // �ش� Ŀ���� isSelect = true
        isSelect = true;

        // �ش� Ŀ���� ��ġ�ϰ� �ִ� ����� ������ 'Selected'�� ����
        SelectManager.instance.airplanesStatus[currentCursorLocation] = SelectManager
            .Airplane
            .Selected;

        // Ŀ�� �̹��� ����
        unselectedCursor.SetActive(false);
        selectedCursor.SetActive(true);

        return currentCursorLocation;
    }

    public void InitCursorLocation(int initIndex)
    {
        currentCursorLocation = initIndex;
    }

    // �ݶ��̴��� ���������� ����� ��������Ʈ ����
    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (other != null)
        {
            other.transform.parent.Find("AirplaneSelected").gameObject.SetActive(true);
            other.transform.parent.Find("AirplaneUnselected").gameObject.SetActive(false);
        }
    }

    // �ݶ��̴��� ���������� ����� ��������Ʈ ����
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