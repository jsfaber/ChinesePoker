using ChinesePoker.enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChinesePoker.objects
{
    public class Hand
    {
        public HandType Type;
        public List<Card> Cards;
        public Card Hight;

        public Hand(List<Card> cards)
        {
            Cards = cards;
            setHandType();
            setHight();
            validate();
        }

        public Hand(string pass)
        {
            setHandType();
        }

        private void setHandType()
        {
            // JF - We guess the type based on the cards
            //      It could be that multiple types are possible, in which case the user is prompted
            if (Cards == null)
                Type = HandType.Pass;
            else if (Cards.Count == 1)
                Type = HandType.Single;
            else if (Cards.Count == 2)
                Type = HandType.Pair;
            else if (Cards.Count == 3)
                Type = HandType.ThreeOfAKind;
            else
            {
                // JF - There are several posabilities; We check everyone and add them to a list
                // TODO - Incorporate jokers
                var possibleTypes = new List<HandType>();
                var suitsGroups = Cards.GroupBy(x => x.Suit).ToList();
                var orderGroups = Cards.GroupBy(x => x.Order).ToList();

                if (orderGroups.Where(x => x.Count() > 3).Count() > 0)
                    possibleTypes.Add(HandType.FourOfAKind);
                if (orderGroups.Where(x => x.Count() == 2).Count() == 1 && orderGroups.Where(x => x.Count() == 3).Count() == 1)
                    possibleTypes.Add(HandType.FullHouse);
                if (Cards.OrderBy(x => (int)x.Order).Skip(1).Where((x, i) => ((int)x.Order - (int)Cards[i].Order) != 1).Count() == 0)
                {
                    // JF - All cards are in consecutive order
                    possibleTypes.Add(HandType.Straight);
                    if (suitsGroups.Count() == 1)
                    {
                        // JF - All cards are of the same suits
                        possibleTypes.Add(HandType.StraightFlush);
                        if (Cards.Where(x => x.Order == Order.Two).Count() == 1)
                            possibleTypes.Add(HandType.RoyalFlush);
                    }
                }

                if (possibleTypes.Count() == 1)
                    Type = possibleTypes[0];
                else if (possibleTypes.Count() > 1)
                {
                    // JF  - Ask player to choose handType
                    Console.WriteLine("There are several possible hands to play, choose one:");
                    for (int i = 0; i < possibleTypes.Count; i++)
                        Console.WriteLine($"{i + 1} : {HandTypeMethods.GetString(possibleTypes[i])}");
                    Console.Write($"Enter a number between 1 and {possibleTypes.Count()}: ");
                    var choice = int.Parse(Console.ReadLine().ToString());
                    // JF - Set hand to choice of player
                    Type = possibleTypes[choice - 1];
                }
                else
                {
                    throw new Exception("Unable to form hand with these cards.");
                }
            }

            bool valid = validate();
        }

        // JF - Returns the card determening the hight of the hand
        private void setHight()
        {
            if (Type == HandType.FourOfAKind || Type == HandType.FullHouse)
            {
                var orderGroups = Cards.GroupBy(x => x.Order).ToList();
                Hight = orderGroups.OrderBy(x => x.Count()).Last().OrderBy(x => (int)x.Order).ThenBy(x => (int)x.Suit).Last();
            }
            else if (Type == HandType.Pass)
                Hight = null;
            else
                Hight = Cards.OrderBy(x => (int)x.Order).ThenBy(x => (int)x.Suit).Last();
        }

        /// <summary>
        /// Checks if the hand is of a valid type
        /// </summary>
        /// <returns></returns>
        private bool validate()
        {
            // JF - Checks if the hand is valid given the input cards
            switch (Type)
            {
                case HandType.Pass:
                case HandType.Single:
                    return true;

                case HandType.Pair:
                    return Cards[0].Order == Cards[1].Order;

                case HandType.ThreeOfAKind:
                    return Cards[0].Order == Cards[1].Order && Cards[0].Order == Cards[2].Order;

                case HandType.FourOfAKind:
                    var grp = Cards.GroupBy(x => x.Order).ToList();
                    return (grp[0].Count() == 1 && grp[1].Count() == 4) || (grp[1].Count() == 1 && grp[0].Count() == 4);

                case HandType.FullHouse:
                    grp = Cards.GroupBy(x => x.Order).ToList();
                    return (grp[0].Count() == 3 && grp[1].Count() == 2) || (grp[1].Count() == 3 && grp[0].Count() == 2);

                case HandType.Straight:
                case HandType.StraightFlush:
                    var ord = Cards.OrderBy(x => x.Order).ToList();
                    bool correctOrder = ord.Skip(1).Where((x, i) => x.Order - ord[i].Order != 1).Count() == 0;
                    bool sameSuit = Cards.GroupBy(x => x.Suit).Count() == 1;
                    if (Type == HandType.StraightFlush)
                        return correctOrder && sameSuit;
                    else
                        return correctOrder;

                case HandType.RoyalFlush:
                    return true; // JF - to be implemented

                default:
                    return true;
            }
        }
    }
}