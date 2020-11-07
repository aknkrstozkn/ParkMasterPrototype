using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPoint : MonoBehaviour
{
    [Header("Car")]    
    [SerializeField] CarMovement carMovement = null;

    [Header("LineRenderer")]
    [SerializeField] float startWidth = 1.2f;
    [SerializeField] float endWidth = 1.2f;

    public List<Vector3> linePoints = new List<Vector3>();
    
    private LineRenderer _lineRenderer;

    private void Awake() 
    {
        _lineRenderer = transform.parent.gameObject.GetComponent<LineRenderer>();
        _lineRenderer.startWidth = startWidth;
        _lineRenderer.endWidth = endWidth; 
    }
    
    public void DrawLine(Vector3 point)
    {
        if(DistanceToLastPoint(point) > 1f)
        {
            SetPointToLists(point);
            
            _lineRenderer.positionCount = linePoints.Count;
            _lineRenderer.SetPositions(linePoints.ToArray());
        }
    }

    public void SetPointToLists(Vector3 point)
    {
        point.y = point.y + 0.01f;
        linePoints.Add(point);
        carMovement.waypoints.Add(point);
    }

    public float DistanceToLastPoint(Vector3 point)
    {
        if(linePoints.Count == 0)
        {
            return Mathf.Infinity;
        }

        return Vector3.Distance(linePoints[linePoints.Count - 1], point);
    }

    public void Init()
    {   
        if(Vector3.Distance(carMovement.GetPosition(), transform.position) > 1f)
        {
            linePoints.Clear();
            ClearLines();
        }        
    }

    public void ClearLines()
    {
        _lineRenderer.positionCount = 0;
    }
}
