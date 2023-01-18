using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wordle
{
    internal class Program
    {
        class Wordle
        {
            private string word { get; set; }
            private int attempt { get; set; }

            private string[] wordBank =
            {
                "howdy",
                "doody",
                "shoot",
                "crypt",
                "mouse",
                "moose",
                "goose",
                "space",
                "eight"
            };

            private void GenerateWord()
            {
                this.word = wordBank[new Random().Next(wordBank.Length-1)];
                return;
            }

            private bool CheckInput(string input)
            {
                if (input.Length != this.word.Length)
                {
                    this.attempt--;
                    Console.WriteLine("Please enter only 5 letter words");
                    return false;
                }
                for (int i = 0; i < input.Length; i++)
                {
                    if (input[i] != this.word[i]) break;
                    if (i == input.Length- 1)
                    {
                        return true;
                    };
                }
                for (int i = 0; i < input.Length; i++)
                {


                    if (input[i] == this.word[i])
                    {
                        Console.Write(input[i].ToString().ToUpper());
                    }
                    else 
                    {
                        bool badIdea = false;
                        for (int j = 0; j < this.word.Length; j++)
                        {
                            if (input[i] == this.word[j])
                            {
                                Console.Write(input[i].ToString());
                                badIdea = true;
                                break;
                            }
                        }
                        if (!badIdea) Console.Write("_");
                    }
                }
                Console.WriteLine();
                return false;
            }

            public void StartGame()
            {
                Console.WriteLine("Hello, are you ready to play Wordle? (y/n)");
                string[] input = Console.ReadLine().Split();
                if (input[0].ToLower() != "y")
                {
                    Console.WriteLine("Okay, have a great day! =)");
                    return;
                }


                GenerateWord();

                Console.WriteLine("Okay, the word is generated" +
                    "\nAll correct letters will be uppercase" +
                    "\nAll letters in the wrong position will be lowercase" +
                    "\nPlease start guessing:");

                Console.WriteLine("_____");

                while (this.attempt < 6) {

                    this.attempt++;

                    input = Console.ReadLine().Split();

                    if (CheckInput(input[0].ToLower()))
                    {
                        Console.WriteLine("Congratulations, you won!" +
                            "\nIt took you " + this.attempt.ToString() + " attempts");
                        return;
                    }
                }

                TellSolution();

                return;
            }

            private void TellSolution()
            {
                Console.WriteLine("You ran out of attempts!" +
                    "\nThe answer was: " + this.word);
                return;
            }
        }
        static void Main(string[] args)
        {
            Wordle MainGame = new Wordle();

            MainGame.StartGame();

            Console.Read();
        }
    }
}