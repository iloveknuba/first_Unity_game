using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameState state;
   
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
                break;
            case GameState.LoseScreen:
                break;
        }

        OnStateChange?.Invoke(state);
        
    }

   

    private  void HandleEnemyTurn()
    {
        ScoreManager.instance.currentStateText.text = "Enemy Turn";
       
    }

    private  void HandlePlayerTurn()
    {

        ScoreManager.instance.currentStateText.text = "Your Turn";

    }
}

public enum GameState
{
    PlayerTurn,
    EnemyTurn,
    VictoryScreen,
    LoseScreen
}
