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

    private List<Transform> _spawnPoints = new List<Transform>();

    private List<GameObject> _spawnedCollectors = new List<GameObject>();

    [HideInInspector]
    public int SelectedLevel;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        SelectedLevel = LevelDataHandler.Instance.ChosenLevel;
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

    public void PickUpCollect(GameObject other)
    {
        _spawnedCollectors.Remove(other);
        Destroy(other);

        if (_spawnedCollectors.Count == 0)
        {
            PlayerPrefs.SetInt($"LEVEL {SelectedLevel}", 1);
            SelectedLevel++;
            if (SelectedLevel == Levels.Count)
                OpenMenu();
            else
                StartCoroutine(SelectLevel(SelectedLevel, false));
        }
    }

    public void SetUpLevel(List<Transform> spawnPoints, int lettersCount, float letterDistance, float letterHeight, Transform child)
    {
        StartCoroutine(SetUpLevelCoroutine(spawnPoints, lettersCount, letterDistance, letterHeight, child));
    }

    private IEnumerator SetUpLevelCoroutine(List<Transform> spawnPoints, int lettersCount, float letterDistance, float letterHeight, Transform child)
    {
        GameManager.Instance.RestartLevel();

        _spawnPoints = spawnPoints;

        foreach (var collector in _spawnedCollectors)
        {
            Destroy(collector);
        }
        _spawnedCollectors.Clear();

        foreach (var point in _spawnPoints)
        {
            var collect = Instantiate(CollectPrefab, point.position, Quaternion.identity);
            _spawnedCollectors.Add(collect);
        }

        LettersCount = lettersCount;
        LetterDistance = letterDistance;
        LetterHeight = letterHeight;

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
            newLetterSpawner.transform.parent = child;
            newX += LetterDistance;
            LetterSpawned.Add(newLetterSpawner);
        }
        LetterSpawned[0].ShowCursor();
        GameManager.Instance.CurrentLetterSpawners = LetterSpawned;
        yield return null;
    }


    public void OpenMenu()
    {
        LevelDataHandler.Instance.Sprite = TransitionImage.sprite;
        LeanTween.alpha(TransitionImage.rectTransform, 1, 0.7f).setOnComplete(() => { SceneManager.LoadScene(0); });
    }
}
