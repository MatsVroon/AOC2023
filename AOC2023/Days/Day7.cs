namespace AOC2023.Days
{
    public class Day7 : Day
    {
        public Day7() { }
        public override object RunA(string[] lines)
        {
            var handsStrings = lines.Select(x => x.Split(' ')[0]);
            var bidStrings = lines.Select(x => x.Split(' ')[1]).ToArray();

            var hands = handsStrings.Select((x, i) => new Hand(x, bidStrings[i])).ToArray();

            Array.Sort(hands);

            var total = 0;
            for (int i = 0; i < hands.Length; i++)
            {
                total += hands[i].Bid * (i + 1);
            }
            return total;
        }

        public override object RunB(string[] lines)
        {
            throw new NotImplementedException();
        }
    }

    public class Hand : IComparable<Hand>
    {
        public Card[] Cards { get; set; }
        public int[] CountingTable { get; set; }
        public HandType Type { get; set; }
        public int Bid { get; set; }

        public Hand(string cardsString, string bid)
        {
            Bid = int.Parse(bid);
            Cards = cardsString.Select(getCardFromChar).ToArray();
            CountingTable = new int[13];
            for (int i = 0; i < Cards.Length; i++)
            {
                CountingTable[(int)Cards[i]]++;
            }

            Type = getHandTypeFromCountingTable();
        }
        public int CompareTo(Hand otherHand)
        {
            var typeCompare = this.Type.CompareTo(otherHand.Type);
            if (typeCompare != 0) return typeCompare;

            for (int i = 0; i < this.Cards.Length; i++)
            {
                var cardCompare = this.Cards[i].CompareTo(otherHand.Cards[i]);
                if (cardCompare != 0) return cardCompare;
            }
            return 0;
        }

        private HandType getHandTypeFromCountingTable()
        {
            var amountOfJokers = CountingTable[0];
            var skippedJoker = CountingTable.Skip(1).ToArray();
            if (skippedJoker.Any(x => x + amountOfJokers == 5)) return HandType.FiveOfAKind;
            if (skippedJoker.Any(x => x + amountOfJokers == 4)) return HandType.FourOfAKind;
            if (checkFullHouse(amountOfJokers, skippedJoker))   return HandType.FullHouse;
            if (skippedJoker.Any(x => x + amountOfJokers == 3)) return HandType.ThreeOfAKind;
            if (checkTwoPair(amountOfJokers, skippedJoker))     return HandType.TwoPair; 
            if (skippedJoker.Any(x => x + amountOfJokers == 2)) return HandType.OnePair;

            return HandType.HighCard;
        }

        private bool checkTwoPair(int amountOfJokers, int[] skippedJoker)
        {
            var firstTwoIndex = -1;
            for (int i = 0; i < skippedJoker.Length; i++)
            {
                if (skippedJoker[i] + amountOfJokers == 2)
                {
                    firstTwoIndex = i;
                }
            }
            if (firstTwoIndex < 0) return false;

            for (int j = 0; j < skippedJoker.Length; j++)
            {
                if (skippedJoker[j] == 2 && firstTwoIndex != j)
                {
                    return true;
                }
            }
            return false;
        }

        private bool checkFullHouse(int amountOfJokers, int[] skippedJoker)
        {
            var threeIndex = -1;
            for (int i = 0; i < skippedJoker.Length; i++) 
            {
                if (skippedJoker[i] + amountOfJokers == 3)
                {
                    threeIndex = i;
                }
            }
            if (threeIndex < 0) return false;

            for (int j = 0; j < skippedJoker.Length; j++)
            {
                if (skippedJoker[j] == 2 && threeIndex != j)
                {
                    return true;
                }
            }
            return false;
        }

        private Card getCardFromChar(char c)
        {
            switch (c)
            {
                case 'J': return Card.J;
                case '2': return Card.Two;
                case '3': return Card.Three;
                case '4': return Card.Four;
                case '5': return Card.Five;
                case '6': return Card.Six;
                case '7': return Card.Seven;
                case '8': return Card.Eight;
                case '9': return Card.Nine;
                case 'T': return Card.Ten;
                case 'Q': return Card.Q;
                case 'K': return Card.K;
                case 'A': return Card.A;
                default: throw new Exception("Char to card conversion error");
            }
        }
    }
   
    public enum Card
    {
        J,
        Two, 
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight,
        Nine,
        Ten,
        Q,
        K,
        A
    }
    public enum HandType
    {
        HighCard,
        OnePair,
        TwoPair,
        ThreeOfAKind,
        FullHouse,
        FourOfAKind,
        FiveOfAKind,
    }
}
