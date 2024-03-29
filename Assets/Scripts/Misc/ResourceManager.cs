using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using System;
using System.Linq;

namespace MemoryLabyrinth.Resources
{
    public static class ResourceManager
    {
        public static event UnityAction<AvailableScene, AvailableScene> activeSceneChanged;

        public static AudioClip GetAudioClip(SoundEffect soundEffect)
        {
            if (!_soundEffectToPath.ContainsKey(soundEffect))
                throw new System.Exception("Sound effect " + soundEffect.ToString() + " doesn't have audio clip");

            return UnityEngine.Resources.Load<AudioClip>(_soundEffectToPath[soundEffect]);
        }

        public static AudioClip GetAudioClip(Music music)
        {
            if (!_musicToPath.ContainsKey(music))
                throw new System.Exception("Music doesn't have audio clip");

            return UnityEngine.Resources.Load<AudioClip>(_musicToPath[music]);
        }

        //Sound effects
        public enum SoundEffect
        {
            MenuClick,
            LevelStarted,
            LevelFinished,
            LevelFailed,
            PlayerHit,
            BonusPickup,
            CheckpointActivated,
            TeleportUsed,
            NewPathPoint
        }

        const string _audioFilesDirectoryPath = "Audio/SoundEffects/";

        private static Dictionary<SoundEffect, string> _soundEffectToPath = new Dictionary<SoundEffect, string>
    {
        { SoundEffect.PlayerHit, _audioFilesDirectoryPath + "PlayerHit" },
        { SoundEffect.BonusPickup, _audioFilesDirectoryPath + "BonusPickup" },
        { SoundEffect.CheckpointActivated, _audioFilesDirectoryPath + "CheckpointActivated" },
        { SoundEffect.TeleportUsed, _audioFilesDirectoryPath + "TeleportUsed" },
        { SoundEffect.LevelFinished, _audioFilesDirectoryPath + "LevelFinished" },
        { SoundEffect.LevelFailed, _audioFilesDirectoryPath + "LevelFailed" },
        { SoundEffect.LevelStarted, _audioFilesDirectoryPath + "LevelStarted" },
        { SoundEffect.NewPathPoint, _audioFilesDirectoryPath + "NewPathPoint" },
        { SoundEffect.MenuClick, _audioFilesDirectoryPath + "MenuClick" },
    };

        //Music
        public enum Music
        {
            MenuMusic,
            LevelMusic
        }

        const string _musicFilesDirectoryPath = "Audio/Music/";

        private static Dictionary<Music, string> _musicToPath = new Dictionary<Music, string>
    {
        { Music.MenuMusic, _musicFilesDirectoryPath + "MenuMusic" },
        { Music.LevelMusic, _musicFilesDirectoryPath + "LevelMusic" }
    };

        public enum AvailableScene
        {
            Bootstrap = 0,
            MainMenu = 1,
            GameField = 2,
            LevelEditor = 3
        }

        public static void LoadScene(AvailableScene sceneToLoad)
        {
            AvailableScene currentScene = GetCurrentScene();
            int sceneBuildIndex = (int)sceneToLoad;
            SceneManager.LoadScene(sceneBuildIndex);

            activeSceneChanged?.Invoke(currentScene, sceneToLoad);
        }

        public static AvailableScene GetCurrentScene()
        {
            return (AvailableScene)SceneManager.GetActiveScene().buildIndex;
        }

        public enum Level
        {
            Level1,
            Level2
        }

        private static string _pathToLevels = "Levels/";

        private static Dictionary<Level, string> _levels = new Dictionary<Level, string>()
    {
        {Level.Level1, _pathToLevels + "Level1"},
        {Level.Level2, _pathToLevels + "Level2"}
    };

        public static GameObject GetLevelObject(Level levelToLoad)
        {
            if (_levels.ContainsKey(levelToLoad))
            {
                return UnityEngine.Resources.Load<GameObject>(_levels[levelToLoad]);
            }
            else
            {
                Debug.LogError($"Level {levelToLoad} not found");
                return null;
            }
        }

        public static int GetLastLevelIndex()
        {
            return Enum.GetValues(typeof(ResourceManager.Level)).Cast<int>().Max();
        }

    }
}