using System;
using System.Collections.Generic;
using System.Text;
using AdventOfCode2019;
using AdventOfCode2019Tests.Properties;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode2019Tests
{
    [TestClass]
    public class Day12SolverTests
    {
        [TestMethod]
        [DataRow("<x=-1, y= 0, z= 2>\n<x= 2, y=-10, z=-7>\n<x= 4, y=-8, z= 8>\n<x= 3, y= 5, z=-1>", 1, "pos=<x= 2, y=-1, z= 1>, vel=<x= 3, y=-1, z=-1>\npos=<x= 3, y=-7, z=-4>, vel=<x= 1, y= 3, z= 3>\npos=<x= 1, y=-7, z= 5>, vel=<x=-3, y= 1, z=-3>\npos=<x= 2, y= 2, z= 0>, vel=<x=-1, y=-3, z= 1>")]
        [DataRow("<x=-1, y= 0, z= 2>\n<x= 2, y=-10, z=-7>\n<x= 4, y=-8, z= 8>\n<x= 3, y= 5, z=-1>", 2, "pos=<x= 5, y=-3, z=-1>, vel=<x= 3, y=-2, z=-2>\npos=<x= 1, y=-2, z= 2>, vel=<x=-2, y= 5, z= 6>\npos=<x= 1, y=-4, z=-1>, vel=<x= 0, y= 3, z=-6>\npos=<x= 1, y=-4, z= 2>, vel=<x=-1, y=-6, z= 2>")]
        [DataRow("<x=-1, y= 0, z= 2>\n<x= 2, y=-10, z=-7>\n<x= 4, y=-8, z= 8>\n<x= 3, y= 5, z=-1>", 3, "pos=<x= 5, y=-6, z=-1>, vel=<x= 0, y=-3, z= 0>\npos=<x= 0, y= 0, z= 6>, vel=<x=-1, y= 2, z= 4>\npos=<x= 2, y= 1, z=-5>, vel=<x= 1, y= 5, z=-4>\npos=<x= 1, y=-8, z= 2>, vel=<x= 0, y=-4, z= 0>")]
        [DataRow("<x=-1, y= 0, z= 2>\n<x= 2, y=-10, z=-7>\n<x= 4, y=-8, z= 8>\n<x= 3, y= 5, z=-1>", 4, "pos=<x= 2, y=-8, z= 0>, vel=<x=-3, y=-2, z= 1>\npos=<x= 2, y= 1, z= 7>, vel=<x= 2, y= 1, z= 1>\npos=<x= 2, y= 3, z=-6>, vel=<x= 0, y= 2, z=-1>\npos=<x= 2, y=-9, z= 1>, vel=<x= 1, y=-1, z=-1>")]
        [DataRow("<x=-1, y= 0, z= 2>\n<x= 2, y=-10, z=-7>\n<x= 4, y=-8, z= 8>\n<x= 3, y= 5, z=-1>", 5, "pos=<x=-1, y=-9, z= 2>, vel=<x=-3, y=-1, z= 2>\npos=<x= 4, y= 1, z= 5>, vel=<x= 2, y= 0, z=-2>\npos=<x= 2, y= 2, z=-4>, vel=<x= 0, y=-1, z= 2>\npos=<x= 3, y=-7, z=-1>, vel=<x= 1, y= 2, z=-2>")]
        [DataRow("<x=-1, y= 0, z= 2>\n<x= 2, y=-10, z=-7>\n<x= 4, y=-8, z= 8>\n<x= 3, y= 5, z=-1>", 7, "pos=<x= 2, y=-2, z= 1>, vel=<x= 3, y= 5, z=-2>\npos=<x= 1, y=-4, z=-4>, vel=<x=-2, y=-4, z=-4>\npos=<x= 3, y=-7, z= 5>, vel=<x= 0, y=-5, z= 4>\npos=<x= 2, y= 0, z= 0>, vel=<x=-1, y= 4, z= 2>")]
        [DataRow("<x=-1, y= 0, z= 2>\n<x= 2, y=-10, z=-7>\n<x= 4, y=-8, z= 8>\n<x= 3, y= 5, z=-1>", 10, "pos=<x= 2, y= 1, z=-3>, vel=<x=-3, y=-2, z= 1>\npos=<x= 1, y=-8, z= 0>, vel=<x=-1, y= 1, z= 3>\npos=<x= 3, y=-6, z= 1>, vel=<x= 3, y= 2, z=-3>\npos=<x= 2, y= 0, z= 4>, vel=<x= 1, y=-1, z=-1>")]
        public void SimulateStepsTest(string input, int steps, string newPositionsAndVelocities)
        {
            Assert.AreEqual(newPositionsAndVelocities, (new Day12Solver(input)).SimulateSteps(steps));
        }

        [TestMethod]
        [DataRow("<x=-1, y= 0, z= 2>\n<x= 2, y=-10, z=-7>\n<x= 4, y=-8, z= 8>\n<x= 3, y= 5, z=-1>", 179)]
        public void CalculateTotalEnergyTest(string input, int expectedTotalEnergy)
        {
            Day12Solver solver = new Day12Solver(input);

            solver.SimulateSteps(10);

            Assert.AreEqual(expectedTotalEnergy, solver.CalculateTotalEnergy());
        }

        [TestMethod]
        [DataRow("<x=-1, y= 0, z= 2>\n<x= 2, y=-10, z=-7>\n<x= 4, y=-8, z= 8>\n<x= 3, y= 5, z=-1>", 2772)]
        [DataRow("<x=-8, y=-10, z=0>\n<x= 5, y= 5, z= 10 >\n< x= 2, y= -7, z= 3>\n<x= 9, y= -8, z=-3>", 4686774924)]
        public void CalculateStepsToPreviousStateTest(string input, long expectedSteps)
        {
            Assert.AreEqual(expectedSteps, (new Day12Solver(input)).CalculateStepsToPreviousState());
        }


        [TestMethod]
        public void Part1Test()
        {
            Assert.AreEqual(10198, (new Day12Solver(Resources.Day12Input)).SolvePart1());
        }

        [TestMethod]
        public void Part2Test()
        {
            Assert.AreEqual(271442326847376, (new Day12Solver(Resources.Day12Input)).SolvePart2());
        }
    }
}
