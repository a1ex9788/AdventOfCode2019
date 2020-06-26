using AdventOfCode2019.Properties;
using System;

namespace AdventOfCode2019
{
    class Program
    {
        static void Main(string[] args)
        {
            Solver solver = new Day1Solver(Resources.Day1Input);

            Console.WriteLine("-----------------------------------------------------------------------");
            Console.WriteLine("Part 1: " + solver.SolvePart1() + "\nPart 2: " + solver.SolvePart2());
            Console.WriteLine("-----------------------------------------------------------------------");
            Console.Read();
        }
    }
}
