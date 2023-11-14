using TMPro;
using UnityEngine;

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
        _textMesh.text = BonusStorage.Instance.GetBonuses().ToString();
    }
}
