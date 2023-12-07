using TMPro;
using UnityEngine;

public class WinPanelInfoController : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _timeTMP;

    [SerializeField]
    private TextMeshProUGUI _gemsTMP;

    [SerializeField]
    private Timer _timer;

    [SerializeField]
    private Gems _gems;

    public void SetLevelCompletionTime()
    {
        if (_timer == null)
            throw new System.Exception("WIN PANEL: SetLevelCompletionTime -> _timer = null");
        _timeTMP.text = ": " + _timer.GetTimerValue();
    }

    public void SetLevelCompletionGemsCount()
    {
        if (_gems == null)
            throw new System.Exception("WIN PANEL: SetLevelCompletionGemsCount -> _gems = null");
        _gemsTMP.text = ": " + _gems.GetGemsCount();
    }
}
