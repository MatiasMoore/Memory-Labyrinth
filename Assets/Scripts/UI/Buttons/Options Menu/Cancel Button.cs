using UnityEngine.Events;

public class CancelButton : Button
{
    public override event UnityAction _buttonClick;

    public override void FireButtonClickAction()
    {
        _buttonClick?.Invoke();
    }

    public override void OnClick()
    {
        // Main logic
        MenuManager.ClosePage(MenuManager.Page.OPTIONS);
        MenuManager.OpenPage(MenuManager.Page.MAIN);

        // Fire events
        FireButtonClickSoundAction();
        FireButtonClickAction();
    }
}
