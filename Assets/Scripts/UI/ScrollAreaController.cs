using UnityEngine;
using UnityEngine.UI;

public class ScrollAreaController : MonoBehaviour
{
    [SerializeField]
    ScrollRect scrollRectObject;

    private void OnEnable()
    {
        scrollRectObject.verticalNormalizedPosition = 1;
    }
}
