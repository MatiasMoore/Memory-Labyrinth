using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    private static GameObject _mainMenu;
    private static GameObject _optionsMenu;
    private static GameObject _achievementsMenu;
    private static GameObject _pauseMenu;
    private static GameObject _winPanel;
    private static GameObject _losePanel;

    public static event UnityAction _buttonClick;

    public static void FireButtonClickAction()
    {
        _buttonClick?.Invoke();
    }

    public enum Page
    {
        MAIN,
        OPTIONS,
        ACHIEVEMENTS,
        PAUSE,
        WIN,
        LOSE
    }

    public static void OpenPage(Page page)
    {
        SetActivePage(GetPageGameObject(page));
    }

    public static void ClosePage(Page page)
    {
        SetInactivePage(GetPageGameObject(page));
    }

    private static GameObject GetPageGameObject(Page page)
    {
        GameObject UI = GameObject.Find("UI");
        if (UI == null)
            throw new System.Exception("MENU MANAGER: GetPageGameObject -> No UI object in scene");

        switch (page)
        {
            case Page.MAIN:
                _mainMenu = UI.transform.Find(GetPageName(Page.MAIN)).gameObject;
                return _mainMenu;
            case Page.OPTIONS:
                _optionsMenu = UI.transform.Find(GetPageName(Page.OPTIONS)).gameObject;
                return _optionsMenu;
            case Page.ACHIEVEMENTS:
                _achievementsMenu = UI.transform.Find(GetPageName(Page.ACHIEVEMENTS)).gameObject;
                return _achievementsMenu;
            case Page.PAUSE:
                _pauseMenu = UI.transform.Find(GetPageName(Page.PAUSE)).gameObject;
                return _pauseMenu;
            case Page.WIN:
                _winPanel = UI.transform.Find(GetPageName(Page.WIN)).gameObject;
                return _winPanel;
            case Page.LOSE:
                _losePanel = UI.transform.Find(GetPageName(Page.LOSE)).gameObject;
                return _losePanel;
            default:
                throw new System.Exception("MENU MANAGER: GetPageGameObject -> page is not defined in switch");
        }
    }

    private static string GetPageName(Page page)
    {
        switch (page)
        {
            case Page.MAIN:
                return new string("Main Menu");
            case Page.OPTIONS:
                return new string("Options Menu");
            case Page.ACHIEVEMENTS:
                return new string("Achievements Menu");
            case Page.PAUSE:
                return new string("Pause Menu");
            case Page.WIN:
                return new string("Win Panel");
            case Page.LOSE:
                return new string("Lose Panel");
            default:
                throw new System.Exception("MENU MANAGER: GetPageName -> page is not defined in switch");
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
        if (menuObject != null)
            menuObject.SetActive(false);
        else
            throw new System.Exception("MENU MANAGER: SetInactivePage -> menuObject is null");
    }
}
