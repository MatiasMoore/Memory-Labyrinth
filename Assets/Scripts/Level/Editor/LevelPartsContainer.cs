using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace Level.Editor
{
    public class LevelPartsContainer : MonoBehaviour
    {
        private List<GameObject> _parts = new List<GameObject>();

        public void AddPart(GameObject newPart)
        {
            if (_parts.Contains(newPart))
            {
                throw new System.Exception($"LevelPartsContainer: {newPart} already exist");
            }

            _parts.Add(newPart);
        }

        public void DeletePart(GameObject part)
        {
            if (!_parts.Contains(part))
            {
                throw new System.Exception($"LevelPartsContainer: {part} does not exist");
            }

            _parts.Remove(part);
        }
    }
}

