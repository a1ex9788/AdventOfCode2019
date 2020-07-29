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

        long neededOreToHave1Fuel;

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
            neededOreToHave1Fuel = CalculateNeededOre();

            return neededOreToHave1Fuel;
        }

        public override long SolvePart2()
        {
            return CalculateMaxFuelWith1TOre();
        }


        public long CalculateNeededOre()
        {
            return CalculateNeededOre(new ReactionElement(1, "FUEL"));
        }

        long CalculateNeededOre(ReactionElement wantedElement)
        {
            long leftOvers = leftOversBag.GetThisElement(wantedElement.elementName), newLeftOvers;

            if (leftOvers > wantedElement.quantity)
            {
                newLeftOvers = leftOvers - wantedElement.quantity;
                if (newLeftOvers > 0)
                    leftOversBag.Add(wantedElement.elementName, newLeftOvers);

                return 0;
            }

            if (wantedElement.elementName.Equals("ORE"))
                return wantedElement.quantity;

            Reaction originalProducingReaction = SearchProducingReaction(wantedElement);
            Reaction producingReaction = originalProducingReaction.Clone();

            while (producingReaction.output.quantity + leftOvers < wantedElement.quantity)
                producingReaction.Sum(originalProducingReaction);

            newLeftOvers = producingReaction.output.quantity + leftOvers - wantedElement.quantity;
            if (newLeftOvers > 0)
                leftOversBag.Add(wantedElement.elementName, newLeftOvers);
            
            return CalculateNeededElements(producingReaction);
        }

        public long CalculateMaxFuelWith1TOre()
        {
            if (neededOreToHave1Fuel == 0)
                neededOreToHave1Fuel = CalculateNeededOre();

            int aproxMaxFuel = (int)(1000000000000 / neededOreToHave1Fuel);

            int oreLeftOvers = (int)(1000000000000 % neededOreToHave1Fuel);

            leftOversBag.MultiplyBy(aproxMaxFuel);

            leftOversBag.Add("ORE", oreLeftOvers);

            return aproxMaxFuel + CalculateExtraFuelWithLeftOversBag();
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

        InvertedReaction SearchProducingInvertedReaction(ReactionElement reactionElement)
        {
            foreach (InvertedReaction invertedReaction in reactions.Select(r => r.GetInvertedReaction()))
                if (invertedReaction.HasThisInput(reactionElement)) return invertedReaction;

            return null;
        }

        int CalculateExtraFuelWithLeftOversBag()
        {
            int extraFuel = 0;

            do
            {
                RecollectOreWithLeftOvers();
            }
            while (CanGetMoreOre());

            while (CalculateNeededOre() == 0)
                extraFuel++;

            return extraFuel;

            bool CanGetMoreOre()
            {
                foreach (ReactionElement re in leftOversBag.GetReactionElements())
                {
                    if (re.elementName.Equals("ORE")) continue;

                    if (SearchProducingInvertedReaction(re).input.quantity < re.quantity) return true;
                }

                return false;
            }
        }

        void RecollectOreWithLeftOvers()
        {
            foreach (ReactionElement reactionElement in leftOversBag.GetReactionElements())
            {
                if (reactionElement.elementName.Equals("ORE")) continue;

                InvertedReaction producingInvertedReaction = SearchProducingInvertedReaction(reactionElement);

                producingInvertedReaction.GrowIHave(reactionElement);


                leftOversBag.Remove(reactionElement.elementName, producingInvertedReaction.input.quantity);

                foreach (ReactionElement re in producingInvertedReaction.outputs)
                    leftOversBag.Add(re.elementName, re.quantity);
            }
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

        public InvertedReaction GetInvertedReaction()
        {
            Reaction clonnedReaction = this.Clone();

            return new InvertedReaction(clonnedReaction.output, clonnedReaction.inputs);
        }
    }

    public class InvertedReaction
    {
        public ReactionElement input;
        public List<ReactionElement> outputs;

        public InvertedReaction(ReactionElement input, List<ReactionElement> outputs)
        {
            this.input = input;
            this.outputs = outputs;
        }


        public bool HasThisInput(ReactionElement expectedElement)
        {
            return input.elementName.Equals(expectedElement.elementName);
        }

        public override string ToString()
        {
            string res = input.ToString() + " => ";

            foreach (ReactionElement re in outputs)
                res += re + ", ";

            return res.Substring(0, res.Length - 2);
        }

        public void GrowIHave(ReactionElement reactionElement)
        {
            long factor = reactionElement.quantity / input.quantity;

            input.quantity *= factor;

            foreach (ReactionElement ra in outputs)
                ra.quantity *= factor;
        }
    }

    public class ReactionElement
    {
        public long quantity;
        public string elementName;

        public ReactionElement(string element)
        {
            string[] elements = element.TrimStart().TrimEnd().Split(' ');

            quantity = int.Parse(elements[0]);
            elementName = elements[1];
        }

        public ReactionElement(long quantity, string elementName)
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
        List<ReactionElement> reactionElements;

        public LeftOversBag()
        {
            reactionElements = new List<ReactionElement>();
        }


        public void Add(string chemicalElement, long quantity)
        {
            if (quantity == 0) return;

            ReactionElement reactionElement = FindReactionElement(chemicalElement);

            if (reactionElement != null)
                reactionElement.quantity += quantity;
            else
                reactionElements.Add(new ReactionElement(quantity, chemicalElement));
        }

        public long GetThisElement(string chemicalElement)
        {
            ReactionElement reactionElement = FindReactionElement(chemicalElement);

            if (reactionElement != null)
            {
                reactionElements.Remove(reactionElement);

                return reactionElement.quantity;
            }

            return 0;
        }

        public void Remove(string elementName, long quantity)
        {
            ReactionElement reactionElement = FindReactionElement(elementName);

            reactionElement.quantity -= quantity;

            if (reactionElement.quantity == 0) reactionElements.Remove(reactionElement);

            if (reactionElement.quantity < 0) throw new Exception("It's not possible to have les than 0 as a quantity of an element.");
        }

        ReactionElement FindReactionElement(string elementName)
        {
            return reactionElements.Where(re => re.elementName.Equals(elementName)).FirstOrDefault();
        }

        public void MultiplyBy(int number)
        {
            foreach (ReactionElement reactionElement in reactionElements)
                reactionElement.quantity *= number;
        }

        public List<ReactionElement> GetReactionElements()
        {
            List<ReactionElement> res = new List<ReactionElement>();

            foreach (ReactionElement re in reactionElements)
                res.Add(re.Clone());

            return res;
        }

        public int Count()
        {
            return reactionElements.Count;
        }
    }
}
