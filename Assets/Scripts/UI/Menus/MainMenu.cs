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
        MenuManager.OpenMenu(Menu.OPTIONS, gameObject);
    }

    public void onClickAchievements()
    {
        MenuManager.OpenMenu(Menu.ACHIEVEMENTS, gameObject);
    }
}
