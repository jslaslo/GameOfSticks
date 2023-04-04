using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F_Delegation.Sticks
{
    public class GameOfSticks
    {
        private readonly Random random;
        public int InitialSticksNumber { get; }
        public Players Turn { get; private set; }
        public int RemaningSticks { get; private set; }
        public GameStatus GameStatus { get; private set; }
        public int[] SetSticks { get; private set; }

        public event Action<int> MachinePlayed;
        public event EventHandler<int[]> HumanPlayed;

        public event Action<Players> EndOfGame;
        public int OrderOfMove { get; private set; }

        public GameOfSticks(int quantitySticks, Players whoMakesFirstMove)
        {
            if (quantitySticks < 7 || quantitySticks > 30)
            {
                throw new ArgumentException("Колличество палочек должно быть >= 7 и <=30");
            }
            InitialSticksNumber = quantitySticks;
            SetSticks = new int[InitialSticksNumber];
            random = new Random();
            GameStatus = GameStatus.NotStarted;
            Turn = whoMakesFirstMove;
        }
        public void Start()
        {
            if (GameStatus == GameStatus.GameIsOver)
            {
                SetSticks = new int[InitialSticksNumber];
            }
            if (GameStatus == GameStatus.InProgress)
            {
                throw new InvalidOperationException("Нельзя вызвать старт...");

            }
            GameStatus = GameStatus.InProgress;
            while (GameStatus == GameStatus.InProgress)
            {
                GetSetSticks();

                if (Turn == Players.Machine)
                {
                    MachineMakeMove();
                }
                else
                {                    
                    HumanMakeMove();
                }
                FireEndOfGameIfRequired();

                Turn = CheckWinner();
                Console.Clear();
            }
        }

        private void FireEndOfGameIfRequired()
        {
            if (SetSticks.Length == 0)
            {
                GameStatus = GameStatus.GameIsOver;
                if (EndOfGame != null)
                {
                    EndOfGame(CheckWinner());
                }
            }
        }

        private void HumanMakeMove()
        {
            if (HumanPlayed != null)
            {
                HumanPlayed(this, SetSticks);
            }
        }

        private void MachineMakeMove()
        {
            Console.WriteLine("Ход компьютера....");

            int maxNumber = SetSticks.Length >= 3? 3: SetSticks.Length;
            int numberSticks = random.Next(1,maxNumber);

            Console.WriteLine($"Принял решение убрать {numberSticks}");
            Console.WriteLine("Нажмите enter....");
            Console.ReadLine();

            TakeSticks(numberSticks);
        }
        public void HumanTakes(int numberSticks)
        {
            if (numberSticks < 1 || numberSticks > 3)
            {
                throw new ArgumentOutOfRangeException("Ты можешь взять от 1 до 3 палочек");
            }

            if (numberSticks > SetSticks.Length)
            {
                throw new ArgumentOutOfRangeException($"Ты можешь взять максимум {SetSticks.Length}");
            }
            TakeSticks(numberSticks);
        }

        private void TakeSticks(int sticks)
        {
            int[] newSetSticks = new int[SetSticks.Length - sticks];
            SetSticks = newSetSticks;
        }

        public void GetSetSticks()
        {
            for (int i = 0; i < SetSticks.Length; i++)
            {
                Console.Write("I");
            }
            Console.WriteLine();
        }             

        public Players CheckWinner()
        {
            return Turn = Turn == Players.Machine ? Players.Human : Players.Machine;
        }
 

    }
}
