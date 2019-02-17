using ChinesePoker.enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChinesePoker
{
    /// <summary>
    /// A card; Holds a value - Should later contain an image
    /// </summary>
    public class Card
    {
        public int Value { get; private set; }
        public Suit Suit { get; private set; }
        public Order Order { get; private set; }

        /// <summary>
        /// Returns a card based on an integer [0,53]
        /// </summary>
        /// <param name="val">Note, val is not necerseaarly Value</param>
        public Card(int val)
        {
            // JF - Set suit and order based on value; 52 and 53 are jokers
            if (val == 52 || val == 53)
            {
                Suit = Suit.Joker;
                Order = Order.Joker;
            }
            else
            {
                // JF - Set suit, it is based on the floor of value / 13
                Suit = (Suit)(val / 13);
                // JF - Set order based on the value modulo 12
                Order = (Order)(val % 13);
            }
            Value = this.getValue();
        }

        /// <summary>
        ///  Returns a card based on the suit and order
        /// </summary>
        /// <param name="suit">The suit</param>
        /// <param name="order">The order</param>
        public Card(Suit suit, Order order)
        {
            Suit = suit;
            Order = order;
            Value = this.getValue();
        }

        /// <summary>
        /// Displays the name of the card (w. special character)
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return SuitMethods.GetString(Suit) + OrderMethods.GetString(Order);
        }

        private int getValue()
        {
            if (Suit == Suit.Joker)
                return 52;
            else
                return (int)Suit * 13 + (int)Order;
        }

        public static int Comparison(Card c1, Card c2)
        {
            if (c1.Value < c2.Value)
                return -1;
            else if (c1.Value == c2.Value)
                return 0;
            else if (c1.Value > c2.Value)
                return 1;

            return 0;
        }

        public static bool operator <(Card c1, Card c2)
        {
            return Comparison(c1, c2) < 0;
        }

        public static bool operator >(Card c1, Card c2)
        {
            return Comparison(c1, c2) > 0;
        }
    }

    public static class CardExtensions
    {
        public static string PrintHand(this List<Card> x)
        {
            if (x.Count == 0)
                return "Empty";
            else
                return string.Join(" ", x.Select(z => z.ToString()));
        }

        /// <summary>
        /// Numbers the cards in a list as options; From number 10+ the option takes two characters, which is handled
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static string PrintOptions(this List<Card> x)
        {
            string options = ""; // JF - Start with space for better spacing
            options += string.Join(" ", x.Where((y, i) => i < 9).Select(
                (y, idx) => y.NameLength() == 2 ? " " + (idx + 1).ToString() : (idx + 1).ToString()).ToList());

            if (x.Count > 9)
                options += " " + string.Join(" ", x.Where((y, i) => i >= 9).Select(
                    (y, idx) => y.NameLength() == 2 ? (idx + 10).ToString() : " " + (idx + 10).ToString()).ToList());

            return options;
        }

        /// <summary>
        /// Returns the length of the name
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static int NameLength(this Card x)
        {
            if (x.Order == Order.Ten)
                return 3;
            else
                return 2;
        }
    }
}