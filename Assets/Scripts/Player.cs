using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Player : Agent
{
    public override int RollDice()
    {
        var dice = Random.Range(1, 7);
        Debug.Log(dice);
        return dice;
    }

    public int rolledDie = 0;


    private void Update()
    {
        if (!IsMyTurn())
            return;

        if (!Input.anyKeyDown)
            return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rolledDie = RollDice();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            PlaceDie(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            PlaceDie(1);            
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            PlaceDie(2);
        }
    }

    private void PlaceDie(int col)
    {
        if (board.IsColFull(col))
        {
            Debug.LogWarning("Column is full");
            return;
        }
        if (rolledDie == 0)
        {
            Debug.LogWarning("You have not rolled the die");
            return;
        }

        board.PlaceDie(rolledDie, col);
        rolledDie = 0;
    }
}