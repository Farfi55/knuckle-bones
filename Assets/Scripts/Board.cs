using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;

public class Board
{
    private readonly int rows;
    private readonly int cols;

    private int[][] dices;


    public Board()
    {
        rows = 3;
        cols = 3;
        dices = new int[cols][];
        for (int col = 0; col < cols; col++)
        {
            dices[col] = new int[rows];
        }
    }

    public int GetScore(int col)
    {
        if (!IsColumnInBounds(col)) throw new IndexOutOfRangeException("column not in bounds");
        var score = 0;
        
        Dictionary<int, int> valueTimesDictionary = new();
        for (var row  = 0; row < rows; row++)
        {
            var val = dices[col][row];
            valueTimesDictionary[val]++;
        }

        foreach (var valueTimePair in valueTimesDictionary)
        {
            var val = valueTimePair.Key;
            var times = valueTimePair.Value;
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

    private bool IsRowFull(int col)
    {
        for (var row = rows - 1; row >= 0; row--)
            if (dices[col][row] == 0)
                return false;
        
        return true;
    }


    public void PlaceDie(int dieValue, int col)
    {
        if (!IsColumnInBounds(col)) throw new IndexOutOfRangeException("column not in bounds");
        if (IsRowFull(col)) throw new IndexOutOfRangeException("row is full");

        var firstEmptyPlace = GetFirstEmptyPlace(col);
        dices[col][firstEmptyPlace] = dieValue;
    }

    public void RemoveDiceWithValue(int dieValue, int col)
    {
        if (!IsColumnInBounds(col)) throw new IndexOutOfRangeException("column not in bounds");

        Debug.Log("Before " + dices.Length);
        dices[col] = dices[col].Where(val => val != dieValue).ToArray();
        Debug.Log("After " + dices.Length);
        
        
        // int removed = 0;
        // for (int row = 0; row < rows; row++)
        // {
        //     while (dices[col][row] == dieValue)
        //     {
        //         removed++;
        //         if (IsRowInBounds(row + removed))
        //             dices[col][row] = dices[col][row + removed];
        //         else
        //             dices[col][row] = 0;
        //     }
        //     
        // }

    }

    private bool IsRowInBounds(int row) => 0 <= row && row < rows;

    private bool IsColumnInBounds(int col) => 0 <= col && col < cols;
}