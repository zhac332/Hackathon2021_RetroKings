using System;

public static class EnumParser
{
    public static Piece ParsePiece(string value)
    {
        if (Enum.TryParse(value, out Piece result))
        {
            return result;
        }
        else
        {
            throw new ArgumentException($"Invalid Piece value: {value}");
        }
    }

    public static PieceColor ParsePieceColor(string value)
    {
        if (Enum.TryParse(value, out PieceColor result))
        {
            return result;
        }
        else
        {
            throw new ArgumentException($"Invalid Piece value: {value}");
        }
    }
}
