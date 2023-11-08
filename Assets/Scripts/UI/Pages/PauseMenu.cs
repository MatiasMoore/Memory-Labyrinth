using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    /*
     * 1) Ќадо все методы (по хорошему) св€зать с Scene Manager
     * 2) Ќадо что-то придумать с загрузкой этого же самого уровн€ при нажатии на кнопку рестарта (сейчас загружаетс€ только тестовый уровень (по ID))
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
        Time.timeScale = 1f;
        ResourceManager.LoadScene(ResourceManager.AvailableScene.MainMenu);
    }

    public void OnClickRestart()
    {
        Time.timeScale = 1f;
        ResourceManager.LoadScene(ResourceManager.AvailableScene.GameField);
        // Ќадо чтоб подгружалс€ префаб нужного уровн€
    }
}
