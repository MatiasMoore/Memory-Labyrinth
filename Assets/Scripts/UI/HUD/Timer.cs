using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _textMesh;

    private static bool _timerStatus;

    public static void SetTimerStatus(bool flag)
    {
        _timerStatus = flag;
    }

    public static bool GetTimerStatus()
    {
        return _timerStatus;
    }

    private float elapsedTime;

    private void Start()
    {
        SetTimerStatus(true);

        _textMesh.text = "00:00";
    }

    private void Update()
    {
        if (_timerStatus)
        {
            elapsedTime += Time.deltaTime;

            DisplayTime(elapsedTime);
        }
    }

    private void DisplayTime(float timeToDisplay)
    {
        int minutes = Mathf.FloorToInt(timeToDisplay / 60);
        int seconds = Mathf.FloorToInt(timeToDisplay % 60);

        _textMesh.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
