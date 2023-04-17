using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GoldManger : MonoBehaviour
{
    public static GoldManger Instance;
    
    public GameObject powerUpPrefab;

    public GameObject[,] powerUps;

    private void Awake()
    {
        Instance = this; 
    }
    private void Start()
    {
       powerUps = new GameObject[9,17];
       refreshCoins();
    }
    private void Update()
    {
        refreshCoins();
    }
    public void refreshCoins()
    {
        var gold = GameObject.FindGameObjectsWithTag("Gold");
        var squares = GameObject.FindGameObjectsWithTag("Player");

       

        if (gold.Length == 0)
        {
            foreach (var square in squares)
            {
                var row = (int)square.transform.position.y;
                var col = (int)square.transform.position.x;

                if (row % 2 != 0 && col % 2 != 0 && col > 1 && col < 15)
                {
                    if (Random.value < 0.15f)
                    {
                        
                        Vector3 powerUpPosition = new Vector3(col, row, -1f);
                        powerUps[row, col] = Instantiate(powerUpPrefab, powerUpPosition, Quaternion.identity);
                        powerUps[row, col].name = $"Coin {col} : {row}";

                    }
                }

            }
        }
    }
}