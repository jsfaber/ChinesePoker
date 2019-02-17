using ChinesePoker.objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChinesePoker
{
    internal class Program
    {
        private static void Main()
        {
            Console.OutputEncoding = Encoding.Unicode;

            // JF - Start game
            var game = new GameState();
            foreach (var player in game.Players)
                Console.WriteLine(player.Cards.PrintHand());
        }
    }
}