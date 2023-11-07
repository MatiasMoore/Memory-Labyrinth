using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelModel : MonoBehaviour
{
    [SerializeField]
    MainCharacter _mainCharacter;

    [SerializeField]
    private Checkpoint _currentCheckpoint;

    [SerializeField]
    int _bonusMoneyAmount;

    public void Start()
    {
        _mainCharacter.AddOnDamageAction(onPlayerDamage);
        _mainCharacter.AddOnDeathAction(onPlayerDeath);
        _mainCharacter.AddOnBonusAction(onPlayerGetBonus);
        _mainCharacter.AddOnCheckpointAction(onPlayerGetCheckpoint);
        _mainCharacter.AddOnFinishAction(onPlayerWin);


    }

    public void onPlayerDeath()
    {
        Debug.Log("Player died");
    }

    public void onPlayerWin()
    {
        Debug.Log("Player win");
    }

    public void onPlayerDamage()
    {
        Debug.Log($"Player damaged");
        _mainCharacter.TeleportTo(_currentCheckpoint.transform.position);
    }

    public void onPlayerGetBonus(int value)
    {
        _bonusMoneyAmount += value;
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
}
