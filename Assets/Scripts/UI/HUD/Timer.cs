using UnityEngine;
using TMPro;

namespace MemoryLabyrinth.UI.HUD
{
    public class Timer : MonoBehaviour
    {
        public static Timer Instance { get; private set; }

        [SerializeField]
        private TextMeshProUGUI _textMesh;

        private bool _isActive;

        private float _elapsedTime = 0f;

        public void Init()
        {
            if (Instance != null)
                return;

            Instance = this;
            _textMesh.text = "00:00";
            _isActive = true;
        }

        private void Update()
        {
            if (_isActive)
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

        public void SetTimerActive(bool state)
        {
            _isActive = state;
        }

        public bool IsTimerActive()
        {
            return _isActive;
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
}