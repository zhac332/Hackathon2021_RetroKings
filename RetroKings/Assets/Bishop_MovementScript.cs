using Assets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bishop_MovementScript : MonoBehaviour, PieceInterface
{
	private GameManagerScript GameManager;
	private int X_Coord = 0, Y_Coord = 0;
	private bool selected = false; // to send this information to the GameManagerScript
	private string color = "";

	private SoundManagerScript SoundManager;

	private bool immune = false;

	// Start is called before the first frame update
	void Start()
	{
		GameManager = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
		if (name.Contains("White")) color = "White";
		else if (name.Contains("Black")) color = "Black";

		SoundManager = GameObject.Find("SoundManager").GetComponent<SoundManagerScript>();
	}

	public void SetCoordinates(int i, int j)
	{
		this.X_Coord = i;
		this.Y_Coord = j;
	}

	public int GetXCoordinate() { return this.X_Coord; }
	public int GetYCoordinate() { return this.Y_Coord; }

	private void OnMouseDown()
	{
		Debug.Log("clicked Bishop");
		if (GameManager.GetTurn() == color) // that means, if the selected piece belongs to the player.
		{
			if (!selected)
			{
				Debug.Log("piece is selected");
				selected = true;
				GameManager.SetSelectedPiece(this.gameObject);
				this.gameObject.GetComponent<Renderer>().material.color = Color.green;
				SoundManager.PieceSelect();
			}
			else
			{
				Debug.Log("piece is not selected");
				selected = false;
				GameManager.SetSelectedPiece(null);
				this.gameObject.GetComponent<Renderer>().material.color = Color.white;
				SoundManager.PieceSelect();
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
				SoundManager.PieceStuck();
			}
		}
	}

	public void MoveToCell(int[] newCoords, CellScript cell)
	{
		Vector3 position = cell.transform.position;

		this.X_Coord = newCoords[0];
		this.Y_Coord = newCoords[1];
		this.selected = false;
		position.z = -5f;
		this.transform.position = position;

		// that means that I am capturing that piece
		if (cell.IsOccupied())
		{
			GameObject go = cell.GetPieceOccupied();
			bool canAttack = false;

			if (go.tag == "Pawn") canAttack = !go.GetComponent<Pawn_MovementScript>().IsImmune();
			else if (go.tag == "Queen") canAttack = !go.GetComponent<Queen_MovementScript>().IsImmune();
			else if (go.tag == "Rook") canAttack = !go.GetComponent<Rook_MovementScript>().IsImmune();
			else if (go.tag == "Knight") canAttack = !go.GetComponent<Knight_MovementScript>().IsImmune();
			else if (go.tag == "Bishop") canAttack = !go.GetComponent<Bishop_MovementScript>().IsImmune();

			if (canAttack)
			{
				cell.Captured(this.gameObject);
				SoundManager.PieceCapture();
			}
		}
		else
		{
			Debug.Log("occupied new cell");
			cell.OccupiedBy(this.gameObject);
			SoundManager.PieceMove();
		}
		this.gameObject.GetComponent<Renderer>().material.color = Color.white;
	}

	public void Deselect()
	{
		this.selected = false;
	}

	public void MakeImmune()
	{
		this.immune = true;
	}

	public void MakeNotImmune()
	{
		this.immune = false;
	}

	public bool IsImmune()
	{
		return this.immune;
	}
}
