using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.TextCore.Text;
using System.Threading.Tasks;

public class Player : MonoBehaviour
{
   
  
    public List<GameObject> units;


    private int currentHeroIndex = 3;
    private int currentEnemyIndex = 3;

  

    public static Player Instance;
   
    private bool ifPLayerTurn= false;

    public bool heroMoved =false;

    public List<GameObject> hero;
   public List<GameObject> enemy;
    public GameObject unit;
  

    

    private void Awake()
    {
       
       GameManager.Instance.OnStateChange += IfGameStateChanged;
        Instance = this;
        
    }
    private void Start()
    {
        spawnUnits();

        //hero = Instantiate(units[0], new Vector3(0, 2, -1f), Quaternion.identity);
        //enemy = Instantiate(units[0], new Vector3(16, 2, -1f), Quaternion.Euler(0, 160, 0));
    }

    void spawnUnits()
    {
        for(int i = 0; i < currentHeroIndex; i++)
        {
            hero.Add(Instantiate(units[0], new Vector3(0, i, -1f), Quaternion.identity));
           
            hero[i].name = $"hero {i}";
        }

        for (int i = 0; i < currentEnemyIndex; i++)
        {
            enemy.Add(Instantiate(units[0], new Vector3(16, i, -1f), Quaternion.Euler(0, 160, 0)));
            enemy[i].name = $"enemy {i}";
        }
    }

 
    private void OnDestroy()
    {
        GameManager.Instance.OnStateChange -= IfGameStateChanged;

    }

    private void IfGameStateChanged(GameState State)
    {      
        ifPLayerTurn = State == GameState.PlayerTurn;
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
            for (int i = 0; i < hero.Count; i++)
            {
                if (hit.collider.gameObject == hero[i] && ifPLayerTurn)
                {
                    unit = hero[i];
                    Debug.Log($"Hero {i} choosen");

                }
            }
            for (int i = 0; i < enemy.Count; i++)
            {
                if (hit.collider.gameObject == enemy[i] && !ifPLayerTurn)
                {
                    unit = enemy[i];
                    Debug.Log($"enemy {i} choosen");

                }
            }
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
                    
                    await Task.Delay(10);

                    GameManager.Instance.UpdateState(state);
                    heroMoved = false;
                   
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
