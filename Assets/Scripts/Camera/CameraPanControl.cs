using MemoryLabyrinth.Controls;
using System.Collections;
using UnityEngine;

namespace MemoryLabyrinth.Cam
{
    public class CameraPanControl : MonoBehaviour
    {
        private Camera _camera;
        private Coroutine _cameraPanCoroutine = null;
        private TouchControls _touchControls;

        public void Init(TouchControls touchControls)
        {
            _camera = GetComponent<Camera>();
            _touchControls = touchControls;
        }

        public bool IsActive()
        {
            return _cameraPanCoroutine != null;
        }

        public void StartCameraPan()
        {
            _cameraPanCoroutine = StartCoroutine(UpdateCameraPan());
        }

        public void StopCameraPan()
        {
            if (_cameraPanCoroutine != null)
            {
                StopCoroutine(_cameraPanCoroutine);
                _cameraPanCoroutine = null;
            }
        }

        private IEnumerator UpdateCameraPan()
        {
            Vector2 lastTouchPos = _touchControls.getTouchWorldPosition2d();
            while (true)
            {
                Debug.Log("panning");
                Vector2 touchDelta = lastTouchPos - _touchControls.getTouchWorldPosition2d();
                _camera.transform.position += new Vector3(touchDelta.x, touchDelta.y, 0);
                yield return null;
            }
        }
    }
}