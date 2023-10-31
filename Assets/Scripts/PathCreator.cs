using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(LineRenderer))]

public class PathCreator : MonoBehaviour
{
    Coroutine _drawingPath;
    private LineRenderer _line;

    List<Vector3> _pathPoints = new List<Vector3>();

    private void Start()
    {
        TouchControls.Instance.addCallbackToTouchDown(touchPressed);
        TouchControls.Instance.addCallbackToTouchUp(touchLetGo);
        _line = GetComponent<LineRenderer>();
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
        if (_drawingPath != null)
        {
            StopCoroutine(_drawingPath);
        }
        _pathPoints.Clear();
        _line.positionCount = 0;
        _drawingPath = StartCoroutine(drawLine());
    }


    private void stopDrawing()
    {
        StopCoroutine(_drawingPath);
        _line.positionCount = 0;
    }


    private void addPositionToPath(Vector3 newPos)
    {
        if (_pathPoints.Contains(newPos))
        {
            int index = _pathPoints.IndexOf(newPos);
            int countToRemove = _pathPoints.Count - index;
            _pathPoints.RemoveRange(index, countToRemove);
            _line.positionCount -= countToRemove;
        }

        _line.positionCount++;
        _line.SetPosition(_line.positionCount - 1, newPos);
        _pathPoints.Add(newPos);
        Debug.Log("Added pos:");
        Debug.Log(newPos);
    }

    private IEnumerator drawLine()
    {
        while (true)
        {
            Vector3 screenCoords = TouchControls.Instance.getTouchPosition();
            screenCoords.z = 5;
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenCoords);
            worldPos.z = 5;

            Vector3 groundOffset = new Vector3(0, 0, -1);

            if (_pathPoints.Count == 0)
            {
                GameObject obj = getObjectAtPosition(worldPos);
                if (obj != null && obj.tag == "Path") 
                { 
                    addPositionToPath(obj.transform.position + groundOffset);
                }
            }
            else
            {
                Vector2 lastPos = _pathPoints[_pathPoints.Count - 1];
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
        foreach (Vector3 pos in _pathPoints)
        {
            Gizmos.DrawSphere(pos, 0.2f);
        }
    }

}
