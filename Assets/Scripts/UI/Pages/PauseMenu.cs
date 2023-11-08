using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    /*
     * 1) Надо все методы (по хорошему) связать с Scene Manager
     * 2) Надо что-то придумать с загрузкой этого же самого уровня при нажатии на кнопку рестарта (сейчас загружается только тестовый уровень (по ID))
    */

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
    }

    public void OnClickToMainMenu()
    {
        // Здесь должен быть Scene Manager (при этом браться ID главного меню)
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    }

    public void OnClickRestart()
    {
        // Здесь должен быть Scene Manager (при этом браться ID текущего уровня)
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}
