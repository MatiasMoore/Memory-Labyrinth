using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowLevelInfo : MonoBehaviour
{
    [SerializeField] public List<ResourceManager.Level> _level;
    [SerializeField] public List<int> _checkpointId;
    [SerializeField] public List<float> _time;
    [SerializeField] public List<List<int>> _collectedBonusesId;
    [SerializeField] public List<int> _livesAmount;
    [SerializeField] public List<bool> _isCompleted;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("ShowLevelInfo: Start()");
        foreach (LevelData level in LevelProgressStorage.Instance.currentLevels)
        {
            _level.Add(level._level);
            _checkpointId.Add(level._checkpointId);
            _time.Add(level._time);
            _collectedBonusesId.Add(level._collectedBonusesId);
            _livesAmount.Add(level._livesAmount);
            _isCompleted.Add(level._isCompleted);
        }
    }

    [ContextMenu("Update In Inspector")]
    public void UpdateInInspector()
    {
        Debug.Log("ShowLevelInfo: UpdateInInspector()");
        foreach (LevelData level in LevelProgressStorage.Instance.currentLevels)
        {
            _level.Add(level._level);
            _checkpointId.Add(level._checkpointId);
            _time.Add(level._time);
            _collectedBonusesId.Add(level._collectedBonusesId);
            _livesAmount.Add(level._livesAmount);
            _isCompleted.Add(level._isCompleted);
        }
    }
}
