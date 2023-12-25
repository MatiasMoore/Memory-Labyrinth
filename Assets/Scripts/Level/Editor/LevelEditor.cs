using MemoryLabyrinth.Controls;
using MemoryLabyrinth.SaveLoad;
using UnityEngine;
using MemoryLabyrinth.Cam;

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
            _interactor.InteractAtPos(worldFromCell);
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
                    break;
                case LevelPartType.Checkpoint:
                    break;
                case LevelPartType.Startpoint:
                    _interactor = new StartPointCreator(_container, config);
                    break;
                case LevelPartType.Finishpoint:
                    _interactor = new FinishPointCreator(_container, config);
                    break;
                case LevelPartType.Bonus:
                    break;
                case LevelPartType.Teleport:
                    break;
                default:
                    break;
            }
        }

        [ContextMenu("Start Destroy")]
        public void StartDestroy()
        {
            _interactor = new DestroyFirstElement(_container);
        }

        [ContextMenu("Get Level Data")]
        public void GetLevelData()
        {
            LevelData levelData = _container.ToLevelData();
            levelData.name = "Test";
            Debug.Log($"{levelData.walls.walls}, {levelData.bonuses.bonuses}");
            LevelDataStorage.Instance.AddLevelInfo(levelData);
            SaveLoadManager.Instance.SaveGame();
        }

    }
}

