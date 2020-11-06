using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] int ParkCount = 0;
    
    public static int parkedCount = 0;
    
    void Start()
    {
        
    }

    
    void Update()
    {
        if(parkedCount == ParkCount)
        {
            //EndGame();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        } 
    }
}
