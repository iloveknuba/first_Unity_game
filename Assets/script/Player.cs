using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.TextCore.Text;

public class Player : MonoBehaviour
{
   
  
    public List<GameObject> heroes;
    public List<GameObject> enemies;




    public static Player Instance;
   
    private bool ifPLayerTurn= false;
   
    

    public GameObject hero;
   public GameObject enemy;
    public GameObject unit;
  

    

    private void Awake()
    {
       
       GameManager.Instance.OnStateChange += IfGameStateChanged;
        Instance = this;
        hero = Instantiate(heroes[0], new Vector3(0, 3, -1f), Quaternion.identity);
        
        enemy = Instantiate(enemies[0], new Vector3(16, 3, -1f), Quaternion.Euler(0, 160, 0));

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

    private void Update()
    {

        UnitTurn(ifPLayerTurn ? GameState.EnemyTurn : GameState.PlayerTurn);

    }

    public void UnitTurn(GameState state)
    {
        

        if (Input.GetMouseButtonDown(0))
        {
           
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);


            int row = Mathf.RoundToInt(hit.collider.gameObject.transform.position.y - transform.position.y);
            int col = Mathf.RoundToInt(hit.collider.gameObject.transform.position.x - transform.position.x);

           
            if (hit.collider != null && hit.collider.gameObject.tag == "Player")
            {

                Debug.Log($"Coordinates of clicked square: {row}{col}");
             
                if (IsNearToHero(row, col))
                {               
                    MoveHero(row, col);
              
                    if (HasPowerUp(row, col))
                    {
                  
                        RemovePowerUp(row, col);
                  
                        ScoreManager.instance.IncreaseScore(3);
               
                    }

                    GameManager.Instance.UpdateState(state);                                          
                }

            }
 
        }
    }

   

   


    bool IsNearToHero(int row, int col)
    {
        
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
        
        Vector3 heroPosition = new Vector3(col, row, -1f);

        unit.transform.position = heroPosition;

    }

    public bool HasPowerUp(int row, int col)
    {
       
        if (GoldManger.Instance.powerUps[row, col] != null)
        {
            return true;
        }

        return false;
    }

    public void RemovePowerUp(int row, int col)
    {
       
        Destroy(GoldManger.Instance.powerUps[row, col]);

    }







}
