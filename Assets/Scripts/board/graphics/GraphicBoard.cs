using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace board.graphics
{
    class GraphicBoard : MonoBehaviour
    {
        [SerializeField] private Transform _columnsParent;
        [SerializeField] private GraphicBoardColumn _columnPrefab;

        private List<GraphicBoardColumn> _dieColumns;
    
        [SerializeField] private TMP_Text _boardScore;

        [SerializeField] private List<Sprite> _dieSprites;
        [SerializeField] private List<Color> _dieColorsPerRepetition;

        private Board _board;
        private bool _reverseDieOrder = false;
        
        

        public void Init(Board board)
        {
            _board = board;
            _dieColumns = new List<GraphicBoardColumn>();
            
            for (var col = 0; col < _board.cols; col++)
            {
                var graphicBoardColumn = Instantiate(_columnPrefab, _columnsParent);
                _dieColumns.Add(graphicBoardColumn);
                graphicBoardColumn.Init(board, col);
                graphicBoardColumn.InitSlots(_dieSprites, _dieColorsPerRepetition);
            }

            // board.OnBoardChanged += UpdateBoard;
            // UpdateBoard(board);
        }
        
        
        public void SetReverseDieOrder(bool reverseDieOrder)
        {
            _reverseDieOrder = reverseDieOrder;
            foreach (var graphicBoardColumn in _dieColumns)
            {
                graphicBoardColumn.SetReverseDieOrder(reverseDieOrder);
            }
        }
    }
}