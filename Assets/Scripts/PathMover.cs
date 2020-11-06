using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathMover : MonoBehaviour
{
    [Header("Car")]    
    [SerializeField] CarMovement carMovement = null;

    public List<Vector3> points = new List<Vector3>();
    private LineRenderer _lineRenderer;
    private void Awake() 
    {
        _lineRenderer = transform.parent.gameObject.GetComponent<LineRenderer>();
        //_lineRenderer.transform.rotation = planeTransform.rotation;    
    }
    
    public void DrawLine(Vector3 point)
    {
        if(DistanceToLastPoint(point) > 1f)
        {   
            _lineRenderer.startWidth = 2f;
            _lineRenderer.endWidth = 2f;

            point.y = point.y + 0.01f;
            points.Add(point);           
            
            _lineRenderer.positionCount = points.Count;
            _lineRenderer.SetPositions(points.ToArray());
        }
    }

    private float DistanceToLastPoint(Vector3 point)
    {
        if(points.Count == 0)
        {
            return Mathf.Infinity;
        }

        return Vector3.Distance(points[points.Count - 1], point);
    }

    public void FollowPath()
    {
        carMovement.Restart();
        carMovement.FollowPath(points.ToArray());
    }

    public void Init()
    {   
        if(Vector3.Distance(carMovement.GetPosition(), transform.position) > 1f)
        {
            points.Clear();
            carMovement.Restart();
            ClearLines();
        }        
    }

    public void ClearLines()
    {
        _lineRenderer.positionCount = 0;
    }
}
