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
        _mainMenuCanvas = UI.transform.Find(GetPageName(Page.MAIN)).gameObject;
        _optionsMenuCanvas = UI.transform.Find(GetPageName(Page.OPTIONS)).gameObject;
        _achievementsMenuCanvas = UI.transform.Find(GetPageName(Page.ACHIEVEMENTS)).gameObject;
    }

    public static void OpenPage(Page page, GameObject callingPage)
    {
        // Подгрузка объектов со сцены (в случае перехода с одной сцены на другую)
        // по идее это должно вызываться при каждом переходе с одной сцены на другую в SceneManager
        UpdatePages();

        // Enable required page
        SetActivePage(GetPageGameObject(page));

        // Disable calling page
        SetInactivePage(callingPage);
    }

    private static void UpdatePages()
    {
        GameObject UI = GameObject.Find("UI");
        // По хорошему сделать enum где хранятся buildindex каждой сцены и из него брать
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            _mainMenuCanvas = UI.transform.Find(GetPageName(Page.MAIN)).gameObject;
            _optionsMenuCanvas = UI.transform.Find(GetPageName(Page.OPTIONS)).gameObject;
            _achievementsMenuCanvas = UI.transform.Find(GetPageName(Page.ACHIEVEMENTS)).gameObject;
        }
        else
        {
            _gameWindow = GameObject.Find(GetPageName(Page.GAME)).transform.gameObject;
            _pauseMenuCanvas = UI.transform.Find(GetPageName(Page.PAUSE)).gameObject;
            _winPanel = UI.transform.Find(GetPageName(Page.WIN)).gameObject;
            _losePanel = UI.transform.Find(GetPageName(Page.LOSE)).gameObject;
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
                Debug.LogError("MENU MANAGER: GetPageGameObject -> return null (page is not defined in switch)");
                return null;
        }
    }

    private static string GetPageName(Page page)
    {
        switch (page)
        {
            case Page.MAIN:
                return new string("Main Menu Canvas");
            case Page.OPTIONS:
                return new string("Options Menu Canvas");
            case Page.ACHIEVEMENTS:
                return new string("Achievements Menu Canvas");
            case Page.PAUSE:
                return new string("Pause Menu (Canvas)");
            case Page.WIN:
                return new string("Win Panel (Canvas)");
            case Page.LOSE:
                return new string("Lose Panel (Canvas)");
            case Page.GAME:
                return new string("Level Model");
            default:
                Debug.LogError("MENU MANAGER: GetPageName -> return null (page is not defined in switch)");
                return null;
        }
    }

    private static void SetActivePage(GameObject menuObject)
    {
        if (menuObject != null)
            menuObject.SetActive(true);
        else
            Debug.LogError("MENU MANAGER: SetActivePage -> menuObject is null");
    }

    private static void SetInactivePage(GameObject menuObject)
    {
        if(menuObject != null)
            menuObject.SetActive(false);
        else
            Debug.LogError("MENU MANAGER: SetInactivePage -> menuObject is null");
    }
}
