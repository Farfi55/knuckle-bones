public class Die
{
    public int value { get; protected set; }
    public int col { get; protected set; } = -1;
    
    public Die(int value, int col)
    {
        this.value = value;
        this.col = col;
    }
    public Die(int value)
    {
        this.value = value;
    }

    public static implicit operator int(Die die) => die.value;
}