using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InputManager : MonoBehaviour
{   
    [SerializeField] CarMovement[] carMovements = null;

    private RaycastHit _rayHit;
    private StartPoint _startPoint;
    private CarMovement _carMovement;
    private void Update()
    {
        Movement();                   
    }

    private void Movement()
    {
        if(Input.GetButtonDown("Fire1"))
        {   
            _startPoint = null;        
            if(SetRayHit())
            {
                if(_rayHit.transform.tag == "StartPoint")
                {
                    _startPoint = _rayHit.transform.gameObject.GetComponent<StartPoint>();
                    _startPoint.Init();
                    RestartCars();                  
                }
                else if(_rayHit.transform.tag == "Car")
                {
                    _carMovement = _rayHit.transform.gameObject.GetComponent<CarMovement>();
                    _startPoint = _carMovement.startPoint;
                    if(_carMovement.IsMoving())
                    {   
                        _startPoint.Init();
                        RestartCars();

                        _startPoint = null;
                    }
                    
                }
            }           
        }

        if(_startPoint != null)
        {
            if(Input.GetButton("Fire1"))
            {    
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if(Physics.Raycast(ray, out hit))
                {                       
                    if(hit.transform.tag == "Car")
                    {
                        _carMovement = hit.transform.gameObject.GetComponent<CarMovement>();
                        _startPoint.DrawLine(_carMovement.ReturnRealPos(hit.point));
                    }
                    else
                    {
                        _startPoint.DrawLine(hit.point);
                    }                    
                }              
                
            }
            else if(Input.GetButtonUp("Fire1"))
            {   
                // If there is a path             
                if(_startPoint.linePoints.Count > 1 && _startPoint.DistanceToLastPoint(_rayHit.transform.position) > 1f)
                {
                    RestartCars();
                    MakeCarsFollowPath();
                }             
            }
        }
    }

    private void RestartCars()
    {
        GameManager._instance.InitScene();
        foreach (var carMovement in carMovements)
        {
            carMovement.Restart();
        }
    }

    private void MakeCarsFollowPath()
    {
        foreach (var carMovement in carMovements)
        {   
            carMovement.waypoints = new List<Vector3>(carMovement.startPoint.linePoints);
            if(carMovement.waypoints.Count > 0)
            {   
                carMovement.FollowPath();
            }            
        }
    }

    private bool SetRayHit()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        if(Physics.Raycast(ray, out _rayHit))
        {
            return true;
        }

        return false;
    }    
}
