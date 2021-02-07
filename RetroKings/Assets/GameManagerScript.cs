/*
Authors:
    Stefan Tanasa
    Haaris Iqbal
*/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The main GameObject that allows the movement of pieces to be made.
/// </summary>
public class GameManagerScript : MonoBehaviour
{
    /**
     * When considering attacking, I think it would be best to implement the turn system.
     * Because otherwise, the mechanism of attacking will be different.
     * 
     * For example, we could do two players and each player has a list of the pieces that belong to him.
     * We also need to assign to each piece the player it belongs to.
     * 
     * This way will be a lot easier when checking an attack.
     * And also, it will be easier to detect an attack, because if we select a piece
     * that is not ours, that means we will attack it straight away.
     * **/


    [Header("White pieces")]
    [SerializeField] private GameObject Pawn_White;
    [SerializeField] private GameObject King_White;
    [SerializeField] private GameObject Queen_White;
    [SerializeField] private GameObject Bishop_White;
    [SerializeField] private GameObject Knight_White;
    [SerializeField] private GameObject Rook_White;
    [Header("Black pieces")]
    [SerializeField] private GameObject Pawn_Black;
    [SerializeField] private GameObject King_Black;
    [SerializeField] private GameObject Queen_Black;
    [SerializeField] private GameObject Bishop_Black;
    [SerializeField] private GameObject Knight_Black;
    [SerializeField] private GameObject Rook_Black;

    private CellScript[,] board_cells = new CellScript[8, 8];
    private GameObject selectedPiece;
    private GameObject selectedPiece2; // used mainly to detect any capturing.
    private CellScript selectedCell;
    private bool attacking = false;

    private string turn = "White"; // white will always start first. I am writing White like this so that it
                                   // will be easier to check which piece has been selected

    // Start is called before the first frame update
    void Start()
    {
        InitBoard();
        ShowWhitePerspective();
    }

    private void InitBoard()
	{
        for (int i = 0; i < 8; i++)
            for (int j = 0; j < 8; j++)
			{
                board_cells[i, j] = GameObject.Find("Cell_" + (7 - i) + j).GetComponent<CellScript>();
                board_cells[i, j].SetCoordinates(7 - i, j);
			}
    }

    private void ShowWhitePerspective()
	{
        /// -----------------------------------------WHITE PIECES-----------------------------------------
        
        // the white pawns will be on row 7
        // the other white pieces will be on row 8

        Vector3 globalPosition = new Vector3();
        GameObject go = new GameObject();

        // setting up the white pawns
        for (int i = 0; i < 8; i++)
		{
            // calculating the position
            globalPosition = board_cells[6, i].transform.position;
            globalPosition.y += 0.1f;
            globalPosition.z = -5f;

            // instantiating the object at that position
            go = Instantiate(Pawn_White, globalPosition, Quaternion.identity);
            go.GetComponent<Pawn_MovementScript>().SetCoordinates(1, i, false);

            // occupying the cell
            board_cells[6, i].GetComponent<CellScript>().OccupiedBy(go);
		}

        // first rook 
        globalPosition = board_cells[7, 0].transform.position;
        globalPosition.z = -5f;
        go = Instantiate(Rook_White, globalPosition, Quaternion.identity);
        board_cells[7, 0].GetComponent<CellScript>().OccupiedBy(go);
        go.GetComponent<Rook_MovementScript>().SetCoordinates(0, 0);

        // second rook
        globalPosition = board_cells[7, 7].transform.position;
        globalPosition.z = -5f;
        go = Instantiate(Rook_White, globalPosition, Quaternion.identity);
        board_cells[7, 7].GetComponent<CellScript>().OccupiedBy(go);
        go.GetComponent<Rook_MovementScript>().SetCoordinates(0, 7);

        // first knight
        globalPosition = board_cells[7, 1].transform.position;
        globalPosition.z = -5f;
        go = Instantiate(Knight_White, globalPosition, Quaternion.identity);
        board_cells[7, 1].GetComponent<CellScript>().OccupiedBy(go);
        go.GetComponent<Knight_MovementScript>().SetCoordinates(0, 1);

        // second knight
        globalPosition = board_cells[7, 6].transform.position;
        globalPosition.z = -5f;
        go = Instantiate(Knight_White, globalPosition, Quaternion.identity);
        board_cells[7, 6].GetComponent<CellScript>().OccupiedBy(go);
        go.GetComponent<Knight_MovementScript>().SetCoordinates(0, 6);

        // first bishop
        globalPosition = board_cells[7, 2].transform.position;
        globalPosition.z = -5f;
        go = Instantiate(Bishop_White, globalPosition, Quaternion.identity);
        board_cells[7, 2].GetComponent<CellScript>().OccupiedBy(go);
        go.GetComponent<Bishop_MovementScript>().SetCoordinates(0, 2);

        // second bishop
        globalPosition = board_cells[7, 5].transform.position;
        globalPosition.z = -5f;
        go = Instantiate(Bishop_White, globalPosition, Quaternion.identity);
        board_cells[7, 5].GetComponent<CellScript>().OccupiedBy(go);
        go.GetComponent<Bishop_MovementScript>().SetCoordinates(0, 5);

        // queen
        globalPosition = board_cells[7, 3].transform.position;
        globalPosition.z = -5f;
        go = Instantiate(Queen_White, globalPosition, Quaternion.identity);
        board_cells[7, 3].GetComponent<CellScript>().OccupiedBy(go);
        go.GetComponent<Queen_MovementScript>().SetCoordinates(0, 3);

        // king
        globalPosition = board_cells[7, 4].transform.position;
        globalPosition.z = -5f;
        go = Instantiate(King_White, globalPosition, Quaternion.identity);
        board_cells[7, 4].GetComponent<CellScript>().OccupiedBy(go);
        go.GetComponent<King_MovementScript>().SetCoordinates(0, 4);

        /// -----------------------------------------BLACK PIECES-----------------------------------------

        // setting up the black pawns
        for (int i = 0; i < 8; i++)
        {
            globalPosition = board_cells[1, i].transform.position;
            globalPosition.y += 0.1f;
            globalPosition.z = -5f;
            go = Instantiate(Pawn_Black, globalPosition, Quaternion.identity);
            go.GetComponent<Pawn_MovementScript>().SetCoordinates(6, i, true);
            board_cells[1, i].GetComponent<CellScript>().OccupiedBy(go);
        }

        // first rook
        globalPosition = board_cells[0, 0].transform.position;
        globalPosition.z = -5f;
        go = Instantiate(Rook_Black, globalPosition, Quaternion.identity);
        board_cells[0, 0].GetComponent<CellScript>().OccupiedBy(go);
        go.GetComponent<Rook_MovementScript>().SetCoordinates(7, 0);

        // second rook
        globalPosition = board_cells[0, 7].transform.position;
        globalPosition.z = -5f;
        go = Instantiate(Rook_Black, globalPosition, Quaternion.identity);
        board_cells[0, 7].GetComponent<CellScript>().OccupiedBy(go);
        go.GetComponent<Rook_MovementScript>().SetCoordinates(7, 7);

        // first knight
        globalPosition = board_cells[0, 1].transform.position;
        globalPosition.z = -5f;
        go = Instantiate(Knight_Black, globalPosition, Quaternion.identity);
        board_cells[0, 1].GetComponent<CellScript>().OccupiedBy(go);
        go.GetComponent<Knight_MovementScript>().SetCoordinates(7, 1);

        // second knight
        globalPosition = board_cells[0, 6].transform.position;
        globalPosition.z = -5f;
        go = Instantiate(Knight_Black, globalPosition, Quaternion.identity);
        board_cells[0, 6].GetComponent<CellScript>().OccupiedBy(go);
        go.GetComponent<Knight_MovementScript>().SetCoordinates(7, 6);

        // first bishop
        globalPosition = board_cells[0, 2].transform.position;
        globalPosition.z = -5f;
        go = Instantiate(Bishop_Black, globalPosition, Quaternion.identity);
        board_cells[0, 2].GetComponent<CellScript>().OccupiedBy(go);
        go.GetComponent<Bishop_MovementScript>().SetCoordinates(7, 2);

        // second bishop
        globalPosition = board_cells[0, 5].transform.position;
        globalPosition.z = -5f;
        go = Instantiate(Bishop_Black, globalPosition, Quaternion.identity);
        board_cells[0, 5].GetComponent<CellScript>().OccupiedBy(go);
        go.GetComponent<Bishop_MovementScript>().SetCoordinates(7, 5);

        // queen
        globalPosition = board_cells[0, 3].transform.position;
        globalPosition.z = -5f;
        go = Instantiate(Queen_Black, globalPosition, Quaternion.identity);
        board_cells[0, 3].GetComponent<CellScript>().OccupiedBy(go);
        go.GetComponent<Queen_MovementScript>().SetCoordinates(7, 3);

        // king
        globalPosition = board_cells[0, 4].transform.position;
        globalPosition.z = -5f;
        go = Instantiate(King_Black, globalPosition, Quaternion.identity);
        board_cells[0, 4].GetComponent<CellScript>().OccupiedBy(go);
        go.GetComponent<King_MovementScript>().SetCoordinates(7, 4);
    }

