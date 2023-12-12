using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MemoryLabyrinth.UI.Achievements
{
    public class AchievementDisplayingData : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _objectName;
        [SerializeField] private Image _objectIcon;
        [SerializeField] private Image _objectCompletionStatusIcon;

        [SerializeField] private Sprite _achievedIcon;
        [SerializeField] private Sprite _unachievedIcon;

        public void SetIcon(Sprite icon)
        {
            if (icon == null)
                throw new System.Exception("AchievementDisplayingData: SetIcon -> icon == null");

            _objectIcon.sprite = icon;
        }

        public void SetCompletionStatusIcon(bool completionStatus)
        {
            if (completionStatus)
                _objectCompletionStatusIcon.sprite = _achievedIcon;
            else
                _objectCompletionStatusIcon.sprite = _unachievedIcon;
        }

        public void SetName(string name)
        {
            if (name == null)
                throw new System.Exception("AchievementDisplayingData: SetName -> name == null");

            _objectName.text = name;
        }
    }

}