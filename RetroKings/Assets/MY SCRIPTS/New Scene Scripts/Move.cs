using System;

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
        PieceColor c = PieceColor.NULL;

        c = (parts[1] == "B" ? PieceColor.Black : PieceColor.White);

        if (parts[0] == "Pawn") p = Piece.Pawn;
        else if (parts[0] == "Bishop") p = Piece.Bishop;
        else if (parts[0] == "Rook") p = Piece.Rook;
        else if (parts[0] == "Knight") p = Piece.Knight;
        else if (parts[0] == "Queen") p = Piece.Queen;
        else if (parts[0] == "King") p = Piece.King;

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

    public static void SelectCell(string cellName, Action<string, string, Tuple<Piece, PieceColor>> executeMove)
    {
        if (!secondCell_Selected)
        {
            secondCell_Selected = true;
            lastCell = cellName;

            // verify if the move is legal
            executeMove(firstCell, lastCell, currentPiece);
        }
        // there is no turning back, if the second cell is already selected. You cannot undo it.
    }
    
    public static void SelectCell(string cellName, string pieceName, Action<string, string, Tuple<Piece, PieceColor>> executeMove)
    {
        if (!secondCell_Selected)
        {
            secondCell_Selected = true;
            lastCell = cellName;

            // verify if the move is legal, and a piece exists on the second cell
            executeMove(firstCell, lastCell, currentPiece);
        }
    }

    public static void ResetMove()
    {
        firstCell_Selected = secondCell_Selected = false;
            firstCell = lastCell = "";
            currentPiece = currentPiece = new Tuple<Piece, PieceColor>(Piece.NULL, PieceColor.NULL);
    }
}
