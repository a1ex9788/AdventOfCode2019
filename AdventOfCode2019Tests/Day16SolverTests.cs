using System;
using System.Collections.Generic;
using System.Text;
using AdventOfCode2019;
using AdventOfCode2019Tests.Properties;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode2019Tests
{
    [TestClass]
    public class Day16SolverTests
    {
        [TestMethod]
        [DataRow("12345678", 0, "10-1010-10")]
        [DataRow("12345678", 1, "01100-1-10")]
        [DataRow("12345678", 2, "00111000")]
        [DataRow("12345678", 3, "00011110")]
        [DataRow("12345678", 4, "00001111")]
        [DataRow("12345678", 5, "00000111")]
        [DataRow("12345678", 6, "00000011")]
        [DataRow("12345678", 7, "00000001")]
        public void CalculatePatternTest(string input, int phase, string expectedPhase)
        {
            Assert.AreEqual(expectedPhase, Day16Solver.IntListToString((new Day16Solver(input)).CalculatePattern(phase)).Substring(0, expectedPhase.Length));
        }

        [TestMethod]
        [DataRow("12345678", 1, 48226158)]
        [DataRow("12345678", 2, 34040438)]
        [DataRow("12345678", 3, 03415518)]
        [DataRow("12345678", 4, 01029498)]
        [DataRow("80871224585914546619083218645595", 100, 24176176)]
        [DataRow("19617804207202209144916044189917", 100, 73745418)]
        [DataRow("69317163492948606335995924319873", 100, 52432133)]
        public void ExecuteTest(string input, int phases, int expectedOutput)
        {
            Assert.AreEqual(expectedOutput, (new Day16Solver(input)).Execute(phases));
        }

        [TestMethod]
        [DataRow("03036732577212944063491565474664", 100, 84462026)]
        [DataRow("02935109699940807407585447034323", 100, 78725270)]
        [DataRow("03081770884921959731165446850517", 100, 53553731)]
        public void ExecuteRepeatingInput10000TimesTest(string input, int phases, int expectedOutput)
        {
            Assert.AreEqual(expectedOutput, (new Day16Solver(input)).ExecuteRepeatingInput10000Times(phases));
        }


        [TestMethod]
        public void Part1Test()
        {
            Assert.AreEqual(29956495, (new Day16Solver(Resources.Day16Input)).SolvePart1());
        }

        [TestMethod]
        public void Part2Test()
        {
            Assert.AreEqual(22, (new Day16Solver(Resources.Day16Input)).SolvePart2());
        }
    }
}
