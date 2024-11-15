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
            string word = GetRandomWord();
            if (!string.IsNullOrEmpty(word))
            {
                List<string> correctGuesses = InitializeCorrectGuesses(word.Length);
                int incorrectGuesses = 0;
                HashSet<string> incorrectLetters = new HashSet<string>(StringComparer.OrdinalIgnoreCase); // Case-insensitive set

                while (incorrectGuesses < maxIncorrectGuesses && correctGuesses.Contains("X"))
                {
                    DisplayCurrentWord(correctGuesses);
                    Console.WriteLine($"Incorrect guesses left: {maxIncorrectGuesses - incorrectGuesses}");
                    string guess = GetGuessFromUser();

                    if (IsValidGuess(guess))
                    {
                        string guessedChar = guess.ToLower(); // Handle case insensitivity

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
                                incorrectLetters.Add(guessedChar); // Add the incorrect guess to the set
                                Console.WriteLine("Incorrect guess.");
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Please enter a valid single letter (no numbers or special characters).");
                    }
                }

                DisplayGameOutcome(incorrectGuesses, word);
            }
            else
            {
                Console.WriteLine("No word was retrieved.");
            }
        }

        // Initializes the list with "X" characters to represent the masked word
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
            return Console.ReadLine().ToLower();
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
                if (char.ToLower(word[i]) == char.ToLower(guessedChar)) // Case-insensitive comparison
                {
                    correctGuesses[i] = word[i].ToString(); // Preserve the original case
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

        // Retrieves a random word from the file
        static string GetRandomWord()
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
