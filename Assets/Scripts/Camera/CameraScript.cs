using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField]
    public bool fitOnStartup = true;

    [SerializeField]
    public bool fitOnUpdate = true;

    [SerializeField]
    private Vector3 _padding = Vector3.zero;

    [SerializeField]
    private Vector3 _centerOffset = Vector3.zero;

    private Camera _cam;

    public void Init()
    {
        _cam = GetComponent<Camera>();

        if (fitOnStartup)
            FitObjectsInsideCam(FindObjectsOfType<GameObject>(), _padding);
    }

    public void FitObjectsInsideCam(GameObject[] objsToFit, Vector3 padding)
    {
        //Create bounds and add every object's bound inside it
        Bounds bounds = new Bounds();
        foreach (GameObject obj in objsToFit)
        {
            // Egor: in order to camera does not scale relative to UI objects
            if (obj.tag != "UI")
            {
                Collider2D collider2D = obj.GetComponent<Collider2D>();
                Collider collider = obj.GetComponent<Collider>();

                if (collider2D != null)
                    bounds.Encapsulate(collider2D.bounds);
                else if (collider != null)
                    bounds.Encapsulate(collider.bounds);
            }
        }

        //Add edge padding
        bounds.Expand(padding);

        float vertical = bounds.size.y;
        float horizontal = bounds.size.x * _cam.pixelHeight / _cam.pixelWidth;

        float size = Mathf.Max(horizontal, vertical) * 0.5f;
        Vector3 center = bounds.center + _centerOffset;
        center.z = _cam.transform.position.z;

        if (size > 0)
        {
            _cam.transform.position = center;
            _cam.orthographicSize = size;
        }
    }

    private void Update()
    { 
        if (fitOnUpdate)
            FitObjectsInsideCam(FindObjectsOfType<GameObject>(), _padding);
    }
}
