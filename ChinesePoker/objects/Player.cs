using ChinesePoker.enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChinesePoker.objects
{
    public class Player
    {
        /// <summary>
        /// Position of player
        /// </summary>
        public int No { get; private set; }

        /// <summary>
        /// Cards of the player
        /// </summary>
        public List<Card> Cards { get; private set; }

        /// <summary>
        /// Bool if player is still in round
        /// </summary>
        public bool InRound { get; set; }

        public Player(int no, List<Card> cards)
        {
            No = no;
            Cards = cards.OrderBy(x => (int)x.Order).ThenBy(x => (int)x.Suit).ToList();
            InRound = true;
        }

        // JF - To be called when player has to make a move
        public Hand Move()
        {
            Hand playingHand;
            bool endMove = false;
            do
            {
                // JF - Print hand and prompt options
                Console.WriteLine("Your hand is:");
                Console.WriteLine(Cards.PrintHand());
                Console.WriteLine(Cards.PrintOptions());
                // JF - Extract chosen cards and return result
                Console.WriteLine("Select the cards (by number, seperated by a comma) you want to play, or P to pass:"); // TODO - Enhance with
                var choices = Console.ReadLine().ToString().Split(',').ToList();
                List<Card> chosenCards;

                if (choices.Contains("P"))
                {
                    Console.WriteLine($"You have chosen to Pass, is that correct? (Y/N)");
                    playingHand = new Hand("P");
                }
                else
                {
                    // JF - Players makes a move
                    chosenCards = choices.Select(int.Parse).Select(x => Cards[x - 1]).ToList();
                    playingHand = new Hand(chosenCards);

                    Console.WriteLine($"You have chosen: {chosenCards.PrintHand()} - {HandTypeMethods.GetString(playingHand.Type)} - with " +
                        $"{playingHand.Hight.ToString()} high - , is that correct? (Y/N)");
                }
                string confirm = Console.ReadLine();
                endMove = confirm.ToUpper() == "Y";
            } while (!endMove);

            return playingHand;
        }

        /// <summary>
        /// Remove cards in hand from player
        /// </summary>
        /// <param name="hand"></param>
        public void LayHand(Hand hand)
        {
            foreach (var card in hand.Cards)
                Cards.Remove(card);
        }

        internal void Pass()
        {
            InRound = false;
        }

        internal void Unpass()
        {
            InRound = true;
        }
    }
}