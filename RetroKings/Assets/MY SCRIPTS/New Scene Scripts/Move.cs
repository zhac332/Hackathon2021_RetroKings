using System;
using System.Collections.Generic;
using UnityEngine;

public static class Move
{
    private static Tuple<Piece, PieceColor> currentPiece;
    private static string firstCell;
    private static string lastCell;
    private static bool firstCell_Selected = false;
    private static bool secondCell_Selected = false;
    private static Action<string, string, Tuple<Piece, PieceColor>, bool> method_ExecuteMove;

    public static bool IsFirstCellSelected()
    {
        return firstCell_Selected;
    }
    
    public static bool IsCellIdenticalWithFirst(string cellName)
    {
        return cellName == firstCell;
    }

    public static bool IsSecondCellSelected()
    {
        return secondCell_Selected;
    }

    public static void SelectPiece(string cellName, string pieceName)
    {
        string[] parts = pieceName.Split('_');
        Piece p = Piece.NULL;
        PieceColor c = (parts[1] == "B" ? PieceColor.Black : PieceColor.White);

        if (parts[0].Contains("Pawn")) p = Piece.Pawn;
        else if (parts[0].Contains("Bishop")) p = Piece.Bishop;
        else if (parts[0].Contains("Rook")) p = Piece.Rook;
        else if (parts[0].Contains("Knight")) p = Piece.Knight;
        else if (parts[0].Contains("Queen")) p = Piece.Queen;
        else if (parts[0].Contains("King")) p = Piece.King;

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

    public static void SelectPiece()
    {
        firstCell_Selected = false;
        firstCell = "";
        currentPiece = new Tuple<Piece, PieceColor>(Piece.NULL, PieceColor.NULL);
    }

    public static void SelectCell(string cellName, Action<string, string, Tuple<Piece, PieceColor>, bool> executeMove)
    {
        if (!secondCell_Selected)
        {
            secondCell_Selected = true;
            lastCell = cellName;

            string fC = firstCell;
            string lC = lastCell;
            Tuple<Piece, PieceColor> currentPiece_Copy = currentPiece;
            // need to save copies of the parameters for the executeMove, because after 1 call, that data will be reset to NULL
            
            // in case there is a castle involved, I need to make 2 moves.
            if (currentPiece_Copy.Item1 == Piece.King)
            {
                Tuple<bool, Tuple<string, string, Tuple<Piece, PieceColor>>> result = MoveChecker.IsMove_Castles(fC, lC, currentPiece_Copy);
                if (result.Item1)
                {
                    Debug.Log("Moving the rook from " + result.Item2.Item1 + " to " + result.Item2.Item2);
                    executeMove(result.Item2.Item1, result.Item2.Item2, result.Item2.Item3, false);
                }
            }

            executeMove(fC, lC, currentPiece_Copy, true);
        }
        // there is no turning back, if the second cell is already selected. You cannot undo it.
    }

    public static void SelectCell_Promote(string cellName, Action<string, string, Tuple<Piece, PieceColor>, bool> method)
    {
        if (!secondCell_Selected)
        {
            secondCell_Selected = true;
            lastCell = cellName;

            method_ExecuteMove = method; // which will be called when a promotional piece has been selected
        }
    }

    public static void PromotionSelected(Piece p)
    {
        currentPiece = new Tuple<Piece, PieceColor>(p, currentPiece.Item2);
        method_ExecuteMove(firstCell, lastCell, currentPiece, true);
        MoveChecker.DisablePromotionalPanel();
    }

    public static void ResetMove()
    {
        firstCell_Selected = secondCell_Selected = false;
        firstCell = lastCell = "";
        currentPiece = currentPiece = new Tuple<Piece, PieceColor>(Piece.NULL, PieceColor.NULL);
    }
}
