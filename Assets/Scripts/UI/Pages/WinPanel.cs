using TMPro;
using UnityEngine;

public class WinPanel : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _time;

    public void SetLevelCompletionTime()
    {
        Timer timer = FindObjectOfType<Timer>();
        if (timer == null)
            Debug.LogError("WIN PANEL: SetLevelCompletionTime -> timer = null");
        _time.text = timer.GetTimerValue();
    }

    public void OnClickNextLevel()
    {
        // Should be transition to next level
        ResourceManager.LoadScene(ResourceManager.AvailableScene.GameField);
        ResourceManager.LoadLevel(ResourceManager.Level.Level1);

        MenuManager.FireButtonClickAction();
    }

    public void OnClickToMainMenu()
    {
        ResourceManager.LoadScene(ResourceManager.AvailableScene.MainMenu);

        MenuManager.FireButtonClickAction();
    }

    public void OnClickRestart()
    {
        // The same level must be loaded
        ResourceManager.LoadScene(ResourceManager.AvailableScene.GameField);
        ResourceManager.LoadLevel(ResourceManager.Level.Level1);

        MenuManager.FireButtonClickAction();
    }
}
