using System;
using UnityEngine;

class GraphicBoard : MonoBehaviour
{
    [SerializeField] private Transform Columns;

    private GameObject[][] dieCells;

    [SerializeField] public Sprite[] dieSprites;
    
    public Board board;

    private void Awake()
    {
        int cols = GameManager.Instance.rules.cols;
        int rows = GameManager.Instance.rules.rows;

        dieCells = new GameObject[cols][];
        for (int col = 0; col < cols; col++)
        {
            dieCells = new GameObject[cols][];
            Transform column = Columns.GetChild(col);
            for (int row = 0; row < rows; row++)
            {
                dieCells[col][row] = column.GetChild(row + 1).gameObject;
            }
        }
    }

    private void Start()
    {
        board.OnBoardChanged += UpdateBoard;
    }

    private void UpdateBoard(Board board)
    {
        
    }
}