using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CarMovement : MonoBehaviour
{
    [SerializeField] float speed = 0.1f;
    private Quaternion _startRotation;
    private Vector3 _startPosition;

    private void Awake() {
        _startRotation = transform.rotation;
        _startPosition = transform.position;
    }

    public void FollowPath(Vector3[] waypoints)
    {
        float duration = waypoints.Length * speed;
        transform.DOPath(waypoints, duration).SetLookAt(0.01f);
    }

    public void Move(Vector3 point)
    {
        transform.DOMove(point, speed);
    }

    public void Restart()
    {
        StopMoving();
        ReturnStart();
        transform.rotation = _startRotation;
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public void StopMoving()
    {
        DOTween.Kill(transform);
    }

    public void ReturnStart()
    {
        var waypoints = new Vector3[]{transform.position, _startPosition};
        transform.DOPath(waypoints, speed);
    }

    
}
