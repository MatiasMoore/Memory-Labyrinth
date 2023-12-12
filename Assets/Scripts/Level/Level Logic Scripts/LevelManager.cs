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

        public event UnityAction _levelStarted;

        private MainCharacter _mainCharacter;

        private CorrectPathRenderer _correctPathBuilder;

        private GameObject _currentLevelObject;

        public static LevelManager Instance;

        public void Init(GameObject playerObj, LevelModel levelModel, HUDController HUDController)
        {
            if (Instance != null)
                return;

            Instance = this;
            
            _playerObj = playerObj;
            _levelModel = levelModel;
            _HUDController = HUDController;

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
            ResourceManager.Level currentLevelEnum = levelData._level;

            GameObject levelPrefab = ResourceManager.GetLevelObject(currentLevelEnum);
            _currentLevelObject = Instantiate(levelPrefab, new Vector3(0, 0, 0), Quaternion.identity);

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
                _levelModel.SetCollectedBonuses(levelData._collectedBonuses);

                //Remove collected bonuses from map
                int currentMoney = 0;
                List<int> bonusesIdToRemove = new List<int>();
                foreach (BonusInfo bonusInfo in levelData._collectedBonuses) 
                {
                    currentMoney += bonusInfo._value;
                    bonusesIdToRemove.Add(bonusInfo._id);
                }
                
                Bonus[] bonuses = FindObjectsOfType<Bonus>();
                foreach (Bonus bonusOnMap in bonuses)
                {
                    if (bonusesIdToRemove.Contains(bonusOnMap.GetID()))
                        bonusOnMap.DestroySelf();
                }

                _levelModel.SetBonusAmount(currentMoney);
            }

            //Reset collected bonuses
            _levelModel.SetCollectedBonuses(new List<BonusInfo>());

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

            _levelStarted?.Invoke();
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
        }

        private void StopShowPath()
        {
            _correctPathBuilder.Hide();
        }

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
