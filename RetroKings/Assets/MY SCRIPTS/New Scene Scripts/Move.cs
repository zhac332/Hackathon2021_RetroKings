using System;
using UnityEngine;

public static class Move
{
    private static Tuple<Piece, PieceColor> currentPiece;
    private static string firstCell;
    private static string lastCell;
    private static bool firstCell_Selected = false;
    private static bool secondCell_Selected = false;

    public static bool IsFirstCellSelected()
    {
        return firstCell_Selected;
    }

    public static bool IsSecondCellSelected()
    {
        return secondCell_Selected;
    }

    public static void SelectPiece(string cellName, Piece p, PieceColor c)
    {
        if (!firstCell_Selected)
        {
            firstCell_Selected = true;
            firstCell = cellName;
            currentPiece = new Tuple<Piece, PieceColor>(p, c);
        }
        else
        {
            firstCell_Selected = false;
            firstCell = "";
            currentPiece = new Tuple<Piece, PieceColor>(Piece.NULL, PieceColor.NULL);
        }
    }

    public static void SelectCell(string cellName)
    {
        if (!secondCell_Selected)
        {
            secondCell_Selected = true;
            lastCell = cellName;
        }
        // there is no turning back, if the second cell is already selected. You cannot undo it.
    }
}
