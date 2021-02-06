using Assets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnWhite_MovementScript : MonoBehaviour, PieceInterface
{
	private GameObject[,] board_cells = new GameObject[8, 8];
	private int X_Coord = 0, Y_Coord = 0;

	private bool downwards = false; // false = Upwards; true = Downwards;

	private void Start()
	{
		board_cells = GameObject.Find("GameManager").GetComponent<GameManagerScript>().GetBoardCells();
	}

	public void SetCoordinates(int i, int j, bool direction)
	{
		this.X_Coord = i;
		this.Y_Coord = j;
		this.downwards = direction;
	}
}
