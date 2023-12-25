using MemoryLabyrinth.Cam;
using MemoryLabyrinth.Controls;
using MemoryLabyrinth.UI.ButtonsLib;
using TMPro;
using UnityEngine;

namespace MemoryLabyrinth.Level.Editor
{
    public class EditorInitializer : MonoBehaviour
    {
        [SerializeField]
        private TouchControls _touchControls;

        [SerializeField]
        private LevelEditor _levelEditor;

        [SerializeField]
        private CameraPanControl _cameraPanControl;
        private Button _wallButton;

        [SerializeField]
        private Button _startPointButton;

        [SerializeField]
        private Button _trapButton;

        [SerializeField]
        private Button _teleportButton;

        [SerializeField]
        private Button _finishPointButton;

        [SerializeField]
        private Button _checkpointButton;

        [SerializeField]
        private Button _bonusButton;

        [SerializeField]
        private Button _pathButton;

        [SerializeField]
        private Button _correctPathButton;

        [SerializeField]
        private Button _destroyButton;

        [SerializeField]
        private Button _destroyCorrectPathButton;

        [SerializeField]
        private Button _saveButton;

        [SerializeField]
        private Button _loadButton;

        [SerializeField]
        private TextMeshProUGUI _levelName;

        void Start()
        {
            _touchControls.Init();
            _cameraPanControl.Init(_touchControls);
            _levelEditor.Init(_touchControls, _cameraPanControl);

            if (_wallButton != null)
                _wallButton._buttonClick += () => _levelEditor.StartCreatingLevelPart(LevelPartType.Wall);
            else
                Debug.LogWarning($"LevelEditorInitializer: Wall button is not set!");
            
            if (_startPointButton != null)
                _startPointButton._buttonClick += () => _levelEditor.StartCreatingLevelPart(LevelPartType.Startpoint);
            else
                Debug.LogWarning($"LevelEditorInitializer: StartPoint button is not set!");

            if (_trapButton != null)
                _trapButton._buttonClick += () => _levelEditor.StartCreatingLevelPart(LevelPartType.Trap);
            else
                Debug.LogWarning($"LevelEditorInitializer: Trap button is not set!");

            if (_teleportButton != null)
                _teleportButton._buttonClick += () => _levelEditor.StartCreatingLevelPart(LevelPartType.Teleport);
            else
                Debug.LogWarning($"LevelEditorInitializer: Teleport button is not set!");

            if (_finishPointButton != null)
                _finishPointButton._buttonClick += () => _levelEditor.StartCreatingLevelPart(LevelPartType.Finishpoint);
            else
                Debug.LogWarning($"LevelEditorInitializer: FinishPoint button is not set!");

            if (_checkpointButton != null)
                _checkpointButton._buttonClick += () => _levelEditor.StartCreatingLevelPart(LevelPartType.Checkpoint);
            else
                Debug.LogWarning($"LevelEditorInitializer: Checkpoint button is not set!");

            if (_bonusButton != null)
                _bonusButton._buttonClick += () => _levelEditor.StartCreatingLevelPart(LevelPartType.Bonus);
            else
                Debug.LogWarning($"LevelEditorInitializer: Bonus button is not set!");

            if (_pathButton != null)
                _pathButton._buttonClick += () => _levelEditor.StartCreatingLevelPart(LevelPartType.Path);
            else
                Debug.LogWarning($"LevelEditorInitializer: Path button is not set!");

            if (_correctPathButton != null)
                _correctPathButton._buttonClick += () => _levelEditor.StartCreatingLevelPart(LevelPartType.CorrectPath);
            else
                Debug.LogWarning($"LevelEditorInitializer: CorrectPath button is not set!");

            if (_destroyButton != null)
                _destroyButton._buttonClick += _levelEditor.StartDestroyParts;
            else
                Debug.LogWarning($"LevelEditorInitializer: Destroy button is not set!");

            if (_destroyCorrectPathButton != null)
                _destroyCorrectPathButton._buttonClick += _levelEditor.StartDestroyCorrectPath;
            else
                Debug.LogWarning($"LevelEditorInitializer: DestroyCorrectPath button is not set!");



        }

    }
}

