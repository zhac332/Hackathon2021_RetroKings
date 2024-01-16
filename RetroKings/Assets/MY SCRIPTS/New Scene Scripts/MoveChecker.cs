using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class MoveChecker
{
    private static List<GameObject> cells;
    private static Piece currentPiece;
    private static PieceColor currentPieceColor;
    private static bool acquired = false;
    private static readonly Tuple<Piece, PieceColor> nullPiece = new Tuple<Piece, PieceColor>(Piece.NULL, PieceColor.NULL);

    public static void AcquireAllCells()
    {
        if (!acquired)
        {
            acquired = true;
            cells = GameObject.FindGameObjectsWithTag("Cell").ToList<GameObject>();
        }
    }

    public static void SetFirstPiece(string pieceName)
    {
        string[] parts = pieceName.Split('_');

        currentPieceColor = (parts[1] == "B" ? PieceColor.Black : PieceColor.White);

        if (parts[0].Contains("Pawn")) currentPiece = Piece.Pawn;
        else if (parts[0].Contains("Bishop")) currentPiece = Piece.Bishop;
        else if (parts[0].Contains("Rook")) currentPiece = Piece.Rook;
        else if (parts[0].Contains("Knight")) currentPiece = Piece.Knight;
        else if (parts[0].Contains("Queen")) currentPiece = Piece.Queen;
        else if (parts[0].Contains("King")) currentPiece = Piece.King;
    }

    public static void MarkAvailableCells(GameObject currentCell)
    {
        if (currentPiece == Piece.Pawn) MarkPawn(currentCell);
        if (currentPiece == Piece.Queen) MarkQueen(currentCell);
        if (currentPiece == Piece.King) MarkKing(currentCell);
        if (currentPiece == Piece.Rook) MarkRook(currentCell);
        if (currentPiece == Piece.Bishop) MarkBishop(currentCell);
        if (currentPiece == Piece.Knight) MarkKnight(currentCell);
    }

    private static void MarkQueen(GameObject currentCell)
    {
        // rook and bishop combined
        MarkBishop(currentCell);
        MarkRook(currentCell);
    }

    private static void MarkKing(GameObject currentCell)
    {

    }

    private static void MarkRook(GameObject currentCell)
    {
        int currentCellIndex = cells.IndexOf(currentCell);

        UnmarkAll(currentCellIndex);

        // Mark horizontally
        int row = currentCellIndex / 8;
        bool stop = false;
        for (int col = 0; col < 8 && !stop; col++)
        {
            Tuple<Piece, PieceColor> pieceOnCell = GetPieceOnCell(col);

            if (pieceOnCell == nullPiece) MarkCell(row, col);
            else
            {
                if (pieceOnCell.Item2 != currentPieceColor) MarkCell(row, col);
                stop = true;
            }
        }

        // Mark vertically
        int colIndex = currentCellIndex % 8;
        stop = false;
        for (int r = 0; r < 8 && !stop; r++)
        {
            Tuple<Piece, PieceColor> pieceOnCell = GetPieceOnCell(r);

            if (pieceOnCell == nullPiece) MarkCell(r, colIndex);
            else
            {
                if (pieceOnCell.Item2 != currentPieceColor) MarkCell(r, colIndex);
                stop = true;
            }
        }
    }

    private static void MarkKnight(GameObject currentCell)
    {
        Debug.Log("Marking for knight on " + currentCell.name);
        int currentCellIndex = cells.IndexOf(currentCell);

        UnmarkAll(currentCellIndex);

        int row = currentCellIndex / 8;
        int colIndex = currentCellIndex % 8;

        // Possible knight moves
        int[,] knightMoves = {
        { -2, -1 }, { -2, 1 },
        { -1, -2 }, { -1, 2 },
        { 1, -2 }, { 1, 2 },
        { 2, -1 }, { 2, 1 }
        };

        for (int i = 0; i < knightMoves.GetLength(0); i++)
        {
            int r = row + knightMoves[i, 0];
            int c = colIndex + knightMoves[i, 1];

            if ((0 <= r && r < 8) && (0 <= c && c < 8) && (0 <= r * 8 + c && r * 8 + c < cells.Count))
            {
                Tuple<Piece, PieceColor> pieceOnCell = GetPieceOnCell(r * 8 + c);

                if (pieceOnCell == nullPiece) MarkCell(r, c);
                else if (pieceOnCell.Item2 != currentPieceColor) MarkCell(r, c);
            }
        }
    }

    private static void MarkBishop(GameObject currentCell)
    {
        int currentCellIndex = cells.IndexOf(currentCell);

        UnmarkAll(currentCellIndex);

        int row = currentCellIndex / 8;
        int colIndex = currentCellIndex % 8;

        // Mark diagonally (top-left to bottom-right)
        MarkDiagonalCells(row - 1, colIndex - 1, -1, -1);

        // Mark diagonally (top-right to bottom-left)
        MarkDiagonalCells(row - 1, colIndex + 1, -1, 1);

        // Mark diagonally (bottom-left to top-right)
        MarkDiagonalCells(row + 1, colIndex - 1, 1, -1);

        // Mark diagonally (bottom-right to top-left)
        MarkDiagonalCells(row + 1, colIndex + 1, 1, 1);
    }

    private static void MarkDiagonalCells(int startRow, int startCol, int rowIncrement, int colIncrement)
    {
        bool stop = false;
        for (int r = startRow, c = startCol; r >= 0 && r < 8 && c >= 0 && c < 8 && !stop; r += rowIncrement, c += colIncrement)
        {
            Tuple<Piece, PieceColor> pieceOnCell = GetPieceOnCell(r * 8 + c);

            if (pieceOnCell == nullPiece) MarkCell(r, c);
            else
            {
                if (pieceOnCell.Item2 != currentPieceColor) MarkCell(r, c);
                stop = true;
            }
        }
    }
    
    private static void MarkPawn(GameObject currentCell)
    {
        int currentCellIndex = cells.IndexOf(currentCell);

        UnmarkAll(currentCellIndex);

        int row = currentCellIndex / 8;
        int colIndex = currentCellIndex % 8;
        int[] rowAdds = { -1, +1 };

        Tuple<Piece, PieceColor> pieceOnCell = GetPieceOnCell(row * 8 + colIndex + 1);

        if (pieceOnCell == nullPiece) MarkCell(row, colIndex + 1);
        else if (pieceOnCell.Item2 != currentPieceColor) MarkCell(row, colIndex + 1);



        for (int i = 0; i < rowAdds.Length; i++)
        {
            pieceOnCell = GetPieceOnCell((row + rowAdds[i]) * 8 + colIndex + 1);
            if (pieceOnCell.Item2 != PieceColor.NULL && pieceOnCell.Item2 != currentPieceColor) MarkCell(row + rowAdds[i], colIndex + 1);
        }

        // Mark cells in front of the pawn
        //MarkPawnMove(row + (currentPieceColor == PieceColor.White ? 1 : -1), colIndex);

        //// Mark diagonal cells for possible captures
        //MarkPawnCapture(row + (currentPieceColor == PieceColor.White ? 1 : -1), colIndex - 1);
        //MarkPawnCapture(row + (currentPieceColor == PieceColor.White ? 1 : -1), colIndex + 1);
    }

    private static Tuple<Piece, PieceColor> GetPieceOnCell(int cellIndex)
    {
        Debug.Log(cells[cellIndex].name);
        if (cells[cellIndex].GetComponent<Cell_Script>().HasAPiece())
        {
            Piece p = Piece.NULL;
            PieceColor c = PieceColor.NULL;

            string pName = cells[cellIndex].transform.GetChild(0).name;

            string[] parts = pName.Split('_');

             c = (parts[1] == "B" ? PieceColor.Black : PieceColor.White);

            if (parts[0] == "Pawn") p = Piece.Pawn;
            else if (parts[0] == "Bishop") p = Piece.Bishop;
            else if (parts[0] == "Rook") p = Piece.Rook;
            else if (parts[0] == "Knight") p = Piece.Knight;
            else if (parts[0] == "Queen") p = Piece.Queen;
            else if (parts[0] == "King") p = Piece.King;

            return new Tuple<Piece, PieceColor>(p, c);
        }
        return nullPiece;
    }

    public static void UnmarkAll(int exceptionIndex)
    {
        for (int i = 0; i < cells.Count; i++)
            if (i != exceptionIndex) cells[i].GetComponent<Cell_Script>().DeselectCell();
    }

    public static void UnmarkAll()
    {
        for (int i = 0; i < cells.Count; i++)
            cells[i].GetComponent<Cell_Script>().DeselectCell();
    }

    private static void MarkCell(int row, int col)
    {
        int index = row * 8 + col;
        if (index >= 0 && index < cells.Count)
        {
            // Mark the cell or perform your desired action
            cells[index].GetComponent<Cell_Script>().MarkForLegalCells(); // Assuming there is a method Mark() in your Cell script.
        }
    }
}
