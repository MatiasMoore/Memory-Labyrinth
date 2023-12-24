using MemoryLabyrinth.Level.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ElementCreatorPrimitive : InteractorPrimitive
{
    private LevelPartConfig _config;

    private LevelPartsContainer _container;

    public ElementCreatorPrimitive(LevelPartsContainer container, LevelPartConfig config)
    {
        _container = container;
        _config = config;
    }
    public override void InteractAtPos(Vector2 position)
    {
        if (CanBePlacedAtPos(position))
        {
            GameObject newPart = CreateAsGameObjectAtPos(position);
            _container.AddPart(newPart);
        } else
        {
            Debug.Log($"ElementCreatorPrimitive: can't create {_config.type} at {position}");
        }
    }

    public GameObject CreateAsGameObjectAtPos(Vector2 position)
    {
        Vector3 posWithDepth = position;
        posWithDepth.z = _config.zDepth;

        return Object.Instantiate(_config.prefab, posWithDepth + _config.offset, Quaternion.identity);
    }

    public List<GameObject> GetAllObjectsAtPos(Vector2 position)
    {
        var colls = Physics2D.OverlapPointAll(position);
        List<GameObject> objects = new List<GameObject>();
        foreach (var coll in colls)
        {
            objects.Add(coll.gameObject);
        }
        return objects;
    }

    public abstract bool CanBePlacedAtPos(Vector2 position);
   
}
