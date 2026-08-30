using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Xml.Linq;


public class Program
{

    static string name;

    static List<string> inventory = new List<string>();
    static string inventoryMessage = "";

    static Dictionary<string, string> itemDescriptions = new Dictionary<string, string>
{
    { "water", "It's just... H2O I guess." },
    { "key 231", "A small rusty key with numbers 231" },
    { "small knife", "A small knife. Useful for cutting things." },
    { "photograph", "An old photograph, two person are in the picture" },
};


    //Animated text version of Console.WriteLine
    static void myText(string input, int speed = 40)
    {
        foreach (char letter in input)
        {
            Console.Write(letter);
            Thread.Sleep(speed);
        }
        Console.WriteLine("");
    }

    //Item Description
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

    //Inventory Management
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


    //static string to call out functions like inventory, help etc.
    static string getMec(List<string> inventory)
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

                string result = inventoryMenu(inventory);

                if (result != "back")
                {
                    return result;
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

    //MAIN
    public static void Main()
    {
        string chapterChoice = "nothing";


        //Intro
        myText("Welcome player, how would you wanna be called?");
        name = Console.ReadLine();
        myText(name + "... That's a nice name");
        myText("Welcome to the game " + name);


        //Chapter selecting
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


    //TUTORIAL
    static void Tutorial(List<string> inventory)
    {
        int tutorialHealth = 10;
        int messageignored = 0;


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
        myText("Some choices can lead to unexpected things");
        Console.WriteLine("");

        //Consequences example
        while (true)
        {
            myText("You see a killer behind the door");
            myText("Now the killer is coming towards you");
            myText("Dou you -run- or -fight-?");
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
                Console.WriteLine("");
                myText("You gotta be quicker, the kille got your hand");
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
        myText("You can type 'use itemname' to use an item while you are in inventory");
        myText("And type only itemname to get information about it");
        Console.WriteLine("");
        inventory.Add("water");

        //Using item example
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
                if (messageignored == 0)
                {
                    Console.WriteLine("");
                    myText("Look, you can die, please drink something");
                    messageignored++;
                }
                else if (messageignored == 1)
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


    //CHAPTER 1
    static void FirstChapter(List<string> inventory)
    {
        inventory.Add("small knife");
        inventory.Add("key 231");
        inventory.Add("photograph");

        bool lookedAround = false;
        bool checkedSelf = false;
        bool listened = false;

        
        Console.WriteLine("");
        myText("Loading...");
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
        myText("\"Hey " + name + ", I'm upstairs. Come here.\"");
        myText("You turn towards the source of the voice.");
        myText("You see her holding a cup of steaming tea in her delicate fingers.");
        myText("Her hair is tied behind her head.");
        myText("Around her neck hangs a necklace with a dark blue emerald in the middle.");
        myText("Her dark green dress makes her look like part of the plants surrounding the balcony.");

        myText("You quickly climb the stairs.");
        myText("Among the flowers covering the balcony, she sits in the middle of the lilies.");
        myText("Although her straw hat covers most of her face, you can tell that she is happy.");
        myText("You walk towards the table.");
        myText("The plate of cookies in the middle immediately catches your attention.");
        myText("\"Your favourites. But you're a little late today.\"");
        myText("You try to say something.");
        myText("But your throat feels as if something is stuck in it.");

        myText("\"Is something wrong?\"");

        myText("Your vision begins to blur.");

        myText("\"What's wrong?\"", 50);
        myText("\"Why?\"", 70);
        myText("\"Why?\"", 80);
        myText("\"Why...\"", 90);
        myText("Your knees begin to shake.");
        myText("She raises her head and looks at you.");

        myText("But you can't see anything anymore.");

        myText("Only a completely white face.");

        myText("\"Why... why... why...\"", 120);
        myText("");
        myText("You wake up in terror.",0);
        myText("For a few seconds, you can do nothing but breathe heavily.");

        myText("Where am I?", 80);

        while (true)
        {
            Console.WriteLine("");
            Console.WriteLine("> look around");
            Console.WriteLine("> check yourself");
            Console.WriteLine("> listen");
            
            string decision = getMec(inventory);

            if (decision == "look around")
            {
                if (!lookedAround)
                {
                    myText("");
                    myText("As you calm down, you look around.");
                    myText("You are lying on the floor in the middle of the room.");
                    myText("There is a door directly in front of you, and several lockers behind you.");
                    myText("The walls are covered with posters of musicians and actors.");
                    myText("There are names written on the lockers.");
                    myText("...");

                    lookedAround = true;
                }
                else
                {
                    myText("");
                    myText("You already looked around.");
                }
            }

            else if (decision == "check yourself")
            {
                if (!checkedSelf)
                {
                    myText("");
                    myText("You check yourself.");
                    myText("You don't seem to have any wounds or feel any pain.");
                    myText("You're wearing what appears to be a waiter's uniform.");
                    myText("You notice a few things inside your inner pocket.");
                    myText("A photograph, a small knife, and a key marked with the number 231.");
                    myText("...");
                    checkedSelf = true;
                }
                else
                {
                    myText("");
                    myText("You already checked yourself.");
                }
            }

            else if (decision == "listen")
            {
                if (!listened)
                {
                    myText("");
                    myText("You listen carefully.");
                    myText("You can hear a violin playing somewhere outside, along with faint conversations.");
                    myText("You can't make out anything else.");
                    myText("The sounds don't seem chaotic, though.");
                    myText("...");
                    listened = true;
                }
                else
                {
                    myText("");
                    myText("You listen again, but don't notice anything new.");
                }
            }
            else if (decision == "use:photograph")
            {
                myText("");
                myText("You look at the photograph.");
                myText("It shows you standing next to a woman.");
                myText("You're wearing an elegant suit, and you look happy.");
                myText("But when you look at the woman beside you, her face appears blurry.");

                myText("You can't remember who she is.");

                myText("You turn the photograph around.");
                myText("There is a number written on the back.");

                myText("231", 200);
            }
            else if (decision == "use:small knife")
            {
                myText("");
                myText("A simple, small knife.");
                myText("There is nothing particularly remarkable about it.");
                myText("Still, it looks like it could be useful.");

                myText("...");
            }
            else if (decision == "use:key 231")
            {
                myText("");
                myText("A slightly old key.");
                myText("The number 231 is engraved on the keychain.");
                myText("You have no idea what it opens.");

                myText("...");
            }
            else if (decision.StartsWith("use:"))
            {
                myText("");
                myText("You can't use that here.");
            }
            else
            {
                myText("");
                myText("You don't know what to do.");
            }
            if ((lookedAround && checkedSelf) || (lookedAround && listened) || (checkedSelf && listened))
            {
                break;
            }
        }

        //Helper comes
        myText("Thats all for now ('-')");








    }





}


