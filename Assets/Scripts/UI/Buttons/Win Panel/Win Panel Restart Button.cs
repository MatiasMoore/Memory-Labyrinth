using MemoryLabyrinth.Level.Logic;
using UnityEngine.Events;

public class WinPanelRestartButton : Button
{
    public override event UnityAction _buttonClick;

    public override void FireButtonClickAction()
    {
        _buttonClick?.Invoke();
    }

    public override void OnClick()
    {
        // Main logic
        MenuManager.ClosePage(MenuManager.Page.WIN);
        LevelManager.Instance.StartCurrentLevelFromSpawn();

        // Fire events
        FireButtonClickAction();
    }
}
