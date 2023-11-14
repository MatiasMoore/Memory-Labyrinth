using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void OnClickPlay()
    {
        MenuManager.ClosePage(MenuManager.Page.MAIN);
        MenuManager.OpenPage(MenuManager.Page.LEVEL_SELECTION);

        MenuManager.FireButtonClickAction();
    }

    public void OnClickOptions()
    {
        MenuManager.ClosePage(MenuManager.Page.MAIN);
        MenuManager.OpenPage(MenuManager.Page.OPTIONS);

        MenuManager.FireButtonClickAction();
    }

    public void OnClickAchievements()
    {
        MenuManager.ClosePage(MenuManager.Page.MAIN);
        MenuManager.OpenPage(MenuManager.Page.ACHIEVEMENTS);

        MenuManager.FireButtonClickAction();
    }
}
