using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelProgressStorage : MonoBehaviour
{
    public static LevelProgressStorage Instance;

    public event Action<LevelData> OnLevelProgressChanged;

    [SerializeField] public List<LevelData> currentLevels { get; set; } = new List<LevelData>();

    private void Awake()
    {
        Instance = this;
    }
}