    public string GetTurn()
	{
        return this.turn;
	}

    public void SetSelectedPiece(GameObject go)
	{
        if (selectedPiece != null)
        {
            // checking if the go is the same with selected piece

            if (go == selectedPiece)
            {
                Debug.Log("deselecting.");
                if (selectedPiece.tag == "Pawn") selectedPiece.GetComponent<Pawn_MovementScript>().Deselect();
                else if (selectedPiece.tag == "Knight") selectedPiece.GetComponent<Knight_MovementScript>().Deselect();
                else if (selectedPiece.tag == "Bishop") selectedPiece.GetComponent<Bishop_MovementScript>().Deselect();
                else if (selectedPiece.tag == "Rook") selectedPiece.GetComponent<Rook_MovementScript>().Deselect();
                else if (selectedPiece.tag == "Queen") selectedPiece.GetComponent<Queen_MovementScript>().Deselect();
                selectedCell = null;
                selectedPiece = null;
                selectedPiece2 = null;
            }
            else
            {
                if (selectedPiece.tag == "Pawn") selectedPiece.GetComponent<Pawn_MovementScript>().Deselect();
                else if (selectedPiece.tag == "Knight") selectedPiece.GetComponent<Knight_MovementScript>().Deselect();
                else if (selectedPiece.tag == "Bishop") selectedPiece.GetComponent<Bishop_MovementScript>().Deselect();
                else if (selectedPiece.tag == "Rook") selectedPiece.GetComponent<Rook_MovementScript>().Deselect();
                else if (selectedPiece.tag == "Queen") selectedPiece.GetComponent<Queen_MovementScript>().Deselect();
                selectedPiece = go;
            }
        }
        else
		{
            Debug.Log("selected piece 1.");
            selectedPiece = go;
        }
    }

    public bool GetSelectedPiece()
	{
        return !(selectedPiece == null);
	}

    public void SetSelectedPiece2(GameObject go)
	{
        if (selectedPiece2 != null)
        {
            // checking if the go is the same with selected piece

            if (go == selectedPiece2)
            {
                Debug.Log("deselecting.");
                selectedPiece2.GetComponent<Pawn_MovementScript>().Deselect();
                selectedPiece2 = null;
            }
            else
            {
                Debug.Log("selected 2nd piece");
                selectedPiece2.GetComponent<Pawn_MovementScript>().Deselect();
                selectedPiece2 = go;
            }
        }
        else
        {
            Debug.Log("selected piece 2");
            selectedPiece2 = go;
            attacking = true;
            CheckValidMove();
        }
    }

    public void SetSelectedCell(GameObject go)
	{
        this.selectedCell = go.GetComponent<CellScript>();
        if (selectedPiece != null)
            CheckValidMove();
	}

    private bool IsCellOccupied(int[] a)
	{
        int x = a[0];
        int y = a[1];
        return board_cells[7 - x, y].IsOccupied();
    }

    private bool IsWithinBorders(int[] a)
	{
        int x = a[0];
        int y = a[1];
        if ((0 <= x && x < 8) && (0 <= y && y < 8)) return true;
        return false;
	}

    private bool IsAttacking(GameObject go1, GameObject go2)
	{
        if (go1.name.Contains("White") && go2.name.Contains("Black")) return true;
        if (go1.name.Contains("Black") && go2.name.Contains("White")) return true;
        return false;
    }

