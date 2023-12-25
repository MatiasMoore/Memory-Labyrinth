using MemoryLabyrinth.Level.Editor;
using MemoryLabyrinth.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using InputField = MemoryLabyrinth.UI.InputField;

namespace MemoryLabyrinth.Level.Editor
{
    public abstract class ConfigurableElementCreatorPrimitive : ElementCreatorPrimitive
    {
        protected InputField _inputField;
        public ConfigurableElementCreatorPrimitive(LevelPartsContainer container, LevelPartConfig config, InputField inputField) : base(container, config)
        {
            _inputField = inputField;
            objectPlaced += (GameObject element) => ConfigurateElement(element);
        }

        public abstract void ConfigurateElement(GameObject element);
    }
}

