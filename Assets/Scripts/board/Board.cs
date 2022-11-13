using System;
using System.Collections.Generic;

namespace board
{
    public class Board
    {
        public const int DefaultDieValue = 0;
        public readonly int rows;
        public readonly int cols;

        private readonly List<List<int>> dices;

        public Action<Board> OnBoardChanged;
        public Action<Board, int, int> OnDiePlaced;
        public Action OnBoardFilled;

        public Board(GameRules rules)
        {
            this.rows = rules.rows;
            this.cols = rules.cols;

            dices = new List<List<int>>(cols);
            for (int col = 0; col < cols; col++)
            {
                dices.Add(new List<int>(rows));
                for (int row = 0; row < rows; row++)
                {
                    dices[col].Add(DefaultDieValue);
                }
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

            var valueTimesDictionary = GetValueRepetitions(col);

            foreach (var (val, times) in valueTimesDictionary)
            {
                score += val * times;
            }

            return score;
        }

        public Dictionary<int, int> GetValueRepetitions(int col)
        {
            Dictionary<int, int> valueRepetitionsDictionary = new();
            for (var row = 0; row < rows; row++)
            {
                var dieValue = dices[col][row];
                if (!valueRepetitionsDictionary.ContainsKey(dieValue))
                    valueRepetitionsDictionary[dieValue] = 1;
                else valueRepetitionsDictionary[dieValue]++;
            }

            return valueRepetitionsDictionary;
        }

        public List<int> GetColumn(int col)
        {
            return dices[col];
        }



        private int GetFirstEmptyPlace(int col)
        {
            for (var row = 0; row < rows; row++)
                if (dices[col][row] == DefaultDieValue)
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
        
            dices[col].RemoveAll(currentDie => currentDie == dieValue);
        
            int removed = rows - dices[col].Count;
            for (int i = 0; i < removed; i++)
                dices[col].Add(DefaultDieValue);
        
            OnBoardChanged?.Invoke(this);
        }

        private bool IsRowInBounds(int row) => 0 <= row && row < rows;

        private bool IsColumnInBounds(int col) => 0 <= col && col < cols;
    }
}