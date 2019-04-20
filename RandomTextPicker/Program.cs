using System;
using System.IO;
using System.Linq;

namespace Randomizer
{
    class MainClass
    {
        public static bool quit = false;

        public static void Main(string[] args)
        {
            while (quit != true)
            {
                string path = "";

                if (File.Exists("../../../input.txt"))
                {
                    path = Path.GetFullPath("../../../input.txt");
                }
                else if (File.Exists("../input.txt"))
                {
                    path = Path.GetFullPath("../input.txt");
                }

                string list = "";
                string[] vetoArray = { };

                Console.WriteLine("Welcome to Randomizer! " +
                    "Would you like to enter values manually (\"m\") or use the input file at " +
                    "{0} (\"f\", or just hit Enter)?", path);
                string method = Console.ReadLine().ToLower();

                QuitCheck(method);

                switch (method)
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
                        if (File.Exists(path))
                        {
                            list = File.ReadAllText(path);
                            Console.WriteLine("Here are the items being chosen from:");
                            Console.WriteLine(list);

                            Console.WriteLine("Would you like to veto any of the above options? " +
                                "List them or press Enter to continue without vetoing any:");
                            string vetos = Console.ReadLine();
                            vetoArray = ListToArray(vetos);
                        }
                        else
                        {
                            Console.WriteLine("Error! There is no input file at that location.");
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

                if (itemArray != null)
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
                    string end = Console.ReadLine().ToLower();

                    QuitCheck(end);

                    Console.WriteLine();
                }
            }

            Environment.Exit(0);
        }

        public static void QuitCheck(string input)
        {
            switch(input)
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
