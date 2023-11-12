using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelModel : MonoBehaviour
{
    [SerializeField]
    private GameObject _player;

    private MainCharacter _mainCharacter;

    [SerializeField]
    private Checkpoint _currentCheckpoint;

    [SerializeField]
    private UnityEvent _onLevelLose;

    [SerializeField]
    private UnityEvent _onLevelWin;

    [SerializeField]
    private UnityEvent _onPlayerGetBonus;

    [SerializeField]
    int _bonusMoneyAmount;

    [SerializeField]
    private GameObject _rightPathBuilder;

    [SerializeField]
    private float _startLevelTime;

    private float _timer;

    private bool _isLevelStarted = false;

    public void Start()
    {
        _mainCharacter = _player.GetComponent<MainCharacter>();

        _mainCharacter._onDamageEvent += onPlayerDamage;
        _mainCharacter._onDeathEvent += onPlayerDeath;
        _mainCharacter._onBonusEvent += onPlayerGetBonus;
        _mainCharacter._onCheckpointEvent += onPlayerGetCheckpoint;
        _mainCharacter._onFinishEvent += onPlayerWin;

        _rightPathBuilder.GetComponent<RightPathBuilder>().ShowRightPath(_startLevelTime * 0.9f);
        _mainCharacter.SetActive(false);
        _player.GetComponent<PathCreator>().SetActive(false);
    }

    public void FixedUpdate()
    {
        _timer += Time.fixedDeltaTime;

        if (_timer > _startLevelTime && _timer != 0)
        {
            StartLevel();
        }

    }

    private void StartLevel()
    {
        _rightPathBuilder.SetActive(false);
        // TODO: Show fog
        _player.GetComponent<PathCreator>().SetActive(true);
        _mainCharacter.SetActive(true);
    }

    public void onPlayerDeath()
    {
        Debug.Log("Player died");
        _onLevelLose.Invoke();
    }

    public void onPlayerWin()
    {
        Debug.Log("Player win");
        _onLevelWin.Invoke();
    }

    public void onPlayerDamage()
    {
        Debug.Log($"Player damaged");
        _mainCharacter.TeleportTo(_currentCheckpoint.transform.position);
    }

    public void onPlayerGetBonus(int value)
    {
        _bonusMoneyAmount += value;
        _onPlayerGetBonus.Invoke();
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

    public void AddOnFinishAction(UnityAction action)
    {
        _onLevelWin.AddListener(action);
    }

    public void AddOnLoseAction(UnityAction action)
    {
        _onLevelLose.AddListener(action);
    }

    public void AddOnBonusAction(UnityAction action)
    {
        _onPlayerGetBonus.AddListener(action);
    }
}
