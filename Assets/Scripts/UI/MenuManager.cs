using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private static GameObject _mainMenuCanvas, _optionsMenuCanvas, _achievementsMenuCanvas, _pauseMenuCanvas, _gameWindow = null;

    private static void Init()
    {
        GameObject UI = GameObject.Find("UI");
        _mainMenuCanvas = UI.transform.Find("Main Menu Canvas").gameObject;
        _optionsMenuCanvas = UI.transform.Find("Options Menu Canvas").gameObject;
        _achievementsMenuCanvas = UI.transform.Find("Achievements Menu Canvas").gameObject;
    }

    public static void OpenPage(Page Page, GameObject callingPage)
    {
        // Подгрузка объектов со сцены (в случае перехода с одной сцены на другую)
        // по идее это должно вызываться при каждом переходе с одной сцены на другую в SceneManager
        UpdatePages();

        switch(Page)
        {
            case Page.MAIN:
                SetActivePage(_mainMenuCanvas);
                break;
            case Page.OPTIONS:
                SetActivePage(_optionsMenuCanvas);
                break;
            case Page.ACHIEVEMENTS:
                SetActivePage(_achievementsMenuCanvas);
                break;
            case Page.PAUSE:
                SetActivePage(_pauseMenuCanvas);
                break;
            case Page.GAME:
                SetActivePage(_gameWindow);
                break;
        }
        
        SetInactivePage(callingPage);
    }

    private static void UpdatePages()
    {
        GameObject UI = GameObject.Find("UI");
        GameObject LevelModel = GameObject.Find("Level Model");
        // По хорошему сделать enum где хранятся buildindex каждой сцены и из него брать
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            _mainMenuCanvas = UI.transform.Find("Main Menu Canvas").gameObject;
            _optionsMenuCanvas = UI.transform.Find("Options Menu Canvas").gameObject;
            _achievementsMenuCanvas = UI.transform.Find("Achievements Menu Canvas").gameObject;
        }
        else
        {
            _gameWindow = LevelModel.transform.gameObject;
            _pauseMenuCanvas = UI.transform.Find("Pause Menu (Canvas)").gameObject;
        }
    }

    private static void SetActivePage(GameObject menuObject)
    {
        if (menuObject != null)
            menuObject.SetActive(true);
    }

    private static void SetInactivePage(GameObject menuObject)
    {
        if(menuObject != null)
            menuObject.SetActive(false);
    }

    private void Awake()
    {
        Init();
    }
}
