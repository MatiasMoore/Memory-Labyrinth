using UnityEngine;
using UnityEngine.UI;

namespace MemoryLabyrinth.UI
{
    public class ScrollAreaController : MonoBehaviour
    {
        [SerializeField]
        ScrollRect scrollRectObject;

        private void OnEnable()
        {
            scrollRectObject.verticalNormalizedPosition = 1;
        }
    }
}