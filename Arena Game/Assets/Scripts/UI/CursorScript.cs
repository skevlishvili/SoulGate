using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CursorScript : MonoBehaviour
{
    Vector2 mouse;
    public Texture2D[] CursorImages;

    void Start()
    {
        Cursor.visible = true;
        Cursor.SetCursor(CursorImages[0], mouse, CursorMode.Auto);
    }

    // Update is called once per frame
    void Update()
    {
        mouse = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
    }
    public void OnMouseOver(int Index)
    {
        Cursor.SetCursor(CursorImages[Index], mouse, CursorMode.Auto);
    }

    public void OnMouseExit()
    {
        Cursor.SetCursor(null, mouse, CursorMode.Auto);
    }
}
