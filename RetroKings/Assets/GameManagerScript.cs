using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The main GameObject that allows the movement of pieces to be made.
/// </summary>
public class GameManagerScript : MonoBehaviour
{
    [SerializeField] private List<GameObject> row1 = new List<GameObject>();
    [SerializeField] private List<GameObject> row2 = new List<GameObject>();
    [SerializeField] private List<GameObject> row3 = new List<GameObject>();
    [SerializeField] private List<GameObject> row4 = new List<GameObject>();
    [SerializeField] private List<GameObject> row5 = new List<GameObject>();
    [SerializeField] private List<GameObject> row6 = new List<GameObject>();
    [SerializeField] private List<GameObject> row7 = new List<GameObject>();
    [SerializeField] private List<GameObject> row8 = new List<GameObject>();
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

    private GameObject selectedPiece;
    private CellScript selectedCell;

    // Start is called before the first frame update
    void Start()
    {
        InitBoard();
        ShowWhitePerspective();
        //ShowBlackPerspective();
    }

    private void InitBoard()
	{
        for (int i = 0; i < row1.Count; i++)
            row1[i].GetComponent<CellScript>().SetCoordinates(0, i);
        for (int i = 0; i < row2.Count; i++)
            row2[i].GetComponent<CellScript>().SetCoordinates(1, i);
        for (int i = 0; i < row3.Count; i++)
            row3[i].GetComponent<CellScript>().SetCoordinates(2, i);
        for (int i = 0; i < row4.Count; i++)
            row4[i].GetComponent<CellScript>().SetCoordinates(3, i);
        for (int i = 0; i < row5.Count; i++)
            row5[i].GetComponent<CellScript>().SetCoordinates(4, i);
        for (int i = 0; i < row6.Count; i++)
            row6[i].GetComponent<CellScript>().SetCoordinates(5, i);
        for (int i = 0; i < row7.Count; i++)
            row7[i].GetComponent<CellScript>().SetCoordinates(6, i);
        for (int i = 0; i < row8.Count; i++)
            row8[i].GetComponent<CellScript>().SetCoordinates(7, i);
    }

    private void ShowWhitePerspective()
	{
        /// -----------------------------------------WHITE PIECES-----------------------------------------
        
        // the white pawns will be on row 7
        // the other white pieces will be on row 8

        Vector3 globalPosition = new Vector3();

        // setting up the white pawns
        for (int i = 0; i < 8; i++)
		{
            // calculating the position
            globalPosition = row7[i].transform.position;
            globalPosition.y += 0.1f;

            // instantiating the object at that position
            GameObject go = Instantiate(Pawn_White, globalPosition, Quaternion.identity);
            go.GetComponent<Pawn_MovementScript>().SetCoordinates(i, 1, false);

            // occupying the cell
            row7[i].GetComponent<CellScript>().OccupiedBy(go);
		}

        globalPosition = row8[0].transform.position;
        Instantiate(Rook_White, globalPosition, Quaternion.identity);
        globalPosition = row8[7].transform.position;
        Instantiate(Rook_White, globalPosition, Quaternion.identity);

        globalPosition = row8[1].transform.position;
        Instantiate(Knight_White, globalPosition, Quaternion.identity);
        globalPosition = row8[6].transform.position;
        Instantiate(Knight_White, globalPosition, Quaternion.identity);

        globalPosition = row8[2].transform.position;
        Instantiate(Bishop_White, globalPosition, Quaternion.identity);
        globalPosition = row8[5].transform.position;
        Instantiate(Bishop_White, globalPosition, Quaternion.identity);

        globalPosition = row8[3].transform.position;
        Instantiate(Queen_White, globalPosition, Quaternion.identity);

        globalPosition = row8[4].transform.position;
        Instantiate(King_White, globalPosition, Quaternion.identity);

        /// -----------------------------------------BLACK PIECES-----------------------------------------

        // setting up the white pawns
        foreach (GameObject cell in row2)
        {
            globalPosition = cell.transform.position;
            globalPosition.y += 0.1f;
            Instantiate(Pawn_Black, globalPosition, Quaternion.identity);
        }

        globalPosition = row1[0].transform.position;
        Instantiate(Rook_Black, globalPosition, Quaternion.identity);
        globalPosition = row1[7].transform.position;
        Instantiate(Rook_Black, globalPosition, Quaternion.identity);

        globalPosition = row1[1].transform.position;
        Instantiate(Knight_Black, globalPosition, Quaternion.identity);
        globalPosition = row1[6].transform.position;
        Instantiate(Knight_Black, globalPosition, Quaternion.identity);

        globalPosition = row1[2].transform.position;
        Instantiate(Bishop_Black, globalPosition, Quaternion.identity);
        globalPosition = row1[5].transform.position;
        Instantiate(Bishop_Black, globalPosition, Quaternion.identity);

        globalPosition = row1[3].transform.position;
        Instantiate(Queen_Black, globalPosition, Quaternion.identity);

        globalPosition = row1[4].transform.position;
        Instantiate(King_Black, globalPosition, Quaternion.identity);
    }

