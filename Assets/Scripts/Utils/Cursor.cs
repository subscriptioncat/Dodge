using UnityEngine;

public class Cursor : MonoBehaviour
{
    protected Direction direction;

    Vector3 cursorPosition = Vector2.zero;
    readonly float cursorSpace = 1.5f;
    bool isSelect = false;
    string playerAirplane;

    protected void CursorDirection(Direction direction)
    {
        if (isSelect != true)
        {
            switch (direction)
            {
                case Direction.Up:
                    cursorPosition = Vector2.up;
                    break;
                case Direction.Left:
                    cursorPosition = Vector2.left;
                    break;
                case Direction.Right:
                    cursorPosition = Vector2.right;
                    break;
                case Direction.Down:
                    cursorPosition = Vector2.down;
                    break;
            }

            transform.position += cursorPosition * cursorSpace;
        }
    }

    public void SelectAirplane()
    {
        isSelect = true;

        Debug.Log("Player : " + playerAirplane);
    }

    protected void OnTriggerEnter2D(Collider2D other)
    {
        other.transform.parent.Find("AirplaneSelected").gameObject.SetActive(true);
        other.transform.parent.Find("AirplaneUnselected").gameObject.SetActive(false);

        if (isSelect == false)
        {
            playerAirplane = other.transform.parent.name;
        }
    }

    protected void OnTriggerExit2D(Collider2D other)
    {
        other.transform.parent.Find("AirplaneSelected").gameObject.SetActive(false);
        other.transform.parent.Find("AirplaneUnselected").gameObject.SetActive(true);
    }

    protected enum Direction
    {
        Up,
        Left,
        Right,
        Down
    }
}
