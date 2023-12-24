using MemoryLabyrinth.Level.Objects;
using MemoryLabyrinth.Level.Objects.BonusLib;
using MemoryLabyrinth.Level.Objects.CheckpointLib;
using MemoryLabyrinth.Level.Objects.FinishLib;
using MemoryLabyrinth.Level.Objects.Path;
using MemoryLabyrinth.Level.Objects.StartpointLib;
using MemoryLabyrinth.Level.Objects.TeleportLib;
using MemoryLabyrinth.Level.Objects.Trap;
using MemoryLabyrinth.Level.Objects.WallLib;
using MemoryLabyrinth.SaveLoad;
using System.Collections.Generic;
using UnityEngine;

namespace Level.Editor
{
    public class LevelPartsContainer
    {
        private List<GameObject> _parts;

        public List<GameObject> GetAllParts() => _parts;

        public LevelPartsContainer()
        {
            _parts = new List<GameObject>();
        }

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

            if (_parts.Contains(part))
            {
                _parts.Remove(part);
            } else
            {
                throw new System.Exception($"LevelPartsContainer: {part} does not exist");
            }

        }

        public List<T> GetPartsOfType<T>()
        {
            List<T> elements = new();
            foreach (var gameObject in GetAllParts())
            {
                T possibleElement = gameObject.GetComponent<T>();
                if (possibleElement != null)
                {
                    elements.Add(possibleElement);
                }
            }
            return elements;
        }

        public LevelData ToLevelData()
        {
            LevelData levelData = new LevelData();
            levelData.bonuses.bonuses = GetStructList<Bonus, BonusStruct>();
            levelData.walls.walls = GetStructList<Wall, WallStruct>();
            levelData.paths.paths = GetStructList<Path, PathStruct>();
            levelData.teleports.teleports = GetStructList<Teleport, TeleportStruct>();
            levelData.traps.traps = GetStructList<Trap, TrapStruct>();
            levelData.checkPoints.checkPoints = GetStructList<Checkpoint, CheckpointStruct>();
            levelData.startPoints.startPoints = GetStructList<StartPoint, StartPointStruct>();
            levelData.finishPoints.finishPoints = GetStructList<FinishPoint, FinishPointStruct>();
            return levelData;
            
        }

        public List<Struct> GetStructList<ClassName, Struct>()
        {
            var objList = GetPartsOfType<ClassName>();
            List<Struct> structList = new List<Struct>();
            foreach (var obj in objList)
            {
                IStructable<Struct> structable = (IStructable<Struct>)obj;
                structList.Add(structable.ToStruct());
            }
            return structList;
        }
    }
}

