using UnityEngine;

public class Cursor : MonoBehaviour
{
    protected Direction direction;

    private int pastCursorLocation;
    public int currentCursorLocation;
    protected bool isSelect;
    private string playerAirplane;
    int airplanesCount;
    int airplanesRowCount;

    private void Start()
    {
        airplanesCount = SelectManager.instance.airplanesCount;
        airplanesRowCount = airplanesCount / 2;
        pastCursorLocation = -1;
        isSelect = false;
    }

    protected void CursorDirection(Direction direction)
    {
        pastCursorLocation = currentCursorLocation;

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

    public void SelectAirplane()
    {
        isSelect = true;

        SelectManager.instance.airplanesStatus[currentCursorLocation] = SelectManager
            .Airplane
            .Selected;

        transform.Find("UnselectedCursor").gameObject.SetActive(false);
        transform.Find("SelectedCursor").gameObject.SetActive(true);
    }

    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (other != null)
        {
            other.transform.parent.Find("AirplaneSelected").gameObject.SetActive(true);
            other.transform.parent.Find("AirplaneUnselected").gameObject.SetActive(false);

            playerAirplane = other.transform.parent.name;
        }
    }

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
