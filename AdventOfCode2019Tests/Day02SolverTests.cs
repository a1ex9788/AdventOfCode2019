using AdventOfCode2019;
using AdventOfCode2019Tests.Properties;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode2019Tests
{
    [TestClass]
    public class Day02SolverTests
    {
        [TestMethod]
        [DataRow("1,0,0,0,99", "2,0,0,0,99")]
        [DataRow("2,3,0,3,99", "2,3,0,6,99")]
        [DataRow("2,4,4,5,99,0", "2,4,4,5,99,9801")]
        [DataRow("1,1,1,4,99,5,6,0,99", "30,1,1,4,2,5,6,0,99")]
        public void IntcodeProgramTest(string intcodeProgram, string expectedResultProgram)
        {
            IntcodeMachine machine = new IntcodeMachine(intcodeProgram);
            machine.Execute();

            Assert.AreEqual(expectedResultProgram, machine.GetIntcodeProgram());
        }


        [TestMethod]
        public void Part1Test()
        {
            Assert.AreEqual(3058646, (new Day2Solver(Resources.Day2Input)).SolvePart1());
        }

        [TestMethod]
        public void Part2Test()
        {
            Assert.AreEqual(8976, (new Day2Solver(Resources.Day2Input)).SolvePart2());
        }
    }
}