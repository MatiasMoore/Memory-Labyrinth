using MemoryLabyrinth.Level.Logic;
using UnityEngine;
using UnityEngine.Events;

public class PausePanelRestartButton : Button
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
        MenuManager.ClosePage(MenuManager.Page.PAUSE);
        LevelManager.Instance.StartCurrentLevelFromSpawn();
        GamePauseStatus.SetPausedGame(false);

        // Fire events
        FireButtonClickAction();
    }
}
