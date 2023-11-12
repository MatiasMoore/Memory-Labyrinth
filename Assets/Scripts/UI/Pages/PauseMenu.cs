using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private static bool _isPausedGame = false;

    public static void setPausedGame(bool flag)
    {
        _isPausedGame = flag;
    }

    public void OnClickResume()
    {
        MenuManager.ClosePage(MenuManager.Page.PAUSE);
        Time.timeScale = 1f;
        setPausedGame(false);

        MenuManager.FireButtonClickAction();
    }

    public void OnClickToMainMenu()
    {
        Time.timeScale = 1f;
        ResourceManager.LoadScene(ResourceManager.AvailableScene.MainMenu);

        MenuManager.FireButtonClickAction();
    }

    public void OnClickRestart()
    {
        Time.timeScale = 1f;
        ResourceManager.LoadScene(ResourceManager.AvailableScene.GameField);
        // Надо чтоб подгружался префаб нужного уровня

        MenuManager.FireButtonClickAction();
    }
}
