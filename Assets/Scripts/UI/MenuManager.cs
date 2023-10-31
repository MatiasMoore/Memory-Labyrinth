using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private static GameObject mainMenuCanvas, optionsMenuCanvas, achievmentsMenuCanvas;
    public static bool isInitialised { get; private set; }

    public static void Init()
    {
        isInitialised = true;

        GameObject UI = GameObject.Find("UI");
        mainMenuCanvas = UI.transform.Find("Main Menu Canvas").gameObject;
        optionsMenuCanvas = UI.transform.Find("Options Menu Canvas").gameObject;
        achievmentsMenuCanvas = UI.transform.Find("Achievements Menu Canvas").gameObject;
    }

    public static void OpenMenu(Menu menu, GameObject callingMenu)
    {
        if (!isInitialised)
            Init();

        switch(menu)
        {
            case Menu.MAIN:
                mainMenuCanvas.SetActive(true);
                break;
            case Menu.OPTIONS:
                optionsMenuCanvas.SetActive(true);
                break;
            case Menu.ACHIEVMENTS:
                achievmentsMenuCanvas.SetActive(true);
                break;
        }

        callingMenu.SetActive(false);
    }
}
