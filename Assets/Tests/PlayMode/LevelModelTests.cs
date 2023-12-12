using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;
using UnityEditor;
using MemoryLabyrinth.SaveLoad;
using MemoryLabyrinth.Level.Logic;
using MemoryLabyrinth.Player;
using MemoryLabyrinth.Level.Objects.CheckpointLib;
using MemoryLabyrinth.Level.Objects.BonusLib;

[TestFixture]
public class LevelModelTests
{
    private LevelModel _levelModel;
    private GameObject _levelModelGameObject;
    private MainCharacter _mainCharacter;
    private GameObject _mainCharacterGameObject;
    private Checkpoint _checkpoint;



    [SetUp]
    public void Setup()
    {   //timescale 
        Time.timeScale = 100;

        string playerPrefabPath = "Assets/Prefabs/Player.prefab";
        GameObject playerPrefab = AssetDatabase.LoadAssetAtPath(playerPrefabPath, typeof(Object)) as GameObject;
        _mainCharacterGameObject = Object.Instantiate(playerPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        _mainCharacter = _mainCharacterGameObject.GetComponent<MainCharacter>();
        _mainCharacter.Init();

        //create gameObject with levelmodel on him
        GameObject levelModelGameObject = new GameObject();
        levelModelGameObject.AddComponent<LevelModel>();
        levelModelGameObject = Object.Instantiate(levelModelGameObject, new Vector3(0, 0, 0), Quaternion.identity);
        _levelModel = levelModelGameObject.GetComponent<LevelModel>();
        _levelModel.Init(_mainCharacter);

        Debug.Log("Setup");
    }

    [TearDown]
    public void Teardown()
    {
        //timescale 
        Time.timeScale = 1;
        Object.DestroyImmediate(_mainCharacterGameObject);
        Object.DestroyImmediate(_levelModelGameObject);
        if (_checkpoint != null)
            Object.DestroyImmediate(_checkpoint.gameObject);
    }

    [Test]
    public void onPlayerDeath_ShouldInvokeLevelLoseEvent()
    {
        bool eventInvoked = false;
        _levelModel._onLevelLose += (levelData) => eventInvoked = true;

        _mainCharacter.SetMaxHealth(100);
        _mainCharacter.SetHealth(100);
        _mainCharacter.getDamage(100);

        Assert.IsTrue(eventInvoked);
    }

    [Test]
    public void onPlayerWin_ShouldInvokeLevelWinEvent()
    {
        bool eventInvoked = false;
        _levelModel._onLevelWin += (levelData) => eventInvoked = true;

        _mainCharacter.Finish();

        Assert.IsTrue(eventInvoked);
    }


    [Test]
    public void onPlayerGetBonus_ShouldIncreaseBonusAmountAndInvokePlayerGetBonusEvent()
    {
        int initialBonusAmount = _levelModel.GetBonusAmount();
        bool eventInvoked = false;
        _levelModel._onPlayerGetBonus += () => eventInvoked = true;

        Bonus bonus = new Bonus();
        bonus.SetValue(10);
        _mainCharacter.getBonus(bonus);

        Assert.AreEqual(initialBonusAmount + bonus.GetValue(), _levelModel.GetBonusAmount());
        Assert.IsTrue(eventInvoked);
    }

    [Test]
    public void onPlayerGetCheckpoint_ShouldSetCurrentCheckpoint()
    {
        Checkpoint _checkpoint = PlayerGetCheckpoint(1, new Vector3(0, 0, 0));

        Assert.AreEqual(_checkpoint, _levelModel.GetCurrentCheckPoint());
    }

    [Test]
    public void onPlayerGetCheckpoint_ShouldSetCurrentCheckpointIfHigherQueue()
    {
        Checkpoint _checkpoint = PlayerGetCheckpoint(1, new Vector3(0, 0, 0));
        Checkpoint newCheckpoint = PlayerGetCheckpoint(2, new Vector3(0, 0, 0));

        Assert.AreEqual(newCheckpoint, _levelModel.GetCurrentCheckPoint());
    }

    [Test]
    public void onPlayerGetCheckpoint_ShouldNotSetCurrentCheckpointIfLowerQueue()
    {
        Checkpoint _checkpoint = PlayerGetCheckpoint(2, new Vector3(0, 0, 0));
        Checkpoint newCheckpoint = PlayerGetCheckpoint(1, new Vector3(0, 0, 0));

        Assert.AreEqual(_checkpoint, _levelModel.GetCurrentCheckPoint());
    }

    [Test]
    public void onPlayerGetCheckpoint_ShouldAddCollectedBonusesToCollection()
    {
        List<BonusInfo> expectedBonuses = new List<BonusInfo>();
        PlayerGetBonus(1,1);
        PlayerGetBonus(2,2);
        PlayerGetBonus(3,3);
        expectedBonuses.Add(new BonusInfo { _id = 1, _value = 1});
        expectedBonuses.Add(new BonusInfo { _id = 2, _value = 2 });
        expectedBonuses.Add(new BonusInfo { _id = 3, _value = 3 });

        Checkpoint _checkpoint = PlayerGetCheckpoint(1, new Vector3(0, 0, 0)); 

        List<BonusInfo> actualBonuses = _levelModel.GetCollectedBonusesIDBeforeCheckpoint();

        foreach (BonusInfo bonusInfo in expectedBonuses)
        {
            Assert.IsTrue(actualBonuses.Contains(bonusInfo));
        }
    }

    [UnityTest]
    public IEnumerator onPlayerDamage_ShouldSetMainCharacterPositionToCheckpoint()
    {
        Vector3 position = new Vector3(1, 1, 0);
        Checkpoint _checkpoint = PlayerGetCheckpoint(1, position);
        _mainCharacter.transform.position = new Vector3(2, 2, 0);
        DamagePlayer();

        float time = 0;
        while (time < 5)
        {
            time += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }

        Assert.AreEqual(_checkpoint, _levelModel.GetCurrentCheckPoint());
        Assert.AreEqual(position, _mainCharacter.transform.position);
    }

    private void KillPlayer()
    {
        _mainCharacter.SetMaxHealth(100);
        _mainCharacter.SetHealth(100);
        _mainCharacter.getDamage(100);
    }

    private void DamagePlayer()
    {
        _mainCharacter.SetMaxHealth(100);
        _mainCharacter.SetHealth(100);
        _mainCharacter.getDamage(10);
    }

    private void FinishLevel()
    {
        _mainCharacter.Finish();
    }

    private void GetBonus(int value)
    {
        Bonus bonus = new Bonus();
        bonus.SetValue(value);
        _mainCharacter.getBonus(bonus);
    }

    private Checkpoint PlayerGetCheckpoint(int queue, Vector3 position)
    {
        GameObject checkpointGameObject = new GameObject();
        checkpointGameObject.AddComponent<Checkpoint>();
        checkpointGameObject = Object.Instantiate(checkpointGameObject, position, Quaternion.identity);
       
        Checkpoint _checkpoint = checkpointGameObject.GetComponent<Checkpoint>();
        _checkpoint.SetQueue(queue);
        
        _mainCharacter.getCheckpoint(_checkpoint);
        
        return _checkpoint;
    }

    private Bonus PlayerGetBonus(int value, int id)
    {
        Bonus bonus = new Bonus();
        bonus.SetValue(value);
        bonus.SetID(id);
        _mainCharacter.getBonus(bonus);
        return bonus;
    }

}
