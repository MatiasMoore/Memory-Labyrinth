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
        if ((int)(LevelManager._currentLevel + 1) > ResourceManager.GetLastLevelIndex())
            ResourceManager.LoadScene(ResourceManager.AvailableScene.MainMenu);
        // Else load next level
        else
            // TODO: load next level
            // ResourceManager.LoadLevel(LevelManager._currentLevel + 1);

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
        Timer.Instance.ResetTimer();
        MenuManager.ClosePage(MenuManager.Page.WIN);

        LevelManager.FireLevelLoadAction();
        MenuManager.FireButtonClickAction();
    }
}
