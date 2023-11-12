using UnityEngine;

public class AchievementsMenu: MonoBehaviour
{
    public void onClickBack()
    {
        MenuManager.ClosePage(MenuManager.Page.ACHIEVEMENTS);
        MenuManager.OpenPage(MenuManager.Page.MAIN);

        MenuManager.FireButtonClickAction();
    }
}
