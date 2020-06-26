using AdventOfCode2019;
using AdventOfCode2019Tests.Properties;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode2019Tests
{
    [TestClass]
    public class Day01SolverTests
    {
        [DataTestMethod]
        [DataRow(12, 2)]
        [DataRow(14, 2)]
        [DataRow(1969, 654)]
        [DataRow(100756, 33583)]
        public void CalculeFuelTest(int mass, int expectedFuel)
        {
            Assert.AreEqual((new Day1Solver()).CalculeFuel(mass), expectedFuel);
        }

        [DataTestMethod]
        [DataRow(14, 2)]
        [DataRow(1969, 966)]
        [DataRow(100756, 50346)]
        public void CalculeFuelWithFuelMassTest(int mass, int expectedFuel)
        {
            Assert.AreEqual((new Day1Solver()).CalculeFuelCountingFuelMass(mass), expectedFuel);
        }


        [TestMethod]
        public void Part1Test()
        {
            Assert.AreEqual((new Day1Solver(Resources.Day1Input)).SolvePart1(), 3414791);
        }

        [TestMethod]
        public void Part2Test()
        {
            Assert.AreEqual((new Day1Solver(Resources.Day1Input)).SolvePart2(), 5119312);
        }
    }
}