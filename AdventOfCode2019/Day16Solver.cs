using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;

namespace AdventOfCode2019
{
    public class Day16Solver : Solver
    {
        List<int> digits;
        List<int> basePattern;

        public Day16Solver(string input)
        {
            digits = StringToIntList(input);

            basePattern = new List<int> { 0, 1, 0, -1 };
        }


        public override long SolvePart1()
        {
            return Execute(100);
        }

        public override long SolvePart2()
        {
            return ExecuteRepeatingInput10000Times(100);
        }


        public int Execute(int phases)
        {
            for (int i = 0; i < phases; i++)
                Execute1Phase();

            return int.Parse(IntListToString(digits).Substring(0, 8));
        }

        public int ExecuteRepeatingInput10000Times(int phases)
        {
            int initialResultPosition = CalculateInitialResultPosition();

            List<int> oldDigits = digits;
            List<int> newDigits = new List<int>();

            for (int i = 0; i < 10000; i++)
                newDigits.AddRange(oldDigits);

            digits = newDigits;

            for (int i = 0; i < phases; i++)
                Execute1Phase();

            return int.Parse(IntListToString(digits).Substring(initialResultPosition, 8));

            int CalculateInitialResultPosition()
            {
                string digitsToSkip = "";

                for (int i = 0; i < 7; i++)
                    digitsToSkip += digits[i];

                return int.Parse(digitsToSkip);
            }
        }

        void Execute1Phase()
        {
            List<int> newDigits = new List<int>();

            for (int i = 0; i < digits.Count; i++)
            {
                List<int> pattern = CalculatePattern(i);

                int digit = 0;

                for (int j = digits.Count - 1; j < digits.Count; j++)
                {
                    if (pattern[j] == 0)
                    {
                        j += i;
                        continue;
                    }

                    digit += digits[j] * pattern[j];
                }

                newDigits.Add(int.Parse(digit.ToString().Last() + ""));
            }

            digits = newDigits;
        }

        public List<int> CalculatePattern(int phase)
        {
            List<int> pattern = new List<int>();

            for (int i = 0; i < basePattern.Count; i++)
                for (int j = -1; j < phase; j++)
                    pattern.Add(basePattern[i]);

            while (pattern.Count - 1 < digits.Count) pattern.AddRange(pattern);

            pattern.RemoveAt(0);

            return pattern;
        }

        List<int> StringToIntList(string s)
        {
            List<int> array = new List<int>();

            for (int i = 0; i < s.Length; i++)
                array.Add(int.Parse(s[i] + ""));

            return array;
        }

        public static string IntListToString(List<int> array)
        {
            string s = "";

            foreach (int i in array)
                s += i;

            return s;
        }
    }
}
