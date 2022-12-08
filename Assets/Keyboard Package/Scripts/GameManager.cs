using UnityEngine;
using TMPro;

using System;
using System.Linq;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject[] LetterPrefab;
    public RectTransform KeyBoard;


    private System.Collections.Generic.Dictionary<string, GameObject> LettersDictionary = new System.Collections.Generic.Dictionary<string, GameObject>();

    public System.Collections.Generic.List<LetterSpawner> CurrentLetterSpawners = new System.Collections.Generic.List<LetterSpawner>();
    private System.Collections.Generic.List<GameObject> CurrentLetters = new System.Collections.Generic.List<GameObject>();
    private int currentLettersCount = 0;

    private bool canAddLetters = true;
    private void Start()
    {
        Instance = this;
        LettersDictionary = LetterPrefab.ToDictionary(item => item.name, item => item);
    }

    public void DeleteLetter()
    {
        if (!canAddLetters)
            return;

        if (CurrentLetters.Count > 0)
        {
            CurrentLetterSpawners[currentLettersCount].HideCursor();
            var letterIndex = CurrentLetters.Count - 1;
            Destroy(CurrentLetters[letterIndex].gameObject);
            CurrentLetters.RemoveAt(letterIndex);
            currentLettersCount--;
            CurrentLetterSpawners[currentLettersCount].ShowCursor();
        }
    }

    public void AddLetter(string letter)
    {
        if (!canAddLetters)
            return;

        var newLetter = Instantiate(LettersDictionary[letter], CurrentLetterSpawners[currentLettersCount].transform.position, Quaternion.identity);

        if (newLetter.TryGetComponent<Rigidbody2D>(out Rigidbody2D rb))
        {
            rb.mass = CurrentLetterSpawners[currentLettersCount].Mass;
            rb.Sleep();
        }

        if (newLetter.transform.childCount > 0)
        {
           
            if (newLetter.transform.GetChild(0).TryGetComponent<Rigidbody2D>(out Rigidbody2D subRb))
            {
                subRb.Sleep();
            }
        }


        CurrentLetters.Add(newLetter);

        CurrentLetterSpawners[currentLettersCount].HideCursor();

        currentLettersCount++;

        if (currentLettersCount == CurrentLetterSpawners.Count)
            SubmitWord();
        else
            CurrentLetterSpawners[currentLettersCount].ShowCursor();
    }

    public void SubmitWord()
    {
        foreach (var letters in CurrentLetters)
        {
            if (letters.TryGetComponent<Rigidbody2D>(out Rigidbody2D rb))
            {
                rb.WakeUp();
            }

            if (letters.transform.childCount > 0)
            {
                if (letters.transform.GetChild(0).TryGetComponent<Rigidbody2D>(out Rigidbody2D subRb))
                {
                    subRb.WakeUp();
                }
            }
        }
        canAddLetters = false;
        foreach (var item in CurrentLetterSpawners)
        {
            item.gameObject.SetActive(false);
        }

        LeanTween.move(KeyBoard, new Vector3(0, -KeyBoard.rect.height, 0), 0.2f);
        StartCoroutine(RestartLevelWithDelay(SpawnPointsSetup.Instance.CurrentLevel.TimeToEnd));
    }

    public void RestartLevel()
    {
        StopAllCoroutines();
        foreach (var item in CurrentLetters)
        {
            Destroy(item);
        }
        CurrentLetters.Clear();
        currentLettersCount = 0;
        canAddLetters = true;
        foreach (var item in CurrentLetterSpawners)
        {
            item.gameObject.SetActive(true);
        }
        LeanTween.move(KeyBoard, new Vector3(0, 0, 0), 0.2f);
    }

    public void RestartLevel(float delay)
    {
        StartCoroutine(RestartLevelWithDelay(delay));
    }

    private IEnumerator RestartLevelWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        GetComponent<SpawnPointsSetup>().ResetLevel();
    }

}
