using MemoryLabyrinth.Achievements;
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
            _levelModel.Init(_mainCharacter, Timer.Instance);
            LevelBuilder.SetupListeners(_levelModel);
            _levelManager.Init(_mainCharacter.gameObject, _levelModel, _hudController);
            _cameraScript.Init();

            var audioController = FindObjectOfType<AudioController>();
            if (audioController != null)
                audioController.SetupListeners(_levelModel, _mainCharacter);

            SetupAchievementTrackers();
        }

        private void SetupAchievementTrackers()
        {
            Achievement achievement = new Achievement(AchievementsStorage.Instance.GetAchievementStruct(Achievement.AchievmentName.collectAllBonusses));
            AchievementTracker tracker = new CollectAllBonussesTracker(achievement, _levelModel, _levelManager);
            _achievementTrackers.Add(tracker);

            achievement = new Achievement(AchievementsStorage.Instance.GetAchievementStruct(Achievement.AchievmentName.finishLevelIn10Seconds));
            tracker = new FinishLevelInSecondsTracker(achievement, _levelModel);
            _achievementTrackers.Add(tracker);

            achievement = new Achievement(AchievementsStorage.Instance.GetAchievementStruct(Achievement.AchievmentName.finishLevelIn20Seconds));
            tracker = new FinishLevelInSecondsTracker(achievement, _levelModel);
            _achievementTrackers.Add(tracker);
        }
    }
}

