using System.Collections;
using UnityEngine;

namespace MemoryLabyrinth.Fog
{
    public class FogMaskTarget : MonoBehaviour
    {
        [SerializeField]
        private float _preferredRadius = 3f;

        private GameObject _maskObj;
        private SpriteMask _mask;

        public float GetPreferredRadius()
        {
            return _preferredRadius;
        }

        public Vector2 GetPos()
        {
            return (Vector2)transform.position;
        }

        public void CreateMask()
        {
            _maskObj = Instantiate(UnityEngine.Resources.Load("FogMask"), this.transform) as GameObject;
            _mask = _maskObj.GetComponent<SpriteMask>();
            SetMaskRadius(_preferredRadius);
        }

        public void DeleteMask()
        {
            if (_maskObj != null)
                Destroy(_maskObj);
        }

        public void FadeMaskRadius(float from, float to, float timeToBlend)
        {
            if (_maskObj == null)
                throw new System.Exception("Can not fade before the mask object is created. Please use CreateMask to do so");

            StopAllCoroutines();
            StartCoroutine(BlendRadius(from, to, timeToBlend));
        }

        private IEnumerator BlendRadius(float from, float to, float timeToBlend)
        {
            float t = 0;
            while (t < 1)
            {
                SetMaskRadius(Mathf.Lerp(from, to, t));
                t += Time.deltaTime / timeToBlend;
                yield return null;
            }

            SetMaskRadius(to);
        }

        private void SetMaskRadius(float radius)
        {
            if (_maskObj == null)
                return;
            var bounds = _mask.sprite.bounds;
            var xSize = bounds.size.x;
            var ySize = bounds.size.y;

            var diameter = radius * 2;

            _maskObj.transform.localScale = new Vector3(diameter / xSize, diameter / ySize, diameter);
        }
    }
}
