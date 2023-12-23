using Level.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementCreatorPrimitive : InteractorPrimitive
{
    private LevelPartConfig _config;

    public override void InteractAtPos(Vector2 position)
    {
        
    }

    public void CreateAsGameObjectAtPos(Vector2 position)
    {
        Vector3 posWithDepth = position;
        posWithDepth.z = _config.zDepth;

        Object.Instantiate(_config.prefab, posWithDepth + _config.offset, Quaternion.identity);
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

    public bool CanBePlacedAtPos(Vector2 position)
    {
        return Physics2D.OverlapPoint(position) == null;
    }
}
