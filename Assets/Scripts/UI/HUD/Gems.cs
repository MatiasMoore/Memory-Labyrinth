using TMPro;
using UnityEngine;

public class Gems : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _textMesh;

    public string GetGemsCount()
    {
        return _textMesh.text;
    }

}
