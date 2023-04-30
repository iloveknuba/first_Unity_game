using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameState state;
    public TextMeshProUGUI currentStateText;
    public TextMeshProUGUI gameResultText;

    public event Action<GameState> OnStateChange;
    void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        UpdateState(GameState.PlayerTurn);
    }

    public void UpdateState(GameState state)
    {
        this.state = state;
        switch (state)
        {
            case GameState.PlayerTurn:
                HandlePlayerTurn();
                break;
            case GameState.EnemyTurn:
                HandleEnemyTurn();
                break;         
            case GameState.VictoryScreen:
                HandleVictory();
                break;
            case GameState.LoseScreen:
                HandleLose();
                break;
            case GameState.DrawScreen:
                HandleDraw();
                break;
        }

        OnStateChange?.Invoke(state);
        
    }

    private void HandleDraw()
    {
        gameResultText.text = "Draw!";
    }

    private void HandleLose()
    {
        gameResultText.text = "You lose!!";
    }

    private void HandleVictory()
    {
        gameResultText.text = "You won!!";
    }

    private  void HandleEnemyTurn()
    {
        currentStateText.text = "Enemy Turn";
       
    }

    private  void HandlePlayerTurn()
    {

        currentStateText.text = "Your Turn";

    }
    
}

public enum GameState
{
    PlayerTurn,
    EnemyTurn,
    VictoryScreen,
    LoseScreen,
    DrawScreen
}
