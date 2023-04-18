using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GoldManger : MonoBehaviour
{
    public static GoldManger Instance;
    
    public GameObject goldPrefab;

    public float spawnChance = 0.6f;

    public GameObject[,] goldArray;

    private void Awake()
    {
        Instance = this; 
    }
    private void Start()
    {
       goldArray = new GameObject[Grid.Instance.numRows,Grid.Instance.numCols];
       refreshCoins();
    }
    private void Update()
    {
        refreshCoins();
    }

    bool goldSpawnedAtUnitSquare(Vector3 goldPosition)
    {
        return Player.Instance.hero.transform.position == goldPosition || Player.Instance.enemy.transform.position == goldPosition;
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

                Vector3 powerUpPosition = new Vector3(col, row, -1f);
                if (row % 2 != 0 && col % 2 != 0 && col > 1 && col < 15 && !goldSpawnedAtUnitSquare(powerUpPosition))
                {
                    if (Random.value < spawnChance)
                    {
                                               
                        goldArray[row, col] = Instantiate(goldPrefab, powerUpPosition, Quaternion.identity);
                        goldArray[row, col].name = $"Coin {col} : {row}";

                    }
                }

            }
        }
    }
}