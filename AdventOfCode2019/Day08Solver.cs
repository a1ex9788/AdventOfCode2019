using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace AdventOfCode2019
{
    public class Day8Solver : Solver
    {
        static int BLACK_PIXEL = 0, WHITE_PIXEL = 1, TRANSPARENT_PIXEL = 2;

        string input;
        int pixelsWide, pixelsTall;
        List<Layer> layers;

        public Day8Solver(string input, int pixelsWide, int pixelsTall)
        {
            this.input = input;
            this.pixelsWide = pixelsWide;
            this.pixelsTall = pixelsTall;
            
            ConstructLayers();
        }


        public override long SolvePart1()
        {
            Layer fewNumberOf0Layer = layers[0];
            int fewNumberOf0 = int.MaxValue;

            foreach (Layer l in layers)
            {
                int numberOf0 = l.CountAparitions(0);

                if (numberOf0 < fewNumberOf0)
                {
                    fewNumberOf0Layer = l;
                    fewNumberOf0 = numberOf0;
                }
            }

            return fewNumberOf0Layer.CountAparitions(1) * fewNumberOf0Layer.CountAparitions(2);
        }

        public override long SolvePart2()
        {
            string finalLayer = "";

            for (int i = 0; i < pixelsTall; i++)
                for (int j = 0; j < pixelsWide; j++)
                    finalLayer += CalculateFinalPixel(j, i);

            string res = (new Layer(finalLayer, pixelsWide, pixelsTall)).ToString();

            Console.WriteLine(res);
            return res.GetHashCode();
        }


        public string ConstructLayers()
        {
            layers = new List<Layer>();

            for (int i = 0; i < input.Length; i += pixelsWide * pixelsTall)
                layers.Add(new Layer(input.Substring(i, pixelsWide * pixelsTall), pixelsWide, pixelsTall));

            string res = "";
            foreach (Layer l in layers)
                res += l.ToString() + "\n\n";

            return res.Substring(0, res.Length - 2);
        }

        private string CalculateFinalPixel(int i, int j)
        {
            int pixelNumber = layers[0].GetNumber(i, j); ;
            int layerNumber = 0;

            while (pixelNumber == TRANSPARENT_PIXEL)
            {
                layerNumber++;

                pixelNumber = layers[layerNumber].GetNumber(i, j);
            }

            return pixelNumber.ToString();
        }
    }
}

class Layer
{
    int[,] rows;
    int pixelsWide, pixelsTall;

    public Layer(string numbers, int pixelsWide, int pixelsTall)
    {
        rows = new int[pixelsWide, pixelsTall];
        this.pixelsWide = pixelsWide;
        this.pixelsTall = pixelsTall;

        for (int i = 0; i < pixelsTall; i++)
            for (int j = 0; j < pixelsWide; j++)
                rows[j, i] = int.Parse(numbers[i * pixelsWide + j].ToString());
    }

    public int GetNumber(int x, int y)
    {
        return rows[x, y];
    }

    public override string ToString()
    {
        string res = "";

        for (int i = 0; i < pixelsTall; i++)
        {
            for (int j = 0; j < pixelsWide; j++)
                res += rows[j, i];
            if (i != pixelsTall - 1) res += "\n";
        }

        return res;
    }

    public int CountAparitions(int number)
    {
        int aparitions = 0;

        for (int i = 0; i < pixelsTall; i++)
            for (int j = 0; j < pixelsWide; j++)
                if (rows[j, i] == number) aparitions++;

        return aparitions;
    }
}
