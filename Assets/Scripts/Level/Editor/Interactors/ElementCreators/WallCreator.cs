using Level.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCreator : ElementCreatorPrimitive
{
    public WallCreator(LevelPartsContainer container, LevelPartConfig config) : base(container, config)
    {
    }

    public override bool CanBePlacedAtPos(Vector2 position)
    {
        return true;
    }
}
