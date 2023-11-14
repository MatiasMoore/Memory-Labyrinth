using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Events;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    [SerializeField]
    private GameObject _levelPrefab;

    private SaveLoadManager _saveLoadManager;

    [SerializeField]
    public ResourceManager.Level _currentLevel;

    [SerializeField]
    private GameObject _player;

    private MainCharacter _mainCharacter;

    [SerializeField]
    private LevelModel _levelModel;

    [SerializeField]
    private float _correctPathSpeed = 10f;

    [SerializeField]
    private float _fadeInFogTime = 2f;

    private CorrectPathRenderer _rightPathBuilder;

    private bool _isPathShown;

    public static event UnityAction _levelLoad;

    public GameObject _currentLevelObject;

    public void SetCurrentLevel(ResourceManager.Level level)
    {
        _currentLevel = level;
    }

    public void Init()
    {
        if (Instance != null)
            return;

        Instance = this;

        _saveLoadManager = GetComponent<SaveLoadManager>();
        //Debug.Log($"LEVELMANAGER: {LevelProgressStorage.Instance.currentLevels}");
        _mainCharacter = _player.GetComponent<MainCharacter>();
       
        _levelModel._onLevelLose += LoseLevel;
        _levelModel._onLevelWin += WinLevel;

        var audioController = FindObjectOfType<AudioController>();
        if (audioController != null)
            audioController.SetupListeners(_levelModel, _mainCharacter);

        // UI Listeners
        MenuController.SetupListeners(_levelModel, _mainCharacter);

        StartLevel();
    }

    public void StartLevel()
    {
        LevelData levelData = CurrentLevel.getCurrentLevel();
        _currentLevel = levelData._level;


        if(_currentLevelObject != null)
        {
            Destroy(_currentLevelObject);
        }
        _player.SetActive(true);
        
        Timer.Instance.ResetTimer();

        _levelPrefab = ResourceManager.LoadLevel(_currentLevel);
        _currentLevelObject = Instantiate(_levelPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        if (levelData._checkpointId != 0 )
        {
            LoadLevelData(levelData);
        } else
        {
            _levelModel.SetBonusAmount(0);
            _levelModel.SetCheckPoint(FindObjectOfType<StartPoint>());
        }

        SetPlayerToCheckpointPosition();

        _mainCharacter.SetActive(false);

        StopAllCoroutines();
        if(levelData._checkpointId == 0)
        {
            StartCoroutine(PlayLevelIntro());
        } else
        {
            _rightPathBuilder = FindObjectOfType<CorrectPathRenderer>();
            _rightPathBuilder.SetActive(false);

            FogController.Instance.SetFogVisibile(true);

            FogController.Instance.FadeInToAllTargets(0);

            _mainCharacter.SetActive(true);
            Timer.Instance.SetTimerStatus(true);
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

    public void SaveCompleteLevel()
    {
        LevelData levelData = new LevelData
        {
            _level = _currentLevel,
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
            _level = _currentLevel,
            _livesAmount = _mainCharacter.GetHealth(),
            _checkpointId = _levelModel.GetCheckPoint().GetQueue(),
            _time = Timer.Instance.GetElapsedTime(),
            _isCompleted = false,
            _collectedBonusesId = new List<int> { 123 }

        };

        CurrentLevel.Save(levelData);

        Debug.Log("LevelManager: uncomplete level saved");
    }

    private void SetPlayerToCheckpointPosition()
    {    
        _player.GetComponent<Transform>().position = _levelModel.GetCheckPoint().transform.position + new Vector3(0, 0, -1);
    }

    private IEnumerator PlayLevelIntro()
    {
        Timer.Instance.SetTimerStatus(false);
        FogController.Instance.SetFogVisibile(false);
        StartShowPath();
        float timer = 0;

        while (!_rightPathBuilder.IsFinished())
        {
            timer += Time.deltaTime;
            yield return null;
        }

        StopShowPath();

        FogController.Instance.SetFogVisibile(true);

        FogController.Instance.FadeInToAllTargets(_fadeInFogTime);

        timer = 0;
        while (timer < _fadeInFogTime)
        {

            timer += Time.deltaTime;
            yield return null;
        }

        _mainCharacter.SetActive(true);
        Timer.Instance.SetTimerStatus(true);
    }
}
