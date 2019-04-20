using System;
using System.IO;
using System.Linq;

namespace Randomizer
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            bool quit = false;

            string path = GetPath();

            while (quit != true)
            {
                string list = "";
                string[] vetoArray = { };

                Console.WriteLine("====================");
                Console.WriteLine();
                Console.WriteLine("Welcome to Randomizer! " +
                    "Would you like to enter values manually (\"m\") or use the input file at " +
                    "{0} (\"f\", or just hit Enter)?", path);
                string method = Console.ReadLine();
                Console.WriteLine();

                QuitCheck(method);

                switch (method.ToLower())
                {
                    case "m":
                    case "manual":
                    case "manually":
                        Console.WriteLine("Please list off the options you would like to pick from, " +
                            "and I'll select one of them randomly. " +
                            "Make sure each option is separated with a comma \",\":");
                        list = Console.ReadLine();
                        break;
                    default:
                        if (path != null)
                        {
                            list = File.ReadAllText(path);
                            Console.WriteLine("Here are the items being chosen from:");
                            Console.WriteLine(list);
                            Console.WriteLine();
                            Console.WriteLine("Would you like to veto any of the above options? " +
                                "List them or press Enter to continue without vetoing any:");
                            string vetos = Console.ReadLine();
                            vetoArray = ListToArray(vetos);
                        }
                        else
                        {
                            Console.WriteLine("Error! There is no input.txt file at that location.");
                        }
                        break;
                }

                Console.WriteLine();

                QuitCheck(list);

                string[] itemArray = ListToArray(list);

                if (vetoArray != null && itemArray != null)
                {
                    itemArray = itemArray.Except(vetoArray).ToArray();
                }

                if (itemArray != null && itemArray.Length >= 2)
                {
                    string selectedItem = RandomPicker(itemArray);
                    if (!string.IsNullOrEmpty(selectedItem))
                    {
                        Console.WriteLine("Here is the randomly-selected item:");
                        Console.WriteLine(selectedItem);
                    }
                    else
                    {
                        Console.WriteLine("Sorry! An error occurred somewhere in the selection process.");
                    }

                    Console.WriteLine();
                    Console.WriteLine("If you'd like to try again, hit Enter, " +
                        "or type \"quit\" (\"q\") to close the program");
                    string end = Console.ReadLine();

                    QuitCheck(end);
                }
                else
                {
                    Console.WriteLine("Error! You have to supply at least 2 options to pick from.");
                    Console.WriteLine();
                }
            }

            Environment.Exit(0);
        }

        public static string GetPath()
        {
            string fromVS = "../../../input.txt";
            string fromTerminal = "../input.txt";

            if (File.Exists(fromVS))
            {
                return Path.GetFullPath(fromVS);
            }

            if (File.Exists(fromTerminal))
            {
                return Path.GetFullPath(fromTerminal);
            }

            return null;
        }

        public static void QuitCheck(string input)
        {
            switch(input.ToLower())
            {
                case "q":
                case "quit":
                    Environment.Exit(0);
                    break;
            }
        }

        public static string[] ListToArray(string input)
        {
            if (!string.IsNullOrEmpty(input))
            {
                string[] items = input.Split(',');
                return items;
            }

            return null;
        }

        public static string RandomPicker(string[] itemArray)
        {
            Random randomizer = new Random();
            int rand = randomizer.Next(0, itemArray.Length);

            int count = 0;
            foreach (string item in itemArray)
            {
                string currentItem = item;

                if (rand == count)
                {
                    if (currentItem.StartsWith(" ", StringComparison.Ordinal))
                    {
                        currentItem = currentItem.Substring(1);
                    }

                    return currentItem;
                }

                count++;
            }

            return null;
        }
    }
}
