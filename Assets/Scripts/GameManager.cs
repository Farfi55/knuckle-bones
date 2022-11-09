using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public int CurrentPlayerSide { get; private set; }

    /// Human or AI
    public int nPlayers { get; private set; } = 2;

    public GameState State
    {
        get => State;
        private set
        {
            if (value == State) return;
            State = value;
            OnGameStateChange?.Invoke(State);
        }
    }

    public Action<int> OnSideChanged;
    public Action<GameState> OnGameStateChange;

    private GameRules rules;
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
        rules = new GameRules();
        FullBoard = new FullBoard(rules);
        var agents = FindObjectsOfType<Agent>();
        
        if (agents.Length != nPlayers)
        {
            Debug.LogException(new Exception("Number of agents does not match number of players"));
            return;
        }

        // todo: Game manager should not worry about agents and their sides
        for (int i = 0; i < nPlayers; i++)
        {
            agents[i].SetSide(i);
            var board = FullBoard.GetBoard(i);
            board.OnBoardFilled += OnBoardFilled;
            board.OnDiePlaced += OnDiePlaced;

        }

        CurrentPlayerSide = Random.Range(0, nPlayers);
        OnSideChanged?.Invoke(CurrentPlayerSide);
        
        State = GameState.PLAYING;
        
    }

    private void OnDiePlaced()
    {
        CurrentPlayerSide = (CurrentPlayerSide + 1) % nPlayers;
        OnSideChanged?.Invoke(CurrentPlayerSide);
    }

    private void OnBoardFilled()
    {
        int winner = -1;
        int maxScore = 0;
        for (int player = 0; player < nPlayers; player++)
        {
            var score = FullBoard.GetBoard(i).GetTotalScore();
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