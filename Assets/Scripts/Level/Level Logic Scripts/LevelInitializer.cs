using MemoryLabyrinth.Audio;
using MemoryLabyrinth.Cam;
using MemoryLabyrinth.Controls;
using MemoryLabyrinth.Fog;
using MemoryLabyrinth.Player;
using MemoryLabyrinth.SaveLoad;
using MemoryLabyrinth.UI.HUD;
using UnityEngine;

namespace MemoryLabyrinth.Level.Logic
{
    public class LevelInitializer : MonoBehaviour
    {
        [SerializeField]
        private TouchControls _touchControls;

        [SerializeField]
        private MainCharacter _mainCharacter;

        [SerializeField]
        private Timer _timer;

        [SerializeField]
        private FogController _fogController;

        [SerializeField]
        private LevelModel _levelModel;

        [SerializeField]
        private LevelManager _levelManager;

        [SerializeField]
        private CameraScript _cameraScript;

        [SerializeField]
        private HUDController _hudController;


        void Start()
        {
            _touchControls.Init();
            _mainCharacter.Init();
            _timer.Init();
            _fogController.Init();
            _levelModel.Init(_mainCharacter);
            CurrentLevel.SetupListeners(_levelModel);
            _levelManager.Init(_mainCharacter.gameObject, _levelModel, _hudController);
            _cameraScript.Init();

            var audioController = FindObjectOfType<AudioController>();
            if (audioController != null)
                audioController.SetupListeners(_levelModel, _mainCharacter);
        }
    }
}

