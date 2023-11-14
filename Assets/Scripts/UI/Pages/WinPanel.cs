using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class WinPanel : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _time;

    [SerializeField]
    private TextMeshProUGUI _gems;

    public void SetLevelCompletionTime()
    {
        Timer timer = FindObjectOfType<Timer>();
        if (timer == null)
            throw new System.Exception("WIN PANEL: SetLevelCompletionTime -> timer = null");
        _time.text = ": " + timer.GetTimerValue();
    }

    public void SetLevelCompletionGemsCount()
    {
        Gems gems = FindObjectOfType<Gems>();
        if (gems == null)
            throw new System.Exception("WIN PANEL: SetLevelCompletionGemsCount -> gems = null");
        _gems.text = ": " + gems.GetGemsCount();
    }

    public void OnClickNextLevel()
    {
        ResourceManager.LoadScene(ResourceManager.AvailableScene.GameField);

        // If level index is out of enum range, load Main Menu
        if ((int)(CurrentLevel.getCurrentLevel()._level + 1) > ResourceManager.GetLastLevelIndex())
            ResourceManager.LoadScene(ResourceManager.AvailableScene.MainMenu);
        // Else load next level
        else
            // TEMP SOLUTION
            ResourceManager.LoadScene(ResourceManager.AvailableScene.GameField);
            // TODO: load next level
            // ResourceManager.LoadLevel(LevelManager._currentLevel + 1);

        LevelManager.FireLevelLoadAction();
        MenuManager.FireButtonClickAction();
    }

    public void OnClickToMainMenu()
    {
        ResourceManager.LoadScene(ResourceManager.AvailableScene.MainMenu);

        MenuManager.FireButtonClickAction();
    }

    public void OnClickRestart()
    {
        // TODO: reload level prefab
        MenuManager.ClosePage(MenuManager.Page.WIN);

        // TEMP SOLUTION
        LevelManager levelManager = FindObjectOfType<LevelManager>();
        levelManager.StartLevel();

        LevelManager.FireLevelLoadAction();
        MenuManager.FireButtonClickAction();
    }
}
