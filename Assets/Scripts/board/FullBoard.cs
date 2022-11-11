using System;

public class FullBoard
{
    private readonly Board[] boards;
    private readonly int sides;
    private readonly int cols;
    private readonly int rows;

    // public FullBoard() : this(new GameRules()) { }
        
    
    public FullBoard(GameRules rules)
    {
        this.sides = rules.sides;
        this.cols = rules.cols;
        this.rows = rules.rows;

        boards = new Board[sides];
        for (int side = 0; side < sides; side++)
        {
            boards[side] = new Board(rules);
            boards[side].OnDiePlaced += OnDiePlaced;
        }

    }

    private void OnDiePlaced(Board board, int dieValue, int col)
    {
        for (int side = 0; side < sides; side++)
        {
            if (boards[side] != board)
            {
                boards[side].RemoveDiceWithValue(dieValue, col);
            }
        }
    }


    public int GetColumnScore(int side, int col)
    {
        if (!IsSideInRange(side))
            throw new ArgumentOutOfRangeException();
        
        return GetBoard(side).GetColumnScore(col);
    }
    
    public int GetSideScore(int side)
    {
        if (!IsSideInRange(side))
            throw new ArgumentOutOfRangeException();
        
        return GetBoard(side).GetTotalScore();
    }

    public Board GetBoard(int side)
    {
        if (!IsSideInRange(side))
            throw new ArgumentOutOfRangeException();
        return boards[side];
    }


    private bool IsSideInRange(int side) => 0 <= side && side < sides;
}