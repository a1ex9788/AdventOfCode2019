using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using AdventOfCode2019;
using AdventOfCode2019Tests.Properties;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode2019Tests
{
    [TestClass]
    public class Day24SolverTests
    {
        [TestMethod]
        [DataRow("....#\n#..#.\n#..##\n..#..\n#....", 1, "#..#.\n####.\n###.#\n##.##\n.##..")]
        [DataRow("....#\n#..#.\n#..##\n..#..\n#....", 2, "#####\n....#\n....#\n...#.\n#.###")]
        [DataRow("....#\n#..#.\n#..##\n..#..\n#....", 3, "#....\n####.\n...##\n#.##.\n.##.#")]
        [DataRow("....#\n#..#.\n#..##\n..#..\n#....", 4, "####.\n....#\n##..#\n.....\n##...")]
        public void SimulateTest(string input, int minutes, string expectedCells)
        {
            Day24Solver solver = new Day24Solver(input);

            solver.Simulate(minutes);

            Assert.AreEqual(expectedCells, solver.GetCells());
        }

        [TestMethod]
        [DataRow(".....\n.....\n.....\n#....\n.#...", 2129920)]
        public void CalculateBiodiversityRatingTest(string input, int expectedBiodiversityRating)
        {
            Assert.AreEqual(expectedBiodiversityRating, (new Day24Solver(input)).CalculateBiodiversityRating());
        }

        [TestMethod]
        [DataRow("....#\n#..#.\n#..##\n..#..\n#....", 2129920)]
        public void CalculateBiodiversityRatingOfFirstRepeatedStateTest(string input, int expectedBiodiversityRating)
        {
            Assert.AreEqual(expectedBiodiversityRating, (new Day24Solver(input)).CalculateBiodiversityRatingOfFirstRepeatedState());
        }


        [TestMethod]
        public void Part1Test()
        {
            Assert.AreEqual(11, (new Day24Solver(Resources.Day24Input)).SolvePart1());
        }

        [TestMethod]
        public void Part2Test()
        {
            Assert.AreEqual(22, (new Day24Solver(Resources.Day24Input)).SolvePart2());
        }
    }
}
