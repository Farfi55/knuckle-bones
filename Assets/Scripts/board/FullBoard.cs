using System;
using board.graphics;

namespace board
{
    public class FullBoard
    {
        private readonly Board[] _boards;
        private readonly int _nPlayers;
        private readonly int _cols;
        private readonly int _rows;

        // public FullBoard() : this(new GameRules()) { }
        
    
        public FullBoard(GameRules rules)
        {
            this._nPlayers = rules.sides;
            this._cols = rules.cols;
            this._rows = rules.rows;

            _boards = new Board[_nPlayers];
            for (int player = 0; player < _nPlayers; player++)
            {
                _boards[player] = new Board(rules);
                _boards[player].OnDiePlaced += OnDiePlaced;
            }

        }

        private void OnDiePlaced(Board board, int dieValue, int col)
        {
            for (int player = 0; player < _nPlayers; player++)
            {
                if (_boards[player] != board)
                {
                    _boards[player].RemoveDiceWithValue(dieValue, col);
                }
            }
        }


        public int GetColumnScore(int player, int col)
        {
            if (!IsPlayerInRange(player))
                throw new ArgumentOutOfRangeException();
        
            return GetBoard(player).GetColumnScore(col);
        }
    
        public int GetPlayerScore(int player)
        {
            if (!IsPlayerInRange(player))
                throw new ArgumentOutOfRangeException();
        
            return GetBoard(player).GetTotalScore();
        }

        public Board GetBoard(int player)
        {
            if (!IsPlayerInRange(player))
                throw new ArgumentOutOfRangeException();
            return _boards[player];
        }


        private bool IsPlayerInRange(int player) => 0 <= player && player < _nPlayers;
    }
}