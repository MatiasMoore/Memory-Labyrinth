using MemoryLabyrinth.Level.Editor;
using MemoryLabyrinth.Level.Objects.BonusLib;
using MemoryLabyrinth.Level.Objects.CheckpointLib;
using MemoryLabyrinth.Level.Objects.FinishLib;
using MemoryLabyrinth.Level.Objects.PathLib;
using MemoryLabyrinth.Level.Objects.StartpointLib;
using MemoryLabyrinth.Level.Objects.TeleportLib;
using MemoryLabyrinth.Level.Objects.TrapLib;
using MemoryLabyrinth.Level.Objects.WallLib;
using MemoryLabyrinth.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MemoryLabyrinth.Level.Editor
{
    public class BonusCreator : ConfigurableElementCreatorPrimitive
    {
        public BonusCreator(LevelPartsContainer container, LevelPartConfig config, InputField inputField) : base(container, config, inputField)
        {
        }

        public override bool CanBePlacedAtPos(Vector2 position)
        {
            var objsAtPos = _container.GetObjectsAtPos(position);

            bool noObjects = objsAtPos.Count == 0;
            bool isPathPresent = _container.ContainsPartAtPos<Path>(position);
            bool isWallPresent = _container.ContainsPartAtPos<Wall>(position);
            bool isTrapPresent = _container.ContainsPartAtPos<Trap>(position);
            bool isBonusPresent = _container.ContainsPartAtPos<Bonus>(position);
            bool isFinishPresent = _container.ContainsPartAtPos<FinishPoint>(position);
            bool isCheckpointPresent = _container.ContainsPartAtPos<Checkpoint>(position);
            bool isStartPresent = _container.ContainsPartAtPos<StartPoint>(position);
            bool isTeleportPresent = _container.ContainsPartAtPos<Teleport>(position);

            return isPathPresent && !isWallPresent && !isBonusPresent;
        }

        public override void ConfigurateElement(GameObject element)
        {
            _inputField.gameObject.SetActive(true);
            _inputField._inputAccept += SetUpData;
        }

        public void SetUpData(string data)
        {
            Bonus bonus = _object.GetComponent<Bonus>();
            int value = 0;
            bool success = int.TryParse(data, out value);
            if (success)
            {
                bonus.SetValue(value);            
                _inputField._inputAccept -= SetUpData;
                _inputField.gameObject.SetActive(false);

                //Create unique id
                List<Bonus> bonuses = _container.GetPartsOfType<Bonus>();
                List<int> ids = new List<int>();
                foreach (var item in bonuses)
                {
                    ids.Add(item.GetID());
                }
                int uniqueId = 0;
                while (ids.Contains(uniqueId))
                {
                    uniqueId++;
                }

                bonus.SetID(uniqueId);
                Debug.Log($"BonusCreator: place bonus with id = {uniqueId}, value = {value}");
            }
        }
    }
}

