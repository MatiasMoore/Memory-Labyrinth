using MemoryLabyrinth.Level.Objects.CorrectPathLib;
using MemoryLabyrinth.SaveLoad;
using MemoryLabyrinth.SaveLoad.Saveable;
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

            int id = 0;
            foreach (var part in GetAllParts())
            {
                var saveable = part.obj.GetComponent<SaveablePrimitive>();
                if (saveable == null)
                    throw new System.Exception("Object must have a saveable primitive!");

                levelData.parts.parts.Add(new LevelPartStruct(id, part.type, new Vec3(part.obj.transform.position), saveable.SaveToString()));
                id++;
            }
            return levelData;
            
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

        public List<Vector3> GetCorrectPath()
        {
            List<CorrectPath> correctPaths = GetPartsOfType<CorrectPath>();
            List<Vector3> correctPath = new();
            foreach (var correctPathItem in correctPaths)
            {
                correctPath.Add(correctPathItem.transform.position);
            }
            return correctPath;
        }

        public void ClearAll()
        {
            foreach (var item in _parts)
            {              
                Object.Destroy(item.obj);
            }
            _parts.Clear();
        }
    }
}

