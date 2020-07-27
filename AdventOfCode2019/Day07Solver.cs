using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2019
{
    public class Day7Solver : Solver
    {
        string intcodeProgram;

        public Day7Solver(string input)
        {
            this.intcodeProgram = input;
        }


        public override long SolvePart1()
        {
            Console.WriteLine("Remember to write 0.");

            return GetMaxThrusterSignal();
        }

        public override long SolvePart2()
        {
            Console.WriteLine("Remember to write 0.");

            return GetMaxThrusterSignalLooping();
        }


        public int GetMaxThrusterSignal(bool testing = false)
        {
            int firstInput = 0;
            if (!testing)
            {
                Console.WriteLine("Write an input: ");
                firstInput = int.Parse(Console.ReadLine());
            }

            return GetPossiblePhaseSettings(new List<int>() { 0, 1, 2, 3, 4 }).Max(i => GetThrusterSignal(i, firstInput));
        }

        public int GetMaxThrusterSignalLooping(bool testing = false)
        {
            int firstInput = 0;
            if (!testing)
            {
                Console.Write("Write an input: ");
                firstInput = int.Parse(Console.ReadLine());
            }

            return GetPossiblePhaseSettings(new List<int>() { 5, 6, 7, 8, 9 }).Max(i => GetThrusterSignalLooping(i, firstInput));
        }

        List<List<int>> GetPossiblePhaseSettings(List<int> firstCombination)
        {
            List<List<int>> possiblePhaseSettings = new List<List<int>>();
            if (firstCombination.Contains(0)) possiblePhaseSettings.Add(firstCombination);

            string A = firstCombination[0].ToString(), B = firstCombination[1].ToString(), C = firstCombination[2].ToString(),
                D = firstCombination[3].ToString(), E = firstCombination[4].ToString();
            int firstNumber = int.Parse(A + B + C + D + E);
            int lastNumber = int.Parse(E + D + C + B + A);

            for (int i = firstNumber; i <= lastNumber; i++)
            {
                string number = i.ToString();
                if (number.Contains(A) && number.Contains(B) && number.Contains(C) && number.Contains(D) && number.Contains(E))
                    possiblePhaseSettings.Add(new List<int>() {
                        int.Parse(number[0].ToString()), int.Parse(number[1].ToString()), int.Parse(number[2].ToString()),
                        int.Parse(number[3].ToString()), int.Parse(number[4].ToString()),
                    });
            }

            return possiblePhaseSettings;
        }

        public int GetThrusterSignal(List<int> phaseSetting, int firstInput)
        {
            int lastOutput = firstInput;

            for (int i = 0; i < 5; i++)
                lastOutput = ExecuteIntcodeProgram(new List<int>() { phaseSetting[i], lastOutput });

            return lastOutput;
        }

        public int GetThrusterSignalLooping(List<int> phaseSetting, int firstInput)
        {
            int lastOutput = firstInput;
            List<IntcodeMachine> machines = new List<IntcodeMachine>();

            for (int i = 0; i < 5; i++)
                machines.Add(new IntcodeMachine(intcodeProgram));

            bool goOn = true;
            while (goOn)
                try
                {
                    for (int i = 0; i < 5; i++)
                        lastOutput = ExecuteIntcodeProgram(new List<int>() { phaseSetting[i], lastOutput }, machines[i]);
                }
                catch (Exception)
                {
                    goOn = false;
                }

            return lastOutput;
        }

        public int ExecuteIntcodeProgram(List<int> input, IntcodeMachine inputMachine = null)
        {
            IntcodeMachine machine = inputMachine;
            if (machine == null) machine = new IntcodeMachine(intcodeProgram);
            return machine.Execute(input);
        }
    }
}
