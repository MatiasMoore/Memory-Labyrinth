using UnityEngine;

public class AchievementsMenu: MonoBehaviour
{
    public void onClickBack()
    {
        MenuManager.OpenPage(Page.MAIN, gameObject);
    }
}
