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

    bool UnitNotAt(Vector3 objectPosition)
    {
        var units = GameObject.FindGameObjectsWithTag("Unit");
        int n = 0;
        foreach( var unit in units)
        {
           if( unit.transform.position == objectPosition)
            {
                n++;
            }
        }
        return n == 0;
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

                Vector3 goldPosition = new Vector3(col, row, -1f);
                if (row % 2 != 0 && col % 2 != 0 && col > 3 && col < 15 && UnitNotAt(goldPosition))
                {
                    if (Random.value < spawnChance)
                    {
                                               
                        goldArray[row, col] = Instantiate(goldPrefab, goldPosition, Quaternion.identity);
                        goldArray[row, col].name = $"Coin {col} : {row}";

                    }
                }

            }
        }
    }
}