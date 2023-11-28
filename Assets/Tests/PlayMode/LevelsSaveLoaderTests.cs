using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class LevelsSaveLoaderTests
{
    private SaveLoadManager _saveLoadManager = new SaveLoadManager();
    [SetUp]
    public void SetUp()
    {
        SceneManager.LoadScene("Bootstrap");
        _saveLoadManager = MonoBehaviour.FindObjectOfType<SaveLoadManager>();
    }

    [Test]
    public void LevelsSaveLoader_LevelProgressStorageOnFirstLoad()
    {
        GameObject dummyStorage = new GameObject("Bonuses Storage");
        BonusStorage bonusStorage = dummyStorage.AddComponent<BonusStorage>();
        bonusStorage.Init();

        GameObject storage = new GameObject("Levels Progress Storage");
        LevelProgressStorage dummyLevelStorage = storage.AddComponent<LevelProgressStorage>();
        dummyLevelStorage.Init();

        GameObject manager = new GameObject("SaveLoadManager");
        SaveLoadManager saveLoadManager = manager.AddComponent<SaveLoadManager>();

        saveLoadManager.DeleteSave();
        saveLoadManager.LoadGame();

        List<LevelData> realLevels = LevelProgressStorage.Instance.GetLevelDataList();

        int expCount = 0;

        Assert.AreEqual(expCount, realLevels.Count);
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator LevelsSaveLoaderTestsWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }
}
