using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelInfoUI : MonoBehaviour
{
    public int Index;
    public Image Image;
    public TextMeshProUGUI LevelText;

    private void Awake()
    {
        if (PlayerPrefs.GetInt($"LEVEL {Index}", 0) == 1)
            LevelText.fontStyle |= FontStyles.Strikethrough;        
    }

    public void SelectThisLevel()
    {
        UISystem.Instance.LoadLevel(Index, Image.sprite);
    }
}
