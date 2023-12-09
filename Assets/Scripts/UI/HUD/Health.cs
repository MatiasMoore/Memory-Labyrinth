using TMPro;
using UnityEngine;

namespace MemoryLabyrinth.UI.HUD
{
    public class Health : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _textMesh;

        public void SetHealth(int amount)
        {
            _textMesh.text = amount.ToString();
        }
    }
}