using System;
using System.Collections.Generic;
using System.Text;
using AdventOfCode2019;
using AdventOfCode2019Tests.Properties;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode2019Tests
{
    [TestClass]
    public class Day22SolverTests
    {
        [TestMethod]
        [DataRow(10, "9 8 7 6 5 4 3 2 1 0")]
        public void ShuffleWithNewStackTest(int deckSize, string shuffledCards)
        {
            Day22Solver solver = new Day22Solver(deckSize);
            solver.ShuffleWithNewStack();

            Assert.AreEqual(shuffledCards, solver.GetCards());
            Assert.IsTrue(solver.AreReferencesCorrect());
        }

        [TestMethod]
        [DataRow(3, 10, "3 4 5 6 7 8 9 0 1 2")]
        [DataRow(-4, 10, "6 7 8 9 0 1 2 3 4 5")]
        public void ShuffleWithCutTest(int cutNumber, int deckSize, string shuffledCards)
        {
            Day22Solver solver = new Day22Solver(deckSize);
            solver.ShuffleWithCut(cutNumber);

            Assert.AreEqual(shuffledCards, solver.GetCards());
            Assert.IsTrue(solver.AreReferencesCorrect());
        }

        [TestMethod]
        [DataRow(3, 10, "0 7 4 1 8 5 2 9 6 3")]
        public void ShuffleWithIncrementTest(int increment, int deckSize, string shuffledCards)
        {
            Day22Solver solver = new Day22Solver(deckSize);
            solver.ShuffleWithIncrement(increment);

            Assert.AreEqual(shuffledCards, solver.GetCards());
            Assert.IsTrue(solver.AreReferencesCorrect());
        }

        [TestMethod]
        [DataRow("deal with increment 7\ndeal into new stack\ndeal into new stack", 10, "0 3 6 9 2 5 8 1 4 7")]
        [DataRow("cut 6\ndeal with increment 7\ndeal into new stack", 10, "3 0 7 4 1 8 5 2 9 6")]
        [DataRow("cut 6\ndeal with increment 7\ndeal into new stack\ncut 2", 10, "7 4 1 8 5 2 9 6 3 0")]
        [DataRow("cut 6\ndeal with increment 7\ndeal into new stack\ncut -2", 10, "9 6 3 0 7 4 1 8 5 2")]
        [DataRow("cut 6\ndeal with increment 7\ndeal into new stack\ndeal with increment 3", 10, "3 2 1 0 9 8 7 6 5 4")]
        [DataRow("deal with increment 7\ndeal with increment 9\ncut -2", 10, "6 3 0 7 4 1 8 5 2 9")]
        [DataRow("deal into new stack\ncut -2\ndeal with increment 7\ncut 8\ncut -4\ndeal with increment 7\ncut 3\ndeal with increment 9\ndeal with increment 3\ncut -1", 10, "9 2 5 8 1 4 7 0 3 6")]
        public void ShuffleTest(string shufflesToApply, int deckSize, string shuffledCards)
        {
            Day22Solver solver = new Day22Solver(shufflesToApply, deckSize);
            solver.Shuffle();

            Assert.AreEqual(shuffledCards, solver.GetCards());
            Assert.IsTrue(solver.AreReferencesCorrect());
        }


        [TestMethod]
        public void Part1Test()
        {
            Assert.AreEqual(1867, (new Day22Solver(Resources.Day22Input)).SolvePart1());
        }

        [TestMethod]
        public void Part2Test()
        {
            Assert.AreEqual(22, (new Day22Solver(Resources.Day22Input)).SolvePart2());
        }
    }
}
