using MemoryLabyrinth.Level.Logic;
using MemoryLabyrinth.Player;
using TMPro;
using UnityEngine;

public class HUDController : MonoBehaviour
{
    [SerializeField] private LevelModel _levelModel;
    [SerializeField] private MainCharacter _mainCharacter;
    [SerializeField] private Gems _gems;
    [SerializeField] private Timer _timer;
    [SerializeField] private Health _health;
    [SerializeField] private TextMeshProUGUI _winPanelTimeTMP;
    [SerializeField] private TextMeshProUGUI _winPanelGemsTMP;
    [SerializeField] private TextMeshProUGUI _losePanelTimeTMP;
    [SerializeField] private TextMeshProUGUI _losePanelGemsTMP;

    public void SetupListeners()
    {
        // LevelModel listeners
        if (_levelModel != null)
        {
            _levelModel._onLevelWin += ShowWinPanelAction;
            _levelModel._onLevelLose += ShowLosePanelAction;
            _levelModel._onBonusAmountChange += UpdateGemsCountAction;
        }

        // MainCharacter listeners
        if (_mainCharacter != null)
        {
            _mainCharacter._onPlayerHealthChangedEvent += UpdateHealthCountAction;
        }
    }
    private void ShowWinPanelAction()
    {
        _timer.SetTimerActive(false);
        MenuManager.OpenPage(MenuManager.Page.WIN);

        // Display data on level completion
        LevelResultInfoManager.SetLevelTime(_timer, _winPanelTimeTMP);
        LevelResultInfoManager.SetLevelGemsCount(_gems, _winPanelGemsTMP);
    }

    private void ShowLosePanelAction()
    {
        _timer.SetTimerActive(false);
        MenuManager.OpenPage(MenuManager.Page.LOSE);

        // Display data on lose
        LevelResultInfoManager.SetLevelTime(_timer, _losePanelTimeTMP);
        LevelResultInfoManager.SetLevelGemsCount(_gems, _losePanelGemsTMP);
    }

    private void UpdateHealthCountAction(int value)
    {
        _health.SetHealth(_mainCharacter.GetHealth());
    }

    private void UpdateGemsCountAction(int value)
    {
        _gems.SetGemsAmount(_levelModel.GetBonusAmount());
    }
}
