using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace AdventOfCode2019
{
    public class IntcodeMachine
    {
        List<int> codes;        

        public IntcodeMachine(string program)
        {
            this.codes = new List<int>();
            foreach (string s in program.Split(',')) codes.Add(int.Parse(s));
        }

        public int Execute(List<int> input = null)
        {
            int jump = 4;
            int output = int.MinValue;

            for (int i = 0; i < codes.Count; i += jump)
            {
                int opCode = GetOpCode(codes[i]);
                int firstParMode = GetParameterMode(codes[i], 1);
                int secondParMode = GetParameterMode(codes[i], 2);
                int thirdParMode = GetParameterMode(codes[i], 3);

                int firstParameter = -1, secondParameter = -1;
                try
                {
                    firstParameter = firstParMode == 0 ? codes[codes[i + 1]] : codes[i + 1];
                    secondParameter = secondParMode == 0 ? codes[codes[i + 2]] : codes[i + 2];
                }
                catch (Exception) { };             
                
                switch (opCode)
                {
                    case 1:
                        if (thirdParMode == 0) codes[codes[i + 3]] = firstParameter + secondParameter;
                        else codes[i + 3] = firstParameter + secondParameter;
                        jump = 4; break;

                    case 2:
                        if (thirdParMode == 0) codes[codes[i + 3]] = firstParameter * secondParameter;
                        else codes[i + 3] = firstParameter * secondParameter;
                        jump = 4; break;

                    case 3:
                        if (firstParMode == 0)
                        {
                            if (input == null) { Console.WriteLine("Write an input: "); codes[codes[i + 1]] = int.Parse(Console.ReadLine()); }
                            else { codes[codes[i + 1]] = input[0]; input.RemoveAt(0); }
                        }
                        else
                        {
                            if (input == null) codes[i + 1] = int.Parse(Console.ReadLine());
                            else { codes[i + 1] = input[0]; input.RemoveAt(0); }
                        }
                        jump = 2; break;

                    case 4:
                        Console.WriteLine(firstParameter);
                        output = firstParameter;
                        jump = 2; break;

                    case 5:
                        if (firstParameter != 0) i = secondParameter;
                        jump = firstParameter != 0 ? 0 : 3; break;

                    case 6:
                        if (firstParameter == 0) i = secondParameter;
                        jump = firstParameter == 0 ? 0 : 3; break;

                    case 7:
                        if (firstParameter < secondParameter)
                        {
                            if (thirdParMode == 0) codes[codes[i + 3]] = 1;
                            else codes[i + 3] = 1;
                        }
                        else
                        {
                            if (thirdParMode == 0) codes[codes[i + 3]] = 0;
                            else codes[i + 3] = 0;
                        }
                        jump = 4; break;

                    case 8:
                        if (firstParameter == secondParameter)
                        {
                            if (thirdParMode == 0) codes[codes[i + 3]] = 1;
                            else codes[i + 3] = 1;
                        }
                        else
                        {
                            if (thirdParMode == 0) codes[codes[i + 3]] = 0;
                            else codes[i + 3] = 0;
                        }
                        jump = 4; break;

                    case 99:
                        return output != int.MinValue ? output : codes[0];
                }
            }

            throw new Exception("The intcode program didn't halt!");

            int GetOpCode(int code)
            {
                string stringCode = code.ToString();
                string opCode = stringCode.Length < 2 ? stringCode : stringCode.Substring(stringCode.Length - 2);

                return int.Parse(opCode);
            }

            int GetParameterMode(int code, int parameterNumber)
            {
                string stringCode = code.ToString();
                while (stringCode.Length < 5) stringCode = "0" + stringCode;

                return int.Parse(stringCode[3 - parameterNumber].ToString());
            }
        }

        public int Execute(int noun, int verb)
        {
            codes[1] = noun;
            codes[2] = verb;

            return Execute();
        }


        public string GetIntcodeProgram()
        {
            string intcodeProgram = "";

            foreach (int code in codes)
                intcodeProgram += code + ",";

            return intcodeProgram.Substring(0, intcodeProgram.Length - 1);
        }
    }
}
