using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointsSetup : MonoBehaviour
{
    public LetterSpawner LetterSpawnerPrefab;
    public int LettersCount = 4;
    public float LetterDistance = 1;

    public float LetterHeight = 8;
    public List<LetterSpawner> LetterSpawned = new List<LetterSpawner>();

    public void SetUpLevel(int lettersCount, float letterDistance, float letterHeight, Transform child)
    {
        LettersCount = lettersCount;
        LetterDistance = letterDistance;
        LetterHeight = letterHeight;

        foreach (var item in LetterSpawned)
        {
            Destroy(item.gameObject);
        }
        LetterSpawned.Clear();

        var totalLenght = (LettersCount - 1)* LetterDistance;

        float newX = -totalLenght / 2;
        for (int i = 0; i < LettersCount; i++)
        {
            LetterSpawner newLetterSpawner = Instantiate(LetterSpawnerPrefab, new Vector3(newX, LetterHeight, 0), Quaternion.identity);
            newLetterSpawner.transform.parent = child;
            newX += LetterDistance;
            LetterSpawned.Add(newLetterSpawner);
        }
    }
}
