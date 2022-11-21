using UnityEngine;

namespace board.graphics
{
    public class GraphicFullBoard : MonoBehaviour
    {
        private GameManager _gm;


        [SerializeField] private Transform _graphicBoardRoot;
        
        [SerializeField] private bool _dynamicallyGenerateBoard = true;
        [SerializeField] private GraphicBoard _graphicBoardPrefab;
        
        [SerializeField] private GraphicBoard[] _graphicBoards;
        
        [SerializeField] private bool _reverseOrderEveryOtherRow;
    
    
        private void Start()
        {
            _gm = GameManager.Instance;
            _gm.OnNextPlayerTurn += OnNextPlayerTurn;
        
            if (_dynamicallyGenerateBoard)
            {
                _graphicBoards = new GraphicBoard[_gm.NPlayers];
                for (int playerID = 0; playerID < _gm.NPlayers; playerID++) 
                    GenerateBoard(playerID);
                
            }
            else
            {
                if (_graphicBoards == null)
                    Debug.LogError("GraphicBoards is null");
                else if (_graphicBoards.Length != _gm.NPlayers)
                    Debug.LogError("GraphicBoards array length does not match number of players");
                else
                {
                    for (int playerID = 0; playerID < _gm.NPlayers; playerID++)
                    {
                        var board = _gm.FullBoard.GetBoard(playerID);
                        _graphicBoards[playerID].Init(board);
                    }
                }
            }
            
        }

        private void GenerateBoard(int side)
        {
            var graphicBoard = Instantiate(_graphicBoardPrefab, _graphicBoardRoot);
            graphicBoard.name = "Board " + side;

            var board = _gm.FullBoard.GetBoard(side);
            graphicBoard.Init(board);
            
            var reverseOrder = _reverseOrderEveryOtherRow && side % 2 == 1;
            graphicBoard.SetReverseDieOrder(reverseOrder);
            
            _graphicBoards[side] = graphicBoard;
        }

        private void OnNextPlayerTurn(int side)
        {
            
        }
    }
}