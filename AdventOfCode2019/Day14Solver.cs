using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2019
{
    public class Day14Solver : Solver
    {
        List<Reaction> reactions;
        LeftOversBag leftOversBag;

        public Day14Solver(string input)
        {
            reactions = new List<Reaction>();
            leftOversBag = new LeftOversBag();

            string separator = "\n";
            if (input.Contains("\r")) separator = "\r\n";
            foreach (string reaction in input.Split(separator))
            {
                string[] reactionParts = reaction.Split("=>");

                reactions.Add(new Reaction(CalculateInputElements(reactionParts[0]), new ReactionElement(reactionParts[1])));
            }

            List<ReactionElement> CalculateInputElements(string reactionInput)
            {
                List<ReactionElement> reactionElements = new List<ReactionElement>();

                foreach (string element in reactionInput.Split(','))
                    reactionElements.Add(new ReactionElement(element));

                return reactionElements;
            }
        }


        public override long SolvePart1()
        {
            return CalculateNeededOre();
        }

        public override long SolvePart2()
        {
            return CalculateMaxFuelWith1TOre();
        }


        public int CalculateMaxFuelWith1TOre()
        {
            long neededOreToHave1Fuel = CalculateNeededOre();

            long aproxNeededOre = 1000000000000 / neededOreToHave1Fuel;

            int wantedFuel = Convert.ToInt32(Math.Truncate(aproxNeededOre * 0.9));

            long pene = CalculateNeededOre(new ReactionElement(wantedFuel, "FUEL"));

            while (CalculateNeededOre(new ReactionElement(wantedFuel, "FUEL")) < 1000000000000)
                wantedFuel += Convert.ToInt32(Math.Truncate(aproxNeededOre * 0.05));

            return wantedFuel;
        }

        public long CalculateNeededOre()
        {
            return CalculateNeededOre(new ReactionElement(1, "FUEL"));
        }

        long CalculateNeededOre(ReactionElement wantedElement)
        {
            if (wantedElement.elementName.Equals("ORE"))
                return wantedElement.quantity;

            Reaction originalProducingReaction = SearchProducingReaction(wantedElement);
            Reaction producingReaction = originalProducingReaction.Clone();

            int leftOvers = leftOversBag.GetThisElement(wantedElement.elementName), newLeftOvers;

            if (leftOvers > wantedElement.quantity)
            {
                newLeftOvers = leftOvers - wantedElement.quantity;
                if (newLeftOvers > 0)
                    leftOversBag.Add(wantedElement.elementName, newLeftOvers);

                return 0;
            }

            while (producingReaction.output.quantity + leftOvers < wantedElement.quantity)
                producingReaction.Sum(originalProducingReaction);

            newLeftOvers = producingReaction.output.quantity + leftOvers - wantedElement.quantity;
            if (newLeftOvers > 0)
                leftOversBag.Add(wantedElement.elementName, newLeftOvers);
            
            return CalculateNeededElements(producingReaction);
        }

        long CalculateNeededElements(Reaction reaction)
        {
            long neededOre = 0;

            foreach (ReactionElement reactionElement in reaction.inputs)
                neededOre += CalculateNeededOre(reactionElement);

            return neededOre;
        }

        Reaction SearchProducingReaction(ReactionElement reactionElement)
        {
            foreach (Reaction reaction in reactions)
                if (reaction.HasThisOutput(reactionElement)) return reaction;

            return null;
        }
    }

    public class Reaction
    {
        public List<ReactionElement> inputs;
        public ReactionElement output;

        public Reaction(List<ReactionElement> inputs, ReactionElement output)
        {
            this.inputs = inputs;
            this.output = output;
        }


        public bool HasThisOutput(ReactionElement expectedElement)
        {
            return output.elementName.Equals(expectedElement.elementName);
        }

        public override string ToString()
        {
            string res = "";

            foreach (ReactionElement re in inputs)
                res += re + ", ";

            return res.Substring(0, res.Length - 2) + " => " + output;
        }

        public void Sum(Reaction otherReaction)
        {
            for (int i = 0; i < inputs.Count; i++)
                inputs[i].Sum(otherReaction.inputs[i]);

            output.Sum(otherReaction.output);
        }

        public Reaction Clone()
        {
            return new Reaction(inputs.Select(re => re.Clone()).ToList(), output.Clone());
        }
    }

    public class ReactionElement
    {
        public int quantity;
        public string elementName;

        public ReactionElement(string element)
        {
            string[] elements = element.TrimStart().TrimEnd().Split(' ');

            quantity = int.Parse(elements[0]);
            elementName = elements[1];
        }

        public ReactionElement(int quantity, string elementName)
        {
            this.quantity = quantity;
            this.elementName = elementName;
        }


        public override string ToString()
        {
            return quantity + " " + elementName;
        }

        public void Sum(ReactionElement otherReactionElement)
        {
            quantity += otherReactionElement.quantity;
        }

        public ReactionElement Clone()
        {
            return new ReactionElement(quantity, elementName);
        }
    }

    public class LeftOversBag
    {
        Dictionary<string, int> leftOversBag;

        public LeftOversBag()
        {
            leftOversBag = new Dictionary<string, int>();
        }


        public void Add(string chemicalElement, int quantity)
        {
            if (leftOversBag.ContainsKey(chemicalElement))
            {
                int oldQuantity = leftOversBag[chemicalElement];

                leftOversBag.Remove(chemicalElement);
                leftOversBag.Add(chemicalElement, oldQuantity + quantity);
            }
            else
                leftOversBag.Add(chemicalElement, quantity);
        }

        public int GetThisElement(string chemicalElement)
        {
            int quantity = 0;

            try
            {
                quantity = leftOversBag[chemicalElement];
                leftOversBag.Remove(chemicalElement);
            }
            catch (Exception) { }

            return quantity;
        }
    }
}
