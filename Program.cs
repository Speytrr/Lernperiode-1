using System.ComponentModel.Design;

string name;
int health = 10;
int messageignored = 0;

string[] inventory = new string[] {"No item", "No item", "No item", "No item", "No item"};

static void myText(string input)
{
    foreach (char letter in input)
    {
        Console.Write(letter);
        Thread.Sleep(20);
    }
    Console.WriteLine("");
}


// static string für inventory, damit inventory immer erreichbar ist
static string GetInput(string[] inventory)
{
    while (true)
    {
        string input = Console.ReadLine();

        if (input == "inventory")
        {
            myText("Inventory");
            myText(string.Join(" | ", inventory));
        }
        else
        {
            return input;
        }
    }
}


myText("What is your name?");
name = Console.ReadLine();

//Tutorial für die Mechanics
myText("Hallo, " + name);
myText("This text based game goes on with your decisions");
myText("You will be making choices and using your items");

myText("Tutorial...");

//Entscheidungen
while (true)
{
    myText("You go into the maze...");
    myText("Which door do you choose? (left/right)");
    string decision = GetInput(inventory);

    if (decision == "left")
    {
        myText("You take the left door.");
        break;
    }
    else if (decision == "right")
    {
        myText("You take the right door.");
        break;
    }
    else
    {
        myText("Try again");
    }
}

myText("See, you made a decision");
myText("But there may also be some consequences");

//Konsequenzen
while (true)
{
    myText("The monster is coming towards you");
    myText("Dou you run or fight?");
    string decision = GetInput(inventory);

    if (decision == "fight")
    {
        myText("It was harsh but you beat the monster");
        break;
    }
    else if (decision == "run")
    {
        myText("You try to run but the monster is faster than you." +
            "You take 2 damage");
        health = health - 2;
        break;
    }
    else
    {
        myText("You gotta be quick, the monster got your hand");
        health = health - 7;
        break;
    }
}

myText("Your max health is 10 and now you have...");
myText(health.ToString());

myText("Last thing, you need to use your inventory");
myText("To look at your inventory write 'inventory'");
myText("And you can try 'use (itemname)' to use an item");

//Item-nutzung
while (true)
{
    inventory[0] = "Water";
    if (messageignored >= 2)
    {
        myText("You died of thirst");
        myText("Happy now?");
        messageignored = 0;
        break;
    }
    myText("You got thirsty after the fight");
    string decision = GetInput(inventory);

    if (decision == "use water")
    {
        myText("You drink the water");
        inventory[0] = "No item";
        break;
    } else
    {
        if (messageignored == 0 && decision == "inventory")
        {
            myText("Look, you can die, please drink something");
        }else if (messageignored == 1)
        {
            myText("You think this is a joke?");
        }
    }
}




