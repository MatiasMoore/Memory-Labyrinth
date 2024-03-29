using MemoryLabyrinth.Audio;
using MemoryLabyrinth.Level.Editor;
using MemoryLabyrinth.Level.Logic;
using MemoryLabyrinth.Resources;
using MemoryLabyrinth.SaveLoad;
using UnityEngine;

namespace MemoryLabyrinth.Bootstrap
{
    public class Game : MonoBehaviour
    {
        [SerializeField]
        private AudioController _audioController;
        [SerializeField]
        private SoundEffectsManager _soundEffectsManager;
        [SerializeField]
        private MusicManager _musicManager;
        [SerializeField]
        private BonusStorage _bonusStorage;
        [SerializeField]
        private LevelProgressStorage _levelProgressStorage;
        [SerializeField]
        private SaveLoadManager _saveLoadManager;
        [SerializeField]
        private SettingsStorage _settingsStorage;
        [SerializeField]
        private MixerControl _mixerControl;
        [SerializeField]
        private AchievementsStorage _achievementsStorage;
        [SerializeField]
        private LevelDataStorage _levelDataStorage;
        [SerializeField]
        private LevelParts _levelPartsDB;
        private void Start()
        {
            
            if (_audioController == null)
                throw new System.Exception("No audio controller is found on startup");
            DontDestroyOnLoad(_audioController.gameObject);

            if (_soundEffectsManager == null)
                throw new System.Exception("No sound effect manager is found on startup");
            DontDestroyOnLoad(_soundEffectsManager.gameObject);

            if (_musicManager == null)
                throw new System.Exception("No music manager is found on startup");
            DontDestroyOnLoad(_musicManager.gameObject);

            if (_levelPartsDB == null)
                throw new System.Exception("No level parts scriptable object is found on startup");
            LevelBuilder.Init(_levelPartsDB);

            if (_bonusStorage == null)
                throw new System.Exception("No BonusStorage is found on startup");
            _bonusStorage.Init();
            DontDestroyOnLoad(_bonusStorage.gameObject);

            if (_levelProgressStorage == null)
                throw new System.Exception("No LevelProgressStorage is found on startup");
            _levelProgressStorage.Init();
            DontDestroyOnLoad(_levelProgressStorage.gameObject);

            if (_saveLoadManager == null)
                throw new System.Exception("No SaveLoadManager is found on startup");
            _saveLoadManager.Init();
            DontDestroyOnLoad(_saveLoadManager.gameObject);

            if (_settingsStorage == null)
                throw new System.Exception("No SettingsStorage is found on startup");
            _settingsStorage.Init();
            DontDestroyOnLoad(_settingsStorage.gameObject);

            if (_achievementsStorage == null)
                throw new System.Exception("No AchievementsStorage is found on startup");
            _achievementsStorage.Init();
            DontDestroyOnLoad(_achievementsStorage.gameObject);

            if (_levelDataStorage == null)
                throw new System.Exception("No LevelDataStorage is found on startup");
            _levelDataStorage.Init();
            DontDestroyOnLoad(_levelDataStorage.gameObject);

            SaveLoadManager.Instance.LoadGame();

            _mixerControl.SetMusicVolume(SettingsStorage.Instance.GetSettingsData(AudioSetting.Music)._volume);
            _mixerControl.SetSfxVolume(SettingsStorage.Instance.GetSettingsData(AudioSetting.SFX)._volume);

            ResourceManager.LoadScene(ResourceManager.AvailableScene.MainMenu);
        }
    }
}