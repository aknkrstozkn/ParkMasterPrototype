using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerControl : MonoBehaviour
{
    [SerializeField] GameObject car = null;
    [SerializeField] Color color = Color.white;

    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject == car)
        {
            car.GetComponent<CarMovement>().Move(transform.position);
        }
    }
}
