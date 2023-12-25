using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace MemoryLabyrinth.UI
{
    public class InputField : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _textField;

        [SerializeField]
        private MemoryLabyrinth.UI.Button.Button _button;

        [SerializeField]
        public event UnityAction<string> _inputAccept;

        public void Start()
        {
            _button._buttonClick += OnInputAccept;            
        }

        public void OnInputAccept()
        {
            _inputAccept?.Invoke(_textField.text);
        }

    }
}

