using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;

public class Board
{
    public readonly int rows;
    public readonly int cols;

    private int[][] dices;

    public Action<Board> OnBoardChanged;
    public Action<Board, int, int> OnDiePlaced;
    public Action OnBoardFilled;

    public Board(GameRules rules)
    {
        this.rows = rules.rows;
        this.cols = rules.cols;

        dices = new int[cols][];
        for (int col = 0; col < cols; col++)
        {
            dices[col] = new int[rows];
        }
    }
    
    public int GetDie(int col, int row)
    {
        return dices[col][row];
    }
    
    

    public int GetTotalScore()
    {
        var score = 0;
        for (int col = 0; col < cols; col++)
            score += GetColumnScore(col);
        return score;
    }

    

    public int GetColumnScore(int col)
    {
        if (!IsColumnInBounds(col)) throw new IndexOutOfRangeException("column not in bounds");
        var score = 0;

        Dictionary<int, int> valueTimesDictionary = new();
        for (var row = 0; row < rows; row++)
        {
            var val = dices[col][row];
            valueTimesDictionary[val]++;
        }

        foreach (var (val, times) in valueTimesDictionary)
        {
            score += val * times;
        }

        return score;
    }


    private int GetFirstEmptyPlace(int col)
    {
        for (var row = 0; row < rows; row++)
            if (dices[col][row] == 0)
                return row;
        return -1;
    }

    public bool IsColFull(int col)
    {
        for (var row = rows - 1; row >= 0; row--)
            if (dices[col][row] == 0)
                return false;

        return true;
    }
    
    private bool IsBoardFull()
    {
        for (var col = 0; col < cols; col++)
            if(!IsColFull(col))
                return false;
        return true;
    }

    public void PlaceDie(int dieValue, int col)
    {
        if (!IsColumnInBounds(col)) throw new IndexOutOfRangeException("column not in bounds");
        if (IsColFull(col)) throw new IndexOutOfRangeException("row is full");

        var firstEmptyPlace = GetFirstEmptyPlace(col);
        dices[col][firstEmptyPlace] = dieValue;
        
        OnBoardChanged?.Invoke(this);
        OnDiePlaced?.Invoke(this, dieValue, col);
        if(IsBoardFull())
            OnBoardFilled?.Invoke();
    }

    public void RemoveDiceWithValue(int dieValue, int col)
    {
        if (!IsColumnInBounds(col)) throw new IndexOutOfRangeException("column not in bounds");

        Debug.Log("Before " + dices.Length);
        dices[col] = dices[col].Where(val => val != dieValue).ToArray();
        Debug.Log("After " + dices.Length);
        
        OnBoardChanged?.Invoke(this);
    }

    private bool IsRowInBounds(int row) => 0 <= row && row < rows;

    private bool IsColumnInBounds(int col) => 0 <= col && col < cols;
}