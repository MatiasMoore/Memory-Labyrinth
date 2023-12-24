using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Pool;

namespace Level.Editor
{
    public class LevelPartsContainer
    {
        private Dictionary<LevelPartType, List<GameObject>> _parts;

        public List<GameObject> GetPartsByType(LevelPartType type) => _parts[type];

        public LevelPartsContainer()
        {
            _parts = new Dictionary<LevelPartType, List<GameObject>>();
            foreach (LevelPartType type in LevelPartType.GetValues(typeof(LevelPartType)))
            {
                _parts[type] = new List<GameObject>();
            }
        }

        public void AddPart(LevelPartType type, GameObject newPart)
        {
            if (_parts[type].Contains(newPart))
            {
                throw new System.Exception($"LevelPartsContainer: {newPart} already exist");
            }

            _parts[type].Add(newPart);
        }

        public void DeletePart(GameObject part)
        {
            bool isRemoved = false;

            foreach (List<GameObject> partList in _parts.Values)
            {
                if (partList.Contains(part))
                {
                    partList.Remove(part);
                    isRemoved = true;
                }
            }

            if (!isRemoved)
            {
                throw new System.Exception($"LevelPartsContainer: {part} does not exist");
            }

        }
    }
}

