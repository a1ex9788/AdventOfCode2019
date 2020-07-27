using AdventOfCode2019;
using AdventOfCode2019Tests.Properties;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode2019Tests
{
    [TestClass]
    public class Day01SolverTests
    {
        [TestMethod]
        [DataRow(12, 2)]
        [DataRow(14, 2)]
        [DataRow(1969, 654)]
        [DataRow(100756, 33583)]
        public void CalculateFuelTest(int mass, int expectedFuel)
        {
            Assert.AreEqual(expectedFuel, (new Day1Solver()).CalculateFuel(mass));
        }

        [TestMethod]
        [DataRow(14, 2)]
        [DataRow(1969, 966)]
        [DataRow(100756, 50346)]
        public void CalculateFuelWithFuelMassTest(int mass, int expectedFuel)
        {
            Assert.AreEqual(expectedFuel, (new Day1Solver()).CalculateFuelCountingFuelMass(mass));
        }


        [TestMethod]
        public void Part1Test()
        {
            Assert.AreEqual(3414791, (new Day1Solver(Resources.Day1Input)).SolvePart1());
        }

        [TestMethod]
        public void Part2Test()
        {
            Assert.AreEqual(5119312, (new Day1Solver(Resources.Day1Input)).SolvePart2());
        }
    }
}