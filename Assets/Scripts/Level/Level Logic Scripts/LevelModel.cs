using MemoryLabyrinth.Level.Objects.BonusLib;
using MemoryLabyrinth.Level.Objects.CheckpointLib;
using MemoryLabyrinth.Player;
using MemoryLabyrinth.SaveLoad;
using MemoryLabyrinth.UI.HUD;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace MemoryLabyrinth.Level.Logic
{
    public class LevelModel : MonoBehaviour
    {
        private MainCharacter _mainCharacter;

        [SerializeField]
        private Checkpoint _currentCheckpoint;

        public event UnityAction<LevelData> _onLevelLose;

        public event UnityAction<LevelData> _onLevelWin;

        public event UnityAction _onPlayerGetBonus;

        public event UnityAction<int> _onBonusAmountChange;

        [SerializeField]
        int _bonusMoneyAmount;

        private bool _isActive;

        private List<int> _collectedBonusesIDBeforeCheckpoint = new List<int>();

        private List<int> _collectedBonusesBuffer = new List<int>();

        private bool _isLevelFinish = false;
        
        public void Init(MainCharacter mainCharacter)
        {
            _mainCharacter = mainCharacter;
            _mainCharacter._onDamageEvent += OnPlayerDamage;
            _mainCharacter._onDeathEvent += OnPlayerDeath;
            _mainCharacter._onBonusEvent += OnPlayerGetBonus;
            _mainCharacter._onCheckpointEvent += OnPlayerGetCheckpoint;
            _mainCharacter._onFinishEvent += OnPlayerWin;

            _isLevelFinish = false;
        }

        public void SetActive(bool isActive)
        {
            _isActive = isActive;
        }

        public LevelData GetLevelData()
        {
            LevelData levelData = new LevelData
            {
                _level = CurrentLevel.GetCurrentLevelData()._level,
                _livesAmount = _mainCharacter.GetHealth(),
                _checkpointId = _isLevelFinish ? 0 : _currentCheckpoint.GetQueue(),
                _time = Timer.Instance.GetElapsedTime(),
                _isCompleted = _isLevelFinish,
                _collectedBonusesId = GetCollectedBonusesIDBeforeCheckpoint()
            };
            return levelData;
        }

        public void OnPlayerDeath()
        {
            Debug.Log("Player died");
            _onLevelLose?.Invoke(GetLevelData());
        }

        public void OnPlayerWin()
        {
            Debug.Log("Player win");
            _isLevelFinish = true;
            _onLevelWin?.Invoke(GetLevelData());
        }

        public void OnPlayerDamage()
        {
            Debug.Log($"Player damaged");
            if (_currentCheckpoint == null)
            {
                Debug.Log("Can't find checkpoint");
                return;
            }

            _mainCharacter.SetPosition2d(_currentCheckpoint.transform.position);
        }

        public void OnPlayerGetBonus(Bonus bonus)
        {
            SetBonusAmount(_bonusMoneyAmount + bonus.GetValue());
            _collectedBonusesBuffer.Add(bonus.GetID());
            _onPlayerGetBonus?.Invoke();
            Debug.Log($"Player get bonus, now he has {_bonusMoneyAmount} money");
        }

        public void OnPlayerGetCheckpoint(Checkpoint checkpoint)
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
}
