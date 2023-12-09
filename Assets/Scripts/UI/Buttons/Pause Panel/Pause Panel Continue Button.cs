using UnityEngine;
using UnityEngine.Events;

public class PausePanelContinueButton : Button
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
        Timer.Instance.SetTimerActive(true);

        // Fire events
        FireButtonClickSoundAction();
        FireButtonClickAction();
    }
}