    private bool IsCellAttacked(int x, int y)
	{
        if (turn == "White")
		{
            // NEED TO CHECK FOR BLACK PIECES

            //---------------check if there are any pawns on the upper-left and right diagonal-------------------
            int[] upperLeftDiag = new int[2];
            upperLeftDiag[0] = x + 1;
            upperLeftDiag[1] = y - 1;
            if (IsWithinBorders(upperLeftDiag) && IsCellOccupied(upperLeftDiag))
                if (board_cells[7 - (x + 1), y - 1].IsPawn() && board_cells[7 - (x + 1), y - 1].IsPieceBlack())
                    return true;

            int[] upperRightDiag = new int[2];
			upperRightDiag[0] = x + 1;
			upperRightDiag[1] = y + 1;
			if (IsWithinBorders(upperLeftDiag) && IsCellOccupied(upperRightDiag))
                if (board_cells[7 - (x + 1), y + 1].IsPawn() && board_cells[7 - (x + 1), y + 1].IsPieceBlack())
                    return true;

            //---------------check if there are any rooks or queens on the row or column--------------------

            // checking for up direction
            for (int i = x + 1; i < 8; i++)
			{
				CellScript cell = board_cells[7 - i, y];
				if (cell.IsOccupied())
				{
                    if (cell.IsRook() && cell.IsPieceBlack()) return true;
                    else if (cell.IsQueen() && cell.IsPieceBlack()) return true;
                }
            }

            // checking for down direction
            for (int i = x - 1; i >= 0; i--)
            {
                CellScript cell = board_cells[7 - i, y];
                if (cell.IsOccupied())
				{
                    if (cell.IsRook() && cell.IsPieceBlack()) return true;
                    else if (cell.IsQueen() && cell.IsPieceBlack()) return true;
                }
            }

            // checking for left direction
            for (int j = y - 1; j >= 0; j--)
            {
                CellScript cell = board_cells[7 - x, j];
                if (cell.IsOccupied())
				{
                    if (cell.IsRook() && cell.IsPieceBlack()) return true;
                    else if (cell.IsQueen() && cell.IsPieceBlack()) return true;
                }
            }

            // checking for right direction
            for (int j = y + 1; j < 8; j++)
            {
                CellScript cell = board_cells[7 - x, j];
                if (cell.IsOccupied())
				{
                    if (cell.IsRook() && cell.IsPieceBlack()) return true;
                    else if (cell.IsQueen() && cell.IsPieceBlack()) return true;
                }
            }

            //---------------check if there are any bishops or queens on the row or column--------------------
            // direction up-right
            for (int i = x + 1, j = y + 1; i < 8 && j < 8; i++, j++)
			{
                CellScript cell = board_cells[7 - i, j];
                if (cell.IsOccupied())
                {
                    if (cell.IsBishop() && cell.IsPieceBlack()) return true;
                    else if (cell.IsQueen() && cell.IsPieceBlack()) return true;
                }
            }

            // direction lower-right
            for (int i = x - 1, j = y - 1; i >= 0 && j >= 0; i--, j--)
            {
                CellScript cell = board_cells[7 - i, j];
                if (cell.IsOccupied())
                {
                    if (cell.IsBishop() && cell.IsPieceBlack()) return true;
                    else if (cell.IsQueen() && cell.IsPieceBlack()) return true;
                }
            }

            // direction lower-left
            for (int i = x - 1, j = y - 1; i >= 0 && j >= 0; i--, j--)
            {
                CellScript cell = board_cells[7 - i, j];
                if (cell.IsOccupied())
                {
                    if (cell.IsBishop() && cell.IsPieceBlack()) return true;
                    else if (cell.IsQueen() && cell.IsPieceBlack()) return true;
                }
            }

            // direction upper-left
            for (int i = x + 1, j = y - 1; i < 8 && j >= 0; i++, j--)
            {
                CellScript cell = board_cells[7 - i, j];
                if (cell.IsOccupied())
                {
                    if (cell.IsBishop() && cell.IsPieceBlack()) return true;
                    else if (cell.IsQueen() && cell.IsPieceBlack()) return true;
                }
            }

            //---------------check if there are any knights-------------------------
            CellScript cell1;

            // L facing up
            int[] xCoordinates = new int[8];
            xCoordinates[0] = x + 2;
            xCoordinates[1] = x + 2;
            xCoordinates[2] = x + 1;
            xCoordinates[3] = x - 1;
            xCoordinates[4] = x - 2;
            xCoordinates[5] = x - 2;
            xCoordinates[6] = x + 1;
            xCoordinates[7] = x - 1;
            int[] yCoordinates = new int[8];
            yCoordinates[0] = y - 1;
            yCoordinates[1] = y + 1;
            yCoordinates[2] = y + 2;
            yCoordinates[3] = y + 2;
            yCoordinates[4] = y - 1;
            yCoordinates[5] = y - 1;
            yCoordinates[6] = y - 2;
            yCoordinates[7] = y - 2;

            for (int i = 0; i < 8; i++)
			{
                int[] a = new int[2];
                a[0] = xCoordinates[i];
                a[1] = yCoordinates[i];
                if (IsWithinBorders(a))
				{
                    cell1 = board_cells[7 - a[0], a[1]];
                    if (cell1.IsOccupied() && cell1.IsKnight() && cell1.IsPieceBlack()) return true;
                }
			}

            //-------------------checking if there is a king nearby------------------
        }
        return false;
	}

    /// <summary>
    /// Adds points to a specified player.
    /// 
    /// TO BE IMPLEMENTED.
    /// </summary>
    public void AddPoints()
	{

	}

    private void CheckValidMove()
	{
        // it is guaranteed to be called when a piece is actually selected.
        // I need to know what kind of piece it is. --> need to check the tag of the object
        if (selectedPiece != null || attacking)
		{
            Debug.Log("check if the piece can be moved");
            if (selectedPiece.tag == "Pawn") CheckMovePawn();
            else if (selectedPiece.tag == "Rook") CheckMoveRook();
            else if (selectedPiece.tag == "Bishop") CheckMoveBishop();
            else if (selectedPiece.tag == "Knight") CheckMoveKnight();
            else if (selectedPiece.tag == "Queen") CheckMoveQueen();
            else if (selectedPiece.tag == "King") CheckMoveKing();
        }
	}

    private void UnOccupyCell(int x, int y)
	{
        board_cells[7 - x, y].UnOccupy();
    }

    private void CheckMovePawn()
	{
        int Pawn_XCoord, Pawn_YCoord;
        bool direction, firstMove;
        int directionModifier;

        // I am guaranteed that the piece selected is a pawn
        // I have the selected cell, but not the coordinates
        // I need the coordinates of the pawn
        Pawn_XCoord = selectedPiece.GetComponent<Pawn_MovementScript>().GetXCoordinate();
        Pawn_YCoord = selectedPiece.GetComponent<Pawn_MovementScript>().GetYCoordinate();
        // I need the direction of the pawn
        direction = selectedPiece.GetComponent<Pawn_MovementScript>().GetDirection(); // false = Upwards, true = Downwards
        // I need to know if the pawn has ever been moved before
        firstMove = selectedPiece.GetComponent<Pawn_MovementScript>().GetFirstMove();
        // I need to find the coordinates of the cell that was clicked in the board_cells matrix.
        int Cell_xcoord = 0, Cell_ycoord = 0; // for the cell
        
        
        if (!attacking) // because otherwise, I'm not selecting a cell
		{
            Cell_xcoord = selectedCell.GetXCoordinate();
            Cell_ycoord = selectedCell.GetYCoordinate();
        }
        else // I need to get the coordinates of that piece
		{
            if (selectedPiece2.tag == "Pawn")
            {
                Cell_xcoord = selectedPiece2.GetComponent<Pawn_MovementScript>().GetXCoordinate();
                Cell_ycoord = selectedPiece2.GetComponent<Pawn_MovementScript>().GetYCoordinate();
            }
            else if (selectedPiece2.tag == "Rook")
            {
                Cell_xcoord = selectedPiece2.GetComponent<Rook_MovementScript>().GetXCoordinate();
                Cell_ycoord = selectedPiece2.GetComponent<Rook_MovementScript>().GetYCoordinate();
            }
            else if (selectedPiece2.tag == "Bishop")
            {
                Cell_xcoord = selectedPiece2.GetComponent<Bishop_MovementScript>().GetXCoordinate();
                Cell_ycoord = selectedPiece2.GetComponent<Bishop_MovementScript>().GetYCoordinate();
            }
            else if (selectedPiece2.tag == "Knight")
            {
                Cell_xcoord = selectedPiece2.GetComponent<Knight_MovementScript>().GetXCoordinate();
                Cell_ycoord = selectedPiece2.GetComponent<Knight_MovementScript>().GetYCoordinate();
            }
            else if (selectedPiece2.tag == "Queen")
            {
                Cell_xcoord = selectedPiece2.GetComponent<Queen_MovementScript>().GetXCoordinate();
                Cell_ycoord = selectedPiece2.GetComponent<Queen_MovementScript>().GetYCoordinate();
            }
        }

        // Then, I need to calculate a list of all the possible movements for that pawn. If the clicked cell
        // is within that list, that's totally correct.
        List<int[]> possibleCoordinates = new List<int[]>();

        if (direction) directionModifier = -1;
        else directionModifier = 1;

		{
            // Obviously, this will be the first thing to check.

            int[] forward_cell = new int[2];

            forward_cell[0] = Pawn_XCoord + 1 * directionModifier;
            forward_cell[1] = Pawn_YCoord;

            if (!IsCellOccupied(forward_cell))
                possibleCoordinates.Add(forward_cell);
        }

        if (firstMove) // that means that the pawn DIDN'T move at all
		{
            int[] double_forward_cell = new int[2];

            double_forward_cell[0] = Pawn_XCoord + 2 * directionModifier;
            double_forward_cell[1] = Pawn_YCoord;

            if (!IsCellOccupied(double_forward_cell))
                possibleCoordinates.Add(double_forward_cell);

        }

        if (attacking)
		{
			Debug.Log("I am attacking");
            // If I can attack on the left, it is a possible move.
            int[] left_diagonal = new int[2];

            left_diagonal[0] = Pawn_XCoord + 1 * directionModifier;
            left_diagonal[1] = Pawn_YCoord - 1;

            if (IsCellOccupied(left_diagonal))
			{
                Debug.Log("yes");
                possibleCoordinates.Add(left_diagonal);
            }


            // If I can attack on the right, that is a possible move as well.
            int[] right_diagonal = new int[2];
            right_diagonal[0] = Pawn_XCoord + 1 * directionModifier;
            right_diagonal[1] = Pawn_YCoord + 1;

            if (IsCellOccupied(right_diagonal))
			{
                Debug.Log("no");
                possibleCoordinates.Add(right_diagonal);
            }
		}

		// now, we need to check if the coordinates of the cell exist in the list
		bool canMove = false;

		int[] cellCoord = new int[2];
		cellCoord[0] = Cell_xcoord;
		cellCoord[1] = Cell_ycoord;

		for (int i = 0; i < possibleCoordinates.Count; i++)
		{
			Debug.Log(possibleCoordinates[i][0] + " " + possibleCoordinates[i][1]);
			if (possibleCoordinates[i][0] == cellCoord[0] && possibleCoordinates[i][1] == cellCoord[1]) canMove = true;
		}

		Debug.Log("Coordinates of the pawn: " + Pawn_XCoord + "," + Pawn_YCoord + ". Coordinates of the cell: " + cellCoord[0] + "," + cellCoord[1] + ". Can Move: " + canMove);

		if (canMove)
		{
            UnOccupyCell(Pawn_XCoord, Pawn_YCoord);
			selectedPiece.GetComponent<Pawn_MovementScript>().MoveToCell(cellCoord, board_cells[7 - cellCoord[0], cellCoord[1]]);
			selectedCell = null;
			selectedPiece = null;
            selectedPiece2 = null;
            attacking = false;

            // changing turns
            if (turn == "White")
			{
                turn = "Black";
                Debug.Log("Black's turn.");
            }
            else
			{
                turn = "White";
                Debug.Log("White's turn.");
			}
		}
        else
		{
            selectedCell = null;
            selectedPiece.GetComponent<Pawn_MovementScript>().Deselect();
            selectedPiece = null;
		}
	}

    private void CheckMoveRook()
	{
        int Rook_XCoord = 0, Rook_YCoord = 0;

        Rook_XCoord = selectedPiece.GetComponent<Rook_MovementScript>().GetXCoordinate();
        Rook_YCoord = selectedPiece.GetComponent<Rook_MovementScript>().GetYCoordinate();
        // the rook can move horizontally and vertically. Don't need direction, nor firstMove.

        int Cell_xcoord = 0, Cell_ycoord = 0; // for the cell


        if (!attacking) // because otherwise, I'm not selecting a cell
        {
            Cell_xcoord = selectedCell.GetXCoordinate();
            Cell_ycoord = selectedCell.GetYCoordinate();
        }
        else // I need to get the coordinates of that piece
        {
            if (selectedPiece2.tag == "Pawn")
			{
                Cell_xcoord = selectedPiece2.GetComponent<Pawn_MovementScript>().GetXCoordinate();
                Cell_ycoord = selectedPiece2.GetComponent<Pawn_MovementScript>().GetYCoordinate();
            }
            else if (selectedPiece2.tag == "Rook")
			{
                Cell_xcoord = selectedPiece2.GetComponent<Rook_MovementScript>().GetXCoordinate();
                Cell_ycoord = selectedPiece2.GetComponent<Rook_MovementScript>().GetYCoordinate();
            }
            else if (selectedPiece2.tag == "Bishop")
            {
                Cell_xcoord = selectedPiece2.GetComponent<Bishop_MovementScript>().GetXCoordinate();
                Cell_ycoord = selectedPiece2.GetComponent<Bishop_MovementScript>().GetYCoordinate();
            }
            else if (selectedPiece2.tag == "Knight")
            {
                Cell_xcoord = selectedPiece2.GetComponent<Knight_MovementScript>().GetXCoordinate();
                Cell_ycoord = selectedPiece2.GetComponent<Knight_MovementScript>().GetYCoordinate();
            }
            else if (selectedPiece2.tag == "Queen")
            {
                Cell_xcoord = selectedPiece2.GetComponent<Queen_MovementScript>().GetXCoordinate();
                Cell_ycoord = selectedPiece2.GetComponent<Queen_MovementScript>().GetYCoordinate();
            }
        }

        List<int[]> possibleCoordinates = new List<int[]>();

        bool obstacle = false; // because the piece may meet another piece in its trajectory.
        // checking cells upwards
        
        for (int i = Rook_XCoord + 1; i < 8 && !obstacle; i++)
		{
            int[] coords = new int[2];
            coords[0] = i;
            coords[1] = Rook_YCoord;

            if (!IsCellOccupied(coords)) possibleCoordinates.Add(coords);
            else
			{
                possibleCoordinates.Add(coords); // because I can attack it, but I cannot move past it
                obstacle = true;
            }
		}

		// checking cells downwards
		obstacle = false;
		for (int i = Rook_XCoord - 1; i >= 0 && !obstacle; i--)
		{
			int[] coords = new int[2];
			coords[0] = i;
			coords[1] = Rook_YCoord;

			if (!IsCellOccupied(coords)) possibleCoordinates.Add(coords);
			else
			{
				possibleCoordinates.Add(coords); // because I can attack it, but I cannot move past it
				obstacle = true;
			}
		}

		// checking cells towards the right side
		obstacle = false;
		for (int j = Rook_YCoord + 1; j < 8 && !obstacle; j++)
		{
			int[] coords = new int[2];
			coords[0] = Rook_XCoord;
			coords[1] = j;

			if (!IsCellOccupied(coords)) possibleCoordinates.Add(coords);
			else
			{
				possibleCoordinates.Add(coords); // because I can attack it, but I cannot move past it
				obstacle = true;
			}
		}

		// checking cells towards the left side
		obstacle = false;
		for (int j = Rook_YCoord - 1; j >= 0 && !obstacle; j--)
		{
			int[] coords = new int[2];
			coords[0] = Rook_XCoord;
			coords[1] = j;

			if (!IsCellOccupied(coords)) possibleCoordinates.Add(coords);
			else
			{
				possibleCoordinates.Add(coords); // because I can attack it, but I cannot move past it
				obstacle = true;
			}
		}

		//------------------Calculated the cells I can move to--------------------
		bool canMove = false;

        int[] cellCoord = new int[2];
        cellCoord[0] = Cell_xcoord;
        cellCoord[1] = Cell_ycoord;

        for (int i = 0; i < possibleCoordinates.Count; i++)
        {
            Debug.Log(possibleCoordinates[i][0] + " " + possibleCoordinates[i][1]);
            if (possibleCoordinates[i][0] == cellCoord[0] && possibleCoordinates[i][1] == cellCoord[1]) canMove = true;
        }

        Debug.Log("Coordinates of the rook: " + Rook_XCoord + "," + Rook_YCoord + ". Coordinates of the cell: " + cellCoord[0] + "," + cellCoord[1] + ". Can Move: " + canMove);

        if (canMove)
        {
            UnOccupyCell(Rook_XCoord, Rook_YCoord);
            selectedPiece.GetComponent<Rook_MovementScript>().MoveToCell(cellCoord, board_cells[7 - cellCoord[0], cellCoord[1]]);
            selectedCell = null;
            selectedPiece = null;
            selectedPiece2 = null;
            attacking = false;

            // changing turns
            if (turn == "White")
            {
                turn = "Black";
                Debug.Log("Black's turn.");
            }
            else
            {
                turn = "White";
                Debug.Log("White's turn.");
            }
        }
        else
        {
            selectedCell = null;
            selectedPiece.GetComponent<Rook_MovementScript>().Deselect();
            selectedPiece = null;
        }
    }

