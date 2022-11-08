using System;

public class FullBoard
{
    private readonly int sides = 2;
    private readonly Board[] boards;

    private readonly int cols = 3;
    private readonly int rows = 3;
    
    public FullBoard()
    {
        boards = new Board[sides];
        for (int side = 0; side < sides; side++)
        {
            boards[side] = new Board(cols, rows);
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