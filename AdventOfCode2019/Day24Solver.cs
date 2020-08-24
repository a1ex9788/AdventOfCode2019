using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace AdventOfCode2019
{
    public class Day24Solver : Solver
    {
        char[,] cells;

        const char BUG_CELL = '#'; 
        const char EMPTY_CELL = '.'; 

        public Day24Solver(string input)
        {
            this.cells = new char[5,5];

            string separator = "\n";
            if (input.Contains("\r")) separator = "\r\n";
            string[] rows = input.Split(separator);
            for (int i = 0; i < 5; i++)
                for (int j = 0; j < 5; j++)
                    cells[j, i] = rows[i][j];
        }


        public override long SolvePart1()
        {
            return CalculateBiodiversityRatingOfFirstRepeatedState();
        }

        public override long SolvePart2()
        {
            return 2;
        }


        public int CalculateBiodiversityRatingOfFirstRepeatedState()
        {
            List<int> biodiversityRatings = new List<int>();
            int currentBiodiversityRating = CalculateBiodiversityRating();

            do
            {
                biodiversityRatings.Add(currentBiodiversityRating);

                SimulateOneMinute();

                currentBiodiversityRating = CalculateBiodiversityRating();
            }
            while (!biodiversityRatings.Contains(currentBiodiversityRating));

            return currentBiodiversityRating;
        }

        public void Simulate(int minutes)
        {
            for (int i = 0; i < minutes; i++)
                SimulateOneMinute();
        }

        void SimulateOneMinute()
        {
            char[,] newCells = new char[5,5];

            for (int i = 0; i < 5; i++)
                for (int j = 0; j < 5; j++)
                {
                    newCells[j, i] = cells[j, i];
                    int adyacentBugs = GetAdyacentBugs(i, j);

                    if (cells[j, i] == BUG_CELL)
                    {
                        if (adyacentBugs != 1) newCells[j, i] = EMPTY_CELL;
                    }
                    else
                        if (adyacentBugs == 1 || adyacentBugs == 2) newCells[j, i] = BUG_CELL;
                }

            cells = newCells;

            int GetAdyacentBugs(int x, int y)
            {
                int adyacentBugs = 0;
                List<(int i, int j)> adyacentCells = new List<(int i, int j)>
                {
                    (x + 1, y),
                    (x - 1, y),
                    (x, y + 1),
                    (x, y - 1),
                };

                foreach ((int i, int j) in adyacentCells)
                    if (i >= 0 && i < 5 && j >= 0 && j < 5
                        && cells[j, i] == BUG_CELL) adyacentBugs++;

                return adyacentBugs;
            }
        }

        public int CalculateBiodiversityRating()
        {
            int totalBiodeversityRating = 0;

            for (int i = 0; i < 5; i++)
                for (int j = 0; j < 5; j++)
                    if (cells[i, j] == BUG_CELL)
                        totalBiodeversityRating += (int) Math.Pow(2, j * 5 + i);

            return totalBiodeversityRating;
        }

        public string GetCells()
        {
            string s = "";

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                    s += cells[j, i];

                s += "\n";
            }

            return s.Substring(0, s.Length - 1);
        }
    }
}
