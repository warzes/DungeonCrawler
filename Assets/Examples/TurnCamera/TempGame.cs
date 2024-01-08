using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempGame : MonoBehaviour
{
	[SerializeField] GameObject wallPrefab;

	private string[] map = new[] {
		"XXXXXXXXXXXX",
		"X          X",
		"XX XX  XX XX",
		"XX XX XXX XX",
		"XX  XXX    X",
		"XXX X X XX X",
		"X     X  X X",
		"X XXXXXXXX X",
		"X      X   X",
		"XXX XX    XX",
		"X    X XX  X",
		"XXXXXXXXXXXX",
	};

	private void Start()
	{
		for (int z = 0; z < map.Length; z++)
		{
			for (int x = 0; x < map[z].Length; x++)
			{
				if (map[z][x] == 'X')
				{
					Vector3 position = new Vector3(x, 0, z);
					Instantiate(wallPrefab, position, Quaternion.identity);
				}
			}
		}
	}

	private void Update()
	{
		//if (Input.GetKeyDown(KeyCode.W))
		//	updatePositionIfNoCollision(this.transform.position + this.transform.rotation * Vector3.forward);
		//else if (Input.GetKeyDown(KeyCode.S))
		//	updatePositionIfNoCollision(this.transform.position + this.transform.rotation * Vector3.back);
		//else if (Input.GetKeyDown(KeyCode.A))
		//	updatePositionIfNoCollision(this.transform.position + this.transform.rotation * Vector3.left);
		//else if (Input.GetKeyDown(KeyCode.D))
		//	updatePositionIfNoCollision(this.transform.position + this.transform.rotation * Vector3.right);
		//else if (Input.GetKeyDown(KeyCode.Q))
		//	this.transform.rotation *= Quaternion.Euler(0, -90, 0);
		//else if (Input.GetKeyDown(KeyCode.E))
		//	this.transform.rotation *= Quaternion.Euler(0, 90, 0);
	}

	private void updatePositionIfNoCollision(Vector3 newPosition)
	{
		var x = System.Convert.ToInt32(newPosition.x);
		var z = System.Convert.ToInt32(newPosition.z);

		if (map[z][x] == ' ')
			this.transform.position = newPosition;
	}
}