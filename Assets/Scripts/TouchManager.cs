using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class TouchManager : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    [SerializeField]
    private GameObject mazeParent;

    [SerializeField]
    private GameObject wallPrefab;

    private PlayerInput playerInput;

    private InputAction touchPositionAction;
    private InputAction touchPressAction;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        touchPressAction = playerInput.actions["TouchPress"];
        touchPositionAction = playerInput.actions["TouchPosition"];
    }

    private void OnEnable()
    {
        touchPressAction.performed += touchPressed;
        Debug.Log("Enabled!");
    }

    private void OnDisable()
    {
        touchPressAction.performed -= touchPressed;

        string localPath = "Assets/Prefabs/" + "Level" + ".prefab";
        bool ok;
        PrefabUtility.SaveAsPrefabAsset(mazeParent, localPath, out ok);
        if (ok)
            Debug.Log("Saved!");
        else
            Debug.Log("Failed to save!");

        Debug.Log("Disabled!");
    }

    private Vector3 lastPos;

    private void touchPressed(InputAction.CallbackContext context)
    {
        Debug.Log("Pressing!");
        /**
        //float value = context.ReadValue<fl/oat>();
        //Debug.Log(value);

        Vector3 touchPos = touchPositionAction.ReadValue<Vector2>();
        Debug.Log(touchPos);
        
        touchPos.z = player.transform.position.z - Camera.main.transform.position.z;
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(touchPos);

        //player.transform.position = worldPos;

        Debug.Log(worldPos);

        Vector3 raycastOrigin = worldPos;
        Vector3 raycastDir = worldPos - raycastOrigin;
        RaycastHit2D hit = Physics2D.Raycast(raycastOrigin, raycastDir);
        if (hit)
        {
            lastPos = hit.point;
            Vector3 gridPos = new Vector3((int)Mathf.Round(lastPos.x), (int)Mathf.Round(lastPos.y), (int)Mathf.Round(lastPos.z));
            Instantiate(wallPrefab, gridPos, Quaternion.identity, mazeParent.transform);
        }
        else
        {
            Debug.Log("Raycast missed");
        }

        /**/
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(lastPos, 0.3f);
    }

    private void touchPosition(InputAction.CallbackContext context)
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
