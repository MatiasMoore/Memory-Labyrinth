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
        MenuManager.OpenPage(MenuManager.Page.OPTIONS, gameObject);
    }

    public void onClickAchievements()
    {
        MenuManager.OpenPage(MenuManager.Page.ACHIEVEMENTS, gameObject);
    }
}
