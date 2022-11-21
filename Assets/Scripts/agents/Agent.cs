using board;
using board.graphics;
using UnityEngine;

namespace agents
{
    public abstract class Agent : MonoBehaviour
    {
        protected int PlayerID = -1;
        protected FullBoard FullBoard;
        protected Board Board;

        public void Init(int side, FullBoard fullBoard)
        {
            PlayerID = side;
            FullBoard = fullBoard;
            Board = FullBoard.GetBoard(side);
            
            GameManager.Instance.OnNextPlayerTurn += OnNextPlayerTurn;
        }

        private void OnNextPlayerTurn(int playerID)
        {
            if (playerID != PlayerID) return;
            
            Board.GetDieRoll().StartRolling();
        }

        protected bool IsItMyTurn()
        {
            return PlayerID == GameManager.Instance.CurrentTurnPlayerID;
        }
    }
}