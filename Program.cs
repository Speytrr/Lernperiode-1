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

    //Animated text version of Console.WriteLine
    static void MyText(string input, int speed = 30)
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
        MyText("Loading...");
        Console.WriteLine("");
        MyText(title);
        MyText("Press SPACE to continue...");


        while (true)
        {
            if (Console.ReadKey(true).Key == ConsoleKey.Spacebar)
            {
                break;
            }
        }

    }

    //This avoids clone items
    static void AddItem(string item)
    {
        if (!inventory.Contains(item))
        {
            inventory.Add(item);
        }
    }

    //Item Description
    static void ShowItemDescription(string item)
    {
        if (itemDescriptions.ContainsKey(item))
        {
            MyText(itemDescriptions[item]); //It takes information from dictionary itemDescriptions
        }
        else
        {
            MyText("You don't know anything about this item.");
        }
    }

    //Inventory Management
    static string InventoryMenu(List<string> inventory)
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
                    MyText("You don't have this item.");
                }
            }
            else if (inventory.Contains(inventoryInput))
            {
                ShowItemDescription(inventoryInput);
            }
            else
            {
                MyText("You are in the inventory. Type 'use (item)', '(item)' or 'back'.");
                Console.WriteLine("");
            }
        }
    }

    //Static string to call out functions like inventory, help etc.
    static string GetPlayerInput(List<string> inventory)
    {
        while (true)
        {
            string input = Console.ReadLine();

            if (input == "inventory")
            {
                string result = InventoryMenu(inventory);
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

    //Fighting Mechanics
    public static bool Play(int startingDelay)
    {
        Console.Clear();
        string character = "x";
        string hitWall = "<-------------------------------------------------------->";
        string rangeBeg = "[";
        string rangeEnd = "]";
        int rangePosition;
        int rangeEndPosition;
        Random rnd = new Random();



        rangePosition = rnd.Next(1, 51);
        rangeEndPosition = rangePosition + 5;

        string currentWall = hitWall;

        currentWall = currentWall.Remove(rangePosition, rangeBeg.Length).Insert(rangePosition, rangeBeg);
        currentWall = currentWall.Remove(rangeEndPosition, rangeEnd.Length).Insert(rangeEndPosition, rangeEnd);


        int delay = startingDelay;
        int direction = 1;

        for (int characterPosition = 1; ; characterPosition += direction)
        {
            if (characterPosition == rangePosition || characterPosition == rangeEndPosition)
            {
                if (characterPosition >= hitWall.Length - 2)
                {
                    direction = -1;
                }
                if (characterPosition <= 1)
                {
                    direction = 1;
                }
                continue;
            }

            if (Console.KeyAvailable)
            {
                Console.ReadKey(true);
                if (rangePosition < characterPosition && characterPosition < rangeEndPosition)
                {
                    Console.Clear();
                    Console.SetCursorPosition(0, 0);
                    Thread.Sleep(100);
                    return true;
                }
                else
                {
                    Console.Clear();
                    Console.SetCursorPosition(0, 0);
                    Thread.Sleep(100);
                    return false;
                }
            }

            Console.SetCursorPosition(37, 3);
            currentWall = currentWall.Remove(characterPosition, 1).Insert(characterPosition, character);
            Console.WriteLine(currentWall);
            currentWall = currentWall.Remove(characterPosition, 1).Insert(characterPosition, "-");
            Thread.Sleep(delay);

            if (delay > 1)
            {
                delay -= 1;
            }

            if (characterPosition >= hitWall.Length - 2)
            {
                direction = -1;
            }
            if (characterPosition <= 1)
            {
                direction = 1;
            }
        }

    }

    //Game Intro
    public static void Main()
    {

        bool hit = Play(15);

        if (hit)
        {
            MyText("Hit! Bravo");
        }
        else
        {
            MyText("Boo! You missed");
        }
        Thread.Sleep(1000);


        Console.WriteLine("      ------------------------------------------------------------------------------------------------- ");
        Console.WriteLine("     ¦                                                                                                 ¦");
        Console.WriteLine("     ¦                                            GAME                                                 ¦");
        Console.WriteLine("     ¦                                                                                                 ¦");
        Console.WriteLine("     ¦                                    BEST GAME OF ALL TIME                                        ¦");
        Console.WriteLine("     ¦                                                                                                 ¦");
        Console.WriteLine("     ¦    LOADING...                                                                                   ¦");
        Console.WriteLine("     ¦                                                                                                 ¦");
        Console.WriteLine("      ------------------------------------------------------------------------------------------------- ");

        Thread.Sleep(4000);
        Console.Clear();
        NameSelecting(inventory);
    }

    //Name Selecting
    static void NameSelecting(List<string> inventory)
    {
        string nametry = "";
        // Intro
        MyText("Welcome player, how would you wanna be called?");

        while (nametry == "")
        {
            nametry = Console.ReadLine();
            if (nametry != "")
            {
                name = nametry;
                break;
            }
            else
            {
                Console.WriteLine("Try again");
            }
        }


        MyText(name + "... That's a nice name");
        MyText("Welcome to the game " + name);
        ChapterSelecting(inventory);
    }

    //Chapter Selecting
    static void ChapterSelecting(List<string> inventory)
    {
        while (true)
        {
            MyText("Would you like to see the tutorial or start with chapter one?");
            MyText("> tutorial");
            MyText("> chapter 1");

            string chapterChoice = Console.ReadLine();

            if (chapterChoice == "tutorial")
            {
                Console.WriteLine("");
                MyText("Very well, have fun");
                Tutorial(inventory);
                break;
            }
            else if (chapterChoice == "chapter 1")
            {
                Console.WriteLine("");
                MyText("Good luck then");
                FirstChapter(inventory);
                break;
            }
            else
            {
                MyText("You have to type in tutorial or chapter 1. You are not messing with me are you?");
                Console.WriteLine("");
            }
        }
    }

    //TUTORIAL
    static void Tutorial(List<string> inventory)
    {
        int tutorialHealth = 10;
        int messageIgnored = 0;

        PressTo("Tutorial");


        Console.WriteLine("");
        MyText("Welcome to the tutorial " + name);
        MyText("This is a choice based game, that goes on with your decisions");
        MyText("You will be making your own choices and using items when needed");
        MyText("Let's start...");
        Console.WriteLine("");


        // Choices example
        while (true)
        {
            MyText("You find yourself in a room. There are two doors");
            MyText("Which door do you choose? (left/right)");

            string decision = GetPlayerInput(inventory);

            if (decision == "left")
            {
                Console.WriteLine("");
                MyText("You take the left door.");
                break;
            }
            else if (decision == "right")
            {
                Console.WriteLine("");
                MyText("You take the right door.");
                break;
            }
            else
            {
                MyText("Try again");
                Console.WriteLine("");
            }
        }


        Console.WriteLine("");
        MyText("See, you made a decision");
        MyText("But it's not always that easy");
        MyText("Some choices can lead to unexpected things");
        Console.WriteLine("");


        // Consequences example
        while (true)
        {
            MyText("You see a killer behind the door");
            MyText("Now the killer is coming towards you");
            MyText(">Run", 0);
            MyText(">Fight", 0);

            string decision = GetPlayerInput(inventory);

            if (decision == "fight")
            {
                Console.WriteLine("");
                MyText("It was harsh but you beat the killer somehow");
                break;
            }
            else if (decision == "run")
            {
                Console.WriteLine("");
                MyText("You try to run but the killer is faster than you.");
                MyText("You take 2 damage");
                tutorialHealth = tutorialHealth - 2;
                break;
            }
            else
            {
                MyText("You gotta be quicker, the killer got your hand");
                MyText("You take 7 damage");
                tutorialHealth = tutorialHealth - 7;
                break;
            }
        }


        Console.WriteLine("");
        MyText("Your max health is 10 and now you have...");
        MyText(tutorialHealth.ToString() + " health");

        tutorialHealth = 10;

        Console.WriteLine("");
        MyText("Last thing you need to know is, how to use your inventory");
        MyText("To look at your inventory write 'inventory'");
        MyText("You can type 'use itemname' to use an item while you are in inventory");
        MyText("And type only itemname to get information about it");
        Console.WriteLine("");

        AddItem("water");


        // Using item example
        while (true)
        {
            if (messageIgnored >= 2)
            {
                Console.WriteLine("");
                MyText("You died of thirst");
                MyText("Happy now?");

                messageIgnored = 0;
                inventory.Remove("water");

                break;
            }

            Console.WriteLine("");
            MyText("You got thirsty after the fight");

            string decision = GetPlayerInput(inventory);

            if (decision == "use:water")
            {
                Console.WriteLine("");
                MyText("You drink the water, well done");
                inventory.Remove("water");
                break;
            }
            else if (decision.StartsWith("use:"))
            {
                MyText("You cant use this here");
            }
            else
            {
                if (messageIgnored == 0 && decision != "back")
                {
                    Console.WriteLine("");
                    MyText("Look, you can die, please drink something");
                    messageIgnored++;
                }
                else if (messageIgnored == 1 && decision != "back")
                {
                    Console.WriteLine("");
                    MyText("You think this is a joke?");
                    messageIgnored++;
                }
            }
        }


        Console.WriteLine("");
        MyText("If you need help you can always type 'help' to get information");

        FirstChapter(inventory);
    }

    //CHAPTER 1
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

        bool hit = Play(15); //Trying fight mechanic out

        if (hit)
        {
            MyText("Hit!");
        }
        else
        {
            MyText("Boo!");
        }

        MyText("Hey " + name + ", I'm upstairs. Come here.");

        MyText("You turn towards the source of the voice.");
        MyText("You see her holding a cup of steaming tea in her delicate fingers.");
        MyText("Her hair is tied behind her head.");
        MyText("Around her neck hangs a necklace with a dark blue emerald in the middle.");
        MyText("Her dark green dress makes her look like part of the plants surrounding the balcony.");
        MyText("You quickly climb the stairs.");
        MyText("Among the flowers covering the balcony, she sits in the middle of the lilies.");
        MyText("Although her straw hat covers most of her face, you can tell that she is happy.");
        MyText("You walk towards the table.");
        MyText("The plate of cookies in the middle immediately catches your attention.");
        MyText("Your favourites. But you're a little late today.");
        MyText("You try to say something.");
        MyText("But your throat feels as if something is stuck in it.");
        MyText("Is something wrong?");
        MyText("Your vision begins to blur.");
        MyText("What's wrong?");
        MyText("Why?");
        MyText("Why?");
        MyText("Why...");
        MyText("Your knees begin to shake.");
        MyText("She raises her head and looks at you.");
        MyText("But you can't see anything anymore.");
        MyText("Only a completely white face.");
        MyText("Why... why... why...", 50);
        MyText("You wake up in terror.");
        MyText("For a few seconds, you can do nothing but breathe heavily.");
        MyText("Where am I?");


        while (true)
        {
            Console.WriteLine("");
            MyText("As you calm down, you look around.");
            MyText("What do you do?");
            MyText(">look around", 0);
            MyText(">check yourself", 0);
            MyText(">listen", 0);

            string doorDecision = GetPlayerInput(inventory);

            if (doorDecision == "look around")
            {
                Console.WriteLine("");
                if (lookedAround == false)
                {
                    MyText("As you calm down, you look around.");
                    MyText("You are lying on the floor in the middle of the room.");
                    MyText("There is a door directly in front of you and several lockers behind you.");
                    MyText("The walls are covered with posters of musicians and actors.");
                    MyText("There are names written on the lockers.");
                    lookedAround = true;
                }
                else
                {
                    MyText("You already looked around.");
                }
            }
            else if (doorDecision == "check yourself")
            {
                Console.WriteLine("");
                if (checkedSelf == false)
                {
                    MyText("You check yourself.");
                    MyText("You don't seem to have any wounds or feel any pain.");
                    MyText("You're wearing what appears to be a waiter's uniform.");
                    MyText("You notice a few things inside your inner pocket.");
                    MyText("A photograph, a small knife, and a key marked with the number 231.");
                    checkedSelf = true;
                }
                else
                {
                    MyText("You already checked yourself.");
                }
            }
            else if (doorDecision == "listen")
            {
                Console.WriteLine("");
                if (listened == false)
                {
                    MyText("You listen carefully.");
                    MyText("You can hear a violin playing somewhere outside, along with faint conversations.");
                    MyText("You can't make out anything else.");
                    MyText("The sounds don't seem chaotic, though.");
                    listened = true;
                }
                else
                {
                    MyText("You listen again, but don't notice anything new.");
                }
            }
            else if (doorDecision == "use photograph")
            {
                Console.WriteLine("");
                MyText("You look at the photograph.");
                MyText("It shows you standing next to a woman.");
                MyText("You're wearing an elegant suit, and you look happy.");
                MyText("But when you look at the woman beside you, her face appears blurry.");
                MyText("You can't remember who she is.");
                MyText("You turn the photograph around.");
                MyText("There is a number written on the back.");
                MyText("231");
            }
            else if (doorDecision == "use small knife")
            {
                Console.WriteLine("");
                MyText("A simple, small knife.");
                MyText("There is nothing particularly remarkable about it.");
                MyText("Still, it looks like it could be useful.");
            }
            else if (doorDecision == "use key 231")
            {
                Console.WriteLine("");
                MyText("A slightly old key.");
                MyText("The number 231 is engraved on the keychain.");
                MyText("You have no idea what it opens.");
            }
            else
            {
                Console.WriteLine("");
                MyText("You don't know what to do.");
            }

            if (lookedAround && checkedSelf)
            {
                break;
            }
            else if (lookedAround && listened)
            {
                break;
            }
            else if (checkedSelf && listened)
            {
                break;
            }
        }

        Console.WriteLine("");

        Console.WriteLine("");
        MyText("You remain where you are for a while.");
        MyText("Suddenly, there is a knock on the door.");
        MyText("Knock knock.");

        Console.WriteLine("");
        MyText("The doorknob turns, and the door creaks open.");
        MyText(name + ", are you still in here?");

        Console.WriteLine("");
        MyText("A short woman, who looks to be in her twenties, stands in front of you.");
        MyText("She seems to be waiting for an answer.");
        MyText(">I will get back to work, sorry. (1)", 0);
        MyText(">Ask where you are. (2)", 0);

        string decision = GetPlayerInput(inventory);

        if (decision == "1")
        {
            Console.WriteLine("");
            MyText("She squints and looks at you again.");
            MyText("Is everything alright?");
            MyText("I... um...");
            MyText("Or are you... ?");
        }
        else if (decision == "2")
        {
            Console.WriteLine("");
            MyText("We're at Brian Hall. Is everything alright?");
            MyText("For a while, the two of you simply stare at each other.");
            MyText(name + "...");
            MyText("Y-yes?");
        }
        else
        {
            Console.WriteLine("");
            MyText("She stares at you for a while.");
        }

        Console.WriteLine("");
        MyText("She approaches you and takes your hand.");
        MyText("You pull your hand away on instinct, but she doesn't react.");
        MyText("Oh no.");
        MyText("Someone erased your memory!");

        Console.WriteLine("");

    }

    //This section is originally made to seperate game logic from dialouge by using txt. file. But I couldn't finish it so it's not being used currently 18.09.2026
/*    public static string ReadStory(string file)
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
    } */
}

