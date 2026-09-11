using System;
using System.Collections.Generic;
using System.IO;
using System.IO;
using System.Linq;
using System.Threading;
using System.Xml.Linq;
using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;


public class Program
{

    static string name;
    public static string parts;

    static List<string> inventory = new List<string>();

    static Dictionary<string, string> itemDescriptions = new Dictionary<string, string>
    {
        { "water", "It's just... H2O I guess." },
        { "key 231", "A small rusty key with numbers 231" },
        { "small knife", "A small knife. Useful for cutting things." },
        { "photograph", "An old photograph, two people are in the picture" },
    };

    //txt into code
    public static string ReadStory(string file)
    {
        string path = Path.Combine(
            @"C:\dev\M319\Entscheidung-Game\Entscheidung-Game\Story",
            file + ".txt"
        );

        return File.ReadAllText(path);

    }

    public static string ReadParts(string part)
    {
        if (part.Contains("[CHOICE]"))
        {
            string txtDecision = Console.ReadLine();
            
        }
        return part;
    }

    // Animated text version of Console.WriteLine
    static void myText(string input, int speed = 30)
    {
        foreach (char letter in input)
        {
            Console.Write(letter);
            Thread.Sleep(speed);
        }
        Console.WriteLine("");
    }

    //Press to continue function
    static void PressTo(string title)
    {
        Console.WriteLine("");
        myText("Loading...");
        Console.WriteLine("");
        myText(title);
        myText("Press SPACE to continue...");


        while (true)
        {
            if (Console.ReadKey(true).Key == ConsoleKey.Spacebar)
            {
                break;
            }
        }

    }


    // This avoids clone items
    static void AddItem(string item)
    {
        if (!inventory.Contains(item))
        {
            inventory.Add(item);
        }
    }


    // Item Description
    static void showItemDescription(string item)
    {
        if (itemDescriptions.ContainsKey(item))
        {
            myText(itemDescriptions[item]);
        }
        else
        {
            myText("You don't know anything about this item.");
        }
    }


    // Inventory Management
    static string inventoryMenu(List<string> inventory)
    {
        Console.WriteLine("");
        Console.WriteLine("Inventory");

        if (inventory.Count == 0)
        {
            Console.WriteLine("[Your inventory is empty]");
        }
        else
        {
            Console.WriteLine(string.Join(" | ", inventory));
        }

        Console.WriteLine("");

        Console.WriteLine("Type 'use item' to use an item");
        Console.WriteLine("Type 'item' to get information about item");
        Console.WriteLine("Or type 'back' to go back");
        Console.WriteLine("");

        while (true)
        {
            string inventoryInput = Console.ReadLine();

            if (inventoryInput == "back")
            {
                return "back";
            }

            if (inventoryInput.StartsWith("use "))
            {
                string item = inventoryInput.Substring(4).Trim();

                if (inventory.Contains(item))
                {
                    return "use:" + item;
                }
                else
                {
                    myText("You don't have this item.");
                }
            }
            else if (inventory.Contains(inventoryInput))
            {
                showItemDescription(inventoryInput);
            }
            else
            {
                myText("You are in the inventory. Type 'use (item)', '(item)' or 'back'.");
                Console.WriteLine("");
            }
        }
    }


    // Static string to call out functions like inventory, help etc.
    static string getMec(List<string> inventory)
    {
        while (true)
        {
            string input = Console.ReadLine();

            if (input == "inventory")
            {
                string result = inventoryMenu(inventory);
                return result;
            }
            else if (input == "help")
            {
                Console.WriteLine("");
                Console.WriteLine("Available commands:");
                Console.WriteLine("- inventory");
                Console.WriteLine("- use (item)");
                Console.WriteLine("- help");
                Console.WriteLine("");

                continue;
            }
            else
            {
                return input;
            }
        }
    }

    // MAIN
    public static void Main()
    {
        string chapterChoice = "nothing";
        string nametry = "";
        // Intro
        myText("Welcome player, how would you wanna be called?");

        while (nametry == "")
        {
            nametry = Console.ReadLine();
            if ( nametry != "")
        {
                name = nametry;
                break;
        }
            else
        {
                Console.WriteLine("Try again");
        }
        }


        myText(name + "... That's a nice name");
        myText("Welcome to the game " + name);


        // Chapter selecting
        while (true)
        {
            myText("Would you like to see the tutorial or start with chapter one?");
            myText("> tutorial");
            myText("> chapter 1");

            chapterChoice = Console.ReadLine();

            if (chapterChoice == "tutorial")
            {
                Console.WriteLine("");
                myText("Very well, have fun");
                Tutorial(inventory);
                break;
            }
            else if (chapterChoice == "chapter 1")
            {
                Console.WriteLine("");
                myText("Good luck then");
                FirstChapter(inventory);
                break;
            }
            else
            {
                myText("You have to type in tutorial or chapter 1. You are not messing with me are you?");
                Console.WriteLine("");
            }
        }
    }


