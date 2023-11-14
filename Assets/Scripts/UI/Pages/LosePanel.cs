using TMPro;
using UnityEngine;

public class LosePanel : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _time;

    [SerializeField]
    private TextMeshProUGUI _gems;

    public void SetLevelTimeOnLose()
    {
        Timer timer = FindObjectOfType<Timer>();
        if (timer == null)
            throw new System.Exception("LOSE PANEL: SetLevelTimeOnLose -> timer = null");
        _time.text = ": " + timer.GetTimerValue();
    }

    public void SetLevelGemsCountOnLose()
    {
        Gems gems = FindObjectOfType<Gems>();
        if (gems == null)
            throw new System.Exception("LOSE PANEL: SetLevelGemsCountOnLose -> gems = null");
        _gems.text = ": " + gems.GetGemsCount();
    }
    public void OnClickToMainMenu()
    {
        ResourceManager.LoadScene(ResourceManager.AvailableScene.GameField);

        MenuManager.FireButtonClickAction();
    }

    public void OnClickRestart()
    {
        MenuManager.ClosePage(MenuManager.Page.LOSE);
        LevelManager.Instance.StartCurrentLevelFromSpawn();

        MenuManager.FireButtonClickAction();
    }
}
