using TMPro;
using UnityEngine;

namespace MemoryLabyrinth.UI.LevelSelectionLib
{
    public class LevelDisplayingData : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _objectTextMesh;

        public void SetName(string name)
        {
            if (name == null)
                throw new System.Exception("AchievementDisplayingData: SetName -> name == null");

            _objectTextMesh.text = name;
        }
    }
}