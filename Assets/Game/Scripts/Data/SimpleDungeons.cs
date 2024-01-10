using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleDungeons : MonoBehaviour
{
	[SerializeField] private GameObject tilePrefab;
	[SerializeField] private Sprite wall;
	[SerializeField] private Sprite floor;

	private Tile[,] tiles;

	private Dictionary<Tile, GameObject> tileToVisual;

	private const int GridSize = 111;

	[SerializeField]
	private int maxRoomCount = 10;
	public List<Room> roomList;

	private void Start()
	{
		SetUpTiles();

		roomList = new List<Room>();

		for (int i = 0; i < maxRoomCount; i++)
		{
			CreateRoom();
		}

		DrawPath();
	}

	private void SetUpTiles()
	{
		tileToVisual = new Dictionary<Tile, GameObject>();
		tiles = new Tile[GridSize, GridSize];

		GameObject fullGrid = new GameObject("Full Grid");

		fullGrid.transform.position = new Vector3(0, 0, 0);

		for (int i = 0; i < GridSize; i++)
		{
			for (int j = 0; j < GridSize; j++)
			{
				Tile t = new Tile(i, j, false);
				tiles[i, j] = t;

				GameObject go = Instantiate(tilePrefab, new Vector3(i, j, 0), Quaternion.identity);
				go.GetComponent<SpriteRenderer>().sprite = wall;
				go.transform.SetParent(fullGrid.transform);

				tileToVisual.Add(tiles[i, j], go);
			}
		}
	}

	private void CreateRoom()
	{
		int roomX = Random.Range(1, 101);
		int roomY = Random.Range(1, 101);
		int rows = Random.Range(5, 11);
		int columns = Random.Range(5, 11);

		Room room = new Room(roomX, roomY, rows, columns);

		for (int i = 0; i < roomList.Count; i++)
		{
			if (room.Intersects(roomList[i]))
			{
				return;
			}
		}

		roomList.Add(room);
		bool walkable = false;

		GameObject go = new GameObject("Room");
		go.transform.position = Vector3.zero;

		for (int i = 0; i < room.Width; i++)
		{
			for (int j = 0; j < room.Height; j++)
			{
				if (i == 0 || j == 0 || i == room.Width - 1 || j == room.Height - 1)
					walkable = false;
				else
					walkable = true;

				tiles[i + room.XPos, j + room.YPos].IsWalkable = walkable;
				Tile t = tiles[i + room.XPos, j + room.YPos];

				if (t.IsWalkable)
					tileToVisual[t].GetComponent<SpriteRenderer>().sprite = floor;

				tileToVisual[t].transform.SetParent(go.transform);
			}
		}
	}

	private void DrawPath()
	{
		int nextRoom = 0;
		int XDir = 1;
		int YDir = 1;

		// логика такая - берем текущую комнату и берем следующую комнату в массиве (по индексу nextRoom), соединяем их. то есть например room[0] будет соединено с room[1], а оно будет соединено с room[2] и т.д.
		for (int i = 0; i < roomList.Count - 1; i++)
		{
			nextRoom += 1;

			int XDiff = (roomList[nextRoom].centerX + roomList[nextRoom].XPos) - (roomList[i].centerX + roomList[i].XPos);
			int YDiff = (roomList[nextRoom].centerY + roomList[nextRoom].YPos) - (roomList[i].centerY + roomList[i].YPos);

			int currentTileX = roomList[i].centerX + roomList[i].XPos;
			int currentTileY = roomList[i].centerY + roomList[i].YPos;

			XDir = (XDiff < 0) ? -1 : 1;
			YDir = (YDiff < 0) ? -1 : 1;

			int XCount = Mathf.Abs(XDiff);
			int YCount = Mathf.Abs(YDiff);

			int endXPos = 0;

			for (int j = 0; j < XCount; j++)
			{
				SetTileFloor(currentTileX + (j * XDir), currentTileY);
				endXPos = currentTileX + (j * XDir);
			}

			for (int j = 0; j < YCount; j++)
				SetTileFloor(endXPos, currentTileY + (j * YDir));
		}
	}

	private void SetTileFloor(int i, int j)
	{
		Tile t = tiles[i, j];
		t.IsWalkable = true;
		tileToVisual[t].GetComponent<SpriteRenderer>().sprite = floor;
	}
}
