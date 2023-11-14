using UnityEngine;

public class MenuController : MonoBehaviour
{
    private static LevelModel _levelModel;

    private static MainCharacter _mainCharacter;

    public static void SetupListeners(LevelModel levelModel, MainCharacter mainCharacter)
    {
        _levelModel = levelModel;
        if (_levelModel != null)
        {
            levelModel._onLevelWin += ShowWinPanelAction;
            levelModel._onLevelLose += ShowLosePanelAction;
            levelModel._onPlayerGetBonus += UpdateGemsCountAction;
        }

        _mainCharacter = mainCharacter;
        if (_mainCharacter != null)
        {
            mainCharacter._onPlayerHealthChangedEvent += UpdateHealthCountAction;
        }
    }
    private static void ShowWinPanelAction()
    {
        Timer.Instance.SetTimerStatus(false);
        MenuManager.OpenPage(MenuManager.Page.WIN);

        // Display data on level completion
        WinPanel winPanel = FindObjectOfType<WinPanel>();
        winPanel.SetLevelCompletionTime();
        winPanel.SetLevelCompletionGemsCount();
    }

    private static void ShowLosePanelAction()
    {
        Timer.Instance.SetTimerStatus(false);
        MenuManager.OpenPage(MenuManager.Page.LOSE);

        // Display data on lose
        LosePanel losePanel = FindObjectOfType<LosePanel>();
        losePanel.SetLevelTimeOnLose();
        losePanel.SetLevelGemsCountOnLose();
    }

    private static void UpdateHealthCountAction(int value)
    {
        Health health = FindObjectOfType<Health>();
        if (health != null)
        {
            health.SetHealth(_mainCharacter.GetHealth());
        }
    }

    private static void UpdateGemsCountAction()
    {
        Gems gems = FindObjectOfType<Gems>();
        Debug.Log("MENU CONTROLLER: UpdateGemsCountAction -> " + gems);
        if (gems != null)
        {
            gems.SetGemsAmount(_levelModel.GetBonusAmount());
        }
    }
}
