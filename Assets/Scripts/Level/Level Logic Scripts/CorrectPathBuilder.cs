using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class CorrectPathRenderer : MonoBehaviour
{
    private ObjectMovementState _objectMovement;

    [SerializeField]
    private float _speed;

    [SerializeField]
    private List<GameObject> _rightPath;

    [SerializeField]
    private LineRenderer _lineRenderer;

    [SerializeField]
    private bool _isActive = false;

    private Transform _transform;

    void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _transform = GetComponent<Transform>();
        _lineRenderer.positionCount = 1;
        _lineRenderer.SetPosition(0, _transform.position);
    }
  
    void FixedUpdate()
    {
        if (_isActive){
            _objectMovement.Update(Time.fixedDeltaTime);
            _lineRenderer.positionCount++;
            _lineRenderer.SetPosition(_lineRenderer.positionCount - 1, _transform.position);

        } else {
            _lineRenderer.positionCount = 1;
            _lineRenderer.SetPosition(0, _transform.position);
        }
    }

    public bool IsFinished()
    {
        return _objectMovement.GetState() == ObjectMovementState.State.Stay && _speed != 0;
    }

    public void ShowRightPath(float speed)
    {
        _speed = speed;
        _objectMovement = new ObjectMovementState(
            GetComponent<Transform>(),
            GetComponent<Rigidbody2D>(),
            _speed
        );
        List<Vector3> rightPathPoints = new List<Vector3>();
        foreach (GameObject point in _rightPath)
        {
            rightPathPoints.Add(point.transform.position);
        }
        _objectMovement.FollowPath(rightPathPoints);
        _isActive = true;
    }

    public void SetActive(bool isActive)
    {
        _isActive = isActive;
    }
}
