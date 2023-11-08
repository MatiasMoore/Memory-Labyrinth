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
        SceneManager.activeSceneChanged += SceneChanged;
    }

    public void SetupListeners()
    {
        var mainCharacter = FindObjectOfType<MainCharacter>();
        if (mainCharacter != null) 
        {
            mainCharacter.AddOnBonusAction(PlayBonusPickupSound);
            mainCharacter.AddOnCheckpointAction(PlayCheckpointActivatedSound);
            mainCharacter.AddOnDamageAction(PlayPlayerDamageSound);
            mainCharacter.AddOnDeathAction(PlayPlayerDeathSound);
        }

        var levelModel = FindObjectOfType<LevelModel>();
        if (levelModel != null) 
        {
            levelModel.AddOnFinishAction(PlayLevelFinishedSound);
            levelModel.AddOnLoseAction(PlayLevelFailedSound);
        }
    }

    private void SceneChanged(Scene prev, Scene current)
    {
        PlayMusicForScene(ResourceManager.GetCurrentScene());
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
