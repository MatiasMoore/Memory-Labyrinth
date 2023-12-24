using MemoryLabyrinth.Level.Objects;
using MemoryLabyrinth.Level.Objects.BonusLib;
using MemoryLabyrinth.Level.Objects.CheckpointLib;
using MemoryLabyrinth.Level.Objects.FinishLib;
using MemoryLabyrinth.Level.Objects.PathLib;
using MemoryLabyrinth.Level.Objects.StartpointLib;
using MemoryLabyrinth.Level.Objects.TeleportLib;
using MemoryLabyrinth.Level.Objects.Trap;
using MemoryLabyrinth.Level.Objects.WallLib;
using MemoryLabyrinth.SaveLoad;
using System.Collections.Generic;
using UnityEngine;

namespace MemoryLabyrinth.Level.Editor
{
    public class LevelPartsContainer
    {
        public struct LevelPartObjectWithType
        {
            public GameObject obj;
            public LevelPartType type;
            public LevelPartObjectWithType(GameObject newObj, LevelPartType newType)
            {
                obj = newObj;
                type = newType;
            }
        }

        private List<LevelPartObjectWithType> _parts;

        public List<LevelPartObjectWithType> GetAllParts() => _parts;

        public LevelPartsContainer()
        {
            _parts = new List<LevelPartObjectWithType>();
        }

        public void AddPart(LevelPartObjectWithType newPart)
        {
            if (_parts.Contains(newPart))
            {
                throw new System.Exception($"LevelPartsContainer: {newPart} already exist");
            }

            _parts.Add(newPart);
        }

        public void DeletePart(GameObject partObj)
        {
            foreach (var part in _parts)
            {
                if (part.obj.Equals(partObj))
                {
                    _parts.Remove(part);
                    return;
                }
            }
            throw new System.Exception($"LevelPartsContainer: {partObj} does not exist");
        }
        
        public List<T> GetPartsOfType<T>()
        {
            List<T> elements = new();
            foreach (var gameObject in GetAllParts())
            {
                T possibleElement = gameObject.obj.GetComponent<T>();
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
            levelData.parts.parts = new List<LevelPartStruct>();
            levelData.bonuses.bonuses = new Dictionary<int, BonusStruct>();
            levelData.walls.walls = new Dictionary<int, WallStruct>();
            levelData.paths.paths = new Dictionary<int, PathStruct>();
            levelData.teleports.teleports = new Dictionary<int, TeleportStruct>();
            levelData.traps.traps = new Dictionary<int, TrapStruct>();
            levelData.checkPoints.checkPoints = new Dictionary<int, CheckpointStruct>();
            levelData.startPoints.startPoints = new Dictionary<int, StartPointStruct>();
            levelData.finishPoints.finishPoints = new Dictionary<int, FinishPointStruct>();

            int id = 0;
            foreach (var part in GetAllParts())
            {
                levelData.parts.parts.Add(new LevelPartStruct(id, part.type));
                SaveClassFromObjAsStructWithId<Bonus, BonusStruct>(part.obj, levelData.bonuses.bonuses, id);
                SaveClassFromObjAsStructWithId<Wall, WallStruct>(part.obj, levelData.walls.walls, id);
                SaveClassFromObjAsStructWithId<Path, PathStruct>(part.obj, levelData.paths.paths, id);
                SaveClassFromObjAsStructWithId<Teleport, TeleportStruct>(part.obj, levelData.teleports.teleports, id);
                SaveClassFromObjAsStructWithId<Trap, TrapStruct>(part.obj, levelData.traps.traps, id);
                SaveClassFromObjAsStructWithId<Checkpoint, CheckpointStruct>(part.obj, levelData.checkPoints.checkPoints, id);
                SaveClassFromObjAsStructWithId<StartPoint, StartPointStruct>(part.obj, levelData.startPoints.startPoints, id);
                SaveClassFromObjAsStructWithId<FinishPoint, FinishPointStruct>(part.obj, levelData.finishPoints.finishPoints, id);
                id++;
            }
            return levelData;
            
        }

        public bool SaveClassFromObjAsStructWithId<ClassName, Struct>(GameObject obj, Dictionary<int, Struct> dict, int id)
        {
            var classComp = obj.GetComponent<ClassName>();
            if (classComp != null)
            {
                var structable = (IStructable<Struct>)classComp;
                dict.Add(id, structable.ToStruct());
                return true;
            }
            return false;
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

        public List<GameObject> GetObjectsAtPos(Vector2 position)
        {
            List<GameObject> objectsAtPos = new();
            foreach (var obj in GetAllParts())
            {
                if((Vector2)obj.obj.transform.position == position)
                {
                    objectsAtPos.Add(obj.obj);
                }
            }

            objectsAtPos.Sort((x, y) => x.transform.position.z.CompareTo(y.transform.position.z));
            return objectsAtPos;
        }

        public bool ContainsPartAtPos<T>(Vector2 pos)
        {
            List<GameObject> objectsAtPos = GetObjectsAtPos(pos);
            List<T> objectsOfType = GetPartsOfType<T>();
            foreach (var obj in objectsAtPos)
            {
                if (objectsOfType.Contains(obj.GetComponent<T>()))
                {
                    return true;
                }
            }
            return false;
        }
    }
}

