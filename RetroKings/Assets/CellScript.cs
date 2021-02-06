using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellScript : MonoBehaviour
{
	private GameManagerScript GameManager;
	private GameObject pieceOccuping;
	private bool occupied = false;
	private int X_Coord, Y_Coord;

	private void Start()
	{
		GameManager = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
		this.occupied = false;
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
		Debug.Log("cell clicked.");
		GameManager.SetSelectedCell(this.gameObject);
	}

	public void OccupiedBy(GameObject piece)
	{
		this.pieceOccuping = piece;
		this.occupied = true;
	}

	public bool IsOccupied() { return this.occupied; }
}
