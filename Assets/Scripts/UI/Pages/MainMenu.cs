using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void onClickPlay()
    {
        ResourceManager.LoadScene(ResourceManager.AvailableScene.GameField);
        LevelManager._currentLevel = ResourceManager.Level.Level1;
        MenuManager.FireButtonClickAction();
    }

    public void onClickOptions()
    {
        MenuManager.ClosePage(MenuManager.Page.MAIN);
        MenuManager.OpenPage(MenuManager.Page.OPTIONS);

        MenuManager.FireButtonClickAction();
    }

    public void onClickAchievements()
    {
        MenuManager.ClosePage(MenuManager.Page.MAIN);
        MenuManager.OpenPage(MenuManager.Page.ACHIEVEMENTS);

        MenuManager.FireButtonClickAction();
    }
}
