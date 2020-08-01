using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;

namespace AdventOfCode2019
{
    public class Day22Solver : Solver
    {
        const string NEW_STACK_SHUFFLE = "deal into new stack";
        const string CUT_SHUFFLE = "cut";
        const string INCREMENT_SHUFFLE = "deal with increment";

        Deck cards;
        List<string> shufflesToApply;

        public Day22Solver(int deckSize)
        {
            CreateCards(deckSize);

            if (shufflesToApply == null)
                shufflesToApply = new List<string>();
        }

        public Day22Solver(string shufflesToApply, int deckSize = 1) 
            : this(deckSize)
        {
            string separator = "\n";
            if (shufflesToApply.Contains("\r")) separator = "\r\n";
            this.shufflesToApply = shufflesToApply.Split(separator).ToList();
        }


        public override long SolvePart1()
        {
            int deckSize = 10007;

            CreateCards(deckSize);

            Shuffle(shufflesToApply);

            return FindPositionOfCard(2019);

            int FindPositionOfCard(int cardNumber)
            {
                for (int i = 0; i < deckSize; i++)
                    if (cards.Get(i) == cardNumber) return i;

                return -1;
            }
        }

        public override long SolvePart2()
        {
            long deckSize = 119315717514047;

            CreateCards(deckSize);

            return cards.Get(2020);
        }


        public void Shuffle(List<string> shufflesToApply = null)
        {
            if (shufflesToApply == null)
                shufflesToApply = this.shufflesToApply;

            foreach (string shuffle in shufflesToApply)
            {
                string shuffleCode = shuffle.StartsWith('c') ? shuffle.Substring(0, CUT_SHUFFLE.Length) : shuffle.Substring(0, NEW_STACK_SHUFFLE.Length);

                switch (shuffleCode)
                {
                    case NEW_STACK_SHUFFLE:
                        ShuffleWithNewStack();
                        break;
                    case CUT_SHUFFLE:
                        ShuffleWithCut(GetCutNumber(shuffle));
                        break;
                    case INCREMENT_SHUFFLE:
                        ShuffleWithIncrement(GetIncrement(shuffle));
                        break;
                }
            }

            int GetCutNumber(string shuffle)
            {
                return int.Parse(shuffle.Substring(CUT_SHUFFLE.Length));
            }

            int GetIncrement(string shuffle)
            {
                return int.Parse(shuffle.Substring(INCREMENT_SHUFFLE.Length));
            }
        }

        public void ShuffleWithNewStack()
        {
            cards.Invert();
        }

        public void ShuffleWithCut(int cutNumber)
        {
            cards.CutAndPaste(cutNumber);
        }

        public void ShuffleWithIncrement(int increment)
        {
            cards.MovePositionsWithIncrement(increment);
        }

        void CreateCards(long deckSize)
        {
            cards = new Deck(deckSize);
        }

        public string GetCards()
        {
            return cards.ToString();
        }

        public bool AreReferencesCorrect()
        {
            return cards.AreReferencesCorrect();
        }
    }

    class Deck
    {
        ListElement firstListElement;
        ListElement lastListElement;

        bool isInTheOriginalOrientation;
        long deckSize;

        public Deck(long deckSize)
        {
            ListElement previousListElement = null;

            for (long i = 0; i < deckSize; i++)
            {
                ListElement newListElement = new ListElement(i, previousListElement, null);

                if (previousListElement == null)
                    this.firstListElement = newListElement;
                else
                    previousListElement.nextElement = newListElement;

                previousListElement = newListElement;
            }

            lastListElement = previousListElement;

            isInTheOriginalOrientation = true;
            this.deckSize = deckSize;
        }

        public void Invert()
        {
            ListElement aux = firstListElement;
            firstListElement = lastListElement;
            lastListElement = aux;

            isInTheOriginalOrientation = !isInTheOriginalOrientation;
        }

