using UnityEngine;

public class AchievementsMenu: MonoBehaviour
{
    public void onClickBack()
    {
        MenuManager.OpenPage(MenuManager.Page.MAIN, gameObject);
    }
}
