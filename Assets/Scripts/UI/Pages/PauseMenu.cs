using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    /*
     * 1) Надо все методы (по хорошему) связать с Scene Manager
     * 2) Надо что-то придумать с загрузкой этого же самого уровня при нажатии на кнопку рестарта (сейчас загружается только тестовый уровень (по ID))
    */

    public static bool _isPaused = false;

    public void OnClickResume()
    {
        MenuManager.OpenPage(MenuManager.Page.GAME, gameObject);
        Time.timeScale = 1f;
        _isPaused = false;
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
