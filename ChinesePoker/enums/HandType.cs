using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChinesePoker.enums
{
    public enum HandType
    {
        Pass = 0,
        Single = 1,
        Pair = 2,
        ThreeOfAKind = 3,
        Straight = 4,
        FullHouse = 5,
        FourOfAKind = 6,
        StraightFlush = 7,
        RoyalFlush = 8
    }

    internal static class HandTypeMethods
    {
        public static string GetString(this HandType x)
        {
            switch (x)
            {
                case HandType.Pass:
                    return "Pass";

                case HandType.Single:
                    return "Single";

                case HandType.Pair:
                    return "Pair";

                case HandType.ThreeOfAKind:
                    return "Three of a kind";

                case HandType.Straight:
                    return "Straight";

                case HandType.FullHouse:
                    return "Full House";

                case HandType.FourOfAKind:
                    return "Four of a Kind";

                case HandType.StraightFlush:
                    return "Straight Flush";

                case HandType.RoyalFlush:
                    return "Royal Flush";

                default:
                    return "Hand of God";
            }
        }
    }
}