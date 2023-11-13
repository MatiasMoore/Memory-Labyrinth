using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelModel : MonoBehaviour
{
    private MainCharacter _mainCharacter;

    [SerializeField]
    private Checkpoint _currentCheckpoint;

    public event UnityAction _onLevelLose;

    public event UnityAction _onLevelWin;

    public event UnityAction _onPlayerGetBonus;

    [SerializeField]
    int _bonusMoneyAmount;

    private bool _isActive;

    public void Init(MainCharacter mainCharacter)
    {
        _mainCharacter = mainCharacter;
        _mainCharacter._onDamageEvent += onPlayerDamage;
        _mainCharacter._onDeathEvent += onPlayerDeath;
        _mainCharacter._onBonusEvent += onPlayerGetBonus;
        _mainCharacter._onCheckpointEvent += onPlayerGetCheckpoint;
        _mainCharacter._onFinishEvent += onPlayerWin;  
    }

    public void StartNewLevel()
    {
        _currentCheckpoint = FindObjectOfType<StartPoint>();
        _bonusMoneyAmount = 0;
    }

    public void LoadLevel(int bonusAmount, Checkpoint checkpoint)
    {
        _bonusMoneyAmount = bonusAmount;
        _currentCheckpoint = checkpoint;
    }

    public void SetActive(bool isActive)
    {
        _isActive = isActive;
    }

    public void onPlayerDeath()
    {
        Debug.Log("Player died");
        _onLevelLose?.Invoke();
    }

    public void onPlayerWin()
    {
        Debug.Log("Player win");
        _onLevelWin?.Invoke();
    }

    public void onPlayerDamage()
    {
        Debug.Log($"Player damaged");
        _mainCharacter.TeleportTo(_currentCheckpoint.transform.position);
    }

    public void onPlayerGetBonus(int value)
    {
        _bonusMoneyAmount += value;
        _onPlayerGetBonus?.Invoke();
        Debug.Log($"Player get bonus, now he has {_bonusMoneyAmount} money");
    }

    public void onPlayerGetCheckpoint(Checkpoint checkpoint)
    {
        Debug.Log($"Player get checkpoint {checkpoint}");
        if (_currentCheckpoint == null)
        {
            _currentCheckpoint = checkpoint;
        }
        else if (checkpoint.GetQueue() > _currentCheckpoint.GetQueue())
        {
            _currentCheckpoint = checkpoint;
        }
    }

    public int GetBonusAmount()
    {
        return _bonusMoneyAmount;
    }
    public void SetBonusAmount(int bonusAmount)
    {
        _bonusMoneyAmount = bonusAmount;
    }
    public Checkpoint GetCheckPoint()
    {
        return _currentCheckpoint;
    }

    public void SetCheckPoint(Checkpoint checkpoint)
    {
        _currentCheckpoint = checkpoint;
    }

}
