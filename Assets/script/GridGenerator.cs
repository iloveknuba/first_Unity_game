using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.TextCore.Text;

public class NewBehaviourScript : MonoBehaviour
{
   
    public Square grassPrefab;
    public Square mountainPrefab;
    public List<GameObject> units;  
    
    public int numRows = 9;
    public int numCols = 16;
   
    public new Transform camera;


    public static NewBehaviourScript Instance;
   
    private bool ifPLayerTurn= false;
   
    

    private GameObject hero;
   private GameObject enemy;
    private GameObject unit;
    private Square squarePrefab;
    private Square[,] squares;
   

    

    private void Awake()
    {
       
       GameManager.Instance.OnStateChange += IfGameStateChanged;
        Instance = this;
        hero = Instantiate(units[0], new Vector3(0, 0, -1f), Quaternion.identity);
        
        enemy = Instantiate(units[1], new Vector3(16, 0, -1f), Quaternion.Euler(0, 160, 0));



       

    }
   
    private void OnDestroy()
    {
        GameManager.Instance.OnStateChange -= IfGameStateChanged;

    }

    private void IfGameStateChanged(GameState State)
    {
       unit = State == GameState.PlayerTurn ? hero : enemy;

        ifPLayerTurn = State == GameState.PlayerTurn;
    }
   
 
    void CreateGrid()
    {
        // Create the grid of squares
        for (int row = 0; row < numRows; row++)
        {
            for (int col = 0; col < numCols; col++)
            {
                var isOffset = (row % 2 == 0 && col % 2 == 0 && col != 0 && col != 16);
                Vector3 squarePosition = new(col, row, 0f);
                squarePrefab = isOffset ? mountainPrefab : grassPrefab;
                squares[row, col] = Instantiate(squarePrefab, squarePosition, Quaternion.identity);
                squares[row, col].name = $"Square: {col} {row}";
            }
        }
    }
   
    void Start()
    {
        squares = new Square[numRows, numCols];
       
        CreateGrid();

       
        camera.transform.position = new Vector3((float)numRows / 1.125f, (float)numCols / 2.7f - 2.3f, -10);
    }

  
    public void UnitTurn(GameState state)
    {
        

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
                        ScoreManager.instance.IncreaseScore(3);

                        
                    }

                    
                    GameManager.Instance.UpdateState(state);
                                           
                }
                

            }
            

        }
    }

    private void Update()
    {
        
        UnitTurn(ifPLayerTurn ? GameState.EnemyTurn : GameState.PlayerTurn);
        
    }

   



    bool IsNearToHero(int row, int col)
    {
        // Check if the hero GameObject exists
        if (unit != null)
        {
            // Get the row and column of the hero GameObject
            int heroRow = Mathf.RoundToInt(unit.transform.position.y - transform.position.y);
            int heroCol = Mathf.RoundToInt(unit.transform.position.x - transform.position.x);

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
        Vector3 heroPosition = new Vector3(col, row, -1f);

        unit.transform.position = heroPosition;

    }

    public bool HasPowerUp(int row, int col)
    {
        // Check if the clicked square has a power-up GameObject
        if (GoldManger.Instance.powerUps[row, col] != null)
        {
            return true;
        }

        return false;
    }

    public void RemovePowerUp(int row, int col)
    {
        // Remove the power-up GameObject from the grid and destroy it
        Destroy(GoldManger.Instance.powerUps[row, col]);

    }







}
