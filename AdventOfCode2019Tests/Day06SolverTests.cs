using AdventOfCode2019;
using AdventOfCode2019Tests.Properties;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode2019Tests
{
    [TestClass]
    public class Day06SolverTests
    {
        [TestMethod]
        [DataRow("COM)B\nB)C\nC)D\nD)E\nE)F\nB)G\nG)H\nD)I\nE)J\nJ)K\nK)L", "D", 3)]
        [DataRow("COM)B\nB)C\nC)D\nD)E\nE)F\nB)G\nG)H\nD)I\nE)J\nJ)K\nK)L", "L", 7)]
        [DataRow("COM)B\nB)C\nC)D\nD)E\nE)F\nB)G\nG)H\nD)I\nE)J\nJ)K\nK)L", "COM", 0)]
        public void HoyManyOrbitsThereAreTest(string orbits, string planetName, int numberOfOrbits)
        {
            Assert.AreEqual(numberOfOrbits, (new Day6Solver(orbits)).HowManyOrbitsThereAre(planetName));
        }

        [TestMethod]
        [DataRow("COM)B\nB)C\nC)D\nD)E\nE)F\nB)G\nG)H\nD)I\nE)J\nJ)K\nK)L\nK)YOU\nI)SAN", "YOU", "SAN", 4)]
        public void HoyManyOrbitsBetweenTest(string orbits, string planetOneName, string planetTwoName, int numberOfOrbits)
        {
            Assert.AreEqual(numberOfOrbits, (new Day6Solver(orbits)).HowManyOrbitsBetween(planetOneName, planetTwoName));
        }


        [TestMethod]
        public void Part1Test()
        {
            Assert.AreEqual(162816, (new Day6Solver(Resources.Day6Input)).SolvePart1());
        }

        [TestMethod]
        public void Part2Test()
        {
            Assert.AreEqual(304, (new Day6Solver(Resources.Day6Input)).SolvePart2());
        }
    }
}