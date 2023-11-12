using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public static AudioController Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null) return;

        Instance = this;
    }

    public void Init()
    {
        ResourceManager.activeSceneChanged += SceneChanged;   
    }

    public void SetupListeners()
    {
        var mainCharacter = FindObjectOfType<MainCharacter>();
        if (mainCharacter != null) 
        {
            mainCharacter._onBonusEvent += PlayBonusPickupSound;
            mainCharacter._onCheckpointEvent += PlayCheckpointActivatedSound;
            mainCharacter._onDamageEvent += PlayPlayerDamageSound;
            mainCharacter._onDeathEvent += PlayPlayerDeathSound;
        }

        var levelModel = FindObjectOfType<LevelModel>();
        if (levelModel != null) 
        {
            levelModel._onLevelWin += PlayLevelFinishedSound;
            levelModel._onLevelLose += PlayLevelFailedSound;
        }
    }

    private void SceneChanged(ResourceManager.AvailableScene prev, ResourceManager.AvailableScene current)
    {
        PlayMusicForScene(current);
    }

    private void PlayMusicForScene(ResourceManager.AvailableScene scene)
    {
        if (scene == ResourceManager.AvailableScene.MainMenu)
            MusicManager.Instance.PlayMusicWithBlending(ResourceManager.Music.MenuMusic);
        else if (scene == ResourceManager.AvailableScene.GameField)
            MusicManager.Instance.PlayMusicWithBlending(ResourceManager.Music.LevelMusic);
    }

    private void PlayBonusPickupSound(int bonusValue)
    {
        SoundEffectsManager.Instance.PlaySoundEffect(ResourceManager.SoundEffect.BonusPickup);
    }

    private void PlayCheckpointActivatedSound(Checkpoint activatedCheckpoint)
    {
        SoundEffectsManager.Instance.PlaySoundEffect(ResourceManager.SoundEffect.CheckpointActivated);
    }

    private void PlayPlayerDamageSound()
    {
        SoundEffectsManager.Instance.PlaySoundEffect(ResourceManager.SoundEffect.PlayerHit);
    }

    private void PlayPlayerDeathSound()
    {
        SoundEffectsManager.Instance.PlaySoundEffect(ResourceManager.SoundEffect.PlayerHit);
    }

    private void PlayLevelFinishedSound()
    {
        SoundEffectsManager.Instance.PlaySoundEffect(ResourceManager.SoundEffect.LevelFinished);
    }

    private void PlayLevelFailedSound()
    {
        SoundEffectsManager.Instance.PlaySoundEffect(ResourceManager.SoundEffect.LevelFailed);
    }
}