    private void ShowBlackPerspective()
	{
        /// -----------------------------------------WHITE PIECES-----------------------------------------

        Vector3 globalPosition = new Vector3();
        GameObject go = new GameObject;

        // setting up the white pawns
        for (int i = 0; i < 8; i++)
        {
            globalPosition = row2[i].transform.position;
            globalPosition.y += 0.1f;
            Instantiate(Pawn_White, globalPosition, Quaternion.identity);
            row2[i].GetComponent<CellScript>().OccupiedBy(go);
        }

        // First Rook.
        globalPosition = row1[0].transform.position;
        go = Instantiate(Rook_White, globalPosition, Quaternion.identity);
        row1[0].GetComponent<CellScript>().OccupiedBy(go);

        // Second Rook.
        globalPosition = row1[7].transform.position;
        go = Instantiate(Rook_White, globalPosition, Quaternion.identity);
        row1[7].GetComponent<CellScript>().OccupiedBy(go);

        // First Knight.
        globalPosition = row1[1].transform.position;
        go = Instantiate(Knight_White, globalPosition, Quaternion.identity);
        row1[1].GetComponent<CellScript>().OccupiedBy(go);

        // Second Knight.
        globalPosition = row1[6].transform.position;
        go = Instantiate(Knight_White, globalPosition, Quaternion.identity);
        row1[6].GetComponent<CellScript>().OccupiedBy(go);

        // First Bishop.
        globalPosition = row1[2].transform.position;
        go = Instantiate(Bishop_White, globalPosition, Quaternion.identity);
        row1[2].GetComponent<CellScript>().OccupiedBy(go);

        // Second Bishop.
        globalPosition = row1[5].transform.position;
        go = Instantiate(Bishop_White, globalPosition, Quaternion.identity);
        row1[5].GetComponent<CellScript>().OccupiedBy(go);

        // Queen.
        globalPosition = row1[3].transform.position;
        go = Instantiate(Queen_White, globalPosition, Quaternion.identity);
        row1[3].GetComponent<CellScript>().OccupiedBy(go);

        // King.
        globalPosition = row1[4].transform.position;
        go = Instantiate(King_White, globalPosition, Quaternion.identity);
        row1[4].GetComponent<CellScript>().OccupiedBy(go);


        /// -----------------------------------------BLACK PIECES-----------------------------------------

        // setting up the white pawns
        for (int i = 0; i < 8; i++)
        {
            globalPosition = row7[i].transform.position;
            globalPosition.y += 0.1f;
            go = Instantiate(Pawn_Black, globalPosition, Quaternion.identity);
            row7[i].GetComponent<CellScript>().OccupiedBy(go);
        }

        // First Rook.
		globalPosition = row8[0].transform.position;
		go = Instantiate(Rook_Black, globalPosition, Quaternion.identity);
        row8[0].GetComponent<CellScript>().OccupiedBy(go);

        // Second Rook.
		globalPosition = row8[7].transform.position;
		go = Instantiate(Rook_Black, globalPosition, Quaternion.identity);
        row8[7].GetComponent<CellScript>().OccupiedBy(go);

        // First Knight.
		globalPosition = row8[1].transform.position;
		go = Instantiate(Knight_Black, globalPosition, Quaternion.identity);
        row8[1].GetComponent<CellScript>().OccupiedBy(go);

        // Second Knight.
		globalPosition = row8[6].transform.position;
		go = Instantiate(Knight_Black, globalPosition, Quaternion.identity);
        row8[6].GetComponent<CellScript>().OccupiedBy(go);

        // First Bishop.
		globalPosition = row8[2].transform.position;
		go = Instantiate(Bishop_Black, globalPosition, Quaternion.identity);
        row8[2].GetComponent<CellScript>().OccupiedBy(go);

        // Second Bishop.
		globalPosition = row8[5].transform.position;
		go = Instantiate(Bishop_Black, globalPosition, Quaternion.identity);
        row8[5].GetComponent<CellScript>().OccupiedBy(go);

        // Queen.
		globalPosition = row8[3].transform.position;
		go = Instantiate(Queen_Black, globalPosition, Quaternion.identity);
        row8[3].GetComponent<CellScript>().OccupiedBy(go);

        // King.
		globalPosition = row8[4].transform.position;
		go = Instantiate(King_Black, globalPosition, Quaternion.identity);
        row8[4].GetComponent<CellScript>().OccupiedBy(go);
	}