    private void CheckMoveBishop()
	{
        int Bishop_XCoord = 0, Bishop_YCoord = 0;

        Bishop_XCoord = selectedPiece.GetComponent<Bishop_MovementScript>().GetXCoordinate();
        Bishop_YCoord = selectedPiece.GetComponent<Bishop_MovementScript>().GetYCoordinate();
        // the rook can move horizontally and vertically. Don't need direction, nor firstMove.

        int Cell_xcoord = 0, Cell_ycoord = 0; // for the cell


        if (!attacking) // because otherwise, I'm not selecting a cell
        {
            Cell_xcoord = selectedCell.GetXCoordinate();
            Cell_ycoord = selectedCell.GetYCoordinate();
        }
        else // I need to get the coordinates of that piece
        {
            if (selectedPiece2.tag == "Pawn")
            {
                Cell_xcoord = selectedPiece2.GetComponent<Pawn_MovementScript>().GetXCoordinate();
                Cell_ycoord = selectedPiece2.GetComponent<Pawn_MovementScript>().GetYCoordinate();
            }
            else if (selectedPiece2.tag == "Rook")
            {
                Cell_xcoord = selectedPiece2.GetComponent<Rook_MovementScript>().GetXCoordinate();
                Cell_ycoord = selectedPiece2.GetComponent<Rook_MovementScript>().GetYCoordinate();
            }
            else if (selectedPiece2.tag == "Bishop")
			{
                Cell_xcoord = selectedPiece2.GetComponent<Bishop_MovementScript>().GetXCoordinate();
                Cell_ycoord = selectedPiece2.GetComponent<Bishop_MovementScript>().GetYCoordinate();
            }
            else if (selectedPiece2.tag == "Knight")
            {
                Cell_xcoord = selectedPiece2.GetComponent<Knight_MovementScript>().GetXCoordinate();
                Cell_ycoord = selectedPiece2.GetComponent<Knight_MovementScript>().GetYCoordinate();
            }
            else if (selectedPiece2.tag == "Queen")
            {
                Cell_xcoord = selectedPiece2.GetComponent<Queen_MovementScript>().GetXCoordinate();
                Cell_ycoord = selectedPiece2.GetComponent<Queen_MovementScript>().GetYCoordinate();
            }
        }

        List<int[]> possibleCoordinates = new List<int[]>();

        bool obstacle = false; // because the piece may meet another piece in its trajectory.
                               // checking cells upwards

        // the bishop moves on the diagonals
        // Upper-Right diagonal
        for (int i = Bishop_XCoord + 1, j = Bishop_YCoord + 1; i < 8 && j < 8; i++, j++)
			    if (!obstacle)
                    {
                        int[] coords = new int[2];
                        coords[0] = i;
                        coords[1] = j;

                        if (!IsCellOccupied(coords)) possibleCoordinates.Add(coords);
                        else
                        {
                            possibleCoordinates.Add(coords);
                            obstacle = true;
                        }
                    }

        // Lower-Right diagonal
        obstacle = false;
        for (int i = Bishop_XCoord - 1, j = Bishop_YCoord + 1; i >= 0 && j < 8; i--, j++)
                if (!obstacle)
                {
                    int[] coords = new int[2];
                    coords[0] = i;
                    coords[1] = j;

                    if (!IsCellOccupied(coords)) possibleCoordinates.Add(coords);
                    else
                    {
                        possibleCoordinates.Add(coords);
                        obstacle = true;
                    }
                }

        // Lower-Left diagonal
        obstacle = false;
        for (int i = Bishop_XCoord - 1, j = Bishop_YCoord - 1; i >= 0 && j >= 0; i--, j--)
                if (!obstacle)
                {
                    int[] coords = new int[2];
                    coords[0] = i;
                    coords[1] = j;

                    if (!IsCellOccupied(coords)) possibleCoordinates.Add(coords);
                    else
                    {
                        possibleCoordinates.Add(coords);
                        obstacle = true;
                    }
                }

        // Upper-Left diagonal
        obstacle = false;
        for (int i = Bishop_XCoord + 1, j = Bishop_YCoord - 1; i < 8 && j >= 0; i++, j--)
                if (!obstacle)
                {
                    int[] coords = new int[2];
                    coords[0] = i;
                    coords[1] = j;

                    if (!IsCellOccupied(coords)) possibleCoordinates.Add(coords);
                    else
                    {
                        possibleCoordinates.Add(coords);
                        obstacle = true;
                    }
                }

        //------------------Calculated the cells I can move to--------------------
        bool canMove = false;

        int[] cellCoord = new int[2];
        cellCoord[0] = Cell_xcoord;
        cellCoord[1] = Cell_ycoord;

        for (int i = 0; i < possibleCoordinates.Count; i++)
        {
            Debug.Log(possibleCoordinates[i][0] + " " + possibleCoordinates[i][1]);
            if (possibleCoordinates[i][0] == cellCoord[0] && possibleCoordinates[i][1] == cellCoord[1]) canMove = true;
        }

        Debug.Log("Coordinates of the bishop: " + Bishop_XCoord + "," + Bishop_YCoord + ". Coordinates of the cell: " + cellCoord[0] + "," + cellCoord[1] + ". Can Move: " + canMove);

        if (canMove)
        {
            UnOccupyCell(Bishop_XCoord, Bishop_YCoord);
            selectedPiece.GetComponent<Bishop_MovementScript>().MoveToCell(cellCoord, board_cells[7 - cellCoord[0], cellCoord[1]]);
            selectedCell = null;
            selectedPiece = null;
            selectedPiece2 = null;
            attacking = false;

            // changing turns
            if (turn == "White")
            {
                turn = "Black";
                Debug.Log("Black's turn.");
            }
            else
            {
                turn = "White";
                Debug.Log("White's turn.");
            }
        }
        else
        {
            selectedCell = null;
            selectedPiece.GetComponent<Bishop_MovementScript>().Deselect();
            selectedPiece = null;
        }
    }

