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
    public class Car
    {
        int speed = 0;
        public event Action<int> TooFastDriving;
        public void Start()
        {
            speed = 10;
            Console.WriteLine("Тртртр.... Завелась");
        }
        public void Accelerate()
        {

            if (speed > 80)
            {
                if (TooFastDriving != null)
                    TooFastDriving(speed);
            }
            speed += 10;

        }
        public void Stop()
        {
            speed = 0;
            Console.WriteLine("ТухТух.... Машина все. Заглохла");
        }
    }
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
                if(int.TryParse(Console.ReadLine(), out int takenSticks))
                {
                    var game = (GameOfSticks)(sender);

                    try
                    {
                        game.HumanTakes(takenSticks);
                        takenCorrectly = true;
                    }
                    catch(ArgumentException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
        }

        private static void Game_MachinePlayed(int sticksTaken)
        {
            Console.WriteLine($"Машина взяла {sticksTaken}");
        }

        static void EventsDemo()
        {
            Car car = new Car();
            car.TooFastDriving += HandleOnTooFast;

            car.Start();
            for (int i = 0; i < 10; i++)
            {
                car.Accelerate();
            }
            void HandleOnTooFast(int currentSpeed)
            {
                Console.WriteLine($"Куда газогнался, машина развалится. Гонишь {currentSpeed}....Глуши мотор ");
                car.Stop();
            }
        }


    }
}
