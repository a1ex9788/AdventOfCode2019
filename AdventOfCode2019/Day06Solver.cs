using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2019
{
    public class Day6Solver : Solver
    {
        List<Orbit> orbits;
        List<string> planets;

        public Day6Solver(string input)
        {
            orbits = new List<Orbit>();
            planets = new List<string>();

            string separator = "\n";
            if (input.Contains("\r")) separator = "\r\n";
            foreach (string orbit in input.Split(separator))
            {
                string[] orbitMembers = orbit.Split(')');
                string father = orbitMembers[0];
                string son = orbitMembers[1];

                orbits.Add(new Orbit(father, son));

                if (!planets.Contains(father)) planets.Add(father);
                if (!planets.Contains(son)) planets.Add(son);
            }
        }


        public override long SolvePart1()
        {
            int numberOfOrbits = 0;

            foreach (string planet in planets) numberOfOrbits += HowManyOrbitsThereAre(planet);

            return numberOfOrbits;
        }

        public override long SolvePart2()
        {
            return HowManyOrbitsBetween("YOU", "SAN");
        }


        Orbit FindOrbitAsChild(string planetName)
        {
            foreach (Orbit o in orbits)
                if (o.son.Equals(planetName)) return o;

            return null;
        }

        public int HowManyOrbitsThereAre(string planetName)
        {
            int numberOfOrbits = 0;
            Orbit currentOrbit = FindOrbitAsChild(planetName);

            while (currentOrbit != null)
            {
                numberOfOrbits++;
                currentOrbit = FindOrbitAsChild(currentOrbit.father);
            }

            return numberOfOrbits;
        }

        public int HowManyOrbitsBetween(string planetOneName, string planetTwoName)
        {
            int numberOfOrbits = 0;

            Orbit currentOrbit = FindOrbitAsChild(planetOneName);
            List<string> planetOneVisitedPlanets = new List<string>();
            while (currentOrbit != null)
            {
                planetOneVisitedPlanets.Add(currentOrbit.father);
                currentOrbit = FindOrbitAsChild(currentOrbit.father);
            }

            currentOrbit = FindOrbitAsChild(planetTwoName);
            while (currentOrbit != null)
            {
                if (planetOneVisitedPlanets.Contains(currentOrbit.father)) break;
                currentOrbit = FindOrbitAsChild(currentOrbit.father);
            }

            string commonFather = currentOrbit.father;

            currentOrbit = FindOrbitAsChild(planetOneName);
            while (currentOrbit != null)
            {
                if (currentOrbit.father.Equals(commonFather)) break;
                numberOfOrbits++;
                currentOrbit = FindOrbitAsChild(currentOrbit.father);
            }

            currentOrbit = FindOrbitAsChild(planetTwoName);
            while (currentOrbit != null)
            {
                if (currentOrbit.father.Equals(commonFather)) break;
                numberOfOrbits++;
                currentOrbit = FindOrbitAsChild(currentOrbit.father);
            }

            return numberOfOrbits;
        }
    }
}

class Orbit
{
    public string father, son;

    public Orbit(string father, string son)
    {
        this.father = father;
        this.son = son;
    }
}