        public void CutAndPaste(int cutNumber)
        {
            long newCutNumber = cutNumber;

            if (cutNumber < 0)
                newCutNumber += deckSize;

            if (isInTheOriginalOrientation)
            {
                firstListElement.previousElement = lastListElement;
                lastListElement.nextElement = firstListElement;

                for (int i = 0; i < newCutNumber; i++)
                {
                    firstListElement = firstListElement.nextElement;
                }

                lastListElement = firstListElement.previousElement;

                firstListElement.previousElement = null;
                lastListElement.nextElement = null;
            }
            else
            {
                firstListElement.nextElement = lastListElement;
                lastListElement.previousElement = firstListElement;

                for (int i = 0; i < newCutNumber; i++)
                {
                    firstListElement = firstListElement.previousElement;
                }

                lastListElement = firstListElement.nextElement;

                firstListElement.nextElement = null;
                lastListElement.previousElement = null;
            }
        }

        public void MovePositionsWithIncrement(int increment)
        {
            long[] newCards = new long[deckSize];

            ListElement currentListElement = firstListElement;

            if (isInTheOriginalOrientation)
            {
                for (long i = 0, k = 0; i < deckSize; i++, k = (k + increment) % deckSize)
                {
                    newCards[k] = currentListElement.value;

                    currentListElement = currentListElement.nextElement == null ? firstListElement : currentListElement.nextElement;
                }

                ListElement previousListElement = null;

                for (long i = 0; i < deckSize; i++)
                {
                    ListElement newListElement = new ListElement(newCards[i], previousListElement, null);

                    if (previousListElement == null)
                        this.firstListElement = newListElement;
                    else
                        previousListElement.nextElement = newListElement;

                    previousListElement = newListElement;
                }

                lastListElement = previousListElement;
            }
            else
            {
                for (long i = 0, k = 0; i < deckSize; i++, k = (k + increment) % deckSize)
                {
                    newCards[k] = currentListElement.value;

                    currentListElement = currentListElement.previousElement == null ? firstListElement : currentListElement.previousElement;
                }

                ListElement previousListElement = null;

                for (long i = 0; i < deckSize; i++)
                {
                    ListElement newListElement = new ListElement(newCards[i], null, previousListElement);

                    if (previousListElement == null)
                        this.firstListElement = newListElement;
                    else
                        previousListElement.previousElement = newListElement;

                    previousListElement = newListElement;
                }

                lastListElement = previousListElement;
            }
        }

        public long Get(long position)
        {
            return GetElement(position).value;
        }

        ListElement GetElement(long position)
        {
            if (position == 0) return firstListElement;

            ListElement currentListElement = firstListElement;
            long currentPosition = 0;

            if (isInTheOriginalOrientation)
            {
                do
                {
                    currentListElement = currentListElement.nextElement;
                }
                while (currentListElement != null && ++currentPosition < position);
            }
            else
            {
                do
                {
                    currentListElement = currentListElement.previousElement;
                }
                while (currentListElement != null && ++currentPosition < position);
            }

            return currentListElement;
        }

        public override string ToString()
        {
            string s = "";
            ListElement currentListElement = firstListElement;

            if (isInTheOriginalOrientation)
            {
                do
                {
                    s += currentListElement.value + " ";

                    currentListElement = currentListElement.nextElement;
                }
                while (currentListElement != null);
            }
            else
            {
                do
                {
                    s += currentListElement.value + " ";

                    currentListElement = currentListElement.previousElement;
                }
                while (currentListElement != null);
            }

            return s.Substring(0, s.Length - 1);
        }

        public bool AreReferencesCorrect()
        {
            try
            {
                ListElement currentListElement = firstListElement;

                if (isInTheOriginalOrientation)
                {
                    do
                    {
                        currentListElement = currentListElement.nextElement;
                        if (!currentListElement.Equals(firstListElement)) currentListElement.previousElement.ToString();
                    }
                    while (!currentListElement.Equals(lastListElement));
                }
                else
                {
                    do
                    {
                        currentListElement = currentListElement.previousElement;
                        if (!currentListElement.Equals(firstListElement)) currentListElement.nextElement.ToString();
                    }
                    while (!currentListElement.Equals(lastListElement));
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }

    class ListElement
    {
        public long value;
        public ListElement previousElement, nextElement;

        public ListElement(long value, ListElement previousElement, ListElement nextElement)
        {
            this.value = value;
            this.previousElement = previousElement;
            this.nextElement = nextElement;
        }

        public override bool Equals(object obj)
        {
            return obj is ListElement otherListElement
                && otherListElement.value == this.value;
        }
    }
}
