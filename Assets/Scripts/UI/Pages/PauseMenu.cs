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
        Time.timeScale = 1f;
        MenuManager.ClosePage(MenuManager.Page.PAUSE);
        setPausedGame(false);
        Timer.SetTimerStatus(true);

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
        // ���� ���� ����������� ������ ������� ������

        MenuManager.FireButtonClickAction();
    }
}
