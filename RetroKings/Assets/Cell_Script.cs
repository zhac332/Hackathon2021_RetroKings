using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell_Script : MonoBehaviour
{
    private void Cell_OnClick()
    {
        Debug.Log("clicked cell " + name + ".");
        GameObject piece = null;
        if (!Move.IsFirstCellSelected())
        {
            if (transform.childCount != 0)
            {
                piece = transform.GetChild(0).gameObject;

                Debug.Log("selected " + piece.name);

                Move.SelectPiece(name, piece.name);
            }
            else Debug.Log("Select a cell with a piece. If there is no piece, how do I know what move to make?");
        }
        else
        {
            if (Move.IsCellIdenticalWithFirst(name))
            {
                Debug.Log("deselected " + name);

                Move.SelectPiece();
            }
            else if (!Move.IsSecondCellSelected())
            {
                piece = null;
                if (transform.childCount != 0) piece = transform.GetChild(0).gameObject;

                Debug.Log("moving to cell " + name);

                if (piece == null) Move.SelectCell(name, (cell1, cell2, piece) => ExecuteMove(cell1, cell2, piece));
                else Move.SelectCell(name, piece.name, (cell1, cell2, piece) => ExecuteMove(cell1, cell2, piece));
            }
        }
    }

    private void ExecuteMove(string cell1, string cell2, Tuple<Piece, PieceColor> piece)
    {
        // I need to hold the child piece from cell1,
        // I need to remove the child piece from cell2, if there is one
        // and put the piece from cell1 to cell2
        GameObject c1 = GameObject.Find(cell1);
        GameObject c2 = GameObject.Find(cell2);
        GameObject p = c1.transform.GetChild(0).gameObject;

        if (c2.transform.childCount != 0)
            Destroy(c2.transform.GetChild(0).gameObject);

        p.transform.SetParent(c2.transform);

        Debug.Log("Moved " + p.name + " from " + c1.name + " to " + c2.name);

        p.transform.localPosition = new Vector3(0f, 0f, 0f);

        Move.ResetMove();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 raycastPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(raycastPosition, Vector2.zero);

            if (hit.collider != null)
            {
                if (hit.collider.name == name)
                {
                    Cell_OnClick();
                }
            }
        }
    }
}
