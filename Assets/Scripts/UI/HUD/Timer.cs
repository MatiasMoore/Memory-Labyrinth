using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public static Timer Instance { get; private set; }

    [SerializeField]
    private TextMeshProUGUI _textMesh;

    private bool _timerStatus;

    private float _elapsedTime = 0f;

    public void Init()
    {
        if (Instance != null)
            return;

        Instance = this;
        _textMesh.text = "00:00";
    }

    private void Update()
    {
        if (_timerStatus)
        {
            _elapsedTime += Time.deltaTime;

            DisplayTime(_elapsedTime);
        }
    }

    private void DisplayTime(float timeToDisplay)
    {
        int minutes = Mathf.FloorToInt(timeToDisplay / 60);
        int seconds = Mathf.FloorToInt(timeToDisplay % 60);

        _textMesh.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public string GetTimerValue()
    {
        return _textMesh.text;
    }

    public void SetTimerStatus(bool flag)
    {
        _timerStatus = flag;
    }

    public bool GetTimerStatus()
    {
        return _timerStatus;
    }

    public float GetElapsedTime()
    {
        return _elapsedTime;
    }

    public void SetElapsedTime(float value)
    {
        _elapsedTime = value;
    }

    public void ResetTimer()
    {
        SetElapsedTime(0f);
        DisplayTime(_elapsedTime);
    }
}
