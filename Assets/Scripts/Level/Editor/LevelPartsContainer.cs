using MemoryLabyrinth.Level.Objects.BonusLib;
using MemoryLabyrinth.Level.Objects.CheckpointLib;
using MemoryLabyrinth.Level.Objects.Wall;
using MemoryLabyrinth.SaveLoad;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Pool;

namespace MemoryLabyrinth.Level.Editor
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
            levelData.bonuses = GetBonusListStruct();
            levelData.walls = GetWallListStruct();
       /*   TODO:
            public string name;
            public PathListStruct paths;
            public TeleportListStruct teleports;
            public TrapListStruct traps;
            public CheckpointListStruct checkPoints;
            public StartPointListStruct startPoints;
            public FinishPointListStruct finishPoints;
        } 
       */
            return levelData;
            
        }

        public WallListStruct GetWallListStruct()
        {
            List<WallStruct> wallList = new();
            List<Wall> walls = GetPartsOfType<Wall>();

            foreach (var wall in walls)
            {
                wallList.Add(wall.ToStruct());
            }

            return new WallListStruct() { walls = wallList };
        }
        public BonusListStruct GetBonusListStruct()
        {
            List<BonusStruct> bonusList = new();
            List<Bonus> bonuses = GetPartsOfType<Bonus>();

            foreach (var bonus in bonuses)
            {
                bonusList.Add(bonus.ToStruct());
            }

            return new BonusListStruct() { bonuses = bonusList };
        }
    }
}

