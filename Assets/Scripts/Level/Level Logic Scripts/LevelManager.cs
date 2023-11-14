using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Events;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _playerObj;

    [SerializeField]
    private LevelModel _levelModel;

    [SerializeField]
    private float _correctPathSpeed = 10f;

    [SerializeField]
    private float _fadeInFogTime = 2f;

    public static event UnityAction _levelLoad;

    private GameObject _levelPrefab;

    private SaveLoadManager _saveLoadManager;

    public ResourceManager.Level _currentLevelEnum;

    private MainCharacter _mainCharacter;  

    private CorrectPathRenderer _rightPathBuilder;

    private bool _isPathShown;

    private GameObject _currentLevelObject;

    public static LevelManager Instance;

    public void SetCurrentLevel(ResourceManager.Level level)
    {
        _currentLevelEnum = level;
    }

    public void Init()
    {
        if (Instance != null)
            return;

        Instance = this;

        _saveLoadManager = GetComponent<SaveLoadManager>();
        _mainCharacter = _playerObj.GetComponent<MainCharacter>();
       
        _levelModel._onLevelLose += LoseLevel;
        _levelModel._onLevelWin += WinLevel;

        // UI Listeners
        MenuController.SetupListeners(_levelModel, _mainCharacter);

        StartLevel();
    }

    public void StartLevel()
    {
        //Destroy previous level if necessary
        if(_currentLevelObject != null)
            Destroy(_currentLevelObject);

        //Get level data
        LevelData levelData = CurrentLevel.getCurrentLevel();
        _currentLevelEnum = levelData._level;

        _levelPrefab = ResourceManager.GetLevelObject(_currentLevelEnum);
        _currentLevelObject = Instantiate(_levelPrefab, new Vector3(0, 0, 0), Quaternion.identity);

        //Reset all temporary parameters
        _mainCharacter.ResetHealth();
        Timer.Instance.ResetTimer();
        _levelModel.SetBonusAmount(0);
        _levelModel.SetCheckPoint(FindObjectOfType<StartPoint>());

        bool levelDataIsFound = levelData._checkpointId != 0;

        //Load parameters if they exist
        if (levelDataIsFound)
            LoadLevelData(levelData);
        
        //Put the player on the checkpoint
        _playerObj.transform.position = _levelModel.GetCheckPoint().transform.position + new Vector3(0, 0, -1);

        if (!levelDataIsFound)
        {
            //Play level intro
            StopAllCoroutines();
            StartCoroutine(PlayLevelIntro());
        } else
        {
            //Instantly load
            _rightPathBuilder = FindObjectOfType<CorrectPathRenderer>();
            _rightPathBuilder.SetActive(false);

            //Instantly fade in
            FogController.Instance.SetFogVisibile(true);
            FogController.Instance.FadeInToAllTargets(0);

            StartTimerAndEnablePlayerControl();
        }

    }

    public void LoadLevelData(LevelData levelData)
    {
        _mainCharacter.SetHealth(levelData._livesAmount);
        Checkpoint[] checkpoints = FindObjectsOfType<Checkpoint>();
        foreach (Checkpoint checkpoint in checkpoints)
        {
            if (checkpoint.GetQueue() == levelData._checkpointId)
                _levelModel.SetCheckPoint(checkpoint);             
            Debug.Log($"LEVELMANAGER: Can't find checkpoint with queue {levelData._checkpointId}");
        }

        Timer.Instance.SetElapsedTime(levelData._time);
    }


    private void WinLevel()
    {
        _mainCharacter.SetActive(false);
        Timer.Instance.SetTimerStatus(false);
        Debug.Log("LevelManager: level win");
        SaveCompleteLevel();
    }

    private void LoseLevel()
    {
        _mainCharacter.SetActive(false);
        Timer.Instance.SetTimerStatus(false);
        Debug.Log("LevelManager: level lose");
    }

    private void StartShowPath()
    {
        _rightPathBuilder = FindObjectOfType<CorrectPathRenderer>();
        _rightPathBuilder.ShowRightPath(_correctPathSpeed);
        _isPathShown = true;
    }

    private void StopShowPath()
    {   
        if (_isPathShown)
        {
            _rightPathBuilder.SetActive(false);
        }
        _isPathShown = false;
    }

    private void SaveCompleteLevel()
    {
        LevelData levelData = new LevelData
        {
            _level = _currentLevelEnum,
            _livesAmount = _mainCharacter.GetHealth(),
            _checkpointId = 0,
            _time = Timer.Instance.GetElapsedTime(),
            _isCompleted = true,
            _collectedBonusesId = new List<int> { 123}

        };

        CurrentLevel.Save(levelData);

        int bonusAmount = _levelModel.GetBonusAmount();
        if (BonusStorage.Instance != null)
        {
            BonusStorage.Instance.EarnBonuses(bonusAmount);
            Debug.Log("LEVELMANAGER: complete level bonuses saved");
        } else
        {
            Debug.Log("LEVELMANAGER: bonuses save failed!");
        }

        _saveLoadManager.SaveGame();
    }

    public void SaveUncompleteLevel()
    {
        LevelData levelData = new LevelData
        {
            _level = _currentLevelEnum,
            _livesAmount = _mainCharacter.GetHealth(),
            _checkpointId = _levelModel.GetCheckPoint().GetQueue(),
            _time = Timer.Instance.GetElapsedTime(),
            _isCompleted = false,
            _collectedBonusesId = new List<int> { 123 }

        };

        CurrentLevel.Save(levelData);

        Debug.Log("LevelManager: uncomplete level saved");
    }

    private IEnumerator PlayLevelIntro()
    {
        //Pause player, timer, and disable fog
        _mainCharacter.SetActive(false);
        Timer.Instance.SetTimerStatus(false);
        FogController.Instance.SetFogVisibile(false);

        //Show the correct path and wait for it to finish
        StartShowPath();
        while (!_rightPathBuilder.IsFinished())
        { 
            yield return null;
        }
        StopShowPath();

        //Fade in fog
        FogController.Instance.SetFogVisibile(true);
        FogController.Instance.FadeInToAllTargets(_fadeInFogTime);

        //Wait until it has faded in completely and enable player movement
        float timer = 0;
        while (timer < _fadeInFogTime)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        StartTimerAndEnablePlayerControl();
    }

    private void StartTimerAndEnablePlayerControl()
    {
        _mainCharacter.SetActive(true);
        Timer.Instance.SetTimerStatus(true);
    }
}
