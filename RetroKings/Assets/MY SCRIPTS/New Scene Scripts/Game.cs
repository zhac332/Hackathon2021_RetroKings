using System;

public static class Game
{
    private static bool myTurn = true;
    private static Action displayTurn_Function;
    private static CapturedPiecesScript whitePiecesCaptured;
    private static CapturedPiecesScript blackPiecesCaptured;

    public static void SetWhitePiecesCaptured(CapturedPiecesScript t)
    {
        whitePiecesCaptured = t;
    }

    public static void SetBlackPiecesCaptured(CapturedPiecesScript t) 
    {
        blackPiecesCaptured = t;
    }

    public static void SetDisplayTurnFunction(Action t)
    {
        displayTurn_Function = t;
    }

    public static void SwitchTurn()
    {
        myTurn = !myTurn;
        displayTurn_Function();
    }

    public static void PieceCaptured(string pieceName)
    {
        PieceColor color = (pieceName.Contains("W") ? PieceColor.White : PieceColor.Black);
        Piece piece = Piece.NULL;

        if (pieceName.Contains("Pawn")) piece = Piece.Pawn;
        else if (pieceName.Contains("Bishop")) piece = Piece.Bishop;
        else if (pieceName.Contains("Rook")) piece = Piece.Rook;
        else if (pieceName.Contains("Knight")) piece = Piece.Knight;
        else if (pieceName.Contains("Queen")) piece = Piece.Queen;
        else if (pieceName.Contains("King")) piece = Piece.King;

        if (color == PieceColor.White) blackPiecesCaptured.AddPieceCaptured(piece);
        else if (color == PieceColor.Black) whitePiecesCaptured.AddPieceCaptured(piece);
    }

    public static bool IsMyTurn()
    {
        return myTurn;
    }
}
