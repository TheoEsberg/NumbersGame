// Theo Esberg, SUT22, Labb3
using System;
using System.Collections.Generic;
using System.Net.Security;
using System.Reflection.Emit;
using System.Threading.Tasks.Dataflow;

namespace NumbersGame
{
    internal class Program
    {
        static void Main(string[] args)
        {
            GameLoop();
        }

        public static void GameLoop() {

            // Declare variables
            short guess = 0;
            bool isCorrect = false;
            bool won = false;
            bool runGame = true;
            
            while(runGame)
            {
                short level = setDifficulty();
                short difficulty = (short)(18 / level);
                short number = getNumber(level);

                Console.WriteLine("Gissa vilket nummer jag tänker på! Du har {0} försök!", difficulty);

                for (short i = 0; i <= difficulty; i++)
                {
                    guess = short.Parse(Console.ReadLine());
                    isCorrect = checkGuess(guess, number);
                    won = answerGuess(guess, number);

                    if (difficulty == i && !won)
                    {
                        Console.WriteLine("Du förlorade!\nJag tänkte på {0}", number);
                    }
                    else if (won) {
                        runGame = hasWon();
                        break;
                    }
                }
            }
        }

        public static short getNumber(short level) {
            int number = rnd(level * 20);
            return (short)number;
        }

        public static short setDifficulty() {
            Console.WriteLine("Välj svårhets grad!\n1. Lätt, 2. Medium, 3. Svår");
            short level = short.Parse(Console.ReadLine());
            return level;
        }

        public static bool checkGuess(short guess, short number) {
            if (guess == number) { return true; }
            else return false;
        }

        public static bool isClose(short guess, short number) {
            if (guess == number - 1 || guess == number + 1) { return true; }
            else return false;
        }

        public static bool isHigh(short guess, short number) {
            if (guess > number) { return true; }
            else return false;
        }

        public static bool isCorrect(short guess, short number) {
            if (guess == number) { return true; }
            else return false;
        }

        public static bool answerGuess(short guess, short number) {
            if (isCorrect(guess, number)) {
                return true;
            } else if (isClose(guess, number)) {
                answerClose();
                return false;
            } else if (isHigh(guess, number)) {
                answerHigh();
                return false;
            } else {
                answerLow(guess);
                return false;
            }
        }

        public static bool hasWon() {
            Console.WriteLine("Grattis, du lyckades!");
            Console.WriteLine("Vill du spela igen? (Y/N)");

            char answer = char.Parse(Console.ReadLine());
            if (answer == 'n' || answer == 'N') {
                Console.Clear();
                return false;
            }
            Console.Clear();
            return true;
        }

        public static void answerClose() {

            // Create a list of answers to say if the guess is close
            List<string> closeAnswers = new List<string>();
            closeAnswers.Add("De var nära!");
            closeAnswers.Add("Inte långt ifrån!");
            closeAnswers.Add("De bränns!");
            closeAnswers.Add("Inte rätt men satans nära!");

            // Use the random number we generated in the start to choose a random answer from the list
            Console.WriteLine(closeAnswers[rnd(closeAnswers.Count - 1)]);
        }

        public static void answerLow(short guess) {
            // Create a list of answers to say if the guess is to low
            List<string> lowAnswers = new List<string>();
            lowAnswers.Add("Tyvärr du gissade för lågt!");
            lowAnswers.Add("Jag tänker på mycket högre!");
            lowAnswers.Add("Dedär är ju alldeles för lågt!");
            lowAnswers.Add("Tror du inte jag kan tänka på högre tal än " + guess + "? \nGissa igen!");

            Console.WriteLine(lowAnswers[rnd(lowAnswers.Count - 1)]);
        }

        public static void answerHigh() {
            // Create a list of answers to say if the guess is to high
            List<string> highAnswers = new List<string>();
            highAnswers.Add("Tyvärr du gissade för högt!");
            highAnswers.Add("Inte ens nära, för högt!");
            highAnswers.Add("Dags o gissa lägre du är långt ifrån!");
            highAnswers.Add("Siktar du mot månen? Gissa lägre!");

            Console.WriteLine(highAnswers[rnd(highAnswers.Count - 1)]);
        }

        public static int rnd(int max) {
            Random random = new Random();
            return random.Next(max + 1);
        }
    }
}
