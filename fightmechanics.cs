using System.Threading;

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


int delay = 15;
int direction = 1;

for (int a = 1; ; a += direction)
{
    if (a == rangePosition || a == rangeEndPosition)
    {
        if (a >= hitWall.Length - 2)
        {
            direction = -1;
        }
        if (a <= 1)
        {
            direction = 1;
        }
        continue;
    }

    if (Console.KeyAvailable)
    {
        Console.ReadKey(true);
        if (rangePosition < a && a< rangeEndPosition)
        {
            Console.WriteLine("Hit");
            break;
        }
        else
        {
            Console.WriteLine("Boo");
            break;
        }
    }

    Console.SetCursorPosition(37, 3);
    currentWall = currentWall.Remove(a, 1).Insert(a, character);
    Console.WriteLine(currentWall);
    currentWall = currentWall.Remove(a, 1).Insert(a, "-");
    Thread.Sleep(delay);

    if (delay > 1)
    {
        delay -= 1;
    }

    if (a >= hitWall.Length - 2)
    {
        direction = -1;
    }
    if (a <= 1)
    {
        direction = 1;
    }
}
    









