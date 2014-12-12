using System;
using NUnit.Framework;

namespace PlusMinusParser.Tests
{
    [TestFixture]
    public class PlusMinusParserUnitTests
    {
        [TestCase("1", ExpectedResult = 1)]
        [TestCase("2", ExpectedResult = 2)]
        [TestCase("1+1", ExpectedResult = 2)]
        [TestCase("1+1+1", ExpectedResult = 3)]
        [TestCase("1-1", ExpectedResult = 0)]
        [TestCase("0-0", ExpectedResult = 0)]
        [TestCase("0-1", ExpectedResult = -1)]
        [TestCase("1+2+3+4+5+6", ExpectedResult = 21)]
        [TestCase("100-100", ExpectedResult = 0)]
        [TestCase("100-100+1", ExpectedResult = 1)]
        [TestCase("100-100+100", ExpectedResult = 100)]
        [TestCase("1 2", ExpectedException = typeof(Exception))]
        [TestCase("+", ExpectedException = typeof(Exception))]
        [TestCase("++", ExpectedException = typeof(Exception))]
        [TestCase("--", ExpectedException = typeof(Exception))]
        [TestCase("+-", ExpectedException = typeof(Exception))]
        [TestCase("-+", ExpectedException = typeof(Exception))]
        [TestCase("-", ExpectedException = typeof(Exception))]
        [TestCase("1-1 2", ExpectedException = typeof(Exception))]
        [TestCase("10-100", ExpectedResult = -90)]
        public int CanParseExpressions(string expression)
        {
            var tokens = new PlusMinusTokenizer().Scan(expression);
            var parser = new PlusMinusParser(tokens);
            var res = parser.Parse();
            return res;
        }
    }
}
