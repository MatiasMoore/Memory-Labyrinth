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

        // Display data on level completion
        WinPanel winPanel = FindObjectOfType<WinPanel>();
        winPanel.SetLevelCompletionTime();
        winPanel.SetLevelCompletionGemsCount();
    }

    private static void ShowLosePanelAction()
    {
        Timer.SetTimerStatus(false);
        MenuManager.OpenPage(MenuManager.Page.LOSE);

        // TODO: Display data when losing (time and gems)
    }
}
