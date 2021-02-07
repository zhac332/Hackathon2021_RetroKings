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
        //ShowBlackPerspective();
    }

    private void InitBoard()
	{
        for (int i = 0; i < 8; i++)
            for (int j = 0; j < 8; j++)
			{
                board_cells[i, j] = GameObject.Find("Cell_" + (7 - i) + j).GetComponent<CellScript>();
                board_cells[i, j].SetCoordinates(7 - i, j);
			}

        //for (int i = 0; i < row1.Count; i++)
        //{
        //    board_cells[7, i] = row1[i].GetComponent<CellScript>();
        //    board_cells[7, i].SetCoordinates(i, 7);
        //}
        //for (int i = 0; i < row2.Count; i++)
        //{
        //    board_cells[6, i] = row2[i].GetComponent<CellScript>();
        //    board_cells[6, i].SetCoordinates(i, 6);
        //}
        //for (int i = 0; i < row3.Count; i++)
        //{
        //    board_cells[5, i] = row3[i].GetComponent<CellScript>();
        //    board_cells[5, i].SetCoordinates(i, 5);
        //}
        //for (int i = 0; i < row4.Count; i++)
        //{
        //    board_cells[4, i] = row4[i].GetComponent<CellScript>();
        //    board_cells[4, i].SetCoordinates(i, 4);
        //}
        //for (int i = 0; i < row5.Count; i++)
        //{
        //    board_cells[3, i] = row5[i].GetComponent<CellScript>();
        //    board_cells[3, i].SetCoordinates(i, 3);
        //}
        //for (int i = 0; i < row6.Count; i++)
        //{
        //    board_cells[2, i] = row6[i].GetComponent<CellScript>();
        //    board_cells[2, i].SetCoordinates(i, 2);
        //}
        //for (int i = 0; i < row7.Count; i++)
        //{
        //    board_cells[1, i] = row7[i].GetComponent<CellScript>();
        //    board_cells[1, i].SetCoordinates(i, 1);
        //}
        //for (int i = 0; i < row8.Count; i++)
        //{
        //    board_cells[0, i] = row8[i].GetComponent<CellScript>();
        //    board_cells[0, i].SetCoordinates(i, 0);
        //}
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

            // instantiating the object at that position
            go = Instantiate(Pawn_White, globalPosition, Quaternion.identity);
            go.GetComponent<Pawn_MovementScript>().SetCoordinates(1, i, false);

            // occupying the cell
            board_cells[6, i].GetComponent<CellScript>().OccupiedBy(go);
		}

        // first rook 
        globalPosition = board_cells[7, 0].transform.position;
        go = Instantiate(Rook_White, globalPosition, Quaternion.identity);
        board_cells[7, 0].GetComponent<CellScript>().OccupiedBy(go);

        // second rook
        globalPosition = board_cells[7, 7].transform.position;
        go = Instantiate(Rook_White, globalPosition, Quaternion.identity);
        board_cells[7, 7].GetComponent<CellScript>().OccupiedBy(go);

        // first knight
        globalPosition = board_cells[7, 1].transform.position;
        go = Instantiate(Knight_White, globalPosition, Quaternion.identity);
        board_cells[7, 1].GetComponent<CellScript>().OccupiedBy(go);

        // second knight
        globalPosition = board_cells[7, 6].transform.position;
        go = Instantiate(Knight_White, globalPosition, Quaternion.identity);
        board_cells[7, 6].GetComponent<CellScript>().OccupiedBy(go);

        // first bishop
        globalPosition = board_cells[7, 2].transform.position;
        go = Instantiate(Bishop_White, globalPosition, Quaternion.identity);
        board_cells[7, 2].GetComponent<CellScript>().OccupiedBy(go);

        // second bishop
        globalPosition = board_cells[7, 5].transform.position;
        go = Instantiate(Bishop_White, globalPosition, Quaternion.identity);
        board_cells[7, 5].GetComponent<CellScript>().OccupiedBy(go);

        // queen
        globalPosition = board_cells[7, 3].transform.position;
        go = Instantiate(Queen_White, globalPosition, Quaternion.identity);
        board_cells[7, 3].GetComponent<CellScript>().OccupiedBy(go);

        // king
        globalPosition = board_cells[7, 4].transform.position;
        go = Instantiate(King_White, globalPosition, Quaternion.identity);
        board_cells[7, 4].GetComponent<CellScript>().OccupiedBy(go);

        /// -----------------------------------------BLACK PIECES-----------------------------------------

        // setting up the black pawns
        for (int i = 0; i < 8; i++)
        {
            globalPosition = board_cells[1, i].transform.position;
            globalPosition.y += 0.1f;
            go = Instantiate(Pawn_Black, globalPosition, Quaternion.identity);
            go.GetComponent<Pawn_MovementScript>().SetCoordinates(6, i, true);
            board_cells[1, i].GetComponent<CellScript>().OccupiedBy(go);
        }

        // first rook
        globalPosition = board_cells[0, 0].transform.position;
        go = Instantiate(Rook_Black, globalPosition, Quaternion.identity);
        board_cells[0, 0].GetComponent<CellScript>().OccupiedBy(go);

        // second rook
        globalPosition = board_cells[0, 7].transform.position;
        go = Instantiate(Rook_Black, globalPosition, Quaternion.identity);
        board_cells[0, 7].GetComponent<CellScript>().OccupiedBy(go);

        // first knight
        globalPosition = board_cells[0, 1].transform.position;
        go = Instantiate(Knight_Black, globalPosition, Quaternion.identity);
        board_cells[0, 1].GetComponent<CellScript>().OccupiedBy(go);

        // second knight
        globalPosition = board_cells[0, 6].transform.position;
        go = Instantiate(Knight_Black, globalPosition, Quaternion.identity);
        board_cells[0, 6].GetComponent<CellScript>().OccupiedBy(go);

        // first bishop
        globalPosition = board_cells[0, 2].transform.position;
        go = Instantiate(Bishop_Black, globalPosition, Quaternion.identity);
        board_cells[0, 2].GetComponent<CellScript>().OccupiedBy(go);

        // second bishop
        globalPosition = board_cells[0, 5].transform.position;
        go = Instantiate(Bishop_Black, globalPosition, Quaternion.identity);
        board_cells[0, 5].GetComponent<CellScript>().OccupiedBy(go);

        // queen
        globalPosition = board_cells[0, 3].transform.position;
        go = Instantiate(Queen_Black, globalPosition, Quaternion.identity);
        board_cells[0, 3].GetComponent<CellScript>().OccupiedBy(go);

        // king
        globalPosition = board_cells[0, 4].transform.position;
        go = Instantiate(King_Black, globalPosition, Quaternion.identity);
        board_cells[0, 4].GetComponent<CellScript>().OccupiedBy(go);
    }

    private void ShowBlackPerspective()
	{
        /// -----------------------------------------WHITE PIECES-----------------------------------------

        Vector3 globalPosition = new Vector3();
        GameObject go = new GameObject();

        // setting up the white pawns
        for (int i = 0; i < 8; i++)
        {
            globalPosition = board_cells[1, i].transform.position;
            globalPosition.y += 0.1f;
            go = Instantiate(Pawn_White, globalPosition, Quaternion.identity);
            go.GetComponent<Pawn_MovementScript>().SetCoordinates(6, i, true);
            board_cells[1, i].GetComponent<CellScript>().OccupiedBy(go);
        }

        // First Rook.
        globalPosition = board_cells[0, 0].transform.position;
        go = Instantiate(Rook_White, globalPosition, Quaternion.identity);
        board_cells[0, 0].GetComponent<CellScript>().OccupiedBy(go);

        // Second Rook.
        globalPosition = board_cells[0, 7].transform.position;
        go = Instantiate(Rook_White, globalPosition, Quaternion.identity);
        board_cells[0, 7].GetComponent<CellScript>().OccupiedBy(go);

        // First Knight.
        globalPosition = board_cells[0, 1].transform.position;
        go = Instantiate(Knight_White, globalPosition, Quaternion.identity);
        board_cells[0, 1].GetComponent<CellScript>().OccupiedBy(go);

        // Second Knight.
        globalPosition = board_cells[0, 6].transform.position;
        go = Instantiate(Knight_White, globalPosition, Quaternion.identity);
        board_cells[0, 6].GetComponent<CellScript>().OccupiedBy(go);

        // First Bishop.
        globalPosition = board_cells[0, 2].transform.position;
        go = Instantiate(Bishop_White, globalPosition, Quaternion.identity);
        board_cells[0, 2].GetComponent<CellScript>().OccupiedBy(go);

        // Second Bishop.
        globalPosition = board_cells[0, 5].transform.position;
        go = Instantiate(Bishop_White, globalPosition, Quaternion.identity);
        board_cells[0, 5].GetComponent<CellScript>().OccupiedBy(go);

        // Queen.
        globalPosition = board_cells[0, 3].transform.position;
        go = Instantiate(Queen_White, globalPosition, Quaternion.identity);
        board_cells[0, 3].GetComponent<CellScript>().OccupiedBy(go);

        // King.
        globalPosition = board_cells[0, 4].transform.position;
        go = Instantiate(King_White, globalPosition, Quaternion.identity);
        board_cells[0, 4].GetComponent<CellScript>().OccupiedBy(go);


        /// -----------------------------------------BLACK PIECES-----------------------------------------

        // setting up the white pawns
        for (int i = 0; i < 8; i++)
        {
            globalPosition = board_cells[6, i].transform.position;
            globalPosition.y += 0.1f;
            go = Instantiate(Pawn_Black, globalPosition, Quaternion.identity);
            go.GetComponent<Pawn_MovementScript>().SetCoordinates(1, i, false);
            board_cells[6, 0].GetComponent<CellScript>().OccupiedBy(go);
        }

        // First Rook.
		globalPosition = board_cells[7, 0].transform.position;
		go = Instantiate(Rook_Black, globalPosition, Quaternion.identity);
        board_cells[7, 0].GetComponent<CellScript>().OccupiedBy(go);

        // Second Rook.
		globalPosition = board_cells[7, 7].transform.position;
		go = Instantiate(Rook_Black, globalPosition, Quaternion.identity);
        board_cells[7, 7].GetComponent<CellScript>().OccupiedBy(go);

        // First Knight.
		globalPosition = board_cells[7, 1].transform.position;
		go = Instantiate(Knight_Black, globalPosition, Quaternion.identity);
        board_cells[7, 1].GetComponent<CellScript>().OccupiedBy(go);

        // Second Knight.
		globalPosition = board_cells[7, 6].transform.position;
		go = Instantiate(Knight_Black, globalPosition, Quaternion.identity);
        board_cells[7, 6].GetComponent<CellScript>().OccupiedBy(go);

        // First Bishop.
		globalPosition = board_cells[7, 2].transform.position;
		go = Instantiate(Bishop_Black, globalPosition, Quaternion.identity);
        board_cells[7, 2].GetComponent<CellScript>().OccupiedBy(go);

        // Second Bishop.
		globalPosition = board_cells[7, 5].transform.position;
		go = Instantiate(Bishop_Black, globalPosition, Quaternion.identity);
        board_cells[7, 5].GetComponent<CellScript>().OccupiedBy(go);

        // Queen.
		globalPosition = board_cells[7, 3].transform.position;
		go = Instantiate(Queen_Black, globalPosition, Quaternion.identity);
        board_cells[7, 3].GetComponent<CellScript>().OccupiedBy(go);

        // King.
		globalPosition = board_cells[7, 4].transform.position;
		go = Instantiate(King_Black, globalPosition, Quaternion.identity);
        board_cells[7, 4].GetComponent<CellScript>().OccupiedBy(go);
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
                selectedPiece.GetComponent<Pawn_MovementScript>().Deselect();
                selectedCell = null;
                selectedPiece = null;
                selectedPiece2 = null;
            }
            else
            {
                // check if there is any potential attack happening
                // if there isn't, that means the player selected another piece
                if (!IsAttacking(selectedPiece, go))
				{
                    Debug.Log("selected the first piece");
                    selectedPiece = go;
                }
                else
				{
                    Debug.Log("selected the second piece");
                    selectedPiece2 = go;
                    attacking = true;
                }
            }
        }
        else
		{
            Debug.Log("selected piece 1.");
            selectedPiece = go;
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
        return board_cells[x, y].IsOccupied();
    }

    private bool IsAttacking(GameObject go1, GameObject go2)
	{
        if (go1.name.Contains("White") && go2.name.Contains("Black")) return true;
        if (go1.name.Contains("Black") && go2.name.Contains("White")) return true;
        return false;
    }

    private void CheckValidMove()
	{
        // it is guaranteed to be called when a piece is actually selected.
        // I need to know what kind of piece it is. --> need to check the tag of the object
        if (selectedPiece != null || attacking)
		{
            Debug.Log("check if the pawn can be moved");
            if (selectedPiece.tag == "Pawn") CheckMovePawn();
        }
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
        int Cell_xcoord, Cell_ycoord; // for the cell
        Cell_xcoord = selectedCell.GetXCoordinate();
        Cell_ycoord = selectedCell.GetYCoordinate();

        // Then, I need to calculate a list of all the possible movements for that pawn. If the clicked cell
        // is within that list, that's totally correct.
        List<int[]> possibleCoordinates = new List<int[]>();
        
        if (direction) directionModifier = -1;
        else directionModifier = 1;

        // we need to also check in advance if the cells are valid (or un-occupied)

        Debug.Log("Pawn coordinates:" + Pawn_XCoord + " " + Pawn_YCoord);

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
            // If I can attack on the left, it is a possible move.
            int[] left_diagonal = new int[2];

            left_diagonal[0] = Pawn_XCoord + 1 * directionModifier;
            left_diagonal[1] = Pawn_YCoord - 1;

            if (IsCellOccupied(left_diagonal))
                possibleCoordinates.Add(left_diagonal);


            // If I can attack on the right, that is a possible move as well.
            int[] right_diagonal = new int[2];
            right_diagonal[0] = Pawn_XCoord + 1 * directionModifier;
            right_diagonal[1] = Pawn_YCoord + 1;

            if (IsCellOccupied(right_diagonal))
                possibleCoordinates.Add(right_diagonal);
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
			selectedPiece.GetComponent<Pawn_MovementScript>().MoveToCell(selectedCell.gameObject.transform.position, cellCoord);
			selectedCell = null;
			selectedPiece = null;

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
}