using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PathCreator : MonoBehaviour
{
    [SerializeField]
    private GameObject wallPrefab;

    [SerializeField]
    private GameObject pathPrefab;

    private PlayerInput playerInput;

    private InputAction touchPositionAction;
    private InputAction touchPressAction;

    Coroutine drawing;
    private GameObject lineObject;
    private LineRenderer line;

    List<Vector3> pathPoints = new List<Vector3>();

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        touchPressAction = playerInput.actions["TouchPress"];
        touchPositionAction = playerInput.actions["TouchPosition"];
    }

    private void OnEnable()
    {
        touchPressAction.performed += touchPressed;
        touchPressAction.canceled += touchLetGo;
        Debug.Log("Enabled Path Creator!");
    }

    private void OnDisable()
    {
        touchPressAction.performed -= touchPressed;
        touchPressAction.canceled -= touchLetGo;
    }

    private void touchPressed(InputAction.CallbackContext context)
    {
        Debug.Log("Pressing!");
        startDrawing();
    }

    private void touchLetGo(InputAction.CallbackContext context)
    {
        Debug.Log("Letting go!");
        stopDrawing();
    }

    private GameObject getObjectAtPosition(Vector3 mousePos)
    {
        Vector3 raycastOrigin = mousePos;
        Vector3 raycastDir = mousePos - raycastOrigin;
        RaycastHit2D hit = Physics2D.Raycast(raycastOrigin, raycastDir);
        if (hit)
        {
            if (hit.transform.gameObject.tag == "Wall" || hit.transform.gameObject.tag == "Path")
            {
                return hit.transform.gameObject;
            }
            else
            {
                return null;
            }
        }
        else
        {
            Debug.Log("Raycast missed");
            return null;
        }
    }

    private void startDrawing()
    {
        if (drawing != null)
        {
            StopCoroutine(drawing);
        }
        pathPoints.Clear();
        lineObject = Instantiate(Resources.Load("Line") as GameObject, new Vector3(0, 0, 0), Quaternion.identity);
        line = lineObject.GetComponent<LineRenderer>();
        line.positionCount = 0;
        drawing = StartCoroutine(drawLine());
    }


    private void stopDrawing()
    {
        StopCoroutine(drawing);
        Destroy(lineObject);
    }


    private void addPositionToPath(Vector3 newPos)
    {
        if (pathPoints.Contains(newPos))
        {
            int index = pathPoints.IndexOf(newPos);
            int countToRemove = pathPoints.Count - index;
            pathPoints.RemoveRange(index, countToRemove);
            line.positionCount -= countToRemove;
        }

        line.positionCount++;
        line.SetPosition(line.positionCount - 1, newPos);
        pathPoints.Add(newPos);
        Debug.Log("Added pos:");
        Debug.Log(newPos);
    }

    private IEnumerator drawLine()
    {
        while (true)
        {
            Vector3 screenCoords = touchPositionAction.ReadValue<Vector2>();
            screenCoords.z = 5;
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenCoords);
            worldPos.z = 5;

            Vector3 groundOffset = new Vector3(0, 0, -1);

            if (pathPoints.Count == 0)
            {
                GameObject obj = getObjectAtPosition(worldPos);
                if (obj != null && obj.tag == "Path") 
                { 
                    addPositionToPath(obj.transform.position + groundOffset);
                }
            }
            else
            {
                Vector2 lastPos = pathPoints[pathPoints.Count - 1];
                Vector2 dir = (Vector2)(worldPos) - lastPos;

                RaycastHit2D[] sphereCasts = Physics2D.RaycastAll(lastPos, dir, dir.magnitude);
                foreach (RaycastHit2D sphereCast in sphereCasts) 
                {
                    if (sphereCast)// hit happened
                    {
                        GameObject obj = sphereCast.transform.gameObject;
                        String tag = obj.tag;
                        Debug.Log(tag);
                        if (tag == "Path")
                        {
                            addPositionToPath(obj.transform.position + groundOffset);
                        }
                        else if (tag == "Wall")
                        {
                            break;
                        }
                    }
                }
            }
            yield return null;
        }
    }

    private void OnDrawGizmos()
    {
        foreach (Vector3 pos in pathPoints)
        {
            Gizmos.DrawSphere(pos, 0.2f);
        }
    }

}
