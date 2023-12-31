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

    public event UnityAction<int> _onBonusAmountChange;

    [SerializeField]
    int _bonusMoneyAmount;

    private bool _isActive;

    private List<int> _collectedBonusesIDBeforeCheckpoint = new List<int>();

    private List<int> _collectedBonusesBuffer = new List<int>();

    public void Init(MainCharacter mainCharacter)
    {
        _mainCharacter = mainCharacter;
        _mainCharacter._onDamageEvent += onPlayerDamage;
        _mainCharacter._onDeathEvent += onPlayerDeath;
        _mainCharacter._onBonusEvent += onPlayerGetBonus;
        _mainCharacter._onCheckpointEvent += onPlayerGetCheckpoint;
        _mainCharacter._onFinishEvent += onPlayerWin;  
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
        _mainCharacter.SetPosition2d(_currentCheckpoint.transform.position);
    }

    public void onPlayerGetBonus(Bonus bonus)
    {
        SetBonusAmount(_bonusMoneyAmount + bonus.GetValue());
        _collectedBonusesBuffer.Add(bonus.GetID());
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

        _collectedBonusesIDBeforeCheckpoint.AddRange(_collectedBonusesBuffer);
        _collectedBonusesBuffer.Clear();

    }

    public int GetBonusAmount()
    {
        return _bonusMoneyAmount;
    }

    public void SetBonusAmount(int bonusAmount)
    {
        _bonusMoneyAmount = bonusAmount;
        _onBonusAmountChange?.Invoke(bonusAmount);
    }

    public Checkpoint GetCurrentCheckPoint()
    {
        return _currentCheckpoint;
    }

    public void SetCurrentCheckPoint(Checkpoint checkpoint)
    {
        _currentCheckpoint = checkpoint;
    }

    public void SetCollectedBonusesIDBeforeCheckPoint(List<int> collectedBonusesID)
    {
        _collectedBonusesIDBeforeCheckpoint = new List<int>(collectedBonusesID);
        
    }

    public void SetCollectedBonusesIDBuffer(List<int> collectedBonusesID)
    {
        _collectedBonusesBuffer = new List<int>(collectedBonusesID);
    }

    public List<int> GetCollectedBonusesIDBeforeCheckpoint()
    {
        return _collectedBonusesIDBeforeCheckpoint;
    }

}
