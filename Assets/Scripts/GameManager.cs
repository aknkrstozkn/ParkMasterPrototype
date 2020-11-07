using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] int ParkCount = 0;
    //[SerializeField] int ScoreCount = 0;
    [SerializeField] GameObject winText = null;
    [SerializeField] GameObject menu = null;
    [SerializeField] TextMeshProUGUI scoreText = null;
    [SerializeField] GameObject[] coins = null;

    public int parkedCount = 0;
    public int scoreCount = 0;

    public static GameManager _instance;
    
    void Awake()
    {
        if (_instance == null){

            _instance = this;            
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void InitScene()
    {
        foreach (var coin in coins)
        {
            coin.SetActive(true);
        }
        scoreCount = 0;
        parkedCount = 0;
    }

    void Update()
    {
        if(parkedCount == ParkCount)
        {
            //EndGame();
            winText.SetActive(true);
            menu.SetActive(true);
        }
        scoreText.text = "Score: " + scoreCount.ToString();

    }
}
