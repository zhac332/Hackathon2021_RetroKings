using Assets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn_MovementScript : MonoBehaviour, PieceInterface
{
	private GameManagerScript GameManager;
	private int X_Coord = 0, Y_Coord = 0;

	private bool downwards = false; // false = Upwards; true = Downwards;
	private bool selected = false; // to send this information to the GameManagerScript
	private bool firstMove = true;

	private string color = "";

	private void Start()
	{
		GameManager = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
		if (name.Contains("White")) color = "White";
		else if (name.Contains("Black")) color = "Black";
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
		if (GameManager.GetTurn() == color) // that means, if the selected piece belongs to the player.
		{
			if (!selected)
			{
				Debug.Log("piece is selected");
				selected = true;
				GameManager.SetSelectedPiece(this.gameObject);
				this.gameObject.GetComponent<Renderer>().material.color = Color.green;
			}
			else
			{
				Debug.Log("piece is not selected");
				selected = false;
				GameManager.SetSelectedPiece(null);
				this.gameObject.GetComponent<Renderer>().material.color = Color.white;
			}
		}
		else
		{
			if (GameManager.GetSelectedPiece()) // if I DID select a first piece
			{
				selected = true;
				GameManager.SetSelectedPiece2(this.gameObject);
			}
			else
			{
				Debug.Log("Not your piece!!");
			}
		}
	}	

	public void MoveToCell(int[] newCoords, CellScript cell)
	{
		Vector3 position = cell.transform.position;

		this.X_Coord = newCoords[0];
		this.Y_Coord = newCoords[1];
		this.selected = false;
		if (firstMove) firstMove = false;
		position.y += 0.1f;
		position.z = -5f;
		this.transform.position = position;

		if (cell.IsOccupied())
		{
			// that means that I am capturing that piece
			cell.Captured(this.gameObject);
			this.gameObject.GetComponent<Renderer>().material.color = Color.white;
		}
		else
		{
			Debug.Log("occupied new cell");
			cell.OccupiedBy(this.gameObject);
			this.gameObject.GetComponent<Renderer>().material.color = Color.white;
		}
	}

	public void Deselect()
	{
		this.selected = false;
	}
}
