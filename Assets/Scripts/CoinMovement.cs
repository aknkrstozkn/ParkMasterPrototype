using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CoinMovement : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 180f;

    private void Update() 
    {
        transform.eulerAngles += new Vector3(0f, rotationSpeed * Time.deltaTime, 0f);
    }

    private void OnTriggerEnter(Collider other) 
    {        
        if(other.transform.tag == "Car")
        {   
            var carMovement = other.transform.gameObject.GetComponent<CarMovement>();
            if(!carMovement.IsCrashed())
            {
                gameObject.SetActive(false);
                GameManager._instance.scoreCount++;
            }
        }
        
    }
}
