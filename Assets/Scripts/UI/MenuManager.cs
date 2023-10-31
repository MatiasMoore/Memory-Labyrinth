using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private static GameObject _mainMenuCanvas, _optionsMenuCanvas, _achievmentsMenuCanvas;
    public static bool isInitialised { get; private set; }

    public static void Init()
    {
        isInitialised = true;

        GameObject UI = GameObject.Find("UI");
        _mainMenuCanvas = UI.transform.Find("Main Menu Canvas").gameObject;
        _optionsMenuCanvas = UI.transform.Find("Options Menu Canvas").gameObject;
        _achievmentsMenuCanvas = UI.transform.Find("Achievements Menu Canvas").gameObject;
    }

    public static void OpenMenu(Menu menu, GameObject callingMenu)
    {
        if (!isInitialised)
            Init();

        switch(menu)
        {
            case Menu.MAIN:
                _mainMenuCanvas.SetActive(true);
                break;
            case Menu.OPTIONS:
                _optionsMenuCanvas.SetActive(true);
                break;
            case Menu.ACHIEVMENTS:
                _achievmentsMenuCanvas.SetActive(true);
                break;
        }

        callingMenu.SetActive(false);
    }
}
