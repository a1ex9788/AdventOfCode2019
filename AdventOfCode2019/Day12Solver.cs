using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace AdventOfCode2019
{
    public class Day12Solver : Solver
    {
        List<Moon> moons;
        UniverseState initalState;

        public Day12Solver(string input)
        {
            moons = new List<Moon>();

            string separator = "\n";
            if (input.Contains("\r")) separator = "\r\n";
            foreach (string s in input.Split(separator))
            {
                string[] positions = s.Split(',');

                moons.Add(new Moon(
                    int.Parse(positions[0].Split('=')[1]),
                    int.Parse(positions[1].Split('=')[1]),
                    int.Parse(positions[2].Substring(0, positions[2].Length - 1).Split('=')[1])));
            }

            initalState = new UniverseState(moons);
        }


        public override long SolvePart1()
        {
            SimulateSteps(1000);

            return CalculateTotalEnergy();
        }

        public override long SolvePart2()
        {
            return CalculateStepsToPreviousState();
        }


        public string SimulateSteps(int steps)
        {
            for (int i = 0; i < steps; i++)
            {
                ApplyGravity();
                ApplyVelocities();
            }

            return GetMoonsString();
        }

        public long CalculateStepsToPreviousState()
        {
            int xFase = CalculateFase(x: true);
            int yFase = CalculateFase(y: true);
            int zFase = CalculateFase(z: true);

            return CommonMinimumMultiple(CommonMinimumMultiple(xFase, yFase), zFase);

            int CalculateFase(bool x = false, bool y = false, bool z = false)
            {
                List<UniverseState> exploredUniverseStates = new List<UniverseState> { initalState };
                UniverseState newUniverseState;
                bool exploredState;

                do
                {
                    if (x) ApplyGravity(x: true);
                    if (y) ApplyGravity(y: true);
                    if (z) ApplyGravity(z: true);

                    ApplyVelocities();

                    newUniverseState = new UniverseState(moons);
                    exploredState = exploredUniverseStates.Contains(newUniverseState);
                    exploredUniverseStates.Add(newUniverseState);
                }
                while (!exploredState);

                return exploredUniverseStates.Count - 1;
            }
        }

        void ApplyGravity(bool x = false, bool y = false, bool z = false)
        {
            if (!x && !y && !z)
            {
                x = true;
                y = true;
                z = true;
            }

            List<(Moon, Moon)> calculatedPairs = new List<(Moon, Moon)>();

            foreach (Moon m in moons)
                foreach (Moon otherMoon in moons)
                {
                    if (m.Equals(otherMoon) || calculatedPairs.Contains((otherMoon, m)))
                        continue;

                    if (x) SimulateXCoordenates(m, otherMoon);
                    if (y) SimulateYCoordenates(m, otherMoon);
                    if (z) SimulateZCoordenates(m, otherMoon);

                    calculatedPairs.Add((m, otherMoon));
                }
        }

        void ApplyVelocities()
        {
            foreach (Moon m in moons)
                m.Move();
        }

        void SimulateXCoordenates(Moon m, Moon otherMoon)
        {
            if (m.x > otherMoon.x)
            {
                m.vx--;
                otherMoon.vx++;
            }
            else if (m.x < otherMoon.x)
            {
                m.vx++;
                otherMoon.vx--;
            }
        }

        void SimulateYCoordenates(Moon m, Moon otherMoon)
        {
            if (m.y > otherMoon.y)
            {
                m.vy--;
                otherMoon.vy++;
            }
            else if (m.y < otherMoon.y)
            {
                m.vy++;
                otherMoon.vy--;
            }
        }

        void SimulateZCoordenates(Moon m, Moon otherMoon)
        {
            if (m.z > otherMoon.z)
            {
                m.vz--;
                otherMoon.vz++;
            }
            else if (m.z < otherMoon.z)
            {
                m.vz++;
                otherMoon.vz--;
            }
        }

        public int CalculateTotalEnergy()
        {
            int totalEnergy = 0;

            foreach (Moon m in moons)
                totalEnergy += m.CalculateTotalEnergy();

            return totalEnergy;
        }

        string GetMoonsString()
        {
            string res = "";

            foreach (Moon m in moons)
                res += m.ToString() + "\n";

            return res.Substring(0, res.Length - 1);
        }

        long CommonMinimumMultiple(long a, long b)
        {
            long max = Math.Max(a, b);
            long min = Math.Min(a, b);

            return (max / CommonMaximumDivisor(max, min)) * min;
        }

        long CommonMaximumDivisor(long a, long b)
        {
            long max = Math.Max(a, b);
            long min = Math.Min(a, b);

            long res = 0;

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

    public class UniverseState
    {
        public List<Moon> moons;

        public UniverseState(List<Moon> moons)
        {
            this.moons = moons.Select(m => m.GetCopy()).ToList();
        }

        public override bool Equals(object obj)
        {
            if (!(obj is UniverseState otherUniverseState)) return false;

            for (int i = 0; i < moons.Count; i++)
                if (!moons[i].Equals(otherUniverseState.moons[i])) return false;

            return true;
        }
    }

    public class Moon
    {
        public int x, y, z;
        public int vx, vy, vz;

        public Moon(int x, int y, int z)
        {
            this.x = x;
            this.y = y;
            this.z = z;

            this.vx = 0;
            this.vy = 0;
            this.vz = 0;
        }

        Moon(int x, int y, int z, int vx, int vy, int vz)
        {
            this.x = x;
            this.y = y;
            this.z = z;

            this.vx = vx;
            this.vy = vy;
            this.vz = vz;
        }

        public void Move()
        {
            x += vx;
            y += vy;
            z += vz;
        }

        public Moon GetCopy()
        {
            return new Moon(x, y, z, vx, vy, vz);
        }

        public int CalculateTotalEnergy()
        {
            return CalculatePotentialEnergy() * CalculateKineticEnergy();
        }

        int CalculatePotentialEnergy()
        {
            return (int)MathF.Abs(x) + (int)MathF.Abs(y) + (int)MathF.Abs(z);
        }

        int CalculateKineticEnergy()
        {
            return (int)MathF.Abs(vx) + (int)MathF.Abs(vy) + (int)MathF.Abs(vz);
        }

        public override string ToString()
        {
            return string.Format("pos=<x={0,2}, y={1,2}, z={2,2}>, vel=<x={3,2}, y={4,2}, z={5,2}>", x, y, z, vx, vy, vz);
        }

        public override bool Equals(object obj)
        {
            return obj is Moon otherMoon
                && x == otherMoon.x
                && y == otherMoon.y
                && z == otherMoon.z
                && vx == otherMoon.vx
                && vy == otherMoon.vy
                && vz == otherMoon.vz;
        }
    }
}
