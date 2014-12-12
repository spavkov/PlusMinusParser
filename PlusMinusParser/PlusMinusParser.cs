using System;
using System.Collections.Generic;

namespace PlusMinusParser
{
    // Expression := Number {Operator Number}
    // Operator   := "+" | "-"
    // Number     := Digit{Digit}
    // Digit      := "0" | "1" | "2" | "3" | "4" | "5" | "6" | "7" | "8" | "9" 
    public class PlusMinusParser
    {
        private readonly IEnumerator<Token> _tokens;

        public PlusMinusParser(IEnumerable<Token> tokens)
        {
            _tokens = tokens.GetEnumerator();
        }

        public int Parse()
        {
            var result = 0;
            while (_tokens.MoveNext())
            {
                result = ParseExpression();

                if (_tokens.MoveNext())
                {
                    if (_tokens.Current is OperatorToken)
                    {
                        var op = _tokens.Current;

                        var secondNumber = Parse();
                        if (op is PlusToken)
                        {
                            return result + secondNumber;
                        }
                        if (op is MinusToken)
                            return result - secondNumber;

                        throw new Exception("Unsupported operator: " + op);

                    }
                    throw new Exception("Expecting operator after expression, but got " + _tokens.Current);
                }
            }

            return result;
        }

        private int ParseExpression()
        {
            var number = ParseNumber();
            if (!_tokens.MoveNext())
                return number;

            if (_tokens.Current is OperatorToken)
            {
                var op = _tokens.Current;
                _tokens.MoveNext();

                var secondNumber = ParseNumber();
                if (op is PlusToken)
                {
                    return number + secondNumber;
                }
                if (op is MinusToken)
                    return number - secondNumber;

                throw new Exception("Unsupported operator: " + op);
            }
            
            throw new Exception("Expecting operator after number, but got " + _tokens.Current);
        }

        private int ParseNumber()
        {
            if (_tokens.Current is NumberConstantToken)
            {
                return (_tokens.Current as NumberConstantToken).Value;
            }

            throw new Exception("Expected a number constant but found " + _tokens.Current);
        }
    }
}