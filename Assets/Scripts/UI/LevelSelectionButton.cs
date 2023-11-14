using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectionButton : MonoBehaviour
{
    [SerializeField]
    public ResourceManager.Level _level;

    [SerializeField]
    public LevelSelectionMenu _levelSelector;

    public void OnClick()
    {
        _levelSelector.LoadLevel(_level);
        CurrentLevel.Load(_level);
        MenuManager.FireButtonClickAction();
    }
}
