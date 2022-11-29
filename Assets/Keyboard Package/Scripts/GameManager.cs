using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System;
using System.Linq;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject[] LetterPrefab;


    private Dictionary<string, GameObject> LettersDictionary = new Dictionary<string, GameObject>();

    private List<LetterSpawner> CurrentLetterSpawners = new List<LetterSpawner>();
    private List<GameObject> CurrentLetters = new List<GameObject>();
    private int currentLettersCount = 0;

    private bool canAddLetters = true;
    private void Start()
    {
        Instance = this;
        LettersDictionary = LetterPrefab.ToDictionary(item => item.name, item => item);
        CurrentLetterSpawners = GetComponent<SpawnPointsSetup>().LetterSpawned;
    }

    public void DeleteLetter()
    {

    }

    public void AddLetter(string letter)
    {
        if (!canAddLetters)
            return;

        var newLetter = Instantiate(LettersDictionary[letter], CurrentLetterSpawners[currentLettersCount].transform.position, Quaternion.identity);
        newLetter.GetComponent<Rigidbody2D>().isKinematic = true;
        CurrentLetters.Add(newLetter);
        currentLettersCount++;
        if (currentLettersCount == CurrentLetterSpawners.Count)
            SubmitWord();
    }

    public void SubmitWord()
    {
        foreach (var letters in CurrentLetters)
        {
            letters.GetComponent<Rigidbody2D>().isKinematic = false;
        }
        canAddLetters = false;
        foreach (var item in CurrentLetterSpawners)
        {
            item.gameObject.SetActive(false);
        }
    }

    public void RestartLevel()
    {
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
}
