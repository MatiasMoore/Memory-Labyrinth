using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISaveLoader
{
    void LoadData();
    void SaveData();
}

public sealed class SaveLoadManager : MonoBehaviour
{
    // public static SaveLoadManager Instance;

    private readonly ISaveLoader[] saveLoaders = {
        new BonusesSaveLoader()
    };


    [ContextMenu("Load")]
    public void LoadGame()
    {
        Repository.LoadState();
        foreach (var saveLoader in this.saveLoaders)
        {
            saveLoader.LoadData();
        }
    }

    [ContextMenu("Save")]
    public void SaveGame()
    {
        foreach (var saveLoader in this.saveLoaders)
        {
            saveLoader.SaveData();
        }
        Repository.SaveState();
    }

    private void Awake()
    {
        //     if (Instance != null) return;
        //      Instance = this;
    }
}