using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2019
{
    public class Day5Solver : Solver
    {
        string intcodeProgram;

        public Day5Solver(string input)
        {
            this.intcodeProgram = input;
        }


        public override int SolvePart1()
        {
            Console.WriteLine("Remember to write 1.");

            return ExecuteIntcodeProgram();
        }

        public override int SolvePart2()
        {
            Console.WriteLine("\nRemember to write 5.");

            return ExecuteIntcodeProgram();
        }


        public int ExecuteIntcodeProgram(List<int> input = null) { return (new IntcodeMachine(intcodeProgram)).Execute(input); }
    }
}
