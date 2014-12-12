namespace PlusMinusParser
{
    public abstract class Token
    {
    }

    public class OperatorToken : Token
    {
        
    }
    public class PlusToken : OperatorToken
    {
    }

    public class MinusToken : OperatorToken
    {
    }

    public class NumberConstantToken : Token
    {
        private readonly int _value;

        public NumberConstantToken(int value)
        {
            _value = value;
        }

        public int Value
        {
            get { return _value; }
        }
    }
}
