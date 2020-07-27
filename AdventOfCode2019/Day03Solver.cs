using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2019
{
    public class Day3Solver : Solver
    {
        string[] path1;
        string[] path2;
        List<Point> commonPoints;

        public Day3Solver(string input)
        {
            path1 = input.Split('\n')[0].Split(',');
            path2 = input.Split('\n')[1].Split(',');
        }


        public override long SolvePart1()
        {
            CalculateCommonPoints();

            return commonPoints.Min(p => Math.Abs(p.x) + Math.Abs(p.y));
        }

        public override long SolvePart2()
        {
            if (commonPoints == null) CalculateCommonPoints();

            return commonPoints.Min(p => StepsToReach(p));
        }


        void CalculateCommonPoints()
        {
            List<Point> path1Points = GetVisitedPoints(path1);
            List<Point> path2Points = GetVisitedPoints(path2);

            commonPoints = (from p in path1Points where path2Points.Contains(p) select p).ToList();
        }

        int StepsToReach(Point p)
        {
            return StepsToReachInPath(p, path1) + StepsToReachInPath(p, path2);
        }

        int StepsToReachInPath(Point p, string[] path)
        {
            int totalSteps = 0;

            Point currentPosition = new Point(0, 0);
            for (int movement = 0; movement < path.Length && !p.Equals(currentPosition); movement++)
            {
                char direction = GetDirection(path[movement]);
                int steps = GetNumberOfSteps(path[movement]);

                int newX = currentPosition.x, newY = currentPosition.y;

                for (int i = 0; i < steps; i++)
                {
                    switch (direction)
                    {
                        case 'U': newY++; break;
                        case 'D': newY--; break;
                        case 'R': newX++; break;
                        case 'L': newX--; break;
                    }

                    totalSteps++;
                    if (p.Equals(new Point(newX, newY))) break;
                }

                currentPosition = new Point(newX, newY);
            }

            return totalSteps;
        }

        List<Point> GetVisitedPoints(string[] path)
        {
            List<Point> visitedPoints = new List<Point>();
            Point currentPosition = new Point(0, 0);

            for (int movement = 0; movement < path.Length; movement++)
            {
                char direction = GetDirection(path[movement]);
                int steps = GetNumberOfSteps(path[movement]);

                int newX = currentPosition.x, newY = currentPosition.y;

                for (int i = 0; i < steps; i++)
                {   
                    switch (direction)
                    {
                        case 'U': newY++; break;
                        case 'D': newY--; break;
                        case 'R': newX++; break;
                        case 'L': newX--; break;
                    }

                    visitedPoints.Add(new Point(newX, newY));
                }

                currentPosition = new Point(newX, newY);
            }
            
            return visitedPoints;
        }

        int GetNumberOfSteps(string movement) { return int.Parse(movement.Substring(1)); }
        char GetDirection(string movement) { return movement[0]; }
    }

    class Point
    {
        public int x, y;

        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public override bool Equals(object obj)
        {
            return obj is Point pair &&
                   x == pair.x &&
                   y == pair.y;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(x, y);
        }
    }
}
