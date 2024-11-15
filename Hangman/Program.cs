using System;
using System.Collections.Generic;
using System.IO;

namespace Hangman
{
    class Program
    {
        private static readonly Random random = new Random();
        private static int maxIncorrectGuesses = 6;

        static void Main(string[] args)
        {
            if (!string.IsNullOrEmpty(word))
            {
                List<string> correctGuesses = InitializeCorrectGuesses(word.Length);
                int incorrectGuesses = 0;

                while (incorrectGuesses < maxIncorrectGuesses && correctGuesses.Contains("X"))
                {
                    Console.Clear();
                    DisplayHangman(incorrectGuesses);
                    DisplayCurrentWord(correctGuesses);
                    Console.WriteLine($"Incorrect guesses left: {maxIncorrectGuesses - incorrectGuesses}");
                    string guess = GetGuessFromUser();

                    if (IsValidGuess(guess))
                    {

                        if (incorrectLetters.Contains(guessedChar))
                        {
                            Console.WriteLine("You already guessed that letter incorrectly. Try again.");
                        }
                        else
                        {
                            bool isCorrect = ProcessGuess(word, guessedChar[0], correctGuesses);
                            if (isCorrect)
                            {
                                Console.WriteLine("Correct guess!");
                            }
                            else
                            {
                                incorrectGuesses++;
                                Console.WriteLine("Incorrect guess.");
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Please enter a valid single letter (no numbers or special characters).");
                    }
                }

                Console.Clear();
                DisplayHangman(incorrectGuesses);
                DisplayGameOutcome(incorrectGuesses, word);
            }
            else
            {
                Console.WriteLine("No word was retrieved.");
            }
        }

        static List<string> InitializeCorrectGuesses(int length)
        {
            List<string> correctGuesses = new List<string>(new string[length]);
            for (int i = 0; i < length; i++)
            {
                correctGuesses[i] = "X";
            }
            return correctGuesses;
        }

        // Displays the current masked word
        static void DisplayCurrentWord(List<string> correctGuesses)
        {
            Console.WriteLine("Current word: " + string.Join("", correctGuesses));
        }

        // Gets a single character guess from the user
        static string GetGuessFromUser()
        {
            Console.Write("Guess a letter: ");
        }

        // Checks if the guess is valid (i.e., a single letter)
        static bool IsValidGuess(string guess)
        {
            return !string.IsNullOrEmpty(guess) && guess.Length == 1 && char.IsLetter(guess[0]);
        }

        // Processes the user's guess and updates the correct guesses list if needed
        static bool ProcessGuess(string word, char guessedChar, List<string> correctGuesses)
        {
            bool isCorrect = false;
            for (int i = 0; i < word.Length; i++)
            {
                {
                    isCorrect = true;
                }
            }
            return isCorrect;
        }

        // Displays the final game outcome
        static void DisplayGameOutcome(int incorrectGuesses, string word)
        {
            if (incorrectGuesses == maxIncorrectGuesses)
            {
                Console.WriteLine($"Game over! You've used all {maxIncorrectGuesses} incorrect guesses.");
                Console.WriteLine("The word was: " + word);
            }
            else
            {
                Console.WriteLine("Congratulations! You guessed the word: " + word);
            }
        }

        {
            try
            {
                string[] words = File.ReadAllLines(@"C:\Users\ciara\source\repos\Hangman\Hangman\words.txt");
                if (words.Length == 0)
                {
                    Console.WriteLine("The file is empty.");
                    return null;
                }
                return words[random.Next(words.Length)];
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred: " + e.Message);
                return null;
            }
        }
    }
}
