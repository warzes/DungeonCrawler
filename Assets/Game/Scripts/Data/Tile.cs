public class Tile
{
	public int X;
	public int Y;
	public bool IsWalkable;

	public Tile(int x, int y, bool isWalkable)
	{
		X = x;
		Y = y;
		IsWalkable = isWalkable;
	}
}
