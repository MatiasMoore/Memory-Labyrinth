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

