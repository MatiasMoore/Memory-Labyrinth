using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public static class Repository
{
    private const string GAME_STATE_KEY = "GameState";

    private static Dictionary<string, string> currentState = new();


    public static void LoadState()
    {
        if (PlayerPrefs.HasKey(GAME_STATE_KEY))
        {
            var serializedState = PlayerPrefs.GetString(GAME_STATE_KEY);
            currentState = JsonConvert.
              DeserializeObject<Dictionary<string, string>>(serializedState);
        }
        else
        {
            currentState = new Dictionary<string, string>();
        }
        Debug.Log($"LoadState()");
    }

    public static void SaveState()
    {
        var serializedState = JsonConvert.SerializeObject(currentState);
        PlayerPrefs.SetString(GAME_STATE_KEY, serializedState);
        Debug.Log($"SaveState()");
    }


    public static void SetData<T>(T value)
    {
        var serializedData = JsonConvert.SerializeObject(value);
        currentState[typeof(T).Name] = serializedData;
        Debug.Log($"SetData({value})");
    }

    public static T GetData<T>()
    {
        var serializedData = currentState[typeof(T).Name];
        Debug.Log($"GetData()");
        return JsonConvert.DeserializeObject<T>(serializedData);

    }
}
