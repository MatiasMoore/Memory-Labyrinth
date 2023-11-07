using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    private static GameObject _mainMenuCanvas;
    private static GameObject _optionsMenuCanvas;
    private static GameObject _achievementsMenuCanvas;
    private static GameObject _pauseMenuCanvas;
    private static GameObject _gameWindow;
    private static GameObject _winPanel;
    private static GameObject _losePanel;

    public enum Page
    {
        MAIN,
        OPTIONS,
        ACHIEVEMENTS,
        PAUSE,
        GAME,
        WIN,
        LOSE
    }

    private void Awake()
    {
        Init();
    }

    private static void Init()
    {
        GameObject UI = GameObject.Find("UI");
        _mainMenuCanvas = UI.transform.Find("Main Menu Canvas").gameObject;
        _optionsMenuCanvas = UI.transform.Find("Options Menu Canvas").gameObject;
        _achievementsMenuCanvas = UI.transform.Find("Achievements Menu Canvas").gameObject;
    }

    public static void OpenPage(Page page, GameObject callingPage)
    {
        // Подгрузка объектов со сцены (в случае перехода с одной сцены на другую)
        // по идее это должно вызываться при каждом переходе с одной сцены на другую в SceneManager
        UpdatePages();

        GameObject activatedPage = GetPageGameObject(page);
        if(activatedPage != null)
            SetActivePage(activatedPage);
        else
            Debug.LogError("MENU MANAGER: OpenPage -> activated page is null");

        SetInactivePage(callingPage);
    }

    private static void UpdatePages()
    {
        GameObject UI = GameObject.Find("UI");
        // По хорошему сделать enum где хранятся buildindex каждой сцены и из него брать
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            _mainMenuCanvas = UI.transform.Find("Main Menu Canvas").gameObject;
            _optionsMenuCanvas = UI.transform.Find("Options Menu Canvas").gameObject;
            _achievementsMenuCanvas = UI.transform.Find("Achievements Menu Canvas").gameObject;
        }
        else
        {
            _gameWindow = GameObject.Find("Level Model").transform.gameObject;
            _pauseMenuCanvas = UI.transform.Find("Pause Menu (Canvas)").gameObject;
            _winPanel = UI.transform.Find("Win Panel (Canvas)").gameObject;
            _losePanel = UI.transform.Find("Lose Panel (Canvas)").gameObject;
        }
    }

    private static GameObject GetPageGameObject(Page page)
    {
        switch (page)
        {
            case Page.MAIN:
                return _mainMenuCanvas;
            case Page.OPTIONS:
                return _optionsMenuCanvas;
            case Page.ACHIEVEMENTS:
                return _achievementsMenuCanvas;
            case Page.PAUSE:
                return _pauseMenuCanvas;
            case Page.WIN:
                return _winPanel;
            case Page.LOSE:
                return _losePanel;
            case Page.GAME:
                return _gameWindow;
            default:
                return null;
        }
    }

    private static void SetActivePage(GameObject menuObject)
    {
        if (menuObject != null)
            menuObject.SetActive(true);
        else
            Debug.LogError("MENU MANAGER: SetActivePage -> page is null");
    }

    private static void SetInactivePage(GameObject menuObject)
    {
        if(menuObject != null)
            menuObject.SetActive(false);
        else
            Debug.LogError("MENU MANAGER: SetInactivePage -> page is null");
    }
}
