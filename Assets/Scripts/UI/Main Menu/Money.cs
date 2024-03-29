using MemoryLabyrinth.SaveLoad;
using TMPro;
using UnityEngine;

namespace MemoryLabyrinth.UI.MainMenuElements
{
    public class Money : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _textMesh;

        private void Start()
        {
            UpdateMoneyAmount();
        }

        private void UpdateMoneyAmount()
        {
            if (BonusStorage.Instance != null)
                _textMesh.text = BonusStorage.Instance.GetBonuses().ToString();
            else
                throw new System.Exception("MONEY: UpdateMoneyAmount -> BonusStorage.Instance == null");
        }
    }
}