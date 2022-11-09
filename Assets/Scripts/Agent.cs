using System.Collections;
using System.Collections.Generic;
using System.Security.Authentication.ExtendedProtection;
using UnityEngine;

public abstract class Agent : MonoBehaviour
{
    protected int side = -1;
    protected Board board;
    public abstract int RollDice();
    // void ChoseColumnToPlaceDie(int dieValue, FullBoard fullBoard);
    
    public void SetSide(int side) => this.side = side;
    public void SetSide(Board board) => this.board = board;
    
    
    
    protected bool IsMyTurn()
    {
        return side == GameManager.Instance.CurrentPlayerSide;
    }
    
    
}
}