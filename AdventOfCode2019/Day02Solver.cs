using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2019
{
    public class Day2Solver : Solver
    {
        string intcodeProgram;

        public Day2Solver(string input)
        {
            this.intcodeProgram = input;
        }


        public override int SolvePart1()
        {
            return ExecuteIntcodeProgram(12, 2);
        }

        public override int SolvePart2()
        {
            for (int noun = 0; noun < 99; noun++)
            {
                for (int verb = 0; verb < 99; verb++)
                {
                    if (ExecuteIntcodeProgram(noun, verb) == 19690720) return int.Parse(noun + "" + verb);
                }
            }

            throw new Exception();
        }


        int ExecuteIntcodeProgram(int noun, int verb) {  return (new IntcodeMachine(intcodeProgram)).Execute(noun, verb); }
    }
}
