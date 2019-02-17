using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChinesePoker.enums
{
    /// <summary>
    /// Describes the order of cards
    /// </summary>
    public enum Order
    {
        Three = 0,
        Four = 1,
        Five = 2,
        Six = 3,
        Seven = 4,
        Eight = 5,
        Nine = 6,
        Ten = 7,
        Jack = 8,
        Queen = 9,
        King = 10,
        Ace = 11,
        Two = 12,
        Joker = 13
    }

    static class OrderMethods
    {
        public static string GetString(this Order x)
        {
            switch (x)
            {
                case Order.Three:
                    return "3";
                case Order.Four:
                    return "4";
                case Order.Five:
                    return "5";
                case Order.Six:
                    return "6";
                case Order.Seven:
                    return "7";
                case Order.Eight:
                    return "8";
                case Order.Nine:
                    return "9";
                case Order.Ten:
                    return "10";
                case Order.Jack:
                    return "J";
                case Order.Queen:
                    return "Q";
                case Order.King:
                    return "K";
                case Order.Ace:
                    return "A";
                case Order.Two:
                    return "2";
                default:
                    return "J";
            }
        }
    }
}
