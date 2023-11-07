using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ResourceManagerForLevels
{
    public enum Level
    {
        Level1
    }

    [SerializeField]
    private static string _pathToLevels = "assets/Prefabs/Levels/";

    [SerializeField]
    private static Dictionary<Level, GameObject> _levels = new Dictionary<Level,GameObject>()
    {
        {
            Level.Level1,
            AssetDatabase
                .LoadAssetAtPath<GameObject>(_pathToLevels + "Level1.prefab")
                .GetComponent<GameObject>()
        }
    };

    public static GameObject LoadLevel(Level level)
    {
        if (_levels.ContainsKey(level))
        {
            // get gameobject from prefab and return it
            GameObject levelPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(_pathToLevels + "Level1.prefab");
            return levelPrefab;
        }
        else
        {
            Debug.LogError($"Level {level} not found");
            return null;
        }
    }
}
