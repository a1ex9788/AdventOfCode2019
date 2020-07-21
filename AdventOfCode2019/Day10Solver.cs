using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading;

namespace AdventOfCode2019
{
    public class Day10Solver : Solver
    {
        string[] map;
        List<Asteroid> asteroids;
        Asteroid monitoringStation;

        public Day10Solver(string input = "")
        {
            string separator = "\n";
            if (input.Contains("\r")) separator = "\r\n";
            map = input.Split(separator);

            asteroids = new List<Asteroid>();

            for (int j = 0; j < map.Length; j++)
                for (int i = 0; i < map[0].Length; i++)
                    if (map[j][i] == '#')
                        asteroids.Add(new Asteroid(i, j));
        }


        public override int SolvePart1()
        {
            int maxNumberOfAsteroids = 0;

            foreach (Asteroid a in asteroids)
            {
                int newNumberOfAsteroids = HowManyVisibleAsteroids(a.x, a.y);

                if (newNumberOfAsteroids > maxNumberOfAsteroids)
                {
                    maxNumberOfAsteroids = newNumberOfAsteroids;
                    monitoringStation = a;
                }
            }

            return maxNumberOfAsteroids;
        }

        public override int SolvePart2()
        {
            (int x, int y) = GetTheNVaporizedAsteroid(200);

            return x * 100 + y;
        }


        public int HowManyVisibleAsteroids(int x, int y)
        {
            return GetVisibleAsteroids(x, y).Count;
        }

        public List<Asteroid> GetVisibleAsteroids(int x, int y, bool vaporizeVisibleAsteroids = false)
        {
            Asteroid refferenceAsteroid = new Asteroid(x, y);
            List<Asteroid> visibleAsteroids = new List<Asteroid>();
            List<Asteroid> asteroidsToVaporize = new List<Asteroid>();

            foreach (Asteroid p in asteroids)
            {
                if (refferenceAsteroid.Equals(p)) continue;

                (int xFactor, int yFactor) = refferenceAsteroid.GetDistanceFactor(p);

                int currentX = refferenceAsteroid.x, currentY = refferenceAsteroid.y;

                do
                {
                    currentX += xFactor;
                    currentY += yFactor;
                }
                while (currentY < map.Length && currentY >= 0 
                    && currentX < map[0].Length && currentX >= 0
                    && map[currentY][currentX] != '#');

                Asteroid currentAsteroid = new Asteroid(currentX, currentY);

                if (p.Equals(currentAsteroid))
                {
                    visibleAsteroids.Add(p);

                    if (vaporizeVisibleAsteroids) asteroidsToVaporize.Add(currentAsteroid);
                }
            }

            foreach (Asteroid a in asteroidsToVaporize) VaporizeAsteroid(a);

            return visibleAsteroids;

            void VaporizeAsteroid(Asteroid a)
            {
                string mapRow = map[a.y];
                string newRow = "";

                for (int i = 0; i < mapRow.Length; i++)
                    if (i == a.x) newRow += '.';
                    else newRow += mapRow[i];

                map[a.y] = newRow;
            }
        }

        public (int x, int y) GetTheNVaporizedAsteroid(int numberOfVaporizedAsteroid)
        {
            if (monitoringStation == null) SolvePart1();

            int vaporizedAsteroids = 0;
            List<Asteroid> visibleAsteroids = new List<Asteroid>();

            do
            {
                vaporizedAsteroids += visibleAsteroids.Count;
                visibleAsteroids = GetVisibleAsteroids(monitoringStation.x, monitoringStation.y, vaporizeVisibleAsteroids: true);
            }
            while (vaporizedAsteroids + visibleAsteroids.Count < numberOfVaporizedAsteroid);

            List<Asteroid> visibleOrderedAsteroids = visibleAsteroids.OrderByDescending(a => RefactorAngle(GetAngleWithMonitoringStation(a))).ToList();

            Asteroid nVaporizedAsteroid = visibleOrderedAsteroids.ElementAt(numberOfVaporizedAsteroid - vaporizedAsteroids - 1);

            return (nVaporizedAsteroid.x, nVaporizedAsteroid.y);

            float GetAngleWithMonitoringStation(Asteroid a) => GetAngleBetweenTwoAsteroids(monitoringStation, a);

            float RefactorAngle(float angle)
            {
                float refactoredAngle = angle + 269.99f;

                if (refactoredAngle < 0) refactoredAngle += 180;

                return refactoredAngle % 360;
            }
        }

        public float GetAngleBetweenTwoAsteroids(Asteroid refferenceAsteroid, Asteroid otherAsteroid)
        {
            float ySub = -otherAsteroid.y - -refferenceAsteroid.y;
            float xSub = otherAsteroid.x - refferenceAsteroid.x;

            if (ySub == 0) return xSub > 0 ? 0 : 180;

            float angle = MathF.Atan(ySub / xSub) * 180 / MathF.PI;

            if (xSub < 0) angle = 180 + angle;

            while (angle < 0) angle += 360;

            return angle;
        }
    }

    public class Asteroid
    {
        public int x, y;

        public Asteroid(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public override bool Equals(object obj)
        {
            return obj is Asteroid pair &&
                   x == pair.x &&
                   y == pair.y;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(x, y);
        }

        public (int x, int y) GetDistanceFactor(Asteroid p)
        {
            int xFactor = p.x - this.x;
            int yFactor = p.y - this.y;

            if (xFactor == 0) return (0, yFactor / Math.Abs(yFactor));
            if (yFactor == 0) return (xFactor / Math.Abs(xFactor), 0);

            int maxComMult = Math.Abs(MaximumCommonMultiple(xFactor, yFactor));

            return (xFactor / maxComMult, yFactor / maxComMult);
        }

        int MaximumCommonMultiple(int a, int b)
        {
            int max = Math.Max(a, b);
            int min = Math.Min(a, b);

            int res = min;

            do
            {
                res = min;
                min = max % min;
                max = res;
            }
            while (min != 0);

            return res;
        }
    }
}
