using UnityEngine;

namespace board.graphics
{
    public class GraphicFullBoard : MonoBehaviour
    {
        private GameManager _gm;
    
        [SerializeField] private Transform _graphicBoardRoot;
        [SerializeField] private GraphicBoard _graphicBoardPrefab;

        private GraphicBoard[] _graphicBoards;
        
        [SerializeField] private bool _reverseOrderEveryOtherRow;
    
    
        private void Start()
        {
            _gm = GameManager.Instance;
            _gm.OnSideChanged += OnSideChanged;
        
            _graphicBoards = new GraphicBoard[_gm.nPlayers];
            for (int side = 0; side < _gm.nPlayers; side++)
            {
                GenerateBoard(side);
            }
        }

        private void GenerateBoard(int side)
        {
            var graphicBoard = Instantiate(_graphicBoardPrefab, _graphicBoardRoot);
            graphicBoard.name = "Board " + side;

            var board = _gm.fullBoard.GetBoard(side);
            graphicBoard.Init(board);
            
            var reverseOrder = _reverseOrderEveryOtherRow && side % 2 == 1;
            graphicBoard.SetReverseDieOrder(reverseOrder);
            
            _graphicBoards[side] = graphicBoard;
        }

        private void OnSideChanged(int side)
        {
        
        }
    }
}