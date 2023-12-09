using MemoryLabyrinth.Audio;
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
            DontDestroyOnLoad(_saveLoadManager.gameObject);

            _saveLoadManager.LoadGame();

            ResourceManager.LoadScene(ResourceManager.AvailableScene.MainMenu);
        }
    }
}