using UnityEngine;

namespace MemoryLabyrinth.UI
{
    public class SafeAreaScript : MonoBehaviour
    {
        private RectTransform _safeAreaRectTransform;

        private void Awake()
        {
            _safeAreaRectTransform = GetComponent<RectTransform>();
            UpdateSafeArea();
        }

        private void UpdateSafeArea()
        {
            Rect safeArea = Screen.safeArea;

            Vector2 bottomLeftAnchor = safeArea.position;
            Vector2 topRightAnchor = safeArea.position + safeArea.size;

            // Transition from pixels to float in the range [0;1]
            bottomLeftAnchor.x /= Screen.width;
            bottomLeftAnchor.y /= Screen.height;
            topRightAnchor.x /= Screen.width;
            topRightAnchor.y /= Screen.height;

            _safeAreaRectTransform.anchorMin = bottomLeftAnchor;
            _safeAreaRectTransform.anchorMax = topRightAnchor;
        }
    }
}
