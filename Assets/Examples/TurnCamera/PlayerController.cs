using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public bool smoothTransition = false;
	public float transitionSpeed = 10f;
	public float transitionRotationSpeed = 500f;

	Vector3 targetGridPos;
	Vector3 prevTargetGridPos;
	Vector3 targetRotation;

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

	private bool isNotCollision(Vector3 newPosition)
	{
		var x = System.Convert.ToInt32(newPosition.x);
		var z = System.Convert.ToInt32(newPosition.z);

		if (map[z][x] == ' ')
			return true;

		return false;
	}

	private void Start()
	{
		targetGridPos = Vector3Int.RoundToInt(transform.position);
	}

	private void FixedUpdate()
	{
		MovePlayer();
	}

	private void MovePlayer()
	{
		if (isNotCollision(targetGridPos))
		{
			prevTargetGridPos = targetGridPos;

			Vector3 targetPosition = targetGridPos;

			if (targetRotation.y > 270f && targetRotation.y < 361f) targetRotation.y = 0;
			if (targetRotation.y < 0f) targetRotation.y = 270f;

			if (!smoothTransition)
			{
				transform.position = targetPosition;
				transform.rotation = Quaternion.Euler(targetRotation);
			}
			else
			{
				transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * transitionSpeed);
				transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(targetRotation), Time.deltaTime * transitionRotationSpeed);
			}
		}
		else
		{
			targetGridPos = prevTargetGridPos;
		}
	}

	public void RotateLeft() { if (AtRest) targetRotation -= Vector3.up * 90f; }
	public void RotateRight() { if (AtRest) targetRotation += Vector3.up * 90f; }
	public void MoveForward() { if (AtRest) targetGridPos += transform.forward; }
	public void MoveBackward() { if (AtRest) targetGridPos -= transform.forward; }
	public void MoveLeft() { if (AtRest) targetGridPos -= transform.right; }
	public void MoveRight() { if (AtRest) targetGridPos += transform.right; }


	bool AtRest
	{
		get
		{
			if ((Vector3.Distance(transform.position, targetGridPos) < 0.05f) &&
				(Vector3.Distance(transform.eulerAngles, targetRotation) < 0.05f))
				return true;
			else
				return false;
		}
	}
}
