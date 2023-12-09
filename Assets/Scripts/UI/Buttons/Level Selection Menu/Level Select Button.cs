using UnityEngine;
using UnityEngine.Events;
using MemoryLabyrinth.Resources;
using MemoryLabyrinth.SaveLoad;

public class LevelSelectButton : Button
{
    [SerializeField]
    public ResourceManager.Level _level;

    public override event UnityAction _buttonClick;

    public override void FireButtonClickAction()
    {
        _buttonClick?.Invoke();
    }

    public override void OnClick()
    {
        // Main logic
        ResourceManager.LoadScene(ResourceManager.AvailableScene.GameField);
        CurrentLevel.Load(_level);

        // Fire events
        FireButtonClickSoundAction();
        FireButtonClickAction();
    }
}
