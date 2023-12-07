using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEditor;
using MemoryLabyrinth.Player;
using MemoryLabyrinth.Fog;
using MemoryLabyrinth.Controls;
using MemoryLabyrinth.Cam;

[TestFixture]
public class FogControllerTests
{
    private MainCharacter _mainCharacter;
    private GameObject _touchControlObject;
    private GameObject _camObj;
    private GameObject _fogObj;
    private FogController _fogController;
    private FogMaskTarget _fogMask;

    [SetUp]
    public void Setup()
    {   //timescale 
        Time.timeScale = 100;

        string camPath = "Assets/Prefabs/Main Camera.prefab";
        GameObject camPrefab = AssetDatabase.LoadAssetAtPath(camPath, typeof(Object)) as GameObject;
        _camObj = Object.Instantiate(camPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        MonoBehaviour.DestroyImmediate(_camObj.GetComponent<CameraScript>());

        string fogPrefabPath = "Assets/Prefabs/Fog Controller.prefab";
        GameObject fogPrefab = AssetDatabase.LoadAssetAtPath(fogPrefabPath, typeof(Object)) as GameObject;
        _fogObj = Object.Instantiate(fogPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        _fogController = _fogObj.GetComponent<FogController>();
        _fogController.Init();

        string playerPrefabPath = "Assets/Prefabs/Player.prefab";
        string touchControlsPrefabPath = "Assets/Tests/TestsPrefabs/TouchControls.prefab";
        GameObject playerPrefab = AssetDatabase.LoadAssetAtPath(playerPrefabPath, typeof(Object)) as GameObject;
        var _playerObj = Object.Instantiate(playerPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        _mainCharacter = _playerObj.GetComponent<MainCharacter>();
        _fogMask = _playerObj.GetComponent<FogMaskTarget>();
        // create touchControls on scene
        GameObject touchControlObject = AssetDatabase.LoadAssetAtPath(touchControlsPrefabPath, typeof(Object)) as GameObject;
        _touchControlObject = Object.Instantiate(touchControlObject, new Vector3(0, 0, 0), Quaternion.identity);
        _touchControlObject.GetComponent<TouchControls>().Init();
        _mainCharacter.Init();

        Debug.Log("Setup");
    }

    [TearDown]
    public void Teardown()
    {
        //timescale 
        Time.timeScale = 1;
        Object.DestroyImmediate(_touchControlObject);
        Object.DestroyImmediate(_mainCharacter.gameObject);
        Object.DestroyImmediate(_camObj);
        Object.DestroyImmediate(_fogObj);
    }

    [UnityTest]
    public IEnumerator CreateMaskObj()
    {
        var fogMask = GameObject.Find("FogMask(Clone)");
        Assert.IsNull(fogMask);
        FogController.Instance.FadeInToAllTargets(0);
        fogMask = GameObject.Find("FogMask(Clone)");
        Assert.IsNotNull(fogMask);
        MonoBehaviour.DestroyImmediate(fogMask);
        return null;
    }

    [UnityTest]
    public IEnumerator CreateAndDestroyMaskObj()
    {
        var fogMask = GameObject.Find("FogMask(Clone)");
        Assert.IsNull(fogMask);
        FogController.Instance.FadeInToAllTargets(0);
        fogMask = GameObject.Find("FogMask(Clone)");
        Assert.IsNotNull(fogMask);
        _mainCharacter.gameObject.GetComponent<FogMaskTarget>().DeleteMask();
        yield return null;
        fogMask = GameObject.Find("FogMask(Clone)");
        Assert.IsNull(fogMask);
    }

}
