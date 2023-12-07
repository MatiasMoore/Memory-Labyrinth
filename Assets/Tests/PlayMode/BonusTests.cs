using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEditor;
using MemoryLabyrinth.Player;
using MemoryLabyrinth.Level.Objects.BonusLib;

[TestFixture]
public class BonusTests
{

    private MainCharacter _mainCharacter;
    private GameObject _mainCharacterGameObject;

    private Bonus _bonus;
    private GameObject _bonusGameObject;
    [SetUp]
    public void Setup()
    {
        //timescale 
        Time.timeScale = 100;

        string playerPrefabPath = "Assets/Prefabs/Player.prefab";
        GameObject playerPrefab = AssetDatabase.LoadAssetAtPath(playerPrefabPath, typeof(Object)) as GameObject;
        _mainCharacterGameObject = Object.Instantiate(playerPrefab, new Vector3(10, 10, 10), Quaternion.identity);
        _mainCharacter = _mainCharacterGameObject.GetComponent<MainCharacter>();
        _mainCharacter.Init();

        string bonusPrefabPath = "Assets/Prefabs/LevelParts/Bonus.prefab";
        _bonusGameObject = AssetDatabase.LoadAssetAtPath(bonusPrefabPath, typeof(Object)) as GameObject;
        _bonusGameObject = Object.Instantiate(_bonusGameObject, new Vector3(0, 0, 0), Quaternion.identity);
        _bonus = _bonusGameObject.GetComponent<Bonus>();

        Debug.Log("Setup");
    }

    [TearDown]
    public void Teardown()
    {
        //timescale 
        Time.timeScale = 1;
        if (_mainCharacterGameObject != null)
            Object.DestroyImmediate(_mainCharacterGameObject);
        if (_bonusGameObject != null)
            Object.DestroyImmediate(_bonusGameObject);
    }

    [Test]
    public void TestSetValue()
    {
        int value = 20;
        _bonus.SetValue(value);
        Assert.AreEqual(value, _bonus.GetValue());
    }

    [Test]
    public void TestSetID()
    {
        int id = 1;
        _bonus.SetID(id);
        Assert.AreEqual(id, _bonus.GetID());
    }

    [UnityTest]
    public IEnumerator TestDestroySelf()
    {
        _bonus.DestroySelf();
        yield return new WaitForFixedUpdate();
        Assert.IsTrue(_bonusGameObject == null);
        Assert.IsTrue(_bonus == null);

    }

    [UnityTest]
    public IEnumerator TestOnTriggerEnter2D_ShouldInvokeGetBonusEvent()
    {
        int value = 20;
        int id = 1;
        _bonus.SetValue(value);
        _bonus.SetID(id);

        bool eventInvoked = false;
        _mainCharacter._onBonusEvent += (bonus) => eventInvoked = true;

        _mainCharacterGameObject.transform.position = new Vector3(10, 10, 10);
        _bonusGameObject.transform.position = new Vector3(10, 10, 10);

        

        yield return new WaitForSeconds(0.1f);

        Assert.IsTrue(eventInvoked);

    }

}
