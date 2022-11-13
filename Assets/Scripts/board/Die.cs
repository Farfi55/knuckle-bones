namespace board
{
    public class Die
    {
        public const int DefaultDieValue = 0;
        
        public int Value { get; protected set; }
        public int Column { get; protected set; } = -1;
    
        public Die(int value, int column)
        {
            this.Value = value;
            this.Column = column;
        }
        public Die(int value)
        {
            this.Value = value;
        }

        public static implicit operator int(Die die) => die.Value;
    
        public static implicit operator Die(int value) => new Die(value);
    
        public bool IsPlaced() => Column != -1;
    
    }
}