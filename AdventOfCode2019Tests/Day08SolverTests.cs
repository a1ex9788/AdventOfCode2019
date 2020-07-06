using AdventOfCode2019;
using AdventOfCode2019Tests.Properties;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace AdventOfCode2019Tests
{
    [TestClass]
    public class Day08SolverTests
    {
        [TestMethod]
        [DataRow("123456789012", 3, 2, "123\n456\n\n789\n012")]
        [DataRow("0222112222120000", 2, 2, "02\n22\n\n11\n22\n\n22\n12\n\n00\n00")]
        public void ConstructLayersTest(string input, int pixelsWide, int pixelsTall, string expectedLayers)
        {
            Assert.AreEqual(expectedLayers, (new Day8Solver(input, pixelsWide, pixelsTall)).ConstructLayers());
        }


        [TestMethod]
        public void Part1Test()
        {
            Assert.AreEqual(2080, (new Day8Solver(Resources.Day8Input, 25, 6)).SolvePart1());
        }

        [TestMethod]
        public void Part2Test()
        {
            string finalImage = "0110010010111000110010001\n1001010010100101001010001\n1001010010100101000001010\n1111010010111001000000100\n1001010010101001001000100\n1001001100100100110000100";

            Assert.AreEqual(finalImage.GetHashCode(), (new Day8Solver(Resources.Day8Input, 25, 6)).SolvePart2());
        }
    }
}