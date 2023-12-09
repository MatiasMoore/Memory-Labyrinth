using TMPro;
using UnityEngine;

namespace MemoryLabyrinth.UI.HUD
{
    public class Gems : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _textMesh;

        public string GetGemsCount()
        {
            return _textMesh.text;
        }

        public void SetGemsAmount(int amount)
        {
            Debug.Log("GEMS: SetGemsAmount -> " + amount);
            _textMesh.text = amount.ToString();
        }
    }

}