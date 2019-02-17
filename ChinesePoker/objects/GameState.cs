using ChinesePoker.enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChinesePoker.objects
{
    /// <summary>
    /// Contains the (current) state of the game;
    /// </summary>
    public class GameState
    {
        public List<Hand> Stack { get; private set; }
        public int PlayerAtTurn { get; private set; }
        public List<Player> Players { get; private set; }
        public HandType GameHandType;
        public bool Winner = false;
        public Form1 form;

        private int turnCount = 0;

        public GameState(int noPlayers = 2, int jokers = 2, int dealingPlayer = 0)
        {
            Stack = new List<Hand>();

            // JF - Start shuffling cards
            Random rnd = new Random();
            var cardVals = Enumerable.Repeat(rnd.Next(), 52 + jokers).ToList();
            var shuffledDeck = cardVals.Select(
                (x, i) => new { val = x, idx = i }).OrderBy(x => x.val).Select(x => new Card(x.idx));

            // JF - Create players
            Players = new List<Player>();
            for (int i = 0; i < Math.Min(noPlayers, 3); i++)
                Players.Add(new Player(i, shuffledDeck.Where((x, k) => k % Math.Max(noPlayers, 3) == i).ToList())); // JF - This does not (YET) incorporates the starting player

            // JF - Create representation
            form = new Form1();
            form.Show();

            // JF - Start game
            PlayerAtTurn = Players.SkipWhile(x => x.Cards.Contains(new Card(Suit.Diamond, Order.Three))).First().No;
            while (Winner == false)
            {
                form.Update(Players[PlayerAtTurn].Cards);
                turn();
            }
        }

        private void turn()
        {
            Hand hand;
            bool valid = false;
            do
            {
                Console.Clear();
                Console.WriteLine($"You are Player {Players[PlayerAtTurn].No}.");
                if (GameHandType == HandType.Pass)
                    Console.WriteLine("You decide the stack type; The stack shows:");
                else
                    Console.WriteLine($"The stack type is {HandTypeMethods.GetString(GameHandType)} - The stack shows:");
                if (Stack.Count == 0)
                    Console.WriteLine("Empty");
                else
                    Console.WriteLine(Stack.Last().Cards.PrintHand());

                hand = Players[PlayerAtTurn].Move(); // JF - Results in a hand

                // JF - Check validity of hand
                if (Stack.Count() == 0)
                {
                    // JF - The hand is the first of the round, thus valid
                    setRoundType(hand.Type);
                    valid = true;
                }
                else
                {
                    // JF - Check if hand is of correct type and is higher
                    if (hand.Type == HandType.Pass)
                        valid = true;
                    else if (GameHandType == HandType.Straight & hand.Type == HandType.FullHouse)
                        valid = true;
                    else if (hand.Type == GameHandType & hand.Hight > Stack.Last().Hight)
                        valid = true;
                    else
                        Console.WriteLine("This hand is not allowed, try again!");
                }
            } while (!valid);

            if (hand.Type != HandType.Pass)
            {
                Stack.Add(hand);
                Players[PlayerAtTurn].LayHand(hand);
            }
            else
            {
                Players[PlayerAtTurn].Pass();
            }

            nextPlayer();
        }

        private void nextPlayer()
        {
            // JF - If everyone passed except one, that one wins the round
            var playersInGame = Players.Where(x => x.InRound).ToList();
            if (playersInGame.Count() == 1)
            {
                Console.WriteLine($"Player {playersInGame[0].No} wins the round!");
                // JF - Reset round
                GameHandType = HandType.Pass;
                PlayerAtTurn = playersInGame[0].No;
                Stack.Clear();
                foreach (var pl in Players)
                    pl.InRound = true;
            }
            else
                PlayerAtTurn = (PlayerAtTurn + 1) % Players.Count;

            // JF - Check if we have a winner
            var winners = Players.Where(x => x.Cards.Count == 0).ToList();
            if (winners.Count() > 0)
            {
                Winner = true;
                Console.WriteLine($"Player {winners[0].No} is the winner!!");
            }
        }

        private void setRoundType(HandType type)
        {
            GameHandType = type;
        }
    }
}