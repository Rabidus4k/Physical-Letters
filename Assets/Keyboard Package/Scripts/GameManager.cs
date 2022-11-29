using UnityEngine;
using TMPro;

using System;
using System.Linq;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject[] LetterPrefab;


    private System.Collections.Generic.Dictionary<string, GameObject> LettersDictionary = new System.Collections.Generic.Dictionary<string, GameObject>();

    private System.Collections.Generic.List<LetterSpawner> CurrentLetterSpawners = new System.Collections.Generic.List<LetterSpawner>();
    private System.Collections.Generic.List<GameObject> CurrentLetters = new System.Collections.Generic.List<GameObject>();
    private int currentLettersCount = 0;

    private bool canAddLetters = true;
    private void Start()
    {
        Application.targetFrameRate = 60;
        Instance = this;
        LettersDictionary = LetterPrefab.ToDictionary(item => item.name, item => item);
        CurrentLetterSpawners = GetComponent<SpawnPointsSetup>().LetterSpawned;
    }

    public void DeleteLetter()
    {
        if (!canAddLetters)
            return;

        if (CurrentLetters.Count > 0)
        {
            var letterIndex = CurrentLetters.Count - 1;
            Destroy(CurrentLetters[letterIndex].gameObject);
            CurrentLetters.RemoveAt(letterIndex);
            currentLettersCount--;
        }
    }

    public void AddLetter(string letter)
    {
        if (!canAddLetters)
            return;

        var newLetter = Instantiate(LettersDictionary[letter], CurrentLetterSpawners[currentLettersCount].transform.position, Quaternion.identity);

        if (newLetter.TryGetComponent<Rigidbody2D>(out Rigidbody2D rb))
        {
            rb.isKinematic = true;
        }

        if (newLetter.transform.childCount > 0)
        {
           
            if (newLetter.transform.GetChild(0).TryGetComponent<Rigidbody2D>(out Rigidbody2D subRb))
            {
                subRb.isKinematic = true;
            }
        }


        CurrentLetters.Add(newLetter);
        currentLettersCount++;
        if (currentLettersCount == CurrentLetterSpawners.Count)
            SubmitWord();
    }

    public void SubmitWord()
    {
        foreach (var letters in CurrentLetters)
        {
            if (letters.TryGetComponent<Rigidbody2D>(out Rigidbody2D rb))
            {
                rb.isKinematic = false;
            }

            if (letters.transform.childCount > 0)
            {
                if (letters.transform.GetChild(0).TryGetComponent<Rigidbody2D>(out Rigidbody2D subRb))
                {
                    subRb.isKinematic = false;
                }
            }
        }
        canAddLetters = false;
        foreach (var item in CurrentLetterSpawners)
        {
            item.gameObject.SetActive(false);
        }

        StartCoroutine(RestartLevelWithDelay(5));
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
    }

    public void RestartLevel(float delay)
    {
        StartCoroutine(RestartLevelWithDelay(delay));
    }

    private IEnumerator RestartLevelWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        RestartLevel();
    }
}
