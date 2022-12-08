using Shapes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class LetterInfo 
{
    public float LetterMass = 1;
}

public class LevelInfo : MonoBehaviour
{
    public List<LetterInfo> LetterInfos = new List<LetterInfo>();
    public Sprite Background;
    public float LetterDistance = 2.5f;
    public float LetterHeight = 8;
    public RestoreObject[] restoreObjects = new RestoreObject[]{ };
    public int CollectorsCount;

    public float TimeToEnd = 5;
    private SpawnPointsSetup SpawnPointsSetup;

    public string CorrectWord;
    public bool ShowTipsOnStart;

    private void Awake()
    {
        SpawnPointsSetup = FindObjectOfType<SpawnPointsSetup>();
    }


    [ContextMenu("Set up level")]
    public void SetUpLevel()
    {
        SpawnPointsSetup.SetUpLevel(this);
    }
}
