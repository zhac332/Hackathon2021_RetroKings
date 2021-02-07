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

	public bool IsKing() { return pieceOccuping.tag == "King"; }
	public bool IsQueen() { return pieceOccuping.tag == "Queen"; }
	public bool IsBishop() { return pieceOccuping.tag == "Bishop"; }
	public bool IsKnight() { return pieceOccuping.tag == "Knight"; }
	public bool IsRook() { return pieceOccuping.tag == "Rook"; }
	public bool IsPawn() { return pieceOccuping.tag == "Pawn"; }

	public bool IsPieceBlack() { return pieceOccuping.name.Contains("Black"); }
	public bool IsPieceWhite() { return pieceOccuping.name.Contains("White"); }

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

	public GameObject GetPieceOccupied()
	{
		return this.pieceOccuping;
	}

	public void Captured(GameObject newPiece)
	{
		Debug.Log("captured piece");

		GameManager.AddPoints(pieceOccuping);
		Destroy(pieceOccuping);

		// replacing the piece that was occupying the cell with the new piece
		pieceOccuping = newPiece;
	}
}
