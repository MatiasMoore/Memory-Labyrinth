using UnityEngine;
using UnityEngine.Events;

public class PauseGameButton : Button
{
    public override event UnityAction _buttonClick;

    public override void FireButtonClickAction()
    {
        _buttonClick?.Invoke();
    }

    public override void OnClick()
    {
        Time.timeScale = 0f;
        MenuManager.OpenPage(MenuManager.Page.PAUSE);
        GamePauseStatus.SetPausedGame(true);
        Timer.Instance.SetTimerActive(false);

        FireButtonClickAction();
    }
}
