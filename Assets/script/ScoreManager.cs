using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI playerScoreText;
    public TextMeshProUGUI enemyScoreText;
    public TextMeshProUGUI timerText;



    private float gameDuration = 300f;
    private float moveDelay = 10.0f; // �������� �� ������ ��'����
    private bool gameEnded  = false;
    private float timer;


    private bool ifPlayerTurn = false;
    private TextMeshProUGUI scoreText;// ������ �� ��������� ����, ������������ ����
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
        scoreText = state == GameState.PlayerTurn ? playerScoreText : enemyScoreText;
        score = state == GameState.PlayerTurn ? scorePlayer : scoreEnemy;

        ifPlayerTurn = state == GameState.PlayerTurn ? true : false;

    }
// ���������� ��� �������� �������� �����
    
private void Start()
    {
        scorePlayer = 0;
        scoreEnemy = 0;
        timer = gameDuration;
    }
    private void Update()
    {

        timeManager(ifPlayerTurn ? GameState.EnemyTurn : GameState.PlayerTurn);
        
    }

    void timeManager(GameState state)
    {
        if (!gameEnded)
        {
            timer -= Time.deltaTime;
            UpdateTimerText();

            if (timer <= 0)
            {
                EndGame();
            }
            else if (timer % moveDelay < Time.deltaTime)
            {
                GameManager.Instance.UpdateState(state);
            }
        }
    }
    void UpdateTimerText()
    {
        int minutes = Mathf.FloorToInt(timer / 60.0f);
        int seconds = Mathf.FloorToInt(timer % 60.0f);
        timerText.text = string.Format("{0:0}:{1:00}", minutes, seconds);
    }

    // ����� ��� ���������� ����� �� �������� ��������
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

    // ����� ��� ���������� ����������� ����� � ��������� ����
    private void UpdateScoreText()
    {
        scoreText.text = "Score: " + score.ToString();
    }

    void EndGame()
    {
        gameEnded = true;
        
        GameManager.Instance.UpdateState(scoreEnemy < scorePlayer ? GameState.VictoryScreen : GameState.LoseScreen);
    }

}
