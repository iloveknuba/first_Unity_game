using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldManger : MonoBehaviour
{
    public GameObject powerUpPrefab;
    private GameObject[,] powerUps;
   
   

    public NewBehaviourScript fromGrid;
    public void CreateGold(Vector3 vec, int row, int col)
    {
            
        // Add a power-up to some of the squares
        if (Random.Range(0f, 1f) < 0.1f)
        {
            Vector3 powerUpPosition = new Vector3(vec.x, vec.y, -1f);
            powerUps[row, col] = Instantiate(powerUpPrefab, powerUpPosition, Quaternion.identity);
            powerUps[row, col].name = $"Coin {col} : {row}";
        }
    }
    private void Start()
    {
        powerUps = new GameObject[fromGrid.numCols, fromGrid.numRows];
       
    }
    public bool HasPowerUp(int row, int col)
    {
        // Check if the clicked square has a power-up GameObject
        if (powerUps[row, col] != null)
        {
            return true;
        }

        return false;
    }

   public  void RemovePowerUp(int row, int col)
    {
        // Remove the power-up GameObject from the grid and destroy it
        Destroy(powerUps[row, col]);


    }

    
}
