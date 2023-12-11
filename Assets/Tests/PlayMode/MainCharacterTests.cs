using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEditor;
using MemoryLabyrinth.Player;
using MemoryLabyrinth.Level.Objects.CheckpointLib;

[TestFixture]
public class MainCharacterTests
{
    private MainCharacter _mainCharacter;

    [SetUp]
    public void Setup()
    {   //timescale 
        Time.timeScale = 100;

        string playerPrefabPath = "Assets/Prefabs/Player.prefab";
        GameObject playerPrefab = AssetDatabase.LoadAssetAtPath(playerPrefabPath, typeof(Object)) as GameObject;
        _mainCharacter = Object.Instantiate(playerPrefab, new Vector3(0, 0, 0), Quaternion.identity).GetComponent<MainCharacter>();
        // create touchControls on scene
        _mainCharacter.Init();

        Debug.Log("Setup");
    }

    [Test]
    public void GetDamage_HealthGreaterThanDamage_DecreasesHealth()
    {
        
        int initialHealth = 100;
        int damage = 20;
        _mainCharacter.SetMaxHealth(initialHealth);
        _mainCharacter.SetHealth(initialHealth);

        _mainCharacter.getDamage(damage);

        Assert.AreEqual(initialHealth - damage, _mainCharacter.GetHealth());
    }

    [Test]
    public void GetDamage_HealthEqualToDamage_InvokesOnDeathEvent()
    {
        
        int initialHealth = 50;
        int damage = 50;
        _mainCharacter.SetMaxHealth(initialHealth);
        _mainCharacter.SetHealth(initialHealth);
        bool onDeathEventInvoked = false;
        _mainCharacter._onDeathEvent += () => onDeathEventInvoked = true;

        _mainCharacter.getDamage(damage);

        Assert.IsTrue(onDeathEventInvoked);
        Assert.AreEqual(0, _mainCharacter.GetHealth());
    }

    [Test]
    public void GetDamage_HealthLessThanDamage_InvokesOnDeathEvent()
    {
        
        int initialHealth = 30;
        int damage = 50;
        _mainCharacter.SetMaxHealth(initialHealth);
        _mainCharacter.SetHealth(initialHealth);
        bool onDeathEventInvoked = false;
        _mainCharacter._onDeathEvent += () => onDeathEventInvoked = true;

        _mainCharacter.getDamage(damage);

        Assert.IsTrue(onDeathEventInvoked);
        Assert.AreEqual(0, _mainCharacter.GetHealth());
    }

    [Test]
    public void GetHealth_ReturnsCorrectHealth()
    {
        int expectedHealth = 3;
        int actualHealth = _mainCharacter.GetHealth();

        Assert.AreEqual(expectedHealth, actualHealth);
    }

    [Test]
    public void SetHealth_ChangesHealthValue()
    {
        int newHealth = 2;
        _mainCharacter.SetHealth(newHealth);

        int actualHealth = _mainCharacter.GetHealth();

        Assert.AreEqual(newHealth, actualHealth);
    }

    [Test]
    public void ResetHealth_ResetsHealthToMaxHealth()
    {
        int maxHealth = 3;
        int newHealth = 2;
        _mainCharacter.SetMaxHealth(maxHealth);
        _mainCharacter.SetHealth(newHealth);

        _mainCharacter.ResetHealth();

        int actualHealth = _mainCharacter.GetHealth();

        Assert.AreEqual(maxHealth, actualHealth);
    }

    [Test]
    public void SetHealth_HealthAboveMaxHealth_SetsHealthToMaxHealth()
    {
        int maxHealth = 3;
        int newHealth = 5;
        _mainCharacter.SetMaxHealth(maxHealth);
        _mainCharacter.SetHealth(newHealth);

        int actualHealth = _mainCharacter.GetHealth();

        Assert.AreEqual(maxHealth, actualHealth);
    }
    
    //coroutine
    [UnityTest]
    public IEnumerator TeleportTo_TeleportsToPosition()
    {
        
        Vector3 position = new Vector3(1, 1, 0);
        _mainCharacter.TeleportTo(position);
        float time = 0;
        while (time < 5)
        {
            time += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
        Assert.AreEqual(position, _mainCharacter.transform.position);
    }

    [UnityTest]
    public IEnumerator TeleportTo_InvokesOnTeleportEvent()
    {
        Vector3 position = new Vector3(1, 1, 0);
        bool onTeleportEventInvoked = false;
        _mainCharacter._onTeleportEvent += () => onTeleportEventInvoked = true;

        _mainCharacter.TeleportTo(position);
        float time = 0;
        while (time < 5)
        {
            time += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }

        Assert.IsTrue(onTeleportEventInvoked);
    }

    [UnityTest]
    public IEnumerator GetCheckpoint_InvokesOnCheckpointEvent()
    {
        bool onCheckpointEventInvoked = false;
        _mainCharacter._onCheckpointEvent += (checkpoint) => onCheckpointEventInvoked = true;

        _mainCharacter.getCheckpoint(new Checkpoint(1));
        yield return new WaitForSeconds(0.1f);

        Assert.IsTrue(onCheckpointEventInvoked);
    }

    [UnityTest]
    public IEnumerator MovingByOneCell()
    {   
        // move character by one cell
        List<Vector3> path = new List<Vector3>();
        path.Add(new Vector3(0, 0, 0));
        path.Add(new Vector3(1, 0, 0));
        _mainCharacter.FollowPath(path);
        // wait for moving and update position of character
        float time = 0;
        while (time < 5)
        {
            time += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }

        // check if character is on the right position
        float delta = 0.1f;
        Assert.AreEqual(1, _mainCharacter.transform.position.x, delta);
        Assert.AreEqual(0, _mainCharacter.transform.position.y, delta);
        Assert.AreEqual(0, _mainCharacter.transform.position.z, delta);

               
    }

    [UnityTest]
    public IEnumerator MovingAndTeleport()
    {
        // move character by 10 cells
        List<Vector3> path = new List<Vector3>();
        path.Add(new Vector3(0, 0, 0));
        path.Add(new Vector3(1, 0, 0));
        path.Add(new Vector3(2, 0, 0));
        path.Add(new Vector3(3, 0, 0));
        path.Add(new Vector3(4, 0, 0));
        path.Add(new Vector3(5, 0, 0));
        path.Add(new Vector3(6, 0, 0));
        path.Add(new Vector3(7, 0, 0));
        path.Add(new Vector3(8, 0, 0));
        path.Add(new Vector3(9, 0, 0));
        path.Add(new Vector3(10, 0, 0));
 
        _mainCharacter.FollowPath(path);
        // wait for moving half way and teleport character
        float time = 0;
        while (time < 1)
        {
            time += Time.fixedDeltaTime;
            
            yield return new WaitForFixedUpdate();
        }

        time = 0;
        _mainCharacter.TeleportTo(new Vector3(0, 0, 0));
        while (time < 1)
        {
            time += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }

        // check if character is on the right position
        float delta = 0.1f;
        Assert.AreEqual(0, _mainCharacter.transform.position.x, delta);
        Assert.AreEqual(0, _mainCharacter.transform.position.y, delta);
        Assert.AreEqual(0, _mainCharacter.transform.position.z, delta);
    }


    [TearDown]
    public void Teardown()
    {
        //timescale 
        Time.timeScale = 1;
        Object.DestroyImmediate(_mainCharacter.gameObject);
    }
}
