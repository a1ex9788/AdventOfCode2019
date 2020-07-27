using System;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2019
{
    public class Day4Solver : Solver
    {
        int firstNumber, secondNumber;

        public Day4Solver() { }

        public Day4Solver(string input)
        {
            firstNumber = int.Parse(input.Split('-')?[0]);
            secondNumber = int.Parse(input.Split('-')?[1]);
        }


        public override long SolvePart1()
        {
            int numbers = 0;

            for (int i = firstNumber; i <= secondNumber; i++)
                if (FitIn(i)) numbers++;

            return numbers;
        }

        public override long SolvePart2()
        {
            int numbers = 0;

            for (int i = firstNumber; i <= secondNumber; i++)
                if (FitInJustTwoAdjacentEqualDigits(i)) numbers++;

            return numbers;
        }


        public bool FitIn(int number)
        {
            return HasAdjacentEqualNumbers(number) && NeverDecreases(number);
        }

        public bool FitInJustTwoAdjacentEqualDigits(int number)
        {            
            return HasJustTwoAdjacentEqualNumbers(number) && NeverDecreases(number);
        }

        bool HasAdjacentEqualNumbers(int number)
        {
            string stringNumber = number.ToString();
            char previousDigit = stringNumber[0];
            int equalAdjacentDigits = 0;

            for (int i = 1; i < stringNumber.Length; i++)
            {
                if (Convert.ToInt32(stringNumber[i]) == Convert.ToInt32(previousDigit)) equalAdjacentDigits++;
                previousDigit = stringNumber[i];
            }

            return equalAdjacentDigits != 0;
        }

        bool NeverDecreases(int number)
        {
            string stringNumber = number.ToString();
            char previousDigit = stringNumber[0];

            for (int i = 1; i < stringNumber.Length; i++)
            {
                if (Convert.ToInt32(stringNumber[i]) < Convert.ToInt32(previousDigit)) return false;
                previousDigit = stringNumber[i];
            }

            return true;
        }

        bool HasJustTwoAdjacentEqualNumbers(int number)
        {
            string stringNumber = number.ToString();
            char previousDigit = stringNumber[0];
            int equalAdjacentDigits = 0;
            bool twoAdjacentDigits = false;

            for (int i = 1; i < stringNumber.Length; i++)
            {
                if (Convert.ToInt32(stringNumber[i]) == Convert.ToInt32(previousDigit))
                {
                    equalAdjacentDigits++;

                    if (equalAdjacentDigits == 1) twoAdjacentDigits = true;
                    if (equalAdjacentDigits > 1) twoAdjacentDigits = false;
                }
                else
                {
                    equalAdjacentDigits = 0;
                    if (twoAdjacentDigits) return true;
                }

                previousDigit = stringNumber[i];
            }

            return twoAdjacentDigits;
        }
    }
}
