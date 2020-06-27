using AdventOfCode2019;
using AdventOfCode2019Tests.Properties;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode2019Tests
{
    [TestClass]
    public class Day03SolverTests
    {
        [TestMethod]
        [DataRow("R8,U5,L5,D3\nU7,R6,D4,L4", 6)]
        [DataRow("R75,D30,R83,U83,L12,D49,R71,U7,L72\nU62,R66,U55,R34,D71,R55,D58,R83", 159)]
        [DataRow("R98,U47,R26,D63,R33,U87,L62,D20,R33,U53,R51\nU98,R91,D20,R16,D67,R40,U7,R15,U6,R7", 135)]
        public void ManhattanDistanceTest(string paths, int expectedManhattanDistance)
        {
            Assert.AreEqual((new Day3Solver(paths)).SolvePart1(), expectedManhattanDistance);
        }

        [TestMethod]
        [DataRow("R8,U5,L5,D3\nU7,R6,D4,L4", 30)]
        [DataRow("R75,D30,R83,U83,L12,D49,R71,U7,L72\nU62,R66,U55,R34,D71,R55,D58,R83", 610)]
        [DataRow("R98,U47,R26,D63,R33,U87,L62,D20,R33,U53,R51\nU98,R91,D20,R16,D67,R40,U7,R15,U6,R7", 410)]
        public void FewestStepsTest(string paths, int expectedSteps)
        {
            Assert.AreEqual((new Day3Solver(paths)).SolvePart2(), expectedSteps);
        }


        [TestMethod]
        public void Part1Test()
        {
            Assert.AreEqual((new Day3Solver(Resources.Day3Input)).SolvePart1(), 627);
        }

        [TestMethod]
        public void Part2Test()
        {
            Assert.AreEqual((new Day3Solver(Resources.Day3Input)).SolvePart2(), 13190);
        }
    }
}
