using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public GameObject heroPrefab;
    public ScoreManager scoreText;

    private GameObject[,] powerUps;
    private GameObject hero;

    
    
   public Player(GameObject[,] powerUps)
    {
        this.powerUps = powerUps;
    }

    private void Start()
    {
        hero = Instantiate(heroPrefab, new Vector3(0, 0, -1f), Quaternion.identity);
       
    }
    void Update()
    {

        EndGame();


        if (Input.GetMouseButtonDown(0))
        {

            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);


            int row = Mathf.RoundToInt(hit.collider.gameObject.transform.position.y - transform.position.y);
            int col = Mathf.RoundToInt(hit.collider.gameObject.transform.position.x - transform.position.x);

            // Check if the ray intersects with a square GameObject
            if (hit.collider != null && hit.collider.gameObject.tag == "Player")
            {



                // Get the row and column of the square that was clicked on

                Debug.Log($"Coordinates of clicked square: {row}{col}");

                // Check if the clicked square is adjacent to the hero GameObject
                if (IsNearToHero(row, col))
                {
                    // Move the hero GameObject to the clicked square
                    MoveHero(row, col);


                    // Check if the clicked square has a power-up GameObject
                    if (HasPowerUp(row, col))
                    {

                        // Remove the power-up GameObject from the grid
                        RemovePowerUp(row, col);

                        // Activate the power-up effect
                        scoreText.IncreaseScore(3);


                    }
                }

            }


        }



    }

    void EndGame()
    {
        if (powerUps.Length == 0 || powerUps == null)
        {
            Debug.Log("No coins");
        }
    }

    bool ClickedOnSquare()
    {
        // Check if the left mouse button is clicked
        if (Input.GetMouseButtonDown(0))
        {

            // Create a ray from the camera through the mouse position
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            // Check if the ray intersects with a square GameObject
            if (hit.collider != null && hit.collider.gameObject.tag == "Player")
            {
                return true;
            }
        }
        return false;
    }



    bool IsNearToHero(int row, int col)
    {
        // Check if the hero GameObject exists
        if (hero != null)
        {
            // Get the row and column of the hero GameObject
            int heroRow = Mathf.RoundToInt(hero.transform.position.y - transform.position.y);
            int heroCol = Mathf.RoundToInt(hero.transform.position.x - transform.position.x);

            // Check if the clicked square is adjacent to the hero GameObject
            if ((Mathf.Abs(row - heroRow) <= 2 && col == heroCol) || (Mathf.Abs(col - heroCol) <= 2 && row == heroRow))
            {
                return true;
            }
        }

        return false;
    }

    void MoveHero(int row, int col)
    {
        // Calculate the position for the hero based on the row, column, and fixed size of each square
        Vector3 heroPosition = new Vector3(col , row , -1f);

        // Move the hero GameObject to the clicked square
        hero.transform.position = heroPosition;
    }



    bool HasPowerUp(int row, int col)
    {
        // Check if the clicked square has a power-up GameObject
        if (powerUps[row, col] != null)
        {
            return true;
        }

        return false;
    }

    void RemovePowerUp(int row, int col)
    {
        // Remove the power-up GameObject from the grid and destroy it
        Destroy(powerUps[row, col]);


    }
   
}
