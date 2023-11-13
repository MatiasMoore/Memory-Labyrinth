using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _levelPrefab;

    [SerializeField]
    static public ResourceManager.Level _currentLevel;

    [SerializeField]
    private GameObject _player;

    private MainCharacter _mainCharacter;

    [SerializeField]
    private LevelModel _levelModel;

    [SerializeField]
    private float _startLevelTime;

    private RightPathBuilder _rightPathBuilder;

    private float _timer;

    private bool _isLevelActive;

    private bool _isPathShown;

    public void SetCurrentLevel(ResourceManager.Level level)
    {
        _currentLevel = level;
    }

    public void Start()
    {
        _player.SetActive(false);
        _mainCharacter = _player.GetComponent<MainCharacter>();
        _mainCharacter.Init();

        _levelModel.Init(_mainCharacter);
        _levelModel._onLevelLose += LoseLevel;
        _levelModel._onLevelWin += WinLevel;

        var audioController = FindObjectOfType<AudioController>();
        if (audioController != null)
            audioController.SetupListeners(_levelModel, _mainCharacter);

        // UI Listeners
        MenuController.SetupListeners(_levelModel, _mainCharacter);

        Timer.SetTimerStatus(false);

        StartLevel();
    }

    public void StartLevel()
    {
        if (!_isLevelActive)
        {
            _player.SetActive(true);

            _levelPrefab = ResourceManager.LoadLevel(_currentLevel);
            Instantiate(_levelPrefab, new Vector3(0, 0, 0), Quaternion.identity);

            _levelModel.StartNewLevel();
            _player.GetComponent<Transform>().position = _levelModel.GetCheckPoint().transform.position + new Vector3(0, 0, -1);


            _rightPathBuilder = FindObjectOfType<RightPathBuilder>();
            _rightPathBuilder.ShowRightPath(_startLevelTime * 0.9f);

            _mainCharacter.SetActive(false);

            _timer = 0;
            _isLevelActive = true;
            _isPathShown = true;
        }
    }


    public void LoadLevel()
    {
        // LevelData levelData = SaveLoadManager.LoadGame();
        // TODO
        
    }


    private void WinLevel()
    {
        _mainCharacter.SetActive(false);
        Timer.SetTimerStatus(false);
        _isLevelActive = false;
        Debug.Log("LevelManager: level win");
        SaveCompleteLevel();

    }

    private void LoseLevel()
    {
        _mainCharacter.SetActive(false);
        Timer.SetTimerStatus(false);
        _isLevelActive = false;
        Debug.Log("LevelManager: level lose");

    }

    private void StopShowPath()
    {   if (_isPathShown)
        {
            _mainCharacter.SetActive(true);
            _rightPathBuilder.SetActive(false);
            Timer.SetTimerStatus(true);
            //TODO: show fog
        }

        _isPathShown = false;

    }

    public void SaveCompleteLevel()
    {
        //BonusStorage.Instance.EarnBonuses(_levelModel.GetBonusAmout());
        // SaveLoadManager.SaveGame();
        // TODO
        Debug.Log("LevelManager: complete level saved");
    }

    public void SaveUncompleteLevel()
    {
        //TODO
        Debug.Log("LevelManager: uncomplete level saved");
    }


    private void Update()
    {
        if (_isLevelActive)
        {
            _timer += Time.deltaTime;

            if (_timer > _startLevelTime && _timer != 0)
            {
                StopShowPath();
            }
        }
    }
}
