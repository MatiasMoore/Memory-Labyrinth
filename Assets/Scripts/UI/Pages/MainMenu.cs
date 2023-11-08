using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void onClickPlay()
    {
        SceneManager.LoadScene(0);
    }

    public void onClickOptions()
    {
        MenuManager.ClosePage(MenuManager.Page.MAIN);
        MenuManager.OpenPage(MenuManager.Page.OPTIONS);
    }

    public void onClickAchievements()
    {
        MenuManager.ClosePage(MenuManager.Page.MAIN);
        MenuManager.OpenPage(MenuManager.Page.ACHIEVEMENTS);
    }
}
