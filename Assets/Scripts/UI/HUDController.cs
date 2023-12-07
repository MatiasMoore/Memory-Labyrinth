using MemoryLabyrinth.Level.Logic;
using MemoryLabyrinth.Player;
using UnityEngine;

public class HUDController : MonoBehaviour
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
            levelModel._onBonusAmountChange += UpdateGemsCountAction;
        }

        _mainCharacter = mainCharacter;
        if (_mainCharacter != null)
        {
            mainCharacter._onPlayerHealthChangedEvent += UpdateHealthCountAction;
        }
    }
    private static void ShowWinPanelAction()
    {
        Timer.Instance.SetTimerActive(false);
        MenuManager.OpenPage(MenuManager.Page.WIN);

        // Display data on level completion
        WinPanelInfoController infoController = FindObjectOfType<WinPanelInfoController>();
        infoController.SetLevelCompletionTime();
        infoController.SetLevelCompletionGemsCount();
    }

    private static void ShowLosePanelAction()
    {
        Timer.Instance.SetTimerActive(false);
        MenuManager.OpenPage(MenuManager.Page.LOSE);

        // Display data on lose
        LosePanelInfoController infoController = FindObjectOfType<LosePanelInfoController>();
        infoController.SetLevelTimeOnLose();
        infoController.SetLevelGemsCountOnLose();
    }

    private static void UpdateHealthCountAction(int value)
    {
        Health health = FindObjectOfType<Health>();
        if (health != null)
        {
            health.SetHealth(_mainCharacter.GetHealth());
        }
    }

    private static void UpdateGemsCountAction(int value)
    {
        Gems gems = FindObjectOfType<Gems>();
        Debug.Log("MENU CONTROLLER: UpdateGemsCountAction -> " + gems);
        if (gems != null)
        {
            Debug.Log(_levelModel.GetBonusAmount());
            gems.SetGemsAmount(_levelModel.GetBonusAmount());
        }
    }
}
