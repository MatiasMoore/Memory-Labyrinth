using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _levelPrefab;

    [SerializeField]
    private ResourceManagerForLevels.Level _currentLevel;

    public void Start()
    {
        StartLevel();
    }

    public void StartLevel()
    {
        //place prefab on scene
        _levelPrefab = ResourceManagerForLevels.LoadLevel(_currentLevel);
        Instantiate(_levelPrefab, new Vector3(0, 0, 0), Quaternion.identity);
    }
}
