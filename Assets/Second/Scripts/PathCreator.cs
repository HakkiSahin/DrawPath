using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;

public class PathCreator : MonoBehaviour
{
    private LineRenderer _lineRenderer;

    private List<Vector3> points = new List<Vector3>();
    [SerializeField] private GameObject obj;

    public Action<IEnumerable<Vector3>> OnNewPathCreated = delegate { };
    // Start is called before the first frame update

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.tag == "Plane")
                {
                    if (DistanceToLastPoint(hit.point) > 1f)
                    {
                        points.Add(hit.point);
                        _lineRenderer.positionCount = points.Count;
                        _lineRenderer.SetPositions(points.ToArray());
                    }
                }
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            OnNewPathCreated(points);
        }

        if (Input.GetKey(KeyCode.A))
        {
            int objCount = 0;
            obj.transform.position = Vector3.Lerp(obj.transform.position, points[objCount], Time.deltaTime);
            Debug.Log("Obj Count: " + objCount + " " + Vector3.Distance(obj.transform.position, points[objCount]));
            if (Vector3.Distance(obj.transform.position, points[objCount]) < 0.01f)
            {
                objCount++;
            }
        }
    }

    private float DistanceToLastPoint(Vector3 hitPoint)
    {
        if (!points.Any())
            return Mathf.Infinity;
        return Vector3.Distance(points.Last(), hitPoint);
    }
}