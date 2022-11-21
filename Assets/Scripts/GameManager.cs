using System;
using agents;
using board;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public int CurrentTurnPlayerID { get; private set; }

    /// Human or AI
    public int NPlayers { get; private set; } = 2;

    private GameState _state;
    public GameState State
    {   
        get => _state;
        private set
        {
            if (value == _state) return;
            _state = value;
            OnGameStateChange?.Invoke(State);
        }
    }

    public Action<int> OnNextPlayerTurn;
    public Action<GameState> OnGameStateChange;

    public GameRules Rules { get; private set; }
    public FullBoard FullBoard { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Debug.LogException(new Exception("Multiple instances of GameManager"));
            Destroy(gameObject);
        }
    }


    private void StartGame()
    {
        State = GameState.STARTING;
        Rules = new GameRules();
        FullBoard = new FullBoard(Rules);
        var agents = FindObjectsOfType<Agent>();
        
        if (agents.Length != NPlayers)
        {
            Debug.LogException(new Exception("Number of agents does not match number of players"));
            return;
        }

        // todo: Game manager should not worry about players and their sides
        for (int player = 0; player < NPlayers; player++)
        {
            agents[player].Init(player, FullBoard);
            var board = FullBoard.GetBoard(player);
            board.OnBoardFilled += OnBoardFilled;
            board.OnDiePlaced += OnDiePlaced;

        }

        CurrentTurnPlayerID = Random.Range(0, NPlayers);
        OnNextPlayerTurn?.Invoke(CurrentTurnPlayerID);
        
        State = GameState.PLAYING;
        Debug.Log("Game started");
        
    }

    private void OnDiePlaced(Board board, int die, int col)
    {
        NextPlayerTurn();
    }

    private void NextPlayerTurn()
    {
        CurrentTurnPlayerID = (CurrentTurnPlayerID + 1) % NPlayers;
        
        // auto roll
        FullBoard.GetBoard(CurrentTurnPlayerID).GetDieRoll().StartRolling();
        
        OnNextPlayerTurn?.Invoke(CurrentTurnPlayerID);
    }

    private void OnBoardFilled()
    {
        int winner = -1;
        int maxScore = 0;
        for (int player = 0; player < NPlayers; player++)
        {
            var score = FullBoard.GetBoard(player).GetTotalScore();
            if (score > maxScore)
            {
                maxScore = score;
                winner = player;
            }
        }

        Debug.Log($"Player {winner} won with score {maxScore}");
        State = GameState.FINISHED;
    }


    private void Start()
    {
        StartGame();
    }

}