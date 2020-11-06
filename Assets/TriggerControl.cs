using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TriggerControl : MonoBehaviour
{
    [SerializeField] GameObject car = null;
    [SerializeField] Color color = Color.white;

    private void OnTriggerEnter(Collider other) 
    {
        //var carMovement = car.GetComponent<CarMovement>();
        //carMovement.Crash(transform.position);
        
        if(other.gameObject == car)
        {     
            var carMovement = car.GetComponent<CarMovement>();
            if(!carMovement.IsCrashed())
            {                
                Vector3[] points = new Vector3[]{carMovement.waypoints[carMovement.waypoints.Count - 1], transform.position};
                carMovement.StopMoving();
                carMovement.FollowPath(points);
                
                GameManager.parkedCount++;
            }
        }
        
    }
}
