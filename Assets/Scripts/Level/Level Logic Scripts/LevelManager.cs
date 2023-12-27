using MemoryLabyrinth.Fog;
using MemoryLabyrinth.Level.Editor;
using MemoryLabyrinth.Level.Objects.BonusLib;
using MemoryLabyrinth.Level.Objects.CheckpointLib;
using MemoryLabyrinth.Level.Objects.CorrectPathLib;
using MemoryLabyrinth.Level.Objects.FinishLib;
using MemoryLabyrinth.Level.Objects.StartpointLib;
using MemoryLabyrinth.Level.Objects.TeleportLib;
using MemoryLabyrinth.Pathing;
using MemoryLabyrinth.Player;
using MemoryLabyrinth.Resources;
using MemoryLabyrinth.SaveLoad;
using MemoryLabyrinth.UI.HUD;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        private float _fadeInFogTime = 2f;

        public event UnityAction _levelStarted;

        private MainCharacter _mainCharacter;

        private CorrectPathRenderer _correctPathBuilder;

        private LevelPartsContainer _currentLevelContainer = new LevelPartsContainer();

        public static LevelManager Instance;

        [SerializeField]
        private float _timeToDrawCorrectPath;

        [SerializeField]
        private float _timeToShowCorrectPath;

        [SerializeField]
        private float _timeToDrawTeleportPath;

        [SerializeField]
        private float _timeToRemovePath;

        [SerializeField]
        private PathRenderer _pathRenderer;

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
            var newLevelData = new LevelProgress { _levelName = LevelBuilder.GetCurrentLevelData()._levelName };
            StartLevel(newLevelData);
        }

        public void StartCurrentLevel()
        {
            StartLevel(LevelBuilder.GetCurrentLevelData());
        }

        private void StartLevel(LevelProgress levelData)
        {
            //Destroy previous level if necessary
            if (_currentLevelContainer.GetAllParts().Count != 0)
            {
                foreach (var obj in _currentLevelContainer.GetAllParts())
                    Destroy(obj.obj);
                    
            }

            //Destroy correct path if exist
            if (_correctPathBuilder != null)
            {
                _correctPathBuilder.Hide();
                Destroy(_correctPathBuilder);
            }

            //Get level objects
            _currentLevelContainer = LevelBuilder.BuildCurrentLevelToScene();

            //Hide CorrectLevelPoints
            List<CorrectPath> correctPaths = _currentLevelContainer.GetPartsOfType<CorrectPath>();
            foreach (var correctPath in correctPaths)
            {
                correctPath.gameObject.SetActive(false);
            }

            //Reset all temporary parameters
            _mainCharacter.ResetHealth();
            Timer.Instance.ResetTimer();
            _levelModel.SetBonusAmount(0);
            _levelModel.SetCurrentCheckPoint(_currentLevelContainer.GetPartsOfType<StartPoint>().First());

            //Reset collected bonuses
            _levelModel.SetCollectedBonuses(new List<BonusInfo>());

            bool startLevelFromSpawn = levelData._checkpointId == 0;

            //Load parameters if they exist
            if (!startLevelFromSpawn)
            {
                _mainCharacter.SetHealth(levelData._livesAmount);
                Timer.Instance.SetElapsedTime(levelData._time);
                ActivateCheckPointWithQueue(levelData._checkpointId);
                _levelModel.SetCollectedBonuses(levelData._collectedBonuses);

                //Remove collected bonuses from map
                List<int> bonusesIdToRemove = new List<int>();
                foreach (BonusInfo bonusInfo in levelData._collectedBonuses) 
                {                  
                    bonusesIdToRemove.Add(bonusInfo._id);
                }

                List<Bonus> bonuses = _currentLevelContainer.GetPartsOfType<Bonus>();
                foreach (Bonus bonusOnMap in bonuses)
                {
                    if (bonusesIdToRemove.Contains(bonusOnMap.GetID()))
                    {
                        _currentLevelContainer.DeletePart(bonusOnMap.gameObject);
                        Destroy(bonusOnMap.gameObject);
                    }
                }
            }

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
            List<Checkpoint> checkpoints = _currentLevelContainer.GetPartsOfType<Checkpoint>();
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

            _pathRenderer.DrawPath(_currentLevelContainer.GetCorrectPath(), _timeToDrawCorrectPath);
            List<Teleport> teleports = _currentLevelContainer.GetPartsOfType<Teleport>();
            foreach (var item in teleports)
            {
                List<Vector3> path = new()
                {
                    item.transform.position,
                    item.GetTeleportPosition()
                };
                item.gameObject.GetComponent<PathRenderer>().DrawPath(path, _timeToDrawTeleportPath);
            }
        }

        private void StopShowPath()
        {
            _pathRenderer.RemoveLine(_timeToRemovePath);

            List<Teleport> teleports = _currentLevelContainer.GetPartsOfType<Teleport>();
            foreach (var item in teleports)
            {
                item.gameObject.GetComponent<PathRenderer>().RemoveLine(_timeToRemovePath);
            }
        }

        private IEnumerator PlayLevelIntro()
        {

            var finishPoints = _currentLevelContainer.GetPartsOfType<FinishPoint>();
            foreach (FinishPoint finishPoint in finishPoints)
            {
                finishPoint.SetOpen(false);
            }

            //Pause player, timer, and disable fog
            _mainCharacter.SetActive(false);
            Timer.Instance.SetTimerActive(false);
            FogController.Instance.SetFogVisibile(false);
            
            StartShowPath();
            while (_pathRenderer.IsDrawing())
            {               
                yield return null;
            }

            foreach (FinishPoint finishPoint in finishPoints)
            {
                finishPoint.SetOpen(true);
            }

            float timer = 0;
            while (timer < _timeToShowCorrectPath)
            {
                timer += Time.unscaledDeltaTime;
                yield return null;
            }
            StopShowPath();            

            //Fade in fog
            FogController.Instance.SetFogVisibile(true);
            FogController.Instance.FadeInToAllTargets(_fadeInFogTime);

            //Wait until it has faded in completely and enable player movement
            timer = 0;
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
