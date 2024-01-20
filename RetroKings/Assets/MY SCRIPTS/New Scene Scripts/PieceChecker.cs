using UnityEngine;

public static class PieceChecker
{
    private static readonly int Pawn_Value = 1;
    private static readonly int Bishop_Value = 3;
    private static readonly int Knight_Value = 3;
    private static readonly int Rook_Value = 5;
    private static readonly int Queen_Value = 9;

    public static bool IsPieceValueLowerThan(GameObject go, int nrPoints)
    {
        if (IsPieceKing(go)) return false;

        if (IsPieceQueen(go) && nrPoints >= Queen_Value) return true;
        if (IsPieceRook(go) && nrPoints >= Rook_Value) return true;
        if (IsPieceBishop(go) && nrPoints >= Bishop_Value) return true;
        if (IsPieceKnight(go) && nrPoints >= Knight_Value) return true;
        if (IsPiecePawn(go) && nrPoints >= Pawn_Value) return true;

        return false;
    }

    public static bool IsWhitePiece(GameObject go)
    {
        return go.transform.GetChild(0).name.Contains("W");
    }

    public static bool IsBlackPiece(GameObject go) 
    {
        return go.transform.GetChild(0).name.Contains("B");
    }

    public static bool HasAPiece(GameObject go)
    {
        return go.transform.childCount > 0;
    }


    public static bool IsPiecePawn(GameObject go)
    {
        return go.transform.GetChild(0).name.Contains("Pawn");
    }

    public static bool IsPieceBishop(GameObject go)
    {
        return go.transform.GetChild(0).name.Contains("Bishop");
    }

    public static bool IsPieceRook(GameObject go)
    {
        return go.transform.GetChild(0).name.Contains("Rook");
    }

    public static bool IsPieceKnight(GameObject go)
    {
        return go.transform.GetChild(0).name.Contains("Knight");
    }

    public static bool IsPieceQueen(GameObject go)
    {
        return go.transform.GetChild(0).name.Contains("Queen");
    }

    public static bool IsPieceKing(GameObject go)
    {
        return go.transform.GetChild(0).name.Contains("King");
    }
}
