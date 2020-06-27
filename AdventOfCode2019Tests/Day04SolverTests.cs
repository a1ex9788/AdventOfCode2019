using AdventOfCode2019;
using AdventOfCode2019Tests.Properties;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode2019Tests
{
    [TestClass]
    public class Day04SolverTests
    {
        [TestMethod]
        [DataRow(111111, true)]
        [DataRow(223450, false)]
        [DataRow(123789, false)]
        public void FitInTest(int number, bool meetsTheCriteria)
        {
            Assert.AreEqual(meetsTheCriteria, (new Day4Solver()).FitIn(number));
        }

        [TestMethod]
        [DataRow(112233, true)]
        [DataRow(123444, false)]
        [DataRow(111122, true)]
        public void FitInJustTwoAdjacentEqualDigitsTest(int number, bool meetsTheCriteria)
        {
            Assert.AreEqual(meetsTheCriteria, (new Day4Solver()).FitInJustTwoAdjacentEqualDigits(number));
        }


        [TestMethod]
        public void Part1Test()
        {
            Assert.AreEqual(1605, (new Day4Solver(Resources.Day4Input)).SolvePart1());
        }

        [TestMethod]
        public void Part2Test()
        {
            Assert.AreEqual(1102, (new Day4Solver(Resources.Day4Input)).SolvePart2());
        }
    }
}