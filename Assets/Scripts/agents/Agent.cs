using board;
using UnityEngine;

namespace agents
{
    public abstract class Agent : MonoBehaviour
    {
        protected int side = -1;
        protected FullBoard fullboard;
        protected Board board;

        public abstract int RollDice();
        // void ChoseColumnToPlaceDie(int dieValue, FullBoard fullBoard);

        public void Init(int side, FullBoard fullBoard)
        {
            this.side = side;
            this.fullboard = fullBoard;
            this.board = fullboard.GetBoard(side);
        }

        protected bool IsMyTurn()
        {
            return side == GameManager.Instance.CurrentPlayerSide;
        }
    }
}