using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    /*
     * 1) Ќадо все методы (по хорошему) св€зать с Menu Manager и Scene Manager
     * 2) Ќадо что-то придумать с загрузкой этого же самого уровн€ при нажатии на кнопку рестарта (сейчас загружаетс€ только тестовый уровень (по ID))
    */

    public GameObject _pauseMenu;

    public static bool _isPaused = false;

    public void OnClickPause()
    {
        // «десь должен быть Menu Manager (включатьс€ Pause Menu через Menu Manager по идее)
        Debug.Log("PAUSED!");
        _pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        _isPaused = true;
    }

    public void OnClickResume()
    {
        // «десь должен быть Menu Manager (выключатьс€ Pause Menu через Menu Manager по идее)
        Debug.Log("RESUMED!");
        _pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        _isPaused = false;
    }

    public void OnClickToMainMenu()
    {
        Debug.Log("MAIN MENU!");
        // «десь должен быть Scene Manager (при этом братьс€ ID главного меню)
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    }

    public void OnClickRestart()
    {
        Debug.Log("RESTARTED!");
        // «десь должен быть Scene Manager (при этом братьс€ ID текущего уровн€)
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}
