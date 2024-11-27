using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Cursor_Test : SingletonManager<Cursor_Test>
{
    public Texture2D customCursor;
    private Vector2 cursorPos = Vector2.zero;

    private void Start()
    {
        if (customCursor != null)
        {
            Cursor.SetCursor(customCursor, cursorPos, CursorMode.Auto);
        }
        else
        {
            Debug.LogError("커스텀 커서가 존재하지 않음!");
        }
    }

    public void CursorChange()
    {
        Cursor.visible = !Cursor.visible;
    }
}
