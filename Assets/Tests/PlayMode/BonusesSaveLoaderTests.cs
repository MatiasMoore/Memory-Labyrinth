using System.Collections;
using MemoryLabyrinth.Achievements;
using MemoryLabyrinth.SaveLoad;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

public class BonusesSaveLoaderTests
{
    private SaveLoadManager _saveLoadManager = new SaveLoadManager();
    [SetUp]
    public void SetUp()
    {
        GameObject dummyStorage = new GameObject("Bonuses Storage");
        BonusStorage bonusStorage = dummyStorage.AddComponent<BonusStorage>();
        bonusStorage.Init();

        GameObject storage = new GameObject("Levels Progress Storage");
        LevelProgressStorage dummyLevelStorage = storage.AddComponent<LevelProgressStorage>();
        dummyLevelStorage.Init();

        GameObject secondDummyStorage = new GameObject("Settings Storage");
        SettingsStorage settingsStorage = secondDummyStorage.AddComponent<SettingsStorage>();
        settingsStorage.Init();

        GameObject achievementDummyStorage = new GameObject("Achievement Storage");
        AchievementsStorage achievementStorage = achievementDummyStorage.AddComponent<AchievementsStorage>();

        string achievementsPath = "Assets/Config/Achievements.asset";
        AchievementList achievements = AssetDatabase.LoadAssetAtPath(achievementsPath, typeof(AchievementList)) as AchievementList;
        AchievementList achList = Object.Instantiate(achievements);
        achievementStorage._defaultAchievements = achList;
        achievementStorage.Init();

        GameObject manager = new GameObject("SaveLoadManager");
        SaveLoadManager saveLoadManager = manager.AddComponent<SaveLoadManager>();
        _saveLoadManager = saveLoadManager;
        _saveLoadManager.Init();
    }
    // A Test behaves as an ordinary method
    [Test]
    public void BonusesSaveLoader_BonusStorageOnFirstLoad()
    {
        SaveLoadManager.Instance.DeleteSave();
        SaveLoadManager.Instance.LoadGame();

        int expectedRes = 0;
        Assert.AreEqual(expectedRes, BonusStorage.Instance.GetBonuses());
    }

    [Test]
    public void BonusesSaveLoader_BonusStorageSetupBonuses()
    {
        BonusStorage.Instance.SetupBonuses(10);

        SaveLoadManager.Instance.SaveGame();
        BonusStorage.Instance.SetupBonuses(0);
        SaveLoadManager.Instance.LoadGame();

        int expectedRes = 10;
        Assert.AreEqual(expectedRes, BonusStorage.Instance.GetBonuses());
    }

    [Test]
    public void BonusesSaveLoader_BonusStorageEarnBonuses()
    {
        BonusStorage.Instance.EarnBonuses(10);

        SaveLoadManager.Instance.SaveGame();
        BonusStorage.Instance.SetupBonuses(0);
        SaveLoadManager.Instance.LoadGame();

        int expectedRes = 10;
        Assert.AreEqual(expectedRes, BonusStorage.Instance.GetBonuses());
    }

    [Test]
    public void BonusesSaveLoader_BonusStorageSpendBonuses()
    {
        BonusStorage.Instance.SetupBonuses(20);
        BonusStorage.Instance.SpendBonuses(10);

        SaveLoadManager.Instance.SaveGame();
        BonusStorage.Instance.SetupBonuses(0);
        SaveLoadManager.Instance.LoadGame();

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

    [TearDown]
    public void AfterTest()
    {
        Debug.Log("Teardown");
        SaveLoadManager.Instance.DeleteSave();
    }
}
