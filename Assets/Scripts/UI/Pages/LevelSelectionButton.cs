using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectionButton : MonoBehaviour
{
    [SerializeField]
    public ResourceManager.Level _level;

    public void OnClick()
    {
        ResourceManager.LoadScene(ResourceManager.AvailableScene.GameField);
        CurrentLevel.Load(_level);
        MenuManager.FireButtonClickAction();
    }
}
