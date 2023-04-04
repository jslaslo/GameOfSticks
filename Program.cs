using F_Delegation.Sticks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace F_Delegation
{

    internal class Program
    {

        static void Main(string[] args)
        {
            var game = new GameOfSticks(10, Players.Human);
            game.MachinePlayed += Game_MachinePlayed;
            game.HumanPlayed += Game_HumanPlayed;
            game.EndOfGame += Game_EndOfGame;

            game.Start();

        }
        private static void Game_EndOfGame(Players player)
        {
            Console.WriteLine($"Победитель: {player}");
            Console.WriteLine("Нажмите enter....");
            Console.ReadLine();

        }

        private static void Game_HumanPlayed(object sender, int[] e)
        {
            Console.WriteLine("Возьмите от 1 до 3 палочек");

            bool takenCorrectly = false;
            while (!takenCorrectly)
            {
                if (int.TryParse(Console.ReadLine(), out int takenSticks))
                {
                    var game = (GameOfSticks)(sender);

                    try
                    {
                        game.HumanTakes(takenSticks);
                        takenCorrectly = true;
                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
        }
    }
}
