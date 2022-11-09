public class GameRules
{
    public readonly int sides = 2;

    public readonly int cols = 3;
    public readonly int rows = 3;

    public GameRules() {}

    public GameRules(int sides, int cols, int rows)
    {
        this.sides = sides;
        this.cols = cols;
        this.rows = rows;
    }
}