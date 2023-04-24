using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.TextCore.Text;
using System.Threading.Tasks;

public class Player : MonoBehaviour
{
    public GameObject heroPrefab;
    public GameObject enemyPrefab;

    public int heroesAmount = 3;
    public int enemiesAmount = 3;
    public int reward = 3;


    public bool heroMoved = false;

    public static Player Instance;
   
    public bool ifPLayerTurn= false;


    public List<GameObject> heroes;
   public List<GameObject> enemies;
    public GameObject unit;
  

    

    private void Awake()
    {
       
       GameManager.Instance.OnStateChange += IfGameStateChanged;
        Instance = this;
        spawnUnits();
        
    }
   
    void spawnUnits()
    {
       
        for(int i = 0; i < heroesAmount; i++)
        {
            heroes.Add(Instantiate(heroPrefab, new Vector3(2, 5, -1f), Quaternion.identity));
            
            heroes[i].name = $"hero {i}";
        }

        for (int i = 0; i < enemiesAmount; i++)
        {
            enemies.Add(Instantiate(enemyPrefab, new Vector3(16, 5, -1f), Quaternion.Euler(0, 160, 0)));
           
            enemies[i].name = $"enemy {i}";
        }
    }

 
    private void OnDestroy()
    {
        GameManager.Instance.OnStateChange -= IfGameStateChanged;

    }

    private void IfGameStateChanged(GameState State)
    {      
        ifPLayerTurn = State == GameState.PlayerTurn;
        unit = ifPLayerTurn ? heroes[0] : enemies[0];
    }

    private void Update()
    {
        
        UnitTurn(ifPLayerTurn ? GameState.EnemyTurn : GameState.PlayerTurn);

    }


    public async void UnitTurn(GameState state)
    {
       

        if (Input.GetMouseButtonDown(0))
        {

           
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);



            int row = Mathf.RoundToInt(hit.collider.gameObject.transform.position.y - transform.position.y);
            int col = Mathf.RoundToInt(hit.collider.gameObject.transform.position.x - transform.position.x);

            for (int i = 0; i < heroes.Count; i++)
            {
                if (hit.collider.gameObject == heroes[i] && ifPLayerTurn)
                {
                   
                    unit = heroes[i];
                    Debug.Log($"Hero {i} choosen");
                   
                   
                }
              
            }
            for (int i = 0; i < enemies.Count; i++)
            {
                if (hit.collider.gameObject == enemies[i] && !ifPLayerTurn)
                {
                    
                     unit = enemies[i];
                     Debug.Log($"enemy {i} choosen");
                   
                }
               
            }
            if (hit.collider.gameObject.tag == "Player")
            {

                Debug.Log($"Coordinates of clicked square: {row}{col}");
             
                if (IsNearToHero(row, col))
                {               
                    MoveHero(row, col);
              
                    if (HasPowerUp(row, col))
                    {
                  
                        RemovePowerUp(row, col);
                  
                        ScoreManager.instance.IncreaseScore(reward);
               
                    }

                    await Task.Delay(10);
                    GameManager.Instance.UpdateState(state);
                    heroMoved = false;
                   
                }

            }
           

        }
    }



   public bool IsNearToHero(int row, int col)
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

        heroMoved = true;
    }

    public bool HasPowerUp(int row, int col)
    {
       
        if (GoldManger.Instance.goldArray[row, col] != null)
        {
            return true;
        }

        return false;
    }

    public void RemovePowerUp(int row, int col)
    {
       
        Destroy(GoldManger.Instance.goldArray[row, col]);

    }







}