    // TUTORIAL
    static void Tutorial(List<string> inventory)
    {
        int tutorialHealth = 10;
        int messageignored = 0;

        PressTo("Tutorial");


        Console.WriteLine("");
        myText("Welcome to the tutorial " + name);
        myText("This is a choice based game, that goes on with your decisions");
        myText("You will be making your own choices and using items when needed");
        myText("Let's start...");
        Console.WriteLine("");


        // Choices example
        while (true)
        {
            myText("You find yourself in a room. There are two doors");
            myText("Which door do you choose? (left/right)");

            string decision = getMec(inventory);

            if (decision == "left")
            {
                Console.WriteLine("");
                myText("You take the left door.");
                break;
            }
            else if (decision == "right")
            {
                Console.WriteLine("");
                myText("You take the right door.");
                break;
            }
            else
            {
                myText("Try again");
                Console.WriteLine("");
            }
        }


        Console.WriteLine("");
        myText("See, you made a decision");
        myText("But it's not always that easy");
        myText("Some choices can lead to unexpected things");
        Console.WriteLine("");


        // Consequences example
        while (true)
        {
            myText("You see a killer behind the door");
            myText("Now the killer is coming towards you");
            myText(">Run", 0);
            myText(">Fight", 0);

            string decision = getMec(inventory);

            if (decision == "fight")
            {
                Console.WriteLine("");
                myText("It was harsh but you beat the killer somehow");
                break;
            }
            else if (decision == "run")
            {
                Console.WriteLine("");
                myText("You try to run but the killer is faster than you.");
                myText("You take 2 damage");
                tutorialHealth = tutorialHealth - 2;
                break;
            }
            else
            {
                myText("You gotta be quicker, the killer got your hand");
                myText("You take 7 damage");
                tutorialHealth = tutorialHealth - 7;
                break;
            }
        }


        Console.WriteLine("");
        myText("Your max health is 10 and now you have...");
        myText(tutorialHealth.ToString() + " health");

        tutorialHealth = 10;

        Console.WriteLine("");
        myText("Last thing you need to know is, how to use your inventory");
        myText("To look at your inventory write 'inventory'");
        myText("You can type 'use itemname' to use an item while you are in inventory");
        myText("And type only itemname to get information about it");
        Console.WriteLine("");

        inventory.Add("water");


        // Using item example
        while (true)
        {
            if (messageignored >= 2)
            {
                Console.WriteLine("");
                myText("You died of thirst");
                myText("Happy now?");

                messageignored = 0;
                inventory.Remove("water");

                break;
            }

            Console.WriteLine("");
            myText("You got thirsty after the fight");

            string decision = getMec(inventory);

            if (decision == "use:water")
            {
                Console.WriteLine("");
                myText("You drink the water, well done");
                inventory.Remove("water");
                break;
            }
            else if (decision.StartsWith("use:"))
            {
                myText("You cant use this here");
            }
            else
            {
                if (messageignored == 0 && decision != "back")
                {
                    Console.WriteLine("");
                    myText("Look, you can die, please drink something");
                    messageignored++;
                }
                else if (messageignored == 1 && decision != "back")
                {
                    Console.WriteLine("");
                    myText("You think this is a joke?");
                    messageignored++;
                }
            }
        }


        Console.WriteLine("");
        myText("If you need help you can always type 'help' to get information");

        FirstChapter(inventory);
    }


    // CHAPTER 1
    static void FirstChapter(List<string> inventory)
    {
        AddItem("small knife");
        AddItem("key 231");
        AddItem("photograph");

        bool lookedAround = false;
        bool checkedSelf = false;
        bool listened = false;


        PressTo("CHAPTER 1");

        Console.WriteLine("");

        string story = ReadStory("Chapter1_3helper");  
        string[] parts = story.Split("[CHOICES]");
        myText(parts[0]);
        myText(parts[1]);

    }
}
