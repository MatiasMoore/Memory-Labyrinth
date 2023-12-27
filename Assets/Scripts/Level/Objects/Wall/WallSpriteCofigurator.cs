using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MemoryLabyrinth.Level.Objects.WallLib
{
    public class WallSpriteCofigurator : MonoBehaviour
    {
        [SerializeField]
        private GameObject _edgeLeft;

        [SerializeField]
        private GameObject _edgeRight;

        [SerializeField]
        private GameObject _edgeTop;

        [SerializeField]
        private GameObject _edgeBottom;

        [SerializeField]
        private GameObject _outerTopLeft;

        [SerializeField]
        private GameObject _outerTopRight;

        [SerializeField]
        private GameObject _outerBottomRight;

        [SerializeField]
        private GameObject _outerBottomLeft;

        [SerializeField]
        private GameObject _innerTopLeft;

        [SerializeField]
        private GameObject _innerTopRight;

        [SerializeField]
        private GameObject _innerBottomRight;

        [SerializeField]
        private GameObject _innerBottomLeft;

        private struct ConfiguratorNeighbours
        {
            public WallSpriteCofigurator topLeft, top, topRight, right, bottomRight, bottom, bottomLeft, left;
        }

        private void Update()
        {
            if (transform.hasChanged)
                UpdateSprite();
        }

        private void OnEnable()
        {
            UpdateSprite();
        }

        private void OnDisable()
        {
            UpdateSprite();
        }

        private void UpdateSprite()
        {
            UpdateMyself(true);
        }

        private void UpdateMyself(bool updateNeighbours)
        {
            var n = GetNeighbours();
            ConfigureByWallBools(
                n.topLeft != null,
                n.topRight != null,
                n.bottomLeft != null,
                n.bottomRight != null,

                n.left != null,
                n.top != null,
                n.right != null,
                n.bottom != null);

            if (updateNeighbours)
            {
                WallSpriteCofigurator[] confs = { n.topLeft,
                n.topRight,
                n.bottomLeft,
                n.bottomRight,

                n.left,
                n.top,
                n.right,
                n.bottom };

                foreach (var conf in confs)
                {
                    if (conf != null)
                        conf.UpdateMyself(false);
                }
            }
        }

        private void ConfigureByWallBools(bool hasTopLeft, bool hasTopRight, bool hasBottomLeft, bool hasBottomRight,
            bool hasLeft, bool hasTop, bool hasRight, bool hasBottom)
        {
            //Edges
            _edgeLeft.SetActive(!hasLeft);
            _edgeRight.SetActive(!hasRight);
            _edgeTop.SetActive(!hasTop);
            _edgeBottom.SetActive(!hasBottom);

            //Outer corners
            _outerTopLeft.SetActive(!hasTop && !hasLeft);// && !hasTopLeft);
            _outerTopRight.SetActive(!hasTop && !hasRight);// && !hasTopRight);
            _outerBottomRight.SetActive(!hasBottom && !hasRight);// && !hasBottomRight);
            _outerBottomLeft.SetActive(!hasBottom && !hasLeft);// && !hasBottomLeft);

            //Inner corners
            _innerTopLeft.SetActive(hasTop && hasLeft && !hasTopLeft);
            _innerTopRight.SetActive(hasTop && hasRight && !hasTopRight);
            _innerBottomRight.SetActive(hasBottom && hasRight && !hasBottomRight);
            _innerBottomLeft.SetActive(hasBottom && hasLeft && !hasBottomLeft);
        }

        private ConfiguratorNeighbours GetNeighbours()
        {
            Vector3 center = transform.position;
            float dist = 1f;

            ConfiguratorNeighbours neighbours = new ConfiguratorNeighbours();
            neighbours.top = GetConfiguratorAt(center + dist * Vector3.up);
            neighbours.bottom = GetConfiguratorAt(center + dist * Vector3.down);
            neighbours.left = GetConfiguratorAt(center + dist * Vector3.left);
            neighbours.right = GetConfiguratorAt(center + dist * Vector3.right);

            neighbours.topLeft = GetConfiguratorAt(center + dist * (Vector3.up + Vector3.left).normalized);
            neighbours.topRight = GetConfiguratorAt(center + dist * (Vector3.up + Vector3.right).normalized);
            neighbours.bottomLeft = GetConfiguratorAt(center + dist * (Vector3.down + Vector3.left).normalized);
            neighbours.bottomRight = GetConfiguratorAt(center + dist * (Vector3.down + Vector3.right).normalized);

            return neighbours;
        }

        private WallSpriteCofigurator GetConfiguratorAt(Vector2 pos)
        {
            var colliders = Physics2D.OverlapPointAll(pos);
            foreach (var collider in colliders)
            {
                var configurator = collider.gameObject.GetComponent<WallSpriteCofigurator>();
                if (configurator != null)
                    return configurator;
            }
            return null;
        }
    }

}