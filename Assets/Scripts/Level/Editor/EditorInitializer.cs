using MemoryLabyrinth.Controls;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MemoryLabyrinth.Level.Editor
{
    public class EditorInitializer : MonoBehaviour
    {
        [SerializeField]
        private TouchControls _touchControls;

        [SerializeField]
        private LevelEditor _levelEditor;

        void Start()
        {
            _touchControls.Init();
            _levelEditor.Init(_touchControls);
        }

    }
}

