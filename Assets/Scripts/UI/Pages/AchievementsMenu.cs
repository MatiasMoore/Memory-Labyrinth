using UnityEngine;

public class AchievementsMenu: MonoBehaviour
{
    public void OnClickBack()
    {
        MenuManager.ClosePage(MenuManager.Page.ACHIEVEMENTS);
        MenuManager.OpenPage(MenuManager.Page.MAIN);

        MenuManager.FireButtonClickAction();
    }
}
