using MemoryLabyrinth.Achievemnts;
using MemoryLabyrinth.Audio;
using MemoryLabyrinth.Cam;
using MemoryLabyrinth.Controls;
using MemoryLabyrinth.Fog;
using MemoryLabyrinth.Player;
using MemoryLabyrinth.SaveLoad;
using MemoryLabyrinth.UI.HUD;
using System.Collections.Generic;
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

        private List<AchievementTracker> _achievementTrackers = new List<AchievementTracker>();

        private void Start()
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

            SetupAchievementTrackers();
        }

        private void SetupAchievementTrackers()
        {
            Achievement a = new Achievement(Achievement.AchievmentName.collectAllBonusses, 0, 1);
            AchievementTracker tracker = new CollectAllBonussesTracker(a, _levelModel, _levelManager);
            _achievementTrackers.Add(tracker);

            a = new Achievement(Achievement.AchievmentName.finishLevelIn10Seconds, 0, 10);
            tracker = new FinishLevelInSecondsTracker(a, _levelModel, _timer);
            _achievementTrackers.Add(tracker);

            a = new Achievement(Achievement.AchievmentName.finishLevelIn20Seconds, 0, 20);
            tracker = new FinishLevelInSecondsTracker(a, _levelModel, _timer);
            _achievementTrackers.Add(tracker);
        }
    }
}

