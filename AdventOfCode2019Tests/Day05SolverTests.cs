using AdventOfCode2019;
using AdventOfCode2019Tests.Properties;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace AdventOfCode2019Tests
{
    [TestClass]
    public class Day05SolverTests
    {
        [TestMethod]
        [DataRow("1002,4,3,4,33", "1002,4,3,4,99")]
        public void IntcodeProgramTest(string intcodeProgram, string expectedResultProgram)
        {
            IntcodeMachine machine = new IntcodeMachine(intcodeProgram);
            machine.Execute();

            Assert.AreEqual(expectedResultProgram, machine.GetIntcodeProgram());
        }

        [TestMethod]
        [DataRow("3,0,4,0,99", 5, 5)]
        [DataRow("3,9,8,9,10,9,4,9,99,-1,8", 8, 1)]
        [DataRow("3,9,8,9,10,9,4,9,99,-1,8", 5, 0)]
        [DataRow("3,9,7,9,10,9,4,9,99,-1,8", 5, 1)]
        [DataRow("3,9,7,9,10,9,4,9,99,-1,8", 19, 0)]
        [DataRow("3,3,1108,-1,8,3,4,3,99", 8, 1)]
        [DataRow("3,3,1108,-1,8,3,4,3,99", 5, 0)]
        [DataRow("3,3,1107,-1,8,3,4,3,99", 5, 1)]
        [DataRow("3,3,1107,-1,8,3,4,3,99", 19, 0)]
        [DataRow("3,12,6,12,15,1,13,14,13,4,13,99,-1,0,1,9", 0, 0)]
        [DataRow("3,12,6,12,15,1,13,14,13,4,13,99,-1,0,1,9", 5, 1)]
        [DataRow("3,3,1105,-1,9,1101,0,0,12,4,12,99,1", 0, 0)]
        [DataRow("3,3,1105,-1,9,1101,0,0,12,4,12,99,1", 5, 1)]
        [DataRow("3,21,1008,21,8,20,1005,20,22,107,8,21,20,1006,20,31,1106,0,36,98,0,0,1002,21,125,20,4,20,1105,1,46,104,999,1105,1,46,1101,1000,1,20,4,20,1105,1,46,98,99", 5, 999)]
        [DataRow("3,21,1008,21,8,20,1005,20,22,107,8,21,20,1006,20,31,1106,0,36,98,0,0,1002,21,125,20,4,20,1105,1,46,104,999,1105,1,46,1101,1000,1,20,4,20,1105,1,46,98,99", 8, 1000)]
        [DataRow("3,21,1008,21,8,20,1005,20,22,107,8,21,20,1006,20,31,1106,0,36,98,0,0,1002,21,125,20,4,20,1105,1,46,104,999,1105,1,46,1101,1000,1,20,4,20,1105,1,46,98,99", 19, 1001)]
        public void ExecuteIntcodeProgramPart2Test(string intcodeProgram, int input, int expectedResult)
        {
            Assert.AreEqual(expectedResult, new Day5Solver(intcodeProgram).ExecuteIntcodeProgram(new List<int>() { input }));
        }


        [TestMethod]
        public void Part1Test()
        {
            Assert.AreEqual(6731945, (new Day5Solver(Resources.Day5Input)).ExecuteIntcodeProgram(new List<int>() { 1 }));
        }

        [TestMethod]
        public void Part2Test()
        {
            Assert.AreEqual(9571668, (new Day5Solver(Resources.Day5Input)).ExecuteIntcodeProgram(new List<int>() { 5 }));
        }
    }
}