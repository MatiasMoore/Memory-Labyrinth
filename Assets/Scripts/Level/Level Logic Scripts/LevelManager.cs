using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _levelPrefab;

    private SaveLoadManager _saveLoadManager;

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

    private bool _isPathShown;

    public void SetCurrentLevel(ResourceManager.Level level)
    {
        _currentLevel = level;
    }

    public void Start()
    {
        _saveLoadManager = GetComponent<SaveLoadManager>();
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

        StartLevel();
    }

    public void StartLevel()
    {
        Timer.SetTimerStatus(false);
        _player.SetActive(true);

        _levelPrefab = ResourceManager.LoadLevel(_currentLevel);
        Instantiate(_levelPrefab, new Vector3(0, 0, 0), Quaternion.identity);

        _levelModel.SetBonusAmount(0);
        _levelModel.SetCheckPoint(FindObjectOfType<StartPoint>());
        _player.GetComponent<Transform>().position = _levelModel.GetCheckPoint().transform.position + new Vector3(0, 0, -1);

        _mainCharacter.SetActive(false);

        StopAllCoroutines();
        StartCoroutine(PlayLevelIntro());
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
        Debug.Log("LevelManager: level win");
        SaveCompleteLevel();

    }

    private void LoseLevel()
    {
        _mainCharacter.SetActive(false);
        Timer.SetTimerStatus(false);
        Debug.Log("LevelManager: level lose");

    }

    private void StartShowPath()
    {
        _rightPathBuilder = FindObjectOfType<RightPathBuilder>();
        _rightPathBuilder.ShowRightPath(_startLevelTime * 0.9f);
        _isPathShown = true;
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
        int bonusAmount = _levelModel.GetBonusAmount();
        if (BonusStorage.Instance != null)
        {
            BonusStorage.Instance.EarnBonuses(bonusAmount);
            _saveLoadManager.SaveGame();
            Debug.Log("LEVELMANAGER: complete level saved");
        } else
        {
            Debug.Log("LEVELMANAGER: save failed!");
        }
        
    }

    public void SaveUncompleteLevel()
    {
        //TODO
        Debug.Log("LevelManager: uncomplete level saved");
    }

    private IEnumerator PlayLevelIntro()
    {
        StartShowPath();
        float timer = 0;

        while (timer < _startLevelTime)
        {

            timer += Time.deltaTime;
            yield return null;
        }

        StopShowPath();
    }
}
