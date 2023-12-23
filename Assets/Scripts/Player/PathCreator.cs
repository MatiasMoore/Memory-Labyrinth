using MemoryLabyrinth.Controls;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MemoryLabyrinth.Path
{
    [RequireComponent(typeof(LineRenderer))]
    public class PathCreator : MonoBehaviour
    {
        Coroutine _drawingPath;
        private LineRenderer _line;
        private Transform _playerTransform;

        //Final list of path points
        private List<Vector3> _pathPoints = new List<Vector3>();

        private bool _isActive;

        [SerializeField]
        private float _maxDistanceToAddPoints = 50;

        //Property to check if the path is ready
        public bool isNewPathReady { get; private set; }

        public static PathCreator Instance { get; private set; }

        public bool IsPathBeingDrawn()
        {
            return _drawingPath != null;
        }

        public List<Vector3> GetNewPath()
        {
            isNewPathReady = false;
            return _pathPoints;
        }

        public void SetActive(bool isActive)
        {
            _isActive = isActive;
            if (!isActive)
            {
                if (_drawingPath != null)
                    StopCoroutine(_drawingPath);
                _drawingPath = null;
                _line.positionCount = 0;
                isNewPathReady = false;
            }
        }

        public void Init()
        {
            _isActive = true;

            if (TouchControls.Instance == null)
            {
                Debug.Log("TouchControls is not initialized");
            }
            else
            {
                //Functions need to be called on up/down touch events
                TouchControls.Instance.touchDown += StartDrawing;
                TouchControls.Instance.touchUp += StopDrawing;
            }

            _line = GetComponent<LineRenderer>();
            _playerTransform = GetComponent<Transform>();
            isNewPathReady = false;
            _line.positionCount = 0;

            if (Instance != null) return;
            Instance = this;
        }

        private void StartDrawing()
        {
            if (!_isActive || _drawingPath != null)
                return;

            //Don't start drawing a path if the player is not touching a path
            Vector2 touchWorldPos = TouchControls.Instance.getTouchWorldPosition2d();
            Collider2D colliderTouchedByPlayer = Physics2D.OverlapPoint(touchWorldPos, 1 << LayerMask.NameToLayer("Path"));
            if (colliderTouchedByPlayer == null)
                return;

            _pathPoints.Clear();
            _line.positionCount = 0;

            //The first position is player's path tile
            Collider2D startingPathCollider = Physics2D.OverlapPoint(_playerTransform.position, 1 << LayerMask.NameToLayer("Path"));
            if (startingPathCollider == null)
            {
                throw new Exception("Player is not standing on a path tile. Can't start creating a path. Please put the player on a path tile.");
            }
            Vector3 firstPos = startingPathCollider.gameObject.transform.position;
            firstPos.z = _playerTransform.position.z;
            AddPositionToPath(firstPos);
            //Starts the main coroutine
            _drawingPath = StartCoroutine(DrawPath());
        }


        private void StopDrawing()
        {
            if (!_isActive || _drawingPath == null)
                return;

            StopCoroutine(_drawingPath);
            _drawingPath = null;
            _line.positionCount = 0;
            isNewPathReady = _pathPoints.Count > 1;
        }

        private void AddPositionToPath(Vector3 newPos)
        {
            //Prevents loops
            if (_pathPoints.Contains(newPos))
            {
                //if it's the last point just don't do anything
                if (_pathPoints[_pathPoints.Count - 1] == newPos)
                    return;

                //Remove all points after the duplicate(including the duplicate)
                int index = _pathPoints.IndexOf(newPos);
                int countToRemove = _pathPoints.Count - index;
                _pathPoints.RemoveRange(index, countToRemove);
                _line.positionCount -= countToRemove;
            }

            //Draws new line position and adds point to list
            _line.positionCount++;
            _line.SetPosition(_line.positionCount - 1, newPos);
            _pathPoints.Add(newPos);

            Debug.Log("Added pos: " + newPos + " to path");
        }

        private IEnumerator DrawPath()
        {
            while (true)
            {
                //Player touch pos
                Vector2 touchWorldPos = TouchControls.Instance.getTouchWorldPosition2d();
                float distToPlayer = (touchWorldPos - (Vector2)_playerTransform.position).magnitude;

                //Raycast from last path point to player touch position
                Vector2 lastPos = _pathPoints[_pathPoints.Count - 1];
                Vector2 dir = touchWorldPos - lastPos;
                RaycastHit2D[] castResults = Physics2D.RaycastAll(lastPos, dir, dir.magnitude);

                //Check every result
                foreach (RaycastHit2D sphereCast in castResults)
                {
                    if (sphereCast)// hit happened
                    {
                        GameObject hitObj = sphereCast.transform.gameObject;
                        String tag = hitObj.tag;

                        //Add every path's position to the path
                        if (tag == "Path")
                        {
                            Vector3 posToAdd = hitObj.transform.position;
                            posToAdd.z = _playerTransform.position.z;

                            //Stop the player from drawing too far away from the starting point
                            if (_pathPoints.Count > 0)
                            {
                                float distToFirstPoint = ((Vector2)_pathPoints[0] - (Vector2)posToAdd).magnitude;
                                if (distToFirstPoint > _maxDistanceToAddPoints)
                                    break;
                            }

                            AddPositionToPath(posToAdd);
                        }
                        //Stop as soon as a wall is hit
                        else if (tag == "Wall")
                        {
                            break;
                        }
                    }
                }

                //Wait for next frame
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

}

