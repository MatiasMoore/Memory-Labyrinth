using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    private static bool _isPausedGame = false;

    public static void SetPausedGame(bool flag)
    {
        _isPausedGame = flag;
    }

    public void OnClickResume()
    {
        Time.timeScale = 1f;
        MenuManager.ClosePage(MenuManager.Page.PAUSE);
        SetPausedGame(false);
        Timer.Instance.SetTimerStatus(true);

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
        MenuManager.ClosePage(MenuManager.Page.PAUSE);
        LevelManager.Instance.StartLevel();
        SetPausedGame(false);

        LevelManager.FireLevelLoadAction();
        MenuManager.FireButtonClickAction();
    }
}
