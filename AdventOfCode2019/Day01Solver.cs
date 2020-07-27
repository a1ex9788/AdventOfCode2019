using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2019
{
    public class Day1Solver : Solver
    {
        List<int> masses = new List<int>();

        public Day1Solver(string input = "1\n1")
        {
            foreach (string s in input.Split('\n')) masses.Add(int.Parse(s));
        }


        public override long SolvePart1()
        {
            int sum = 0;

            foreach (int mass in masses) sum += CalculateFuel(mass);

            return sum;
        }

        public override long SolvePart2()
        {
            int sum = 0;

            foreach (int mass in masses) sum += CalculateFuelCountingFuelMass(mass);

            return sum;
        }


        public int CalculateFuel(int mass) { return Convert.ToInt32(Math.Floor(mass / 3.0)) - 2; }

        public int CalculateFuelCountingFuelMass(int mass)
        {
            int fuelMass = mass, sum = 0;

            while (fuelMass > 0)
            {
                fuelMass = CalculateFuel(fuelMass);
                if (fuelMass < 0) break;
                sum += fuelMass;
            }

            return sum;
        }
    }
}
