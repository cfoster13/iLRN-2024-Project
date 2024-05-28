using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class customCusor : MonoBehaviour
{
    public Texture2D cursor;
    public Texture2D cursorClicked;

    Vector2 hotspot2;

    

    private void mouseClicked()
    {
        if (Input.GetKey(KeyCode.Mouse0)) // when user left clicks switch the texture to pointer
        {
            Cursor.SetCursor(cursorClicked, hotspot2, CursorMode.Auto);
        }
        else
        {
            Cursor.SetCursor(cursor, hotspot2, CursorMode.Auto);
        }
    }


    void Update()
    {
        hotspot2 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseClicked();
    }

    
}
