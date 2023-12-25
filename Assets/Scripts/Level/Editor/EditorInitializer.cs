using MemoryLabyrinth.Cam;
using MemoryLabyrinth.Controls;
using UnityEngine;

namespace MemoryLabyrinth.Level.Editor
{
    public class EditorInitializer : MonoBehaviour
    {
        [SerializeField]
        private TouchControls _touchControls;

        [SerializeField]
        private LevelEditor _levelEditor;

        [SerializeField]
        private CameraPanControl _cameraPanControl;

        void Start()
        {
            _touchControls.Init();
            _cameraPanControl.Init(_touchControls);
            _levelEditor.Init(_touchControls, _cameraPanControl);
        }

    }
}

