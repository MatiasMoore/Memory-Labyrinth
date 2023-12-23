using System.Collections;
using System.Collections.Generic;
using MemoryLabyrinth.Achievements;
using MemoryLabyrinth.Resources;
using MemoryLabyrinth.SaveLoad;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

public class LevelsSaveLoaderTests
{
    private SaveLoadManager _saveLoadManager;

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

    [Test]
    public void LevelsSaveLoader_LevelProgressStorageOnFirstLoad()
    {
        SaveLoadManager.Instance.DeleteSave();
        SaveLoadManager.Instance.LoadGame();

        List<LevelProgress> realLevels = LevelProgressStorage.Instance.GetLevelDataList();

        int expCount = 0;

        Assert.AreEqual(expCount, realLevels.Count);
    }

    [Test]
    public void LevelsSaveLoader_LevelProgressStorageAddFirstLevelInfo()
    {
        LevelProgress testData = new LevelProgress();

        testData._level = ResourceManager.Level.Level1;
        testData._checkpointId = 1;
        testData._collectedBonuses = new List<BonusInfo>();
        testData._collectedBonuses.Add(new BonusInfo { _id = 1, _value = 10 });
        testData._collectedBonuses.Add(new BonusInfo { _id = 2, _value = 20 });
        testData._isCompleted = false;
        testData._livesAmount = 2;
        testData._time = 10f;

        List<LevelProgress> dummyList = LevelProgressStorage.Instance.GetLevelDataList();
        LevelProgressStorage.Instance.AddLevelInfo(testData);
        SaveLoadManager.Instance.SaveGame();
        LevelProgressStorage.Instance.SetLevelDataList(dummyList);
        SaveLoadManager.Instance.LoadGame();

        List<LevelProgress> realLevels = LevelProgressStorage.Instance.GetLevelDataList();
        List<LevelProgress> expList = new List<LevelProgress>();
        expList.Add(testData);

        AssertListOfLevelData(expList, realLevels);
    }

    [Test]
    public void LevelsSaveLoader_LevelProgressStorageUpdateFirstLevelInfo()
    {
        LevelProgress testData = new LevelProgress();

        testData._level = ResourceManager.Level.Level1;
        testData._checkpointId = 1;
        testData._collectedBonuses = new List<BonusInfo>();
        testData._collectedBonuses.Add(new BonusInfo { _id = 1, _value = 10 });
        testData._collectedBonuses.Add(new BonusInfo { _id = 2, _value = 20 });
        testData._isCompleted = false;
        testData._livesAmount = 2;
        testData._time = 10f;

        LevelProgress newTestData = testData;
        newTestData._isCompleted = true;
        newTestData._time = 2f;

        List<LevelProgress> dummyList = LevelProgressStorage.Instance.GetLevelDataList();
        LevelProgressStorage.Instance.AddLevelInfo(testData);
        SaveLoadManager.Instance.SaveGame();
        LevelProgressStorage.Instance.SetLevelDataList(dummyList);
        SaveLoadManager.Instance.LoadGame();
        LevelProgressStorage.Instance.AddLevelInfo(newTestData);
        SaveLoadManager.Instance.SaveGame();
        LevelProgressStorage.Instance.SetLevelDataList(dummyList);
        SaveLoadManager.Instance.LoadGame();

        List<LevelProgress> realLevels = LevelProgressStorage.Instance.GetLevelDataList();
        List<LevelProgress> expList = new List<LevelProgress>();
        expList.Add(newTestData);

        AssertListOfLevelData(expList, realLevels);
    }

