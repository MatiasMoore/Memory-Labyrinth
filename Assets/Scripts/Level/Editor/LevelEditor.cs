using MemoryLabyrinth.Controls;
using MemoryLabyrinth.SaveLoad;
using UnityEngine;
using MemoryLabyrinth.Cam;
using MemoryLabyrinth.UI;
using MemoryLabyrinth.Level.Objects.TeleportLib;
using TMPro;
using MemoryLabyrinth.Level.Objects.StartpointLib;
using MemoryLabyrinth.Level.Objects.FinishLib;
using MemoryLabyrinth.Level.Logic;

namespace MemoryLabyrinth.Level.Editor
{
    public class LevelEditor : MonoBehaviour
    {
        private LevelPartsContainer _container;
        private InteractorPrimitive _interactor;

        [SerializeField]
        private Grid _grid;

        [SerializeField]
        private LevelParts _levelPartsDataBase;

        private CameraPanControl _cameraPanControl;

        [SerializeField]
        private InputField _inputField;


        public void Init(TouchControls touchControls, CameraPanControl cameraPanControl)
        {
            _cameraPanControl = cameraPanControl;

            touchControls.touchDown += OnPlayerTouch;
            touchControls.touchHold += OnPlayerTouch;

            touchControls.touchDown += StartCameraPan;
            touchControls.touchUp += StopCameraPan;

            _container = new LevelPartsContainer();
            _interactor = null;
        }

        private void StartCameraPan()
        {
            if (_interactor != null)
                return;

            _cameraPanControl.StartCameraPan();
        }

        private void StopCameraPan()
        {
            if (_cameraPanControl.IsActive())
            {
                _cameraPanControl.StopCameraPan();
            }
        }

        private void OnPlayerTouch()
        {
            if (_interactor == null)
                return;

            Vector2 pos = TouchControls.Instance.getTouchWorldPosition2d();
            var cellPos = _grid.WorldToCell(pos);
            var worldFromCell = _grid.GetCellCenterWorld(cellPos);
            _interactor = _interactor.InteractAtPos(worldFromCell);
            Debug.Log($"LevelEditor: interacted with {_interactor}");
        }

        [ContextMenu("Start Panning Camera")]
        public void StartPanningCamera()
        {
            _interactor = null;
        }

        [ContextMenu("Start Creating Wall")]
        public void StartCreatingWall()
        {
            StartCreatingLevelPart(LevelPartType.Wall);
        }

        [ContextMenu("Start Creating Path")]
        public void StartCreatingPath()
        {
            StartCreatingLevelPart(LevelPartType.Path);
        }

        [ContextMenu("Start Creating StartPoint")]
        public void StartCreatingStartPoint()
        {
            StartCreatingLevelPart(LevelPartType.Startpoint);
        }

        [ContextMenu("Start Creating FinishPoint")]
        public void StartCreatingFinishPoint()
        {
            StartCreatingLevelPart(LevelPartType.Finishpoint);
        }

        [ContextMenu("Start Creating Trap")]
        public void StartCreatingTrap()
        {
            StartCreatingLevelPart(LevelPartType.Trap);
        }

        [ContextMenu("Start Creating Checkpoint")]
        public void StartCreatingCheckpoint()
        {
            StartCreatingLevelPart(LevelPartType.Checkpoint);
        }

        [ContextMenu("Start Creating Bonus")]
        public void StartCreatingBonus()
        {
            StartCreatingLevelPart(LevelPartType.Bonus);
        }

        [ContextMenu("Start Creating Teleport")]
        public void StartCreatingTeleport()
        {
            StartCreatingLevelPart(LevelPartType.Teleport);
        }

        [ContextMenu("Start Creating Correct Path")]
        public void StartCreatingCorrectPath()
        {
            StartCreatingLevelPart(LevelPartType.CorrectPath);
        }

        public void StartCreatingLevelPart(LevelPartType type)
        {
            LevelPartConfig config = _levelPartsDataBase.GetConfigByType(type);
            switch (type)
            {
                case LevelPartType.Wall:
                    _interactor = new WallCreator(_container, config);
                    break;
                case LevelPartType.Path:
                    _interactor = new PathCreator(_container, config);
                    break;
                case LevelPartType.Trap:

                    _interactor = new TrapCreator(_container, config, _inputField);
                    break;
                case LevelPartType.Checkpoint:

                    _interactor = new CheckpointCreator(_container, config, _inputField);
                    break;
                case LevelPartType.Startpoint:
                    _interactor = new StartPointCreator(_container, config);
                    break;
                case LevelPartType.Finishpoint:
                    _interactor = new FinishPointCreator(_container, config);
                    break;
                case LevelPartType.Bonus:

                    _interactor = new BonusCreator(_container, config, _inputField);
                    break;
                case LevelPartType.Teleport:

                    _interactor = new TeleportCreator(_container, config, _inputField);
                    break;
                case LevelPartType.CorrectPath:
                    _interactor = new CorrectPathCreator(_container, config);
                    break;
                default:
                    break;
            }
        }

        [ContextMenu("Start Destroy Parts")]
        public void StartDestroyParts()
        {
            _interactor = new DestroyFirstElement(_container);
        }

        [ContextMenu("Start Destroy Correct Path")]
        public void StartDestroyCorrectPath()
        {
            _interactor = new CorrectPathDestroyer(_container);
        }

        [ContextMenu("Get Level Data")]
        public LevelData GetLevelData()
        {
            LevelData levelData = _container.ToLevelData();
            //Debug.Log($"{levelData.walls.walls}, {levelData.bonuses.bonuses}");

            return levelData;
        }

        public bool IsSaveLevelCanBeSaved()
        {
            bool isStartPointExist = _container.GetPartsOfType<StartPoint>().Count > 0;
            if (!isStartPointExist)
                Debug.Log($"LevelEditor: no start point!");
            bool isFinishPointExist = _container.GetPartsOfType<FinishPoint>().Count > 0;
            if (!isFinishPointExist)
                Debug.Log($"LevelEditor: no finish point!");
            bool isCorrectPathExist = _container.GetCorrectPath().Count > 0;
            if (!isCorrectPathExist)
                Debug.Log($"LevelEditor: no correct path!");

            return isStartPointExist && isFinishPointExist && isCorrectPathExist;
        }

        public void SaveLevel(string name)
        {
            if (!IsSaveLevelCanBeSaved())
            {
                return;
            }

            LevelData levelData = GetLevelData();
            levelData.name = name;

            LevelDataStorage.Instance.AddLevelInfo(levelData);
            SaveLoadManager.Instance.SaveGame();

        }

        public void ClearLevelMap()
        {
            _container.ClearAll();
        }

        public void LoadLevel(string name)
        {
            if(!LevelDataStorage.Instance.IsLevelAlreadySaved(name))
            {
                Debug.Log($"LevelEditor: level {name} not found!");
                return;
            }
            _container.ClearAll();
            LevelBuilder.Load(name);
            _container = LevelBuilder.BuildCurrentLevelToScene();
        }

    }
}

