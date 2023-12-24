using Level.Editor;
using MemoryLabyrinth.Controls;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LevelEditor : MonoBehaviour
{
    private LevelPartsContainer _container;
    private InteractorPrimitive _interactor;
    [SerializeField]
    private LevelParts _levelPartsDataBase;

    public void Init(TouchControls touchControls)
    {
        touchControls.touchDown += OnPlayerTouch;
        _container = new LevelPartsContainer();
        StartCreatingLevelPart(LevelPartType.Wall);

    }

    private void OnPlayerTouch()
    {
        Vector2 pos = TouchControls.Instance.getTouchWorldPosition2d();
        _interactor.InteractAtPos(pos);

    }

    [ContextMenu("Start Creating Wall")]
    public void StartCreatingWall()
    {
        StartCreatingLevelPart(LevelPartType.Wall);
    }

    public void StartCreatingLevelPart(LevelPartType type)
    {
        LevelPartConfig config = _levelPartsDataBase.GetConfigByType(type);
        switch (type)
        {
            case LevelPartType.Wall:
                _interactor = new WallCreator(_container, config);
                break;
            case LevelPartType.Path:
                break;
            case LevelPartType.Trap:
                break;
            case LevelPartType.Checkpoint:
                break;
            case LevelPartType.Startpoint:
                break;
            case LevelPartType.Finishpoint:
                break;
            case LevelPartType.Bonus:
                break;
            case LevelPartType.Teleport:
                break;
            default:
                break;
        }
    }

    [ContextMenu("Start Destroy")]
    public void StartDestroy()
    {
        _interactor = new DestroyFirstElement(_container);
    }

}