    private void CheckMoveKnight()
	{
        int Knight_XCoord = 0, Knight_YCoord = 0;

        Knight_XCoord = selectedPiece.GetComponent<Knight_MovementScript>().GetXCoordinate();
        Knight_YCoord = selectedPiece.GetComponent<Knight_MovementScript>().GetYCoordinate();
        // the rook can move horizontally and vertically. Don't need direction, nor firstMove.

        int Cell_xcoord = 0, Cell_ycoord = 0; // for the cell


        if (!attacking) // because otherwise, I'm not selecting a cell
        {
            Cell_xcoord = selectedCell.GetXCoordinate();
            Cell_ycoord = selectedCell.GetYCoordinate();
        }
        else // I need to get the coordinates of that piece
        {
            if (selectedPiece2.tag == "Pawn")
            {
                Cell_xcoord = selectedPiece2.GetComponent<Pawn_MovementScript>().GetXCoordinate();
                Cell_ycoord = selectedPiece2.GetComponent<Pawn_MovementScript>().GetYCoordinate();
            }
            else if (selectedPiece2.tag == "Rook")
            {
                Cell_xcoord = selectedPiece2.GetComponent<Rook_MovementScript>().GetXCoordinate();
                Cell_ycoord = selectedPiece2.GetComponent<Rook_MovementScript>().GetYCoordinate();
            }
            else if (selectedPiece2.tag == "Bishop")
            {
                Cell_xcoord = selectedPiece2.GetComponent<Bishop_MovementScript>().GetXCoordinate();
                Cell_ycoord = selectedPiece2.GetComponent<Bishop_MovementScript>().GetYCoordinate();
            }
            else if (selectedPiece2.tag == "Knight")
            {
                Cell_xcoord = selectedPiece2.GetComponent<Knight_MovementScript>().GetXCoordinate();
                Cell_ycoord = selectedPiece2.GetComponent<Knight_MovementScript>().GetYCoordinate();
            }
            else if (selectedPiece2.tag == "Queen")
            {
                Cell_xcoord = selectedPiece2.GetComponent<Queen_MovementScript>().GetXCoordinate();
                Cell_ycoord = selectedPiece2.GetComponent<Queen_MovementScript>().GetYCoordinate();
            }
        }

        List<int[]> possibleCoordinates = new List<int[]>();

        // the knight moves in L-shape
        int[] left = new int[2];
        int[] right = new int[2];

        // L facing up
        left[0] = Knight_XCoord + 2;
        left[1] = Knight_YCoord - 1;
        right[0] = Knight_XCoord + 2;
        right[1] = Knight_YCoord + 1;

        if (IsWithinBorders(left)) possibleCoordinates.Add(left);
        if (IsWithinBorders(right)) possibleCoordinates.Add(right);

        // L facing right
        int[] left1 = new int[2];
        int[] right1 = new int[2];
		left1[0] = Knight_XCoord + 1;
		left1[1] = Knight_YCoord + 2;
		right1[0] = Knight_XCoord - 1;
		right1[1] = Knight_YCoord + 2;

		if (IsWithinBorders(left1)) possibleCoordinates.Add(left1);
		if (IsWithinBorders(right1)) possibleCoordinates.Add(right1);

        // L facing down
        int[] left2 = new int[2];
        int[] right2 = new int[2];
		left2[0] = Knight_XCoord - 2;
		left2[1] = Knight_YCoord - 1;
		right2[0] = Knight_XCoord - 2;
		right2[1] = Knight_YCoord + 1;

		if (IsWithinBorders(left2)) possibleCoordinates.Add(left2);
		if (IsWithinBorders(right2)) possibleCoordinates.Add(right2);

        // L facing left
        int[] left3 = new int[2];
        int[] right3 = new int[2];
		left3[0] = Knight_XCoord + 1;
		left3[1] = Knight_YCoord - 2;
		right3[0] = Knight_XCoord - 1;
		right3[1] = Knight_YCoord - 2;

		if (IsWithinBorders(left3)) possibleCoordinates.Add(left3);
		if (IsWithinBorders(right3)) possibleCoordinates.Add(right3);

		//------------------Calculated the cells I can move to--------------------
		bool canMove = false;

        int[] cellCoord = new int[2];
        cellCoord[0] = Cell_xcoord;
        cellCoord[1] = Cell_ycoord;

        for (int i = 0; i < possibleCoordinates.Count; i++)
        {
            Debug.Log(possibleCoordinates[i][0] + " " + possibleCoordinates[i][1]);
            if (possibleCoordinates[i][0] == cellCoord[0] && possibleCoordinates[i][1] == cellCoord[1]) canMove = true;
        }

        Debug.Log("Coordinates of the Knight: " + Knight_XCoord + "," + Knight_YCoord + ". Coordinates of the cell: " + cellCoord[0] + "," + cellCoord[1] + ". Can Move: " + canMove);

        if (canMove)
        {
            UnOccupyCell(Knight_XCoord, Knight_YCoord);
            selectedPiece.GetComponent<Knight_MovementScript>().MoveToCell(cellCoord, board_cells[7 - cellCoord[0], cellCoord[1]]);
            selectedCell = null;
            selectedPiece = null;
            selectedPiece2 = null;
            attacking = false;

            // changing turns
            if (turn == "White")
            {
                turn = "Black";
                Debug.Log("Black's turn.");
            }
            else
            {
                turn = "White";
                Debug.Log("White's turn.");
            }
        }
        else
        {
            selectedCell = null;
            selectedPiece.GetComponent<Knight_MovementScript>().Deselect();
            selectedPiece = null;
        }
    }

    private void CheckMoveQueen()
	{
        int Queen_XCoord = 0, Queen_YCoord = 0;

        Queen_XCoord = selectedPiece.GetComponent<Queen_MovementScript>().GetXCoordinate();
        Queen_YCoord = selectedPiece.GetComponent<Queen_MovementScript>().GetYCoordinate();
        // the rook can move horizontally and vertically. Don't need direction, nor firstMove.

        int Cell_xcoord = 0, Cell_ycoord = 0; // for the cell


        if (!attacking) // because otherwise, I'm not selecting a cell
        {
            Cell_xcoord = selectedCell.GetXCoordinate();
            Cell_ycoord = selectedCell.GetYCoordinate();
        }
        else // I need to get the coordinates of that piece
        {
            if (selectedPiece2.tag == "Pawn")
            {
                Cell_xcoord = selectedPiece2.GetComponent<Pawn_MovementScript>().GetXCoordinate();
                Cell_ycoord = selectedPiece2.GetComponent<Pawn_MovementScript>().GetYCoordinate();
            }
            else if (selectedPiece2.tag == "Rook")
            {
                Cell_xcoord = selectedPiece2.GetComponent<Rook_MovementScript>().GetXCoordinate();
                Cell_ycoord = selectedPiece2.GetComponent<Rook_MovementScript>().GetYCoordinate();
            }
            else if (selectedPiece2.tag == "Bishop")
            {
                Cell_xcoord = selectedPiece2.GetComponent<Bishop_MovementScript>().GetXCoordinate();
                Cell_ycoord = selectedPiece2.GetComponent<Bishop_MovementScript>().GetYCoordinate();
            }
            else if (selectedPiece2.tag == "Knight")
            {
                Cell_xcoord = selectedPiece2.GetComponent<Knight_MovementScript>().GetXCoordinate();
                Cell_ycoord = selectedPiece2.GetComponent<Knight_MovementScript>().GetYCoordinate();
            }
            else if (selectedPiece2.tag == "Queen")
            {
                Cell_xcoord = selectedPiece2.GetComponent<Queen_MovementScript>().GetXCoordinate();
                Cell_ycoord = selectedPiece2.GetComponent<Queen_MovementScript>().GetYCoordinate();
            }
        }

        List<int[]> possibleCoordinates = new List<int[]>();

        // the queen moves in + and on diagonals.
        bool obstacle = false;

        // checking cells upwards
        for (int i = Queen_XCoord + 1; i < 8 && !obstacle; i++)
        {
            int[] coords = new int[2];
            coords[0] = i;
            coords[1] = Queen_YCoord;

            if (!IsCellOccupied(coords)) possibleCoordinates.Add(coords);
            else
            {
                possibleCoordinates.Add(coords); // because I can attack it, but I cannot move past it
                obstacle = true;
            }
        }

		// checking cells downwards
		obstacle = false;
		for (int i = Queen_XCoord - 1; i >= 0 && !obstacle; i--)
		{
			int[] coords = new int[2];
			coords[0] = i;
			coords[1] = Queen_YCoord;

			if (!IsCellOccupied(coords)) possibleCoordinates.Add(coords);
			else
			{
				possibleCoordinates.Add(coords); // because I can attack it, but I cannot move past it
				obstacle = true;
			}
		}

		// checking cells towards the right side
		obstacle = false;
		for (int j = Queen_YCoord + 1; j < 8 && !obstacle; j++)
		{
			int[] coords = new int[2];
			coords[0] = Queen_XCoord;
			coords[1] = j;

			if (!IsCellOccupied(coords)) possibleCoordinates.Add(coords);
			else
			{
				possibleCoordinates.Add(coords); // because I can attack it, but I cannot move past it
				obstacle = true;
			}
		}

		// checking cells towards the left side
		obstacle = false;
		for (int j = Queen_YCoord - 1; j >= 0 && !obstacle; j--)
		{
			int[] coords = new int[2];
			coords[0] = Queen_XCoord;
			coords[1] = j;

			if (!IsCellOccupied(coords)) possibleCoordinates.Add(coords);
			else
			{
				possibleCoordinates.Add(coords); // because I can attack it, but I cannot move past it
				obstacle = true;
			}
		}

        // Upper-Right diagonal
        obstacle = false;
		for (int i = Queen_XCoord + 1, j = Queen_YCoord + 1; i < 8 && j < 8; i++, j++)
			if (!obstacle)
			{
				int[] coords = new int[2];
				coords[0] = i;
				coords[1] = j;

				if (!IsCellOccupied(coords)) possibleCoordinates.Add(coords);
				else
				{
					possibleCoordinates.Add(coords);
					obstacle = true;
				}
			}

		// Lower-Right diagonal
		obstacle = false;
		for (int i = Queen_XCoord - 1, j = Queen_YCoord + 1; i >= 0 && j < 8; i--, j++)
			if (!obstacle)
			{
				int[] coords = new int[2];
				coords[0] = i;
				coords[1] = j;

				if (!IsCellOccupied(coords)) possibleCoordinates.Add(coords);
				else
				{
					possibleCoordinates.Add(coords);
					obstacle = true;
				}
			}

		// Lower-Left diagonal
		obstacle = false;
		for (int i = Queen_XCoord - 1, j = Queen_YCoord - 1; i >= 0 && j >= 0; i--, j--)
			if (!obstacle)
			{
				int[] coords = new int[2];
				coords[0] = i;
				coords[1] = j;

				if (!IsCellOccupied(coords)) possibleCoordinates.Add(coords);
				else
				{
					possibleCoordinates.Add(coords);
					obstacle = true;
				}
			}

		// Upper-Left diagonal
		obstacle = false;
		for (int i = Queen_XCoord + 1, j = Queen_YCoord - 1; i < 8 && j >= 0; i++, j--)
			if (!obstacle)
			{
				int[] coords = new int[2];
				coords[0] = i;
				coords[1] = j;

				if (!IsCellOccupied(coords)) possibleCoordinates.Add(coords);
				else
				{
					possibleCoordinates.Add(coords);
					obstacle = true;
				}
			}

		//------------------Calculated the cells I can move to--------------------
		bool canMove = false;

        int[] cellCoord = new int[2];
        cellCoord[0] = Cell_xcoord;
        cellCoord[1] = Cell_ycoord;

        for (int i = 0; i < possibleCoordinates.Count; i++)
        {
            Debug.Log(possibleCoordinates[i][0] + " " + possibleCoordinates[i][1]);
            if (possibleCoordinates[i][0] == cellCoord[0] && possibleCoordinates[i][1] == cellCoord[1]) canMove = true;
        }

        Debug.Log("Coordinates of the Queen: " + Queen_XCoord + "," + Queen_YCoord + ". Coordinates of the cell: " + cellCoord[0] + "," + cellCoord[1] + ". Can Move: " + canMove);

        if (canMove)
        {
            UnOccupyCell(Queen_XCoord, Queen_YCoord);
            selectedPiece.GetComponent<Queen_MovementScript>().MoveToCell(cellCoord, board_cells[7 - cellCoord[0], cellCoord[1]]);
            selectedCell = null;
            selectedPiece = null;
            selectedPiece2 = null;
            attacking = false;

            // changing turns
            if (turn == "White")
            {
                turn = "Black";
                Debug.Log("Black's turn.");
            }
            else
            {
                turn = "White";
                Debug.Log("White's turn.");
            }
        }
        else
        {
            selectedCell = null;
            selectedPiece.GetComponent<Queen_MovementScript>().Deselect();
            selectedPiece = null;
        }
    }
    
    private void CheckMoveKing()
	{
        //Debug.Log();

        int King_XCoord, King_YCoord;

        King_XCoord = selectedPiece.GetComponent<King_MovementScript>().GetXCoordinate();
        King_YCoord = selectedPiece.GetComponent<King_MovementScript>().GetYCoordinate();

        if (IsCellAttacked(King_XCoord, King_YCoord))
		{
            Debug.Log("King is being attacked!");
		}
	}
}