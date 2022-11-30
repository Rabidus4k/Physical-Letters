using Shapes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelInfo : MonoBehaviour
{
    public Sprite Background;

    public int LettersCount = 4;
    public float LetterDistance = 2.5f;
    public float LetterHeight = 8;

    private SpawnPointsSetup SpawnPointsSetup;
    public List<Transform> SpawnPoints = new List<Transform>();

    public Color MeshColor;

    public List<ShapeRenderer> shapes = new List<ShapeRenderer>();

    private void Awake()
    {
        SpawnPointsSetup = FindObjectOfType<SpawnPointsSetup>();
    }

    [ContextMenu("Set up level")]
    public void SetUpLevel()
    {
        foreach (var shape in shapes)
        {
            shape.Color = MeshColor;
        }
        SpawnPointsSetup.SetUpLevel(SpawnPoints, LettersCount, LetterDistance, LetterHeight, transform);
    }
}
