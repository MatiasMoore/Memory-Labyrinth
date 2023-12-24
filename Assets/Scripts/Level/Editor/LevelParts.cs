using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Level.Editor
{
    [CreateAssetMenu]
    public class LevelParts : ScriptableObject
    {
        [SerializeField]
        private List<LevelPartConfig> _partsList;

        public LevelPartConfig GetConfigByType(LevelPartType type)
        {
            int configIndex = _partsList.FindIndex(x => x.type == type);
            if(configIndex != -1)
            {
                return _partsList[configIndex];
            } else
            {
                Debug.Log($"LevelParts: {type} don't exist");
                return new LevelPartConfig();
            }
        }

    }

    [Serializable]
    public struct LevelPartConfig
    {
        [SerializeField]
        public LevelPartType type;
        [SerializeField]
        public GameObject prefab;
        [SerializeField]
        public float zDepth;
        [SerializeField]
        public Vector3 offset;
    }

    public enum LevelPartType
    {
        Wall,
        Path,
        Trap,
        Checkpoint,
        Startpoint,
        Finishpoint,
        Bonus,
        Teleport
    }
}

