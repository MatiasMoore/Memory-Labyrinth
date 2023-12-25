using MemoryLabyrinth.Level.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MemoryLabyrinth.Level.Editor
{
    public abstract class ConfigurableElementCreatorPrimitive : ElementCreatorPrimitive
    {
        public ConfigurableElementCreatorPrimitive(LevelPartsContainer container, LevelPartConfig config) : base(container, config)
        {
            objectPlaced += ConfigurateElement;
        }

        public abstract void ConfigurateElement(GameObject element);
    }
}

