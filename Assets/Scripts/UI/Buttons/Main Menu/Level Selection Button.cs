using UnityEngine.Events;

public class LevelSelectionButton : Button
{
    public override event UnityAction _buttonClick;

    public override void FireButtonClickAction()
    {
        _buttonClick?.Invoke();
    }

    public override void OnClick()
    {
        // Main logic
        MenuManager.ClosePage(MenuManager.Page.MAIN);
        MenuManager.OpenPage(MenuManager.Page.LEVEL_SELECTION);

        // Fire events
        FireButtonClickSoundAction();
        FireButtonClickAction();
    }
}
