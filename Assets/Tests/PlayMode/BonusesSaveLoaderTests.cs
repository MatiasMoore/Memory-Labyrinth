using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class BonusesSaveLoaderTests
{
    private SaveLoadManager _saveLoadManager = new SaveLoadManager();
    [SetUp]
    public void SetUp()
    {
         SceneManager.LoadScene("Bootstrap");
         _saveLoadManager = MonoBehaviour.FindObjectOfType<SaveLoadManager>();
    }
    // A Test behaves as an ordinary method
    [Test]
    public void BonusesSaveLoader_BonusStorageOnFirstLoad()
    {
        GameObject storage = new GameObject("Bonuses Storage");
        BonusStorage bonusStorage = storage.AddComponent<BonusStorage>();
        bonusStorage.Init();

        GameObject dummyStorage = new GameObject("Levels Progress Storage");
        LevelProgressStorage dummyLevelStorage = dummyStorage.AddComponent<LevelProgressStorage>();
        dummyLevelStorage.Init();

        GameObject manager = new GameObject("SaveLoadManager");
        SaveLoadManager saveLoadManager = manager.AddComponent<SaveLoadManager>();

        saveLoadManager.DeleteSave();
        saveLoadManager.LoadGame();

        int expectedRes = 0;
        Assert.AreEqual(expectedRes, BonusStorage.Instance.GetBonuses());
    }

    [Test]
    public void BonusesSaveLoader_BonusStorageSetupBonuses()
    {
        GameObject storage = new GameObject("Bonuses Storage");
        BonusStorage bonusStorage = storage.AddComponent<BonusStorage>();
        bonusStorage.Init();

        GameObject dummyStorage = new GameObject("Levels Progress Storage");
        LevelProgressStorage dummyLevelStorage = dummyStorage.AddComponent<LevelProgressStorage>();
        dummyLevelStorage.Init();

        GameObject manager = new GameObject("SaveLoadManager");
        SaveLoadManager saveLoadManager = manager.AddComponent<SaveLoadManager>();

        BonusStorage.Instance.SetupBonuses(10);
        
        saveLoadManager.SaveGame();
        BonusStorage.Instance.SetupBonuses(0);
        saveLoadManager.LoadGame();

        int expectedRes = 10;
        Assert.AreEqual(expectedRes, BonusStorage.Instance.GetBonuses());
    }

    [Test]
    public void BonusesSaveLoader_BonusStorageEarnBonuses()
    {
        GameObject storage = new GameObject("Bonuses Storage");
        BonusStorage bonusStorage = storage.AddComponent<BonusStorage>();
        bonusStorage.Init();

        GameObject dummyStorage = new GameObject("Levels Progress Storage");
        LevelProgressStorage dummyLevelStorage = dummyStorage.AddComponent<LevelProgressStorage>();
        dummyLevelStorage.Init();

        GameObject manager = new GameObject("SaveLoadManager");
        SaveLoadManager saveLoadManager = manager.AddComponent<SaveLoadManager>();

        BonusStorage.Instance.EarnBonuses(10);

        saveLoadManager.SaveGame();
        BonusStorage.Instance.SetupBonuses(0);
        saveLoadManager.LoadGame();

        int expectedRes = 10;
        Assert.AreEqual(expectedRes, BonusStorage.Instance.GetBonuses());
    }

    [Test]
    public void BonusesSaveLoader_BonusStorageSpendBonuses()
    {
        GameObject storage = new GameObject("Bonuses Storage");
        BonusStorage bonusStorage = storage.AddComponent<BonusStorage>();
        bonusStorage.Init();

        GameObject dummyStorage = new GameObject("Levels Progress Storage");
        LevelProgressStorage dummyLevelStorage = dummyStorage.AddComponent<LevelProgressStorage>();
        dummyLevelStorage.Init();

        GameObject manager = new GameObject("SaveLoadManager");
        SaveLoadManager saveLoadManager = manager.AddComponent<SaveLoadManager>();

        BonusStorage.Instance.SetupBonuses(20);
        BonusStorage.Instance.SpendBonuses(10);

        saveLoadManager.SaveGame();
        BonusStorage.Instance.SetupBonuses(0);
        saveLoadManager.LoadGame();

        int expectedRes = 10;
        Assert.AreEqual(expectedRes, BonusStorage.Instance.GetBonuses());
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator BonusesSaveLoaderTestsWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }
}
