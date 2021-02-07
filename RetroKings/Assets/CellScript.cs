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
		GameManager.SetSelectedCell(this.gameObject);
	}

	public void OccupiedBy(GameObject piece)
	{
		this.pieceOccuping = piece;
		this.occupied = true;
	}	

	public void UnOccupy()
	{
		pieceOccuping = null;
		occupied = false;
	}

	public bool IsOccupied() { return this.occupied; }

	public void Captured(GameObject newPiece)
	{
		Debug.Log("captured piece");

		Destroy(pieceOccuping);
		GameManager.AddPoints();

		// replacing the piece that was occupying the cell with the new piece
		pieceOccuping = newPiece;
	}
}
