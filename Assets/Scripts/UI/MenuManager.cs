using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    private static GameObject _mainMenuCanvas;
    private static GameObject _optionsMenuCanvas;
    private static GameObject _achievementsMenuCanvas;
    private static GameObject _pauseMenuCanvas;
    private static GameObject _winPanel;
    private static GameObject _losePanel;

    public enum Page
    {
        MAIN,
        OPTIONS,
        ACHIEVEMENTS,
        PAUSE,
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

    public static void OpenPage(Page page)
    {
        // Подгрузка объектов со сцены (в случае перехода с одной сцены на другую)
        // по идее это должно вызываться при каждом переходе с одной сцены на другую в SceneManager
        UpdatePages();

        // Enable required page
        SetActivePage(GetPageGameObject(page));
    }

    public static void ClosePage(Page page)
    {
        // Подгрузка объектов со сцены (в случае перехода с одной сцены на другую)
        // по идее это должно вызываться при каждом переходе с одной сцены на другую в SceneManager
        UpdatePages();

        // Disable required page
        SetInactivePage(GetPageGameObject(page));
    }

    private static void UpdatePages()
    {
        GameObject UI = GameObject.Find("UI");
        var currentScene = ResourceManager.GetCurrentScene();

        switch (currentScene)
        {
            case ResourceManager.AvailableScene.MainMenu:
                _mainMenuCanvas = UI.transform.Find(GetPageName(Page.MAIN)).gameObject;
                _optionsMenuCanvas = UI.transform.Find(GetPageName(Page.OPTIONS)).gameObject;
                _achievementsMenuCanvas = UI.transform.Find(GetPageName(Page.ACHIEVEMENTS)).gameObject;
                break;
            case ResourceManager.AvailableScene.GameField:
                _pauseMenuCanvas = UI.transform.Find(GetPageName(Page.PAUSE)).gameObject;
                _winPanel = UI.transform.Find(GetPageName(Page.WIN)).gameObject;
                _losePanel = UI.transform.Find(GetPageName(Page.LOSE)).gameObject;
                break;
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
            default:
                throw new System.Exception("MENU MANAGER: GetPageGameObject -> return null (page is not defined in switch)");
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
            default:
                throw new System.Exception("MENU MANAGER: GetPageName -> return null (page is not defined in switch)");
        }
    }

    private static void SetActivePage(GameObject menuObject)
    {
        if (menuObject != null)
            menuObject.SetActive(true);
        else
            throw new System.Exception("MENU MANAGER: SetActivePage -> menuObject is null");
    }

    private static void SetInactivePage(GameObject menuObject)
    {
        if(menuObject != null)
            menuObject.SetActive(false);
        else
            throw new System.Exception("MENU MANAGER: SetInactivePage -> menuObject is null");
    }
}
