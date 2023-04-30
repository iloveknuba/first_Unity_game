using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI playerScoreText;
    public TextMeshProUGUI enemyScoreText;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI currentStateText;

   

    public float gameDuration = 300f;
    public float moveDelay = 10.0f;

   

   
    private bool gameEnded  = false;
    private float timer;
    private float moveTime;


   
    

    private TextMeshProUGUI scoreText;
    private int scorePlayer;
    private int scoreEnemy;
    private int score;


    public static ScoreManager instance;
    void Awake()
    {
        instance = this;
        GameManager.Instance.OnStateChange += SubscribeGameState;
       
    }
    private void OnDestroy()
    {
        GameManager.Instance.OnStateChange -= SubscribeGameState;
    }

    private void SubscribeGameState(GameState state)
    {
        scoreText = Player.Instance.ifPLayerTurn ? enemyScoreText : playerScoreText;
        score = Player.Instance.ifPLayerTurn ? scoreEnemy : scorePlayer;

    }

    
    private void Start()
    {
        
        scorePlayer = 0;
        scoreEnemy = 0;
        timer = gameDuration;
        moveTime = Time.time;
        UpdateScoreText();
       
    }
    private void Update()
    {
       
        timeManager();
        checkDelay(Player.Instance.ifPLayerTurn ? GameState.EnemyTurn : GameState.PlayerTurn);
    }

    public void BuyHero(int price)
    {
        if(scorePlayer >= price)
        {
            Player.Instance.heroes.Add(Instantiate(Player.Instance.heroPrefab, new Vector3(2, 5, -1f), Quaternion.identity));
            scorePlayer -= price;
            playerScoreText.text =  "Coins: " + scorePlayer.ToString();
        }
        else
        {
            Debug.Log("Not enough coins");
        }
       
    }
    public void BuyEnemy(int price)
    {
        if (scoreEnemy >= price)
        {
            Player.Instance.enemies.Add(Instantiate(Player.Instance.enemyPrefab, new Vector3(16, 5, -1f), Quaternion.Euler(0, 160, 0)));
            scoreEnemy -= price;
            enemyScoreText.text = "Coins: " + scoreEnemy.ToString();
        }
        else
        {
            Debug.Log("Not enough coins");
        }

    }

    void timeManager()
    {
        if (!gameEnded)
        {
            timer -= Time.deltaTime;
            UpdateTimerText(timerText);

            if (timer <= 0)
            {
                EndGame();
            }
            
        }
    }

    void checkDelay(GameState state)
    {
        
        if (Player.Instance.heroMoved)
        {
           moveTime = Time.time;

           
        }
        if (Time.time - moveTime > moveDelay)
        {
            
            Player.Instance.heroMoved = true;
            GameManager.Instance.UpdateState(state);
            moveTime = Time.time;
            
        }

        Player.Instance.heroMoved = false;
       
    }
    
    void UpdateTimerText(TextMeshProUGUI timerText)
    {
        int minutes = Mathf.FloorToInt(timer / 60.0f);
        int seconds = Mathf.FloorToInt(timer % 60.0f);
        timerText.text = string.Format("{0:0}:{1:00}", minutes, seconds);
    }

    
    public void IncreaseScore(int value)
    {

        if (scoreText == playerScoreText)
        {
            scorePlayer += value;
            score = scorePlayer;
        }
        else if (scoreText == enemyScoreText)
        {
            scoreEnemy += value;
            score = scoreEnemy;
        }

        UpdateScoreText();
    }

   
    public void UpdateScoreText()
    {
        scoreText.text = "Coins: " + score.ToString();
    }

    void EndGame()
    {
        gameEnded = true;
        
        GameManager.Instance.UpdateState(scoreEnemy < scorePlayer ? GameState.VictoryScreen : GameState.LoseScreen);
    }

}
