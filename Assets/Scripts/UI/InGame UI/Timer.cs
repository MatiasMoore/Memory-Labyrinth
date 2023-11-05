using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _textMesh;

    public bool _isRunning;

    private float elapsedTime;

    void Start()
    {
        _isRunning = true;

        _textMesh.text = "00:00";
    }

    void Update()
    {
        if (_isRunning)
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
