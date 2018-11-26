using System;

namespace Randomizer
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            bool quit = false;

            while (quit != true)
            {
                Console.WriteLine("Welcome to Randomizer! Please list off the options you would like to pick from, and I'll select one of them randomly. " +
                                  "Make sure each option is separated with a comma \",\":");
                string list = Console.ReadLine();
                Console.WriteLine();

                if (list == "quit")
                {
                    quit = true;
                }
                else if (!string.IsNullOrEmpty(list))
                {
                    string[] items = list.Split(',');

                    Random randomizer = new Random();
                    int rand = randomizer.Next(0, items.Length);

                    int count = 0;
                    foreach (string item in items)
                    {
                        string currentItem = item;

                        if (rand == count)
                        {
                            if (currentItem.StartsWith(" "))
                            {
                                currentItem = currentItem.Substring(1);
                            }

                            Console.WriteLine("Here is the randomly-selected item:");
                            Console.WriteLine(currentItem);
                        }

                        count++;
                    }

                    Console.WriteLine();
                    Console.WriteLine("If you'd like to try again, hit Enter, or type \"quit\" to close the program");
                    string end = Console.ReadLine().ToLower();

                    if (end == "quit")
                    {
                        quit = true;
                    }
                    else
                    {
                        Console.WriteLine();
                    }
                }
                else
                {
                    Console.WriteLine("Sorry! You need to input text for this program to work.");

                }
            }

            Environment.Exit(0);
        }
    }
}
