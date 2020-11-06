using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LineDrawer : MonoBehaviour
{   
    [SerializeField] GameObject secondPointPref = null;
    private RaycastHit _rayHit;
    private PathMover _pathMover;
    private GameObject _secondPoint;
    
    private void Update()
    {
        Movement();                   
    }

    private void Movement()
    {
        if(Input.GetButtonDown("Fire1"))
        {   
            _pathMover = null;        
            if(SetRayHit())
            {
                if(_rayHit.transform.tag == "FirstPoint")
                {
                    
                    _pathMover = _rayHit.transform.gameObject.GetComponent<PathMover>();
                    
                    if(_secondPoint != null)
                    {
                        Destroy (_secondPoint);
                        _secondPoint = null;
                    }         
                                   
                    _pathMover.Init();
                    
                }
                else if(_rayHit.transform.tag == "SecondPoint")
                {
                    _pathMover = _rayHit.transform.gameObject.GetComponent<SecondPointMover>().pathMover;                    
                    Destroy (_secondPoint);
                    _secondPoint = null;
                }
            }           
        }

        if(_pathMover != null)
        {
            if(Input.GetButton("Fire1"))
            {    
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if(Physics.Raycast(ray, out hit))
                {                       
                    _pathMover.DrawLine(hit.point);
                }              
                
            }
            else if(Input.GetButtonUp("Fire1"))
            {   
                // If there is a path             
                if(_pathMover.points.Count > 1)
                {
                    _secondPoint = Instantiate(secondPointPref, _pathMover.points[_pathMover.points.Count - 1], secondPointPref.transform.rotation);
                    _secondPoint.GetComponent<SecondPointMover>().pathMover = _pathMover;
                    _pathMover.FollowPath();
                }             
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
