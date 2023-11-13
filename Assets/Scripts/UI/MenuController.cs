using UnityEngine;

public class MenuController : MonoBehaviour
{
    private static LevelModel levelModel;

    private static MainCharacter mainCharacter;

    public static void SetupListeners()
    {
        levelModel = FindObjectOfType<LevelModel>();
        if (levelModel != null)
        {
            levelModel._onLevelWin += ShowWinPanelAction;
            levelModel._onLevelLose += ShowLosePanelAction;
            levelModel._onPlayerGetBonus += UpdateGemsCountAction;
        }

        mainCharacter = FindObjectOfType<MainCharacter>();
        if (mainCharacter != null)
        {
            mainCharacter._onDamageEvent += UpdateHealthCountAction;
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

        // Display data on lose
        LosePanel losePanel = FindObjectOfType<LosePanel>();
        losePanel.SetLevelTimeOnLose();
        losePanel.SetLevelGemsCountOnLose();
    }

    private static void UpdateHealthCountAction()
    {
        Health health = FindObjectOfType<Health>();
        if (health != null)
        {
            health.SetHealth(mainCharacter.GetHealth());
        }
    }

    private static void UpdateGemsCountAction()
    {
        Gems gems = FindObjectOfType<Gems>();
        Debug.Log("MENU CONTROLLER: UpdateGemsCountAction -> " + gems);
        if (gems != null)
        {
            gems.SetGemsAmount(levelModel.GetMoneyAmount());
        }
    }
}