    public void GetSelectedPiece(GameObject go)
	{
        if (selectedPiece != null)
		{
            // I need to de-select the object inside its script
            if (selectedPiece.tag == "Pawn") selectedPiece.GetComponent<Pawn_MovementScript>().Deselect();


            
        }
        this.selectedPiece = go;
    }

    public void SetSelectedCell(GameObject go)
	{
        this.selectedCell = go.GetComponent<CellScript>();
        if (selectedPiece != null)
            CheckValidMove();
	}

    private void CheckValidMove()
	{
        // it is guaranteed to be called when a piece is actually selected.
        // I need to know what kind of piece it is. --> need to check the tag of the object
        if (selectedPiece != null)
		{
            //Debug.Log("check if the pawn can be moved");
            if (selectedPiece.tag == "Pawn") CheckMovePawn();
        }
	}

    private void CheckMovePawn()
	{
        int Pawn_XCoord, Pawn_YCoord; // for the piece
        bool direction, firstMove;

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
        Cell_xcoord = 7 - selectedCell.GetXCoordinate();
        Cell_ycoord = selectedCell.GetYCoordinate();

        // Then, I need to calculate a list of all the possible movements for that pawn. If the clicked cell
        // is within that list, that's totally correct.
        List<int[]> possibleCoordinates = new List<int[]>();


        // we need to also check in advance if the cells are valid (or un-occupied)

		if (!firstMove) // the pawn DID make the first move
		{
            //Debug.Log("can move only one cell");
            if (!direction) // if it is upwards
			{
                int[] possibleCoord = new int[2];
                possibleCoord[0] = Pawn_XCoord;
                possibleCoord[1] = Pawn_YCoord + 1;

                possibleCoordinates.Add(possibleCoord);
			}
            else
			{
                int[] possibleCoord = new int[2];
                possibleCoord[0] = Pawn_XCoord;
                possibleCoord[1] = Pawn_YCoord - 1;

                possibleCoordinates.Add(possibleCoord);
            }
		    
            // I also need to check if the pawn can attack on the diagonal --  TO BE DONE LATER.
        }
		else
		{
            if (!direction) // if it is upwards
            {
                // first cell forward
                int[] possibleCoord = new int[2];
                possibleCoord[0] = Pawn_XCoord;
                possibleCoord[1] = Pawn_YCoord + 1;

                possibleCoordinates.Add(possibleCoord);

                // second cell forward
                int[] possibleCoord2 = new int[2];
                possibleCoord2[0] = Pawn_XCoord;
                possibleCoord2[1] = Pawn_YCoord + 2;

                possibleCoordinates.Add(possibleCoord2);
            }
            else
            {
                // first cell forward
                int[] possibleCoord = new int[2];
                possibleCoord[0] = Pawn_XCoord;
                possibleCoord[1] = Pawn_YCoord - 1;

                possibleCoordinates.Add(possibleCoord);

                // second cell forward
                int[] possibleCoord2 = new int[2];
                possibleCoord2[0] = Pawn_XCoord;
                possibleCoord2[1] = Pawn_YCoord - 2;

                possibleCoordinates.Add(possibleCoord2);
            }
        }


        // now, we need to check if the coordinates of the cell exist in the list
        bool canMove = false;

        int[] cellCoord = new int[2];
        cellCoord[0] = Cell_ycoord;
        cellCoord[1] = Cell_xcoord;

        for (int i = 0; i < possibleCoordinates.Count && !canMove; i++)
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
        }
	}
}