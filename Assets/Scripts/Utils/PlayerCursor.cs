using UnityEngine;

public class PlayerCursor : Cursor
{
    // WASD Ű �Է� �޾Ƽ� CursorDirection �޼��� ȣ��
    private void Update()
    {
        switch (player)
        {
            case 1:
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
                break;
            case 2:
                // �÷��̾� 2 �߰� ���� Ȯ�� && ���� �÷��̾� ����⸦ �������� �ʾҴٸ�
                if (SelectManager.Instance.player[2] == true && isSelect == false)
                {
                    if (Input.GetKeyDown(KeyCode.UpArrow))
                    {
                        CursorDirection(Direction.Up);
                    }
                    else if (Input.GetKeyDown(KeyCode.LeftArrow))
                    {
                        CursorDirection(Direction.Left);
                    }
                    else if (Input.GetKeyDown(KeyCode.RightArrow))
                    {
                        CursorDirection(Direction.Right);
                    }
                    else if (Input.GetKeyDown(KeyCode.DownArrow))
                    {
                        CursorDirection(Direction.Down);
                    }
                }
                break;
            default:
                break;
        }
    }
}
