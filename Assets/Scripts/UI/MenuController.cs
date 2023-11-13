using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public static void SetupListeners()
    {
        var levelModel = FindObjectOfType<LevelModel>();
        if (levelModel != null)
        {
            levelModel._onLevelWin += ShowWinPanelAction;
            levelModel._onLevelLose += ShowLosePanelAction;
        }
    }

    private static void ShowWinPanelAction()
    {
        Timer.SetTimerStatus(false);
        MenuManager.OpenPage(MenuManager.Page.WIN);
    }

    private static void ShowLosePanelAction()
    {
        Timer.SetTimerStatus(false);
        MenuManager.OpenPage(MenuManager.Page.LOSE);
    }
}
