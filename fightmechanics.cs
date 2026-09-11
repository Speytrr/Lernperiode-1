string character = "x";
string hitWall = "<------------------------------------------------------------>";
string hitRangeBeginning = "[";
string hitRangeEnding = "]";
int characterPosition = 0;

Random rnd = new Random();
int i = rnd.Next(1, 53);


i = rnd.Next(1, 53);


Console.SetCursorPosition(30, 3);

//Hitrange is being integrated into the wall
if (hitWall.Length > hitRangeBeginning.Length + i)
{
    hitWall = hitWall.Remove(i, hitRangeBeginning.Length).Insert(i, hitRangeBeginning);
    hitWall = hitWall.Remove(i += 8, hitRangeEnding.Length).Insert(i += 8, hitRangeEnding);

    Console.WriteLine(hitWall);
}

Console.SetCursorPosition(30, 3);

hitWall = hitWall.Remove(characterPosition).Insert(characterPosition, character);
Console.WriteLine(hitWall);


