using MemoryLabyrinth.Fog;
using MemoryLabyrinth.Level.Objects.BonusLib;
using MemoryLabyrinth.Level.Objects.CheckpointLib;
using MemoryLabyrinth.Level.Objects.StartpointLib;
using MemoryLabyrinth.Path;
using MemoryLabyrinth.Player;
using MemoryLabyrinth.Resources;
using MemoryLabyrinth.SaveLoad;
using MemoryLabyrinth.UI.HUD;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace MemoryLabyrinth.Level.Logic
{
    public class LevelManager : MonoBehaviour
    {
        private GameObject _playerObj;

        private LevelModel _levelModel;

        private HUDController _HUDController;

        [SerializeField]
        private float _correctPathSpeed = 10f;

        [SerializeField]
        private float _fadeInFogTime = 2f;

        public static event UnityAction _levelLoad;

        private GameObject _levelPrefab;

        private SaveLoadManager _saveLoadManager;

        public ResourceManager.Level _currentLevelEnum;

        private MainCharacter _mainCharacter;

        private CorrectPathRenderer _correctPathBuilder;

        private bool _isPathShown;

        private GameObject _currentLevelObject;

        public static LevelManager Instance;

        public void SetCurrentLevel(ResourceManager.Level level)
        {
            _currentLevelEnum = level;
        }

        public void Init(GameObject playerObj, LevelModel levelModel, HUDController HUDController)
        {
            if (Instance != null)
                return;

            Instance = this;
            
            _playerObj = playerObj;
            _levelModel = levelModel;
            _HUDController = HUDController;

            _saveLoadManager = GetComponent<SaveLoadManager>();
            _mainCharacter = _playerObj.GetComponent<MainCharacter>();

            _levelModel._onLevelLose += (levelData) => LoseLevel();
            _levelModel._onLevelWin += (levelData) => WinLevel();

            // HUD Listeners
            _HUDController.SetupListeners();

            StartCurrentLevel();
        }

        public void StartCurrentLevelFromSpawn()
        {
            var newLevelData = new LevelData { _level = CurrentLevel.GetCurrentLevelData()._level };
            StartLevel(newLevelData);
        }

        public void StartCurrentLevel()
        {
            StartLevel(CurrentLevel.GetCurrentLevelData());
        }

        private void StartLevel(LevelData levelData)
        {
            //Destroy previous level if necessary
            if (_currentLevelObject != null)
                Destroy(_currentLevelObject);

            //Get level data
            _currentLevelEnum = levelData._level;

            _levelPrefab = ResourceManager.GetLevelObject(_currentLevelEnum);
            _currentLevelObject = Instantiate(_levelPrefab, new Vector3(0, 0, 0), Quaternion.identity);

            //Reset all temporary parameters
            _mainCharacter.ResetHealth();
            Timer.Instance.ResetTimer();
            _levelModel.SetBonusAmount(0);
            _levelModel.SetCurrentCheckPoint(FindObjectOfType<StartPoint>());

            bool startLevelFromSpawn = levelData._checkpointId == 0;

            //Load parameters if they exist
            if (!startLevelFromSpawn)
            {
                _mainCharacter.SetHealth(levelData._livesAmount);
                Timer.Instance.SetElapsedTime(levelData._time);
                ActivateCheckPointWithQueue(levelData._checkpointId);
                _levelModel.SetCollectedBonusesIDBeforeCheckPoint(levelData._collectedBonusesId);

                //Remove collected bonuses from map
                int currentMoney = 0;
                Bonus[] bonuses = FindObjectsOfType<Bonus>();
                foreach (Bonus bonusOnMap in bonuses)
                {
                    if (levelData._collectedBonusesId.Contains(bonusOnMap.GetID()))
                    {
                        currentMoney += bonusOnMap.GetValue();
                        bonusOnMap.DestroySelf();
                    }
                }
                _levelModel.SetBonusAmount(currentMoney);
            }

            //Reset collected bonuses
            _levelModel.SetCollectedBonusesIDBuffer(new List<int>());

            //Put the player on the checkpoint
            _playerObj.transform.position = _levelModel.GetCurrentCheckPoint().transform.position + new Vector3(0, 0, -1);

            if (startLevelFromSpawn)
            {
                //Play level intro
                StopAllCoroutines();
                StartCoroutine(PlayLevelIntro());
            }
            else
            {
                //Instantly fade in
                FogController.Instance.SetFogVisibile(true);
                FogController.Instance.FadeInToAllTargets(0);
            }

        }

        private void ActivateCheckPointWithQueue(int targetQueue)
        {
            Checkpoint[] checkpoints = FindObjectsOfType<Checkpoint>();
            foreach (Checkpoint checkpoint in checkpoints)
            {
                if (checkpoint.GetQueue() == targetQueue)
                    _levelModel.SetCurrentCheckPoint(checkpoint);
                Debug.Log($"LEVELMANAGER: Can't find checkpoint with queue {targetQueue}");
            }
        }


        private void WinLevel()
        {
            _mainCharacter.SetActive(false);
            Timer.Instance.SetTimerActive(false);
            Debug.Log("LevelManager: level win");
        }

        private void LoseLevel()
        {
            _mainCharacter.SetActive(false);
            Timer.Instance.SetTimerActive(false);
            Debug.Log("LevelManager: level lose");
        }

        private void StartShowPath()
        {
            _correctPathBuilder.Init();
            _correctPathBuilder.ShowRightPath(_correctPathSpeed);
            _isPathShown = true;
        }

        private void StopShowPath()
        {
            if (_isPathShown)
            {
                _correctPathBuilder.Hide();
            }
            _isPathShown = false;
        }
        /*
        private void SaveCompleteLevel()
        {
            LevelData levelData = new LevelData
            {
                _level = _currentLevelEnum,
                _livesAmount = _mainCharacter.GetHealth(),
                _checkpointId = 0,
                _time = Timer.Instance.GetElapsedTime(),
                _isCompleted = true,
                _collectedBonusesId = _levelModel.GetCollectedBonusesIDBeforeCheckpoint()

            };

            CurrentLevel.Save(levelData);

            int bonusAmount = _levelModel.GetBonusAmount();
            if (BonusStorage.Instance != null)
            {
                BonusStorage.Instance.EarnBonuses(bonusAmount);
                Debug.Log("LEVELMANAGER: complete level bonuses saved");
            }
            else
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
                _checkpointId = _levelModel.GetCurrentCheckPoint().GetQueue(),
                _time = Timer.Instance.GetElapsedTime(),
                _isCompleted = false,
                _collectedBonusesId = _levelModel.GetCollectedBonusesIDBeforeCheckpoint()

            };

            CurrentLevel.Save(levelData);

            Debug.Log("LevelManager: uncomplete level saved");
        }
        */

        private IEnumerator PlayLevelIntro()
        {
            //Pause player, timer, and disable fog
            _mainCharacter.SetActive(false);
            Timer.Instance.SetTimerActive(false);
            FogController.Instance.SetFogVisibile(false);

            //Show the correct path and wait for it to finish
            _correctPathBuilder = FindObjectOfType<CorrectPathRenderer>();
            StartShowPath();
            while (!_correctPathBuilder.IsFinished())
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
            Timer.Instance.SetTimerActive(true);
        }
    }
}
