using System;
using System.Collections.Generic;
using System.IO;

namespace PlusMinusParser
{
    // Expression := Number {Operator Number}
    // Operator   := "+" | "-"
    // Number     := Digit{Digit}
    // Digit      := "0" | "1" | "2" | "3" | "4" | "5" | "6" | "7" | "8" | "9" 
    public class PlusMinusTokenizer
    {
        private StringReader _reader;

        public IEnumerable<Token> Scan(string expression)
        {
            _reader = new StringReader(expression);

            var tokens = new List<Token>();
            while (_reader.Peek() != -1)
            {
                var c = (char) _reader.Peek();
                if (Char.IsWhiteSpace(c))
                {
                    _reader.Read();
                    continue;
                }
                
                if (Char.IsDigit(c))
                {
                    var nr = ParseNumber();
                    tokens.Add(new NumberConstantToken(nr));
                }
                else if (c == '-')
                {
                    tokens.Add(new MinusToken());
                    _reader.Read();
                }
                else if (c == '+')
                {
                    tokens.Add(new PlusToken());
                    _reader.Read();
                }
                else
                    throw new Exception("Unknown character in expression: " + c);
            }

            return tokens;
        }

        private int ParseNumber()
        {
            var digits = new List<int>();
            while (Char.IsDigit((char) _reader.Peek()))
            {
                var digit = (char) _reader.Read();
                int i;
                if (int.TryParse(Char.ToString(digit), out i))
                {
                    digits.Add(i);
                }
                else
                    throw new Exception("Could not parse integer number when parsing digit: " + digit);
            }

            var nr = 0;
            var mul = 1;
            digits.Reverse();
            digits.ForEach(d =>
            {
                nr += d*mul;
                mul *= 10;
            });

            return nr;
        }
    }
}