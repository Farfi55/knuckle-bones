using System;
using UnityEngine;

class GraphicBoard : MonoBehaviour
{
    [SerializeField] private Transform Columns;

    private GameObject[][] dieCells;

    [SerializeField] public Sprite[] dieSprites;
    
    public Board board { get; private set; }

    private void Awake()
    {
        int cols = GameManager.Instance.rules.cols;
        int rows = GameManager.Instance.rules.rows;

        dieCells = new GameObject[cols][];
        for (int col = 0; col < cols; col++)
        {
            dieCells[col] = new GameObject[rows];
            Transform column = Columns.GetChild(col);
            for (int row = 0; row < rows; row++)
            {
                dieCells[col][row] = column.GetChild(row + 1).gameObject;
            }
        }
    }

    private void Start()
    {
        
    }

    public void Init(Board board)
    {
        this.board = board;
        board.OnBoardChanged += UpdateBoard;
        UpdateBoard(board);
    }

    private void UpdateBoard(Board board)
    {
        for (int col = 0; col < board.cols; col++)
        {
            for (int row = 0; row < board.rows; row++)
            {
                var dieValue = board.GetDie(col, row);
                Sprite sprite = dieValue == 0 ? null : dieSprites[dieValue - 1];
                
                dieCells[col][row].GetComponent<SpriteRenderer>().sprite = sprite;
            }
        }            
    }
}