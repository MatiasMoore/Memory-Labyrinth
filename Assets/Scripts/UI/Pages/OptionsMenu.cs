using UnityEngine;
using UnityEngine.Events;

public class OptionsMenu : MonoBehaviour
{
    public void OnClickAccept()
    {
        MenuManager.ClosePage(MenuManager.Page.OPTIONS);
        MenuManager.OpenPage(MenuManager.Page.MAIN);

        // TODO: save audio and music settings

        MenuManager.FireButtonClickAction();
    }

    public void OnClickDecline()
    {
        MenuManager.ClosePage(MenuManager.Page.OPTIONS);
        MenuManager.OpenPage(MenuManager.Page.MAIN);

        MenuManager.FireButtonClickAction();
    }
}
