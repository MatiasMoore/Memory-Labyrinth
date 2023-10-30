using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class LevelEditor : MonoBehaviour
{
    [SerializeField]
    private GameObject canvas;

    [SerializeField]
    private GameObject parent;

    [SerializeField]
    private GameObject wallPrefab;

    [SerializeField]
    private GameObject pathPrefab;

    [SerializeField]
    private GameObject enemyPrefab;

    [SerializeField]
    private GameObject bonusPrefab;

    [SerializeField]
    private GameObject checkpointPrefab;

    private PlayerInput playerInput;

    //Buttons
    private InputAction addWallAction;
    private InputAction addPathAction;
    private InputAction addEnemyAction;
    private InputAction addBonusAction;
    private InputAction addCheckpointAction;

    //Positions
    private InputAction mousePositionAction;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();

        addWallAction = playerInput.actions["AddWall"];
        addPathAction = playerInput.actions["AddPath"];
        addEnemyAction = playerInput.actions["AddEnemy"];
        addBonusAction = playerInput.actions["AddBonus"];
        addCheckpointAction = playerInput.actions["AddCheckpoint"];

        mousePositionAction = playerInput.actions["MousePosition"];
    }

    private void OnEnable()
    {
        addWallAction.performed += addWall;
        addPathAction.performed += addPath;
        addEnemyAction.performed += addEnemy;
        addBonusAction.performed += addBonus;
        addCheckpointAction.performed += addCheckpoint;
        Debug.Log("Enabled editor!");
    }

    private void OnDisable()
    {
        addWallAction.performed -= addWall;
        addPathAction.performed -= addPath;
        addEnemyAction.performed -= addEnemy;
        addBonusAction.performed -= addBonus;
        addCheckpointAction.performed -= addCheckpoint;

        PrefabUtility.SaveAsPrefabAsset(parent, "Assets/Prefabs/Levels/Level.prefab");

        Debug.Log("Disabled editor!");
    }

    private Vector3 getMousePosAllignedWithGrid()
    {
        Vector3 mousePos = mousePositionAction.ReadValue<Vector2>();
        mousePos.z = 3 - Camera.main.transform.position.z;

        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
        worldPos.z = -1;

        return new Vector3((int)Mathf.Round(worldPos.x), (int)Mathf.Round(worldPos.y), (int)Mathf.Round(worldPos.z));
    }

    private GameObject getObjectAtPosition(Vector3 mousePos)
    {
        Vector3 raycastOrigin = mousePos;
        Vector3 raycastDir = mousePos - raycastOrigin;
        RaycastHit2D hit = Physics2D.Raycast(raycastOrigin, raycastDir);
        if (hit)
        {
            if (hit.transform.gameObject == this.canvas)
                return null;
            else
            {
                Debug.Log("hit prefab");
                return hit.transform.gameObject;
            }
        }
        else
        {
            return null;
            Debug.Log("Raycast missed");
        }
    }

    private void addWall(InputAction.CallbackContext context)
    {
        Vector3 mouseGridPos = getMousePosAllignedWithGrid();
        GameObject prevObject = getObjectAtPosition(mouseGridPos);
        if (prevObject == null || prevObject.GetPrefabDefinition() == pathPrefab)
        {
            if (prevObject != null)
                Destroy(prevObject);
            GameObject prefab = PrefabUtility.InstantiatePrefab(wallPrefab) as GameObject;
            prefab.transform.parent = parent.transform;
            prefab.transform.position = mouseGridPos;
            Debug.Log("Added wall");
        }
    }

    private void addPath(InputAction.CallbackContext context)
    {
        Vector3 mouseGridPos = getMousePosAllignedWithGrid();
        if (getObjectAtPosition(mouseGridPos) == null)
        {
            GameObject prefab = PrefabUtility.InstantiatePrefab(pathPrefab) as GameObject;
            prefab.transform.parent = parent.transform;
            prefab.transform.position = mouseGridPos;
            Debug.Log("Added path");
        }
    }

    private void addEnemy(InputAction.CallbackContext context)
    {
        Debug.Log("Added enemy");

    }

    private void addBonus(InputAction.CallbackContext context)
    {
        Debug.Log("Added bonus");

    }

    private void addCheckpoint(InputAction.CallbackContext context)
    {
        Debug.Log("Added checkpoint");

    }

}
