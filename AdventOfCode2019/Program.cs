using AdventOfCode2019.Properties;
using System;

namespace AdventOfCode2019
{
    class Program
    {
        static void Main(string[] args)
        {
            Solver solver = new Day7Solver(Resources.Day7Input);

            long part1Result = solver.SolvePart1();
            long part2Result = solver.SolvePart2();

            Console.WriteLine("-------------------");
            Console.WriteLine("Part 1: " + part1Result + "\nPart 2: " + part2Result);
            Console.WriteLine("-------------------");
            Console.Read();
        }
    }
}
