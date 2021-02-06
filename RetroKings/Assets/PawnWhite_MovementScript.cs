using Assets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnWhite_MovementScript : MonoBehaviour, PieceInterface
{
	private GameManagerScript GameManager;
	private GameObject[,] board_cells = new GameObject[8, 8];
	private int X_Coord = 0, Y_Coord = 0;

	private bool downwards = false; // false = Upwards; true = Downwards;
	private bool selected = false; // to send this information to the GameManagerScript

	private void Start()
	{
		GameManager = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
		board_cells = GameManager.GetBoardCells();
	}

	public void SetCoordinates(int i, int j, bool direction)
	{
		this.X_Coord = i;
		this.Y_Coord = j;
		this.downwards = direction;
	}

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
}
