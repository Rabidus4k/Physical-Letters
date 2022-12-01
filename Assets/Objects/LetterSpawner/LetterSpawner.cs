using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LetterSpawner : MonoBehaviour
{
    public GameObject Cursor;
    public TextMeshPro Tip;

    public float Mass;

    public void UpdateTipText(char letter)
    {
        Tip.SetText(letter.ToString());
    }

    public void ShowCursor()
    {
        Cursor.SetActive(true);
    }

    public void HideCursor()
    {
        Cursor.SetActive(false);
    }

    public void ShowTip()
    {
        Tip.gameObject.SetActive(true);
    }

    public void HideTip()
    {
        Tip.gameObject.SetActive(false);
    }
}
