using UnityEngine;

public class AchievementsMenu: MonoBehaviour
{
    public void onClickBack()
    {
        MenuManager.OpenMenu(Menu.MAIN, gameObject);
    }
}
