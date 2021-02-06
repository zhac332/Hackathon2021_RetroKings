using Assets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnWhite_MovementScript : MonoBehaviour, PieceInterface
{
	private GameManagerScript GameManager;
	private int X_Coord = 0, Y_Coord = 0;

	private bool downwards = false; // false = Upwards; true = Downwards;
	private bool selected = false; // to send this information to the GameManagerScript
	private bool firstMove = true;

	private void Start()
	{
		GameManager = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
	}

	public void SetCoordinates(int i, int j, bool direction)
	{
		this.X_Coord = i;
		this.Y_Coord = j;
		this.downwards = direction;
	}

	public int GetXCoordinate() { return this.X_Coord; }
	public int GetYCoordinate() { return this.Y_Coord; }
	public bool GetDirection() { return this.downwards; }
	public bool GetFirstMove() { return this.firstMove; }

	private void OnMouseDown()
	{
		if (!selected)
		{
			Debug.Log("piece is selected");
			selected = true;
			GameManager.GetSelectedPiece(this.gameObject);
		}
		else
		{
			Debug.Log("piece is not selected");
			selected = false;
			GameManager.GetSelectedPiece(null);
		}
	}	

	public void MoveToCell(Vector3 position, int[] newCoords)
	{
		this.X_Coord = newCoords[0];
		this.Y_Coord = newCoords[1];
		this.selected = false;
		position.y += 0.1f;
		position.z = -5f;
		this.transform.position = position;
	}
}
