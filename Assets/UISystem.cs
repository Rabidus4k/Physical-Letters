using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UISystem : MonoBehaviour
{
    public static UISystem Instance;

    public Image TransitionImage;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        LoadMenu();
    }

    public void LoadLevel(int index, Sprite sprite)
    {
        LevelDataHandler.Instance.ChosenLevel = index;
        TransitionImage.sprite = sprite;
        LeanTween.alpha(TransitionImage.rectTransform, 1, 0.7f).setOnComplete(()=> { SceneManager.LoadScene(1); });
    }

    public void LoadMenu()
    {
        TransitionImage.sprite = LevelDataHandler.Instance.Sprite;
        TransitionImage.color = Color.white;
        LeanTween.alpha(TransitionImage.rectTransform, 0, 0.7f);
    }
}
