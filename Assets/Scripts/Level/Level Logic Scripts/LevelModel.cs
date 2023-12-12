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

        public event UnityAction<LevelData> _onPlayerGetCheckpoint;

        public event UnityAction _onPlayerGetBonus;

        public event UnityAction<int> _onBonusAmountChange;

        [SerializeField]
        int _bonusMoneyAmount;

        private List<BonusInfo> _collectedBonusesIDBeforeCheckpoint = new List<BonusInfo>();

        private List<BonusInfo> _collectedBonusesBuffer = new List<BonusInfo>();

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

        public LevelData GetLevelData()
        {
            LevelData levelData = new LevelData
            {
                _level = CurrentLevel.GetCurrentLevelData()._level,
                _livesAmount = _mainCharacter.GetHealth(),
                _checkpointId = _isLevelFinish ? 0 : _currentCheckpoint.GetQueue(),
                _time = Timer.Instance.GetElapsedTime(),
                _isCompleted = _isLevelFinish,
                _collectedBonuses = GetCollectedBonusesIDBeforeCheckpoint()
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
            BonusInfo bonusInfo = new BonusInfo{_id = bonus.GetID(), _value = bonus.GetValue() };
            _collectedBonusesBuffer.Add(bonusInfo);
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

            _onPlayerGetCheckpoint?.Invoke(GetLevelData());

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

        public void SetCollectedBonusesIDBeforeCheckPoint(List<BonusInfo> collectedBonusesID)
        {
            _collectedBonusesIDBeforeCheckpoint = new List<BonusInfo>(collectedBonusesID);

        }

        public void SetCollectedBonusesIDBuffer(List<BonusInfo> collectedBonusesID)
        {
            _collectedBonusesBuffer = new List<BonusInfo>(collectedBonusesID);
        }

        public List<BonusInfo> GetCollectedBonusesIDBeforeCheckpoint()
        {
            return _collectedBonusesIDBeforeCheckpoint;
        }

    }
}
