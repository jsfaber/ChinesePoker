using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChinesePoker.enums
{
    /// <summary>
    /// Describes the different suits
    /// </summary>
    public enum Suit
    {
        Diamond = 0,
        Clover = 1,
        Hearth = 2,
        Spade = 3,
        Joker = 4
    }

    static class SuitMethods
    {
        public static string GetString(this Suit x)
        {

                switch (x)
            {
                case Suit.Diamond:
                    return @"♦";
                 case Suit.Clover:
                    return @"♣";
                    case Suit.Hearth:
                    return @"♥";
              
                case Suit.Spade:
                    return @"♠";
                default:
                    return "J";
            
            }
        }        
    }
}
