using System;
using System.Threading;
using System.Linq;

public class Program
{
    static string inventoryMessage = "";
    
    //Animated text version of Console.WriteLine
    static void myText(string input)
        {
            foreach (char letter in input)
            {
                Console.Write(letter);
                Thread.Sleep(0);
            }
            Console.WriteLine("");
        }


    // static string for functions, it will be reachable whenever is x written
    static string getMec(string[] inventory)
        {
            while (true)
            {
                string input = Console.ReadLine();

                if (input == "inventory")
                {
                    if (inventoryMessage != "")
                    {
                        myText(inventoryMessage);
                        continue;
                    }
                Console.WriteLine("");
                Console.WriteLine("Inventory");
                Console.WriteLine(string.Join(" | ", inventory));
                Console.WriteLine("");
                    
                Console.WriteLine("Type 'use item' to use an item");
                Console.WriteLine("Or type 'back' to go back");

                while (true)
                {
                    string inventoryInput = Console.ReadLine();

                    if (inventoryInput == "back")
                    {
                        break;
                    }

                    if (inventoryInput.StartsWith("use "))
                    {
                        string[] words = inventoryInput.Split(' ');

                        if (inventory.Contains(words[1]))
                        {
                            return words[1];
                        }
                        else
                        {
                            myText("You don't have this item.");
                        }
                    } else
                    {
                        myText("You are in the inventory. Type 'use (item)' or 'back'.");
                    }   
                continue;
                }
                    
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
    public static void Main()
    {
        string name;
        string gameChapter = "nothing";
        
        int tutorialHealth = 10;
        int messageignored = 0;

        bool tutorial = false;
        bool firstChapter = false;

        string[] inventory = new string[] { "No item", "No item", "No item", "No item", "No item" };


        //Intro
        myText("Welcome player, how would you wanna be called?");
        name = Console.ReadLine();
        myText(name + "... That's a good name");
        myText("Welcome to the game " + name);


        //Chapter selecting
        do
        {
            myText("Would you like to see the tutorial or start with chapter one (tutorial/start)");
            gameChapter = Console.ReadLine();

            if (gameChapter == "tutorial")
            {
                Console.WriteLine("");
                myText("Very well, have fun");
                tutorial = true;
                break;

            }
            else if (gameChapter == "start")
            {
                Console.WriteLine("");
                myText("Good luck then");
                firstChapter = true;
            }
            else
            {
                myText("You have to type in tutorial or start. You are not messing with me are you?");
                Console.WriteLine("");
            }
        } while (true);



        //Tutorial section for players
        while (tutorial == true)
        {
            Console.WriteLine("");
            myText("Tutorial loading...");
            myText("Press SPACE to continue...");

            while (true)
            {
                if (Console.ReadKey(true).Key == ConsoleKey.Spacebar)
                {
                    break;
                }
            }
            Console.WriteLine("");
            myText("Welcome to the tutorial " + name);
            myText("This is a choice based game, that goes on with your decisions");
            myText("You will be making your own choices and using items when needed");
            myText("Let's start...");
            Console.WriteLine("");


            //Choices example
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
            myText("Some choices can lead to bad ways");
            Console.WriteLine("");

            //Consequences example
            while (true)
            {
                myText("You see a monster behind the door");
                myText("Now the monster is coming towards you");
                myText("Dou you -run- or -fight-?");
                string decision = getMec(inventory);

                if (decision == "fight")
                {
                    Console.WriteLine("");
                    myText("It was harsh but you beat the monster somehow");
                    break;
                }
                else if (decision == "run")
                {
                    Console.WriteLine("");
                    myText("You try to run but the monster is faster than you.");
                    myText("You take 2 damage");
                    tutorialHealth = tutorialHealth - 2;
                    break;
                }
                else
                {
                    Console.WriteLine("");
                    myText("You gotta be quicker, the monster got your hand");
                    myText("You take 7 damage");
                    tutorialHealth = tutorialHealth - 7;
                    break;
                }
            }

            Console.WriteLine("");
            myText("Your max health is 10 and now you have...");
            myText(tutorialHealth.ToString() + " healths");
            tutorialHealth = 10;
            Console.WriteLine("");
            myText("Last thing you need to know is, how to use your inventory");
            myText("To look at your inventory write 'inventory'");
            myText("And you can try 'use itemname' to use an item");
            Console.WriteLine("");

            //Using item example
            while (true)
            {
                inventory[0] = "water";
                if (messageignored >= 2)
                {
                    Console.WriteLine("");
                    myText("You died of thirst");
                    myText("Happy now?");
                    messageignored = 0;
                    inventory[0] = "No item";
                    break;
                }
                Console.WriteLine("");
                myText("You got thirsty after the fight");
                string decision = getMec(inventory);

                if (decision == "water")
                {
                    Console.WriteLine("");
                    myText("You drink the water, well done");
                    inventory[0] = "No item";
                    break;
                } else
                {
                    if (messageignored == 0 && decision != "inventory")
                    {
                        Console.WriteLine("");
                        myText("Look, you can die, please drink something");
                        messageignored++;
                    } else if (messageignored == 1 && decision != "inventory")
                    {
                        Console.WriteLine("");
                        myText("You think this is a joke?");
                        messageignored++;
                    }
                }
            }

            Console.WriteLine("");
            myText("If you need help you can always type 'help' to get information, help is not working currently (- _ -)");
            tutorial = false;
            firstChapter = true;
            break;
        }



        //First chapter of the game
        while (firstChapter == true)
        {
            inventory[0] = "knife";
            inventory[1] = "key";
            inventory[2] = "fotograph";
            Console.WriteLine("");
            myText("CHAPTER 1");
            myText("Press SPACE to continue...");

            while (true)
            {
                if (Console.ReadKey(true).Key == ConsoleKey.Spacebar)
                {
                    break;
                }
            }

            myText("");
            myText("");
            myText("");


            break;
        }






        //CHOICE MAKING TEMPLATE (Copy-paste)
        while (true)
        {
            myText("");
            string decision = getMec(inventory);

            if (decision == "")
            {
                Console.WriteLine("");
                myText("");
                break;
            }
            else if (decision == "")
            {
                Console.WriteLine("");
                myText("");
                break;
            }
            else
            {
                myText("");
            }
        }
        //SPACE TEMPLATE
        while (true)
        {
            if (Console.ReadKey(true).Key == ConsoleKey.Spacebar)
            {
                break;
            }
        }
    }
    }