using UnityEngine;

public class Player1Cursor : SelectCursor
{
    // WASD 키 입력 받아서 CursorDirection 메서드 호출
    private void Update()
    {
        // 아직 플레이어 비행기를 선택하지 않았다면
        if (isSelect == false)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                CursorDirection(Direction.Up);
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                CursorDirection(Direction.Left);
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                CursorDirection(Direction.Right);
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                CursorDirection(Direction.Down);
            }
        }
    }
}
