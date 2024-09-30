using System;
using System.Collections.Generic;
using System.IO;

namespace InteractiveMenu
{
    class Program
    {
        // initates randomization
        static Random random = new Random(); 

        static void Main(string[] args)
        {
            int choice;
            // This list stores user inputs
            List<string> userInputs = new List<string>(); 

            do
            {
                MenuPrompt.DisplayMenu();
                choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        // Keep asking random questions until another number is pressed
                        AskRandomQuestions(userInputs);
                        break;
                    case 2:
                        // Repeats the random question before saving
                        string question = GetRandomQuestion();
                        Console.WriteLine(question);
                        string userInput = Console.ReadLine();
                        // saves the user inputs
                        SaveUserInput(userInput);
                        userInputs.Add(userInput); 
                        break;
                    case 3:
                        // Display all saved inputs
                        if (userInputs.Count > 0)
                        {
                            Console.WriteLine("Saved user inputs:");
                            foreach (string input in userInputs)
                            {
                                Console.WriteLine(input);
                            }
                        }
                        else
                        {
                            Console.WriteLine("No saved user inputs found.");
                        }
                        break;
                    case 4:
                        // Load user saved file
                        string fileName = GetFileName();
                        userInputs = LoadUserInput(fileName);
                        break;
                    case 5:
                        Console.WriteLine("Quitting...");
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }

                Console.WriteLine();
            } while (choice != 5);
        }

        static void AskRandomQuestions(List<string> userInputs)
        {
            string question;
            do
            {
                question = GetRandomQuestion();
                Console.WriteLine(question);
                string userInput = Console.ReadLine();
                userInputs.Add(userInput); // Saves 
                Console.Write("Continue asking random questions (Y/N)? ");
            } while (Console.ReadLine().ToUpper() == "Y");
        }

        static string GetRandomQuestion()
        {
            string[] questions =
            {
                "What is your most remarkable experience?",
                "What do you wish to improve on?",
                "Do you know you can improve daily by following the examples of christ?",
                "What is your favourite scripture?",
                "How was yout day?",
                "Tell me a fun fact about yourself.",
                "what motivates you",
                "which is your favourite sports?"
            };

        return questions[random.Next(questions.Length)];
        }

        static void SaveUserInput(string userInput)
        {
            string fileName = GetFileName();
            using (StreamWriter writer = new StreamWriter(fileName))
            {
                writer.WriteLine(userInput);
                Console.WriteLine("User input saved to file: " + fileName);
            }
        }

        static List<string> LoadUserInput(string fileName)
        {
            List<string> loadedInputs = new List<string>();
            try
            {
                using (StreamReader reader = new StreamReader(fileName))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        loadedInputs.Add(line);
                    }
                    Console.WriteLine("User inputs loaded from file: " + fileName);
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("File not found: " + fileName);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error loading file: " + ex.Message);
            }
            return loadedInputs;
        }

        static string GetFileName()
        {
            Console.Write("Enter file name: ");
            return Console.ReadLine();
        }
    }

    public static class MenuPrompt
    {
        public static void DisplayMenu()
        {
            Console.WriteLine("KINDLY SELECT ANY OF THE FOLLOWING TO PROCEED:");
            Console.WriteLine("1. WRITE");
            Console.WriteLine("2. SAVE");
            Console.WriteLine("3. DISPLAY");
            Console.WriteLine("4. LOAD");
            Console.Write("(5. QUIT):");
            Console.Write("WHAT WILL YOU LIKE TO DO? ");
        }
    }
}