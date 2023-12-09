using MemoryLabyrinth.Level.Logic;
using MemoryLabyrinth.Resources;
using UnityEngine;
using UnityEngine.Events;

public class PausePanelExitButton : Button
{
    public override event UnityAction _buttonClick;

    public override void FireButtonClickAction()
    {
        _buttonClick?.Invoke();
    }

    public override void OnClick()
    {
        // Main logic
        Time.timeScale = 1f;
        LevelManager.Instance.SaveUncompleteLevel();
        ResourceManager.LoadScene(ResourceManager.AvailableScene.MainMenu);

        // Fire events
        FireButtonClickSoundAction();
        FireButtonClickAction();
    }
}