    [Test]
    public void LevelsSaveLoader_LevelProgressStorageAddSecondLevelInfo()
    {
        LevelProgress testData = new LevelProgress();

        testData._level = ResourceManager.Level.Level1;
        testData._checkpointId = 1;
        testData._collectedBonuses = new List<BonusInfo>();
        testData._collectedBonuses.Add(new BonusInfo { _id = 1, _value = 10 });
        testData._collectedBonuses.Add(new BonusInfo { _id = 2, _value = 20 });
        testData._isCompleted = false;
        testData._livesAmount = 2;
        testData._time = 10f;

        LevelProgress secondTestData = testData;
        secondTestData._level = ResourceManager.Level.Level2;

        List<LevelProgress> dummyList = LevelProgressStorage.Instance.GetLevelDataList();
        LevelProgressStorage.Instance.AddLevelInfo(testData);
        SaveLoadManager.Instance.SaveGame();
        LevelProgressStorage.Instance.SetLevelDataList(dummyList);
        SaveLoadManager.Instance.LoadGame();
        LevelProgressStorage.Instance.AddLevelInfo(secondTestData);
        SaveLoadManager.Instance.SaveGame();
        LevelProgressStorage.Instance.SetLevelDataList(dummyList);
        SaveLoadManager.Instance.LoadGame();

        List<LevelProgress> realLevels = LevelProgressStorage.Instance.GetLevelDataList();
        List<LevelProgress> expList = new List<LevelProgress>();
        expList.Add(testData);
        expList.Add(secondTestData);

        AssertListOfLevelData(expList, realLevels);
    }

    [Test]
    public void LevelsSaveLoader_LevelProgressStorageUpdateSecondLevelInfo()
    {
        LevelProgress testData = new LevelProgress();

        testData._level = ResourceManager.Level.Level1;
        testData._checkpointId = 1;
        testData._collectedBonuses = new List<BonusInfo>();
        testData._collectedBonuses.Add(new BonusInfo { _id = 1, _value = 10 });
        testData._collectedBonuses.Add(new BonusInfo { _id = 2, _value = 20 });
        testData._isCompleted = false;
        testData._livesAmount = 2;
        testData._time = 10f;

        LevelProgress secondTestData = testData;
        secondTestData._level = ResourceManager.Level.Level2;

        List<LevelProgress> dummyList = LevelProgressStorage.Instance.GetLevelDataList();
        LevelProgressStorage.Instance.AddLevelInfo(testData);
        SaveLoadManager.Instance.SaveGame();
        LevelProgressStorage.Instance.SetLevelDataList(dummyList);
        SaveLoadManager.Instance.LoadGame();
        LevelProgressStorage.Instance.AddLevelInfo(secondTestData);
        SaveLoadManager.Instance.SaveGame();
        LevelProgressStorage.Instance.SetLevelDataList(dummyList);
        SaveLoadManager.Instance.LoadGame();


        LevelProgress updatedSecondTestData = secondTestData;
        updatedSecondTestData._isCompleted = true;
        updatedSecondTestData._livesAmount = 1;
        updatedSecondTestData._time = 4f;

        LevelProgressStorage.Instance.AddLevelInfo(updatedSecondTestData);
        SaveLoadManager.Instance.SaveGame();
        LevelProgressStorage.Instance.SetLevelDataList(dummyList);
        SaveLoadManager.Instance.LoadGame();

        List<LevelProgress> realLevels = LevelProgressStorage.Instance.GetLevelDataList();
        List<LevelProgress> expList = new List<LevelProgress>();
        expList.Add(testData);
        expList.Add(updatedSecondTestData);

        AssertListOfLevelData(expList, realLevels);
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

    [TearDown]
    public void AfterTest()
    {
        Debug.Log("Teardown");
        SaveLoadManager.Instance.DeleteSave();
    }

    private void AssertAllFields(LevelProgress exp, LevelProgress real)
    {
        Assert.AreEqual(exp._level, real._level, "Level mismatch");
        Assert.AreEqual(exp._checkpointId, real._checkpointId, "Checkpoint ID mismatch");
        Assert.AreEqual(exp._time, real._time, 0.001f, "Time mismatch"); 
        CollectionAssert.AreEqual(exp._collectedBonuses, real._collectedBonuses, "Collected Bonuses ID mismatch");
        Assert.AreEqual(exp._livesAmount, real._livesAmount, "Lives Amount mismatch");
        Assert.AreEqual(exp._isCompleted, real._isCompleted, "Is Completed flag mismatch");
    }
    private void AssertListOfLevelData(List<LevelProgress> expectedList, List<LevelProgress> actualList)
    {
        Assert.AreEqual(expectedList.Count, actualList.Count, "List count mismatch");

        for (int i = 0; i < expectedList.Count; i++)
        {
            AssertAllFields(expectedList[i], actualList[i]);
        }
    }
}
