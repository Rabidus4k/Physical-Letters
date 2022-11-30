using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterSpawner : MonoBehaviour
{
    public GameObject Cursor;
    public void ShowCursor()
    {
        Cursor.SetActive(true);
    }

    public void HideCursor()
    {
        Cursor.SetActive(false);
    }
}
