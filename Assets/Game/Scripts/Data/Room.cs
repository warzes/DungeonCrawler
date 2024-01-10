using UnityEngine;

[System.Serializable]
public class Room
{
	public int XPos;
	public int YPos;

	public int Width;
	public int Height;

	public int centerX;
	public int centerY;

	public Room(int x, int y, int w, int h)
	{
		XPos = x;
		YPos = y;
		Width = w;
		Height = h;
		centerX = Width / 2;
		centerY = Height / 2;
	}

	public bool Intersects(Room rectB)
	{
		return (Mathf.Abs(XPos - rectB.XPos) <= (Mathf.Abs(Width + rectB.Width) / 2.0f))
			 && (Mathf.Abs(YPos - rectB.YPos) <= (Mathf.Abs(Height + rectB.Height) / 2.0f));
	}
}