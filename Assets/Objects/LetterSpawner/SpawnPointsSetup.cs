using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SpawnPointsSetup : MonoBehaviour
{
    public static SpawnPointsSetup Instance;
    public Image Background;
    [HideInInspector]
    public int LettersCount = 4;
    [HideInInspector]
    public float LetterDistance = 1;
    [HideInInspector]
    public float LetterHeight = 8;
    public LetterSpawner LetterSpawnerPrefab;
    public GameObject CollectPrefab;
    public Image TransitionImage;

    public List<LevelInfo> Levels = new List<LevelInfo>();
    [HideInInspector]
    public List<LetterSpawner> LetterSpawned = new List<LetterSpawner>();

    [HideInInspector]
    public int SelectedLevel;

    [Space]
    [Header("DEBUG")]
    public int Level;

    public RestoreObject[] RestoreGameObjects = new RestoreObject[] { };

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        if (LevelDataHandler.Instance != null)
            SelectedLevel = LevelDataHandler.Instance.ChosenLevel;
        else
            SelectedLevel = Level;
        StartCoroutine(SelectLevel(SelectedLevel, true));
    }

    private IEnumerator SelectLevel(int selectedLevel, bool isFirstTime)
    {
        if (isFirstTime)
        {
            LeanTween.alpha(TransitionImage.rectTransform, 0, 0.7f);
        }
        else
        {
            LeanTween.alpha(TransitionImage.rectTransform, 1, 0.7f).setLoopPingPong(1);
            yield return new WaitForSeconds(0.7f);
        }
        

        TransitionImage.sprite = Levels[selectedLevel].Background;
        Background.sprite = Levels[selectedLevel].Background;
        for (int i = 0; i < Levels.Count; i++)
        {
            Levels[i].gameObject.SetActive(i == selectedLevel);
        }
        Levels[selectedLevel].SetUpLevel();
    }

    public void ResetLevel()
    {
        Levels[SelectedLevel].SetUpLevel();
    }

    private int collectCount;
    public void PickUpCollect(GameObject other)
    {
        other.SetActive(false);
        collectCount--;
        if (collectCount == 0)
        {
            PlayerPrefs.SetInt($"LEVEL {SelectedLevel}", 1);
            SelectedLevel++;
            if (SelectedLevel == Levels.Count)
                OpenMenu();
            else
                StartCoroutine(SelectLevel(SelectedLevel, false));
        }
    }

    public void SetUpLevel(LevelInfo levelInfo)
    {
        StartCoroutine(SetUpLevelCoroutine(levelInfo));
    }

    private IEnumerator SetUpLevelCoroutine(LevelInfo levelInfo)
    {
        GameManager.Instance.RestartLevel();

        RestoreGameObjects = levelInfo.restoreObjects;
        collectCount = levelInfo.CollectorsCount;
        foreach (var item in RestoreGameObjects)
        {
            item.gameObject.SetActive(true);
        }

        LettersCount = levelInfo.LetterInfos.Count;
        LetterDistance = levelInfo.LetterDistance;
        LetterHeight = levelInfo.LetterHeight;

        foreach (var item in LetterSpawned)
        {
            Destroy(item.gameObject);
        }
        LetterSpawned.Clear();

        var totalLenght = (LettersCount - 1) * LetterDistance;

        float newX = -totalLenght / 2;
        for (int i = 0; i < LettersCount; i++)
        {
            LetterSpawner newLetterSpawner = Instantiate(LetterSpawnerPrefab, new Vector3(newX, LetterHeight, 0), Quaternion.identity);
            newLetterSpawner.Mass = levelInfo.LetterInfos[i].LetterMass;
            newLetterSpawner.transform.parent = levelInfo.transform;
            newX += LetterDistance;
            LetterSpawned.Add(newLetterSpawner);
            newLetterSpawner.UpdateTipText(levelInfo.CorrectWord[i]);
        }
        LetterSpawned[0].ShowCursor();
        if (levelInfo.ShowTipsOnStart)
            ShowTips();
        GameManager.Instance.CurrentLetterSpawners = LetterSpawned;
        yield return null;
    }


    public void OpenMenu()
    {
        LevelDataHandler.Instance.Sprite = TransitionImage.sprite;
        LeanTween.alpha(TransitionImage.rectTransform, 1, 0.7f).setOnComplete(() => { SceneManager.LoadScene(0); });
    }

    public void ShowTips()
    {
        foreach (var letter in LetterSpawned)
        {
            letter.ShowTip();
        }
    }
}
