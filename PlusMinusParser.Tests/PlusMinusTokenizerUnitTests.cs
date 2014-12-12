using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using NUnit.Framework;

namespace PlusMinusParser.Tests
{
    [TestFixture]
    public class PlusMinusTokenizerUnitTests
    {
        [TestCase("+", ExpectedResult = typeof (PlusToken))]
        [TestCase("-", ExpectedResult = typeof (MinusToken))]
        [TestCase(" +", ExpectedResult = typeof (PlusToken))]
        [TestCase(" -", ExpectedResult = typeof (MinusToken))]
        [TestCase("+ ", ExpectedResult = typeof (PlusToken))]
        [TestCase("- ", ExpectedResult = typeof (MinusToken))]
        [TestCase(" + ", ExpectedResult = typeof (PlusToken))]
        [TestCase(" - ", ExpectedResult = typeof (MinusToken))]
        [TestCase("a", ExpectedException = typeof (Exception))]
        [TestCase("1+1a", ExpectedException = typeof(Exception))]
        public Type CanParseOperatorTokens(string expression)
        {
            var tokens = new PlusMinusTokenizer().Scan(expression);
            return (tokens.First().GetType());
        }

        [Test]
        public void CanParseSingleDigitIntegers()
        {
            for (int i = 0; i < 9; i++)
            {
                var tokens = new PlusMinusTokenizer().Scan(i.ToString(CultureInfo.InvariantCulture)).ToList();
                Assert.AreEqual(i, (tokens.First() as NumberConstantToken).Value);
            }
        }

        [TestCase("123", ExpectedResult = 123)]
        [TestCase("23", ExpectedResult = 23)]
        [TestCase("4103", ExpectedResult = 4103)]
        public int CanParseNumbersNumbersWithMoreDigits(string expression)
        {
            var tokens = new PlusMinusTokenizer().Scan(expression).ToList();
            return (tokens.First() as NumberConstantToken).Value;
        }

        [TestCaseSource("TestExpressions")]
        public void CanParseExpressions(KeyValuePair<string, List<Token>> sampleExpressionAndExpectedResult)
        {
            var tokens = new PlusMinusTokenizer().Scan(sampleExpressionAndExpectedResult.Key).ToList();
            var expected = sampleExpressionAndExpectedResult.Value;

            Assert.AreEqual(expected.Count, tokens.Count);
        }

        private static List<KeyValuePair<string, List<Token>>> TestExpressions()
        {
            return new List<KeyValuePair<string, List<Token>>>
            {
                    new KeyValuePair<string, List<Token>>("1+2", new List<Token>
                    {
                        new NumberConstantToken(1),
                        new PlusToken(),
                        new NumberConstantToken(2)
                    }),
                    new KeyValuePair<string, List<Token>>("1+2-3", new List<Token>
                    {
                        new NumberConstantToken(1),
                        new PlusToken(),
                        new NumberConstantToken(2),
                        new MinusToken(),
                        new NumberConstantToken(3)
                    }),
            };

        }
    }
}
