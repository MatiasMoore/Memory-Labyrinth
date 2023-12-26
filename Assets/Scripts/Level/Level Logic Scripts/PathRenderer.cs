using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MemoryLabyrinth.Level.Logic
{
    [RequireComponent(typeof(LineRenderer))]
    public class PathRenderer : MonoBehaviour
    {
        bool _isDrawing = false;
        bool _isRemoving = false;
        LineRenderer _lineRenderer;
        float _timeToDrawOnepPoint = 0f;
        float _timeToRemoveOnePoint = 0f;
        float _timer;
        List<Vector3> _path;
        Vector3 _currentPos;
        void Update()
        {
            if (_isDrawing && _path.Count > 0)
            {
                _timer += Time.unscaledDeltaTime;
                float lerp = _timer / _timeToDrawOnepPoint;

                Vector3 newPos = Vector3.Lerp(_currentPos, _path[0], lerp);
                _lineRenderer.positionCount++;
                _lineRenderer.SetPosition(_lineRenderer.positionCount - 1, newPos);

                if (lerp >= 1f)
                {
                    _timer = 0f;                 
                    _currentPos = _path[0];
                    _path.RemoveAt(0);
                    _isDrawing = _path.Count > 0;                
                }
            }

            if (_isRemoving && _lineRenderer.positionCount > 0)
            {
                _timer += Time.unscaledDeltaTime;
                int pointsToRemove = (int)(_timer / _timeToRemoveOnePoint);
                _timer -= pointsToRemove * _timeToRemoveOnePoint;
                if (_lineRenderer.positionCount - pointsToRemove > 0)
                {
                    _lineRenderer.positionCount -= pointsToRemove;
                } else
                {
                    _lineRenderer.positionCount = 0;
                    _isRemoving = false;
                }
            }
        }

        public void DrawPath(List<Vector3> path, float time)
        {
            if (path.Count < 2)
                return;
            _timer = 0;
            _path = new List<Vector3>(path);
            _currentPos = _path[0];
            _path.RemoveAt(0);
            _lineRenderer = GetComponent<LineRenderer>();
            _lineRenderer.positionCount = 1;
            _lineRenderer.SetPosition(0, _currentPos);
            _timeToDrawOnepPoint = time / _path.Count;
            _isDrawing = true;
        }

        public bool IsDrawing()
        {
            return _isDrawing;
        }

        public void RemoveLine(float time)
        {
            _timer = 0;
            _timeToRemoveOnePoint = time / _lineRenderer.positionCount;
            _isRemoving = true;
        }
    }
}

