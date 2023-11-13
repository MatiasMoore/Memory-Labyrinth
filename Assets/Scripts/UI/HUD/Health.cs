using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _textMesh;

    public void SetHealth(int amount)
    {
        _textMesh.text = amount.ToString();
    }
}
