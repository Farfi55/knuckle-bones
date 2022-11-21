using System;
using System.Collections.Generic;
using UnityEngine;

namespace board
{
    public class Board
    {
        public const int DefaultDieValue = 0;
        public readonly int Rows;
        public readonly int Cols;

        private IDieRoll _dieRoll;

        private readonly List<List<int>> dices;

        public Action<Board> OnBoardChanged;
        public Action<Board, int, int> OnDiePlaced;
        public Action OnBoardFilled;


        public Board(GameRules rules)
        {
            this.Rows = rules.rows;
            this.Cols = rules.cols;

            dices = new List<List<int>>(Cols);
            for (int col = 0; col < Cols; col++)
            {
                dices.Add(new List<int>(Rows));
                for (int row = 0; row < Rows; row++)
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
            for (int col = 0; col < Cols; col++)
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
                score += val * times * times;
            }

            return score;
        }

        public Dictionary<int, int> GetValueRepetitions(int col)
        {
            Dictionary<int, int> valueRepetitionsDictionary = new();
            for (var row = 0; row < Rows; row++)
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

        public IDieRoll GetDieRoll() => _dieRoll;

        public void SetDieRoll(IDieRoll value)
        {
            if (_dieRoll != null) return;
            _dieRoll = value;
        }

        private int GetFirstEmptyPlace(int col)
        {
            for (var row = 0; row < Rows; row++)
                if (dices[col][row] == DefaultDieValue)
                    return row;
            return -1;
        }

        public bool IsColFull(int col)
        {
            for (var row = Rows - 1; row >= 0; row--)
                if (dices[col][row] == 0)
                    return false;

            return true;
        }

        private bool IsBoardFull()
        {
            for (var col = 0; col < Cols; col++)
                if (!IsColFull(col))
                    return false;
            return true;
        }


        public void PlaceRolledDie(int col)
        {
            if (_dieRoll.Die != null)
            {
                PlaceDie(_dieRoll.Die, col);
                _dieRoll.Clear();
            } 
        }

        public void PlaceDie(int dieValue, int col)
        {
            if (!IsColumnInBounds(col)) throw new IndexOutOfRangeException("column not in bounds");
            if (IsColFull(col)) throw new IndexOutOfRangeException("row is full");

            var firstEmptyPlace = GetFirstEmptyPlace(col);
            dices[col][firstEmptyPlace] = dieValue;

            OnBoardChanged?.Invoke(this);
            OnDiePlaced?.Invoke(this, dieValue, col);
            if (IsBoardFull())
                OnBoardFilled?.Invoke();
        }

        public void RemoveDiceWithValue(int dieValue, int col)
        {
            if (!IsColumnInBounds(col)) throw new IndexOutOfRangeException("column not in bounds");

            dices[col].RemoveAll(currentDie => currentDie == dieValue);

            int removed = Rows - dices[col].Count;
            for (int i = 0; i < removed; i++)
                dices[col].Add(DefaultDieValue);

            OnBoardChanged?.Invoke(this);
        }

        private bool IsRowInBounds(int row) => 0 <= row && row < Rows;

        private bool IsColumnInBounds(int col) => 0 <= col && col < Cols;
    }
}