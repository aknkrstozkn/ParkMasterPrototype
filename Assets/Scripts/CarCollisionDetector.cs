using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarCollisionDetector : MonoBehaviour
{
    
    private CarMovement _carMovement;
    
    private void Awake() {
        _carMovement = transform.parent.gameObject.GetComponent<CarMovement>();
    }

    private void OnCollisionEnter(Collider other) 
    {
        if(other.transform.tag == "Car" || other.transform.tag == "Obstacle")
        {
            _carMovement.Crash(other.transform.position);
        }        
    }
}
