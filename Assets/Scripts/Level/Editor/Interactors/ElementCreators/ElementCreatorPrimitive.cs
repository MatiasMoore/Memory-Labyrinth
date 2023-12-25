using MemoryLabyrinth.Level.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static MemoryLabyrinth.Level.Editor.LevelPartsContainer;

public abstract class ElementCreatorPrimitive : InteractorPrimitive
{
    protected LevelPartConfig _config;

    protected LevelPartsContainer _container;

    protected event UnityAction<GameObject> objectPlaced;

    protected GameObject _object;

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
            _container.AddPart(new LevelPartObjectWithType(newPart, _config.type));
            _object = newPart;
            objectPlaced?.Invoke(newPart);
        } else
        {
            Debug.Log($"ElementCreatorPrimitive: can't create {_config.type} at {position}");
        }
    }

    protected GameObject CreateAsGameObjectAtPos(Vector2 position)
    {
        Vector3 posWithDepth = position;
        posWithDepth.z = _config.zDepth;

        return Object.Instantiate(_config.prefab, posWithDepth + _config.offset, Quaternion.identity);
    }

    public abstract bool CanBePlacedAtPos(Vector2 position);
   
}
