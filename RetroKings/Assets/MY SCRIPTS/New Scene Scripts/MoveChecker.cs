using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class MoveChecker
{
    private static List<GameObject> cells;
    private static GameObject PromotionalPanel;
    private static Piece currentPiece;
    private static PieceColor currentPieceColor;
    private static bool acquired = false;
    private static readonly Tuple<Piece, PieceColor> nullPiece = new Tuple<Piece, PieceColor>(Piece.NULL, PieceColor.NULL);
    private static List<string> markedCells;
    private static Action<bool> updatePromotionalPieces_Function;

    public static void AcquireAllCells()
    {
        if (!acquired)
        {
            acquired = true;
            cells = GameObject.FindGameObjectsWithTag("Cell").ToList();
            PromotionalPanel = GameObject.Find("MAIN CANVAS/Promotion Panel");
        }
    }

    public static void SetUpdatePromotionalPiecesFunction(Action<bool> method)
    {
        updatePromotionalPieces_Function = method;
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
        if (currentPiece == Piece.Pawn) MarkPawn(currentCell, (currentPieceColor == PieceColor.White) ? false : true);
        if (currentPiece == Piece.Queen) MarkQueen(currentCell);
        if (currentPiece == Piece.King) MarkKing(currentCell);
        if (currentPiece == Piece.Rook) MarkRook(currentCell);
        if (currentPiece == Piece.Bishop) MarkBishop(currentCell);
        if (currentPiece == Piece.Knight) MarkKnight(currentCell);
    }

    private static void MarkQueen(GameObject currentCell)
    {
        int currentCellIndex = cells.IndexOf(currentCell);

        UnmarkAll(currentCellIndex);
        markedCells = new List<string>();

        int row = currentCellIndex / 8;
        int colIndex = currentCellIndex % 8;
        bool stop = false;

        // top left diagonal
        for (int r = row + 1, c = colIndex - 1; IsIndexValid(r, 0, 8) && IsIndexValid(c, 0, 8) && !stop; r++, c--)
        {
            Tuple<Piece, PieceColor> pieceOnCell = GetPieceOnCell(r * 8 + c);

            if (pieceOnCell == nullPiece) MarkCell(r, c, false);
            else
            {
                if (pieceOnCell.Item2 != currentPieceColor) MarkCell(r, c, true);
                stop = true;
            }
        }

        // top right diagonal
        stop = false;
        for (int r = row + 1, c = colIndex + 1; IsIndexValid(r, 0, 8) && IsIndexValid(c, 0, 8) && !stop; r++, c++)
        {
            Tuple<Piece, PieceColor> pieceOnCell = GetPieceOnCell(r * 8 + c);

            if (pieceOnCell == nullPiece) MarkCell(r, c, false);
            else
            {
                if (pieceOnCell.Item2 != currentPieceColor) MarkCell(r, c, true);
                stop = true;
            }
        }

        //bot left diagonal
        stop = false;
        for (int r = row - 1, c = colIndex - 1; IsIndexValid(r, 0, 8) && IsIndexValid(c, 0, 8) && !stop; r--, c--)
        {
            Tuple<Piece, PieceColor> pieceOnCell = GetPieceOnCell(r * 8 + c);

            if (pieceOnCell == nullPiece) MarkCell(r, c, false);
            else
            {
                if (pieceOnCell.Item2 != currentPieceColor) MarkCell(r, c, true);
                stop = true;
            }
        }

        // bot right diagonal
        stop = false;
        for (int r = row - 1, c = colIndex + 1; IsIndexValid(r, 0, 8) && IsIndexValid(c, 0, 8) && !stop; r--, c++)
        {
            Tuple<Piece, PieceColor> pieceOnCell = GetPieceOnCell(r * 8 + c);

            if (pieceOnCell == nullPiece) MarkCell(r, c, false);
            else
            {
                if (pieceOnCell.Item2 != currentPieceColor) MarkCell(r, c, true);
                stop = true;
            }
        }

        // vertical downwards
        stop = false;
        for (int c = colIndex - 1; IsIndexValid(c, 0, 8) && !stop; c--)
        {
            Tuple<Piece, PieceColor> pieceOnCell = GetPieceOnCell(row * 8 + c);

            if (pieceOnCell == nullPiece) MarkCell(row, c, false);
            else
            {
                if (pieceOnCell.Item2 != currentPieceColor) MarkCell(row, c, true);
                stop = true;
            }
        }

        // vertical upwards
        stop = false;
        for (int c = colIndex + 1; IsIndexValid(c, 0, 8) && !stop; c++)
        {
            Tuple<Piece, PieceColor> pieceOnCell = GetPieceOnCell(row * 8 + c);

            if (pieceOnCell == nullPiece) MarkCell(row, c, false);
            else
            {
                if (pieceOnCell.Item2 != currentPieceColor) MarkCell(row, c, true);
                stop = true;
            }
        }

        // horizontal left
        stop = false;
        for (int r = row - 1; IsIndexValid(r, 0, 8) && !stop; r--)
        {
            Tuple<Piece, PieceColor> pieceOnCell = GetPieceOnCell(r * 8 + colIndex);

            if (pieceOnCell == nullPiece) MarkCell(r, colIndex, false);
            else
            {
                if (pieceOnCell.Item2 != currentPieceColor) MarkCell(r, colIndex, true);
                stop = true;
            }
        }

        // horizontal right
        stop = false;
        for (int r = row + 1; IsIndexValid(r, 0, 8) && !stop; r++)
        {
            Tuple<Piece, PieceColor> pieceOnCell = GetPieceOnCell(r * 8 + colIndex);

            if (pieceOnCell == nullPiece) MarkCell(r, colIndex, false);
            else
            {
                if (pieceOnCell.Item2 != currentPieceColor) MarkCell(r, colIndex, true);
                stop = true;
            }
        }
    }

    private static bool IsIndexValid(int value, int minInclusive, int maxExclusive)
    {
        return (minInclusive <= value) && (value < maxExclusive);
    }

    private static int FindCellOfName(string name)
    {
        for (int i = 0; i < cells.Count; i++)
            if (cells[i].name == name) return i;
        return -1;
    }

    private static void MarkKing(GameObject currentCell)
    {
        int currentCellIndex = cells.IndexOf(currentCell);

        UnmarkAll(currentCellIndex);
        markedCells = new List<string>();

        int row = currentCellIndex / 8;
        int colIndex = currentCellIndex % 8;
        int[] rDelta = { -1, 0, 1, -1, 1, -1, 0, 1 };
        int[] cDelta = { 1, 1, 1, 0, 0, -1, -1, -1 };
        
        for (int i = 0; i < rDelta.Length; i++)
        {
            int r = row + rDelta[i];
            int c = colIndex + cDelta[i];
            int cellIndex = r * 8 + c;

            if (IsIndexValid(r, 0, 8) && IsIndexValid(c, 0, 8) && IsIndexValid(cellIndex, 0, cells.Count))
            {
                Tuple<Piece, PieceColor> pieceOnCell = GetPieceOnCell(cellIndex);

                if (pieceOnCell == nullPiece) MarkCell(r, c, false);
                else if (pieceOnCell.Item2 != currentPieceColor) MarkCell(r, c, true);
            }
        }

        // need to consider castles positions
        if (currentPieceColor == PieceColor.White)
        {
            Debug.Log("Checking castles position for white king");
            CheckForCastles_WhiteKing(currentCell.name);
        }
        else CheckForCastles_BlackKing(currentCell.name);
    }

    private static void CheckForCastles_WhiteKing(string currentCellName)
    {
        // if the king is on E1
        // and the white rook is either on H1, or A1
        // and no pieces in between
        if (currentCellName != "E1") return;

        Tuple<Piece, PieceColor> rightCornerPiece = GetPieceOnCell(FindCellOfName("H1"));
        Tuple<Piece, PieceColor> leftCornerPiece = GetPieceOnCell(FindCellOfName("A1"));

        if (rightCornerPiece.Item1 == Piece.Rook && rightCornerPiece.Item2 == PieceColor.White)
        {
            Debug.Log("There is a white rook on H1");
            // I need to make sure that there are no pieces on F1, and G1
            string[] cellsToCheck = { "F1", "G1" };
            bool isValidForCastles = true;

            for (int i = 0; i < cellsToCheck.Length && isValidForCastles; i++)
            {
                var piece = GetPieceOnCell(FindCellOfName(cellsToCheck[i]));
                if (piece != nullPiece) isValidForCastles = false;
            }

            if (isValidForCastles)
            {
                Debug.Log("Valid for short castles");
                // then I will mark G1
                int cellIndex = FindCellOfName("G1");
                int r = cellIndex / 8;
                int c = cellIndex % 8;
                MarkCell(r, c, false);
            }
            else Debug.Log("Invalid for short castles");
        }

        if (leftCornerPiece.Item1 == Piece.Rook && leftCornerPiece.Item2 == PieceColor.White)
        {
            // I need to make sure that there are no pieces on B1, C1, D1
            string[] cellsToCheck = { "B1", "C1", "D1" };
            bool isValidForCastles = true;

            for (int i = 0; i < cellsToCheck.Length && isValidForCastles; i++)
            {
                var piece = GetPieceOnCell(FindCellOfName(cellsToCheck[i]));
                if (piece != nullPiece) isValidForCastles = false;
            }

            if (isValidForCastles)
            {
                // then I will mark C1
                int cellIndex = FindCellOfName("C1");
                int r = cellIndex / 8;
                int c = cellIndex % 8;
                MarkCell(r, c, false);
            }
        }
    }

    private static void CheckForCastles_BlackKing(string currentCellName)
    {

    }

    private static void MarkRook(GameObject currentCell)
    {
        int currentCellIndex = cells.IndexOf(currentCell);

        UnmarkAll(currentCellIndex);
        markedCells = new List<string>();

        int row = currentCellIndex / 8;
        int colIndex = currentCellIndex % 8;
        // vertical downwards
        bool stop = false;
        for (int c = colIndex - 1; IsIndexValid(c, 0, 8) && !stop; c--)
        {
            Tuple<Piece, PieceColor> pieceOnCell = GetPieceOnCell(row * 8 + c);

            if (pieceOnCell == nullPiece) MarkCell(row, c, false);
            else
            {
                if (pieceOnCell.Item2 != currentPieceColor) MarkCell(row, c, true);
                stop = true;
            }
        }

        // vertical upwards
        stop = false;
        for (int c = colIndex + 1; IsIndexValid(c, 0, 8) && !stop; c++)
        {
            Tuple<Piece, PieceColor> pieceOnCell = GetPieceOnCell(row * 8 + c);

            if (pieceOnCell == nullPiece) MarkCell(row, c, false);
            else
            {
                if (pieceOnCell.Item2 != currentPieceColor) MarkCell(row, c, true);
                stop = true;
            }
        }

        // horizontal left
        stop = false;
        for (int r = row - 1; IsIndexValid(r, 0, 8) && !stop; r--)
        {
            Tuple<Piece, PieceColor> pieceOnCell = GetPieceOnCell(r * 8 + colIndex);

            if (pieceOnCell == nullPiece) MarkCell(r, colIndex, false);
            else
            {
                if (pieceOnCell.Item2 != currentPieceColor) MarkCell(r, colIndex, true);
                stop = true;
            }
        }

        // horizontal right
        stop = false;
        for (int r = row + 1; IsIndexValid(r, 0, 8) && !stop; r++)
        {
            Tuple<Piece, PieceColor> pieceOnCell = GetPieceOnCell(r * 8 + colIndex);

            if (pieceOnCell == nullPiece) MarkCell(r, colIndex, false);
            else
            {
                if (pieceOnCell.Item2 != currentPieceColor) MarkCell(r, colIndex, true);
                stop = true;
            }
        }
    }

    private static void MarkKnight(GameObject currentCell)
    {
        int currentCellIndex = cells.IndexOf(currentCell);

        UnmarkAll(currentCellIndex);
        markedCells = new List<string>();

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
            int cellIndex = r * 8 + c;

            if (IsIndexValid(r, 0, 8) && IsIndexValid(c, 0, 8) && IsIndexValid(cellIndex, 0, cells.Count))
            {
                Tuple<Piece, PieceColor> pieceOnCell = GetPieceOnCell(cellIndex);

                if (pieceOnCell == nullPiece) MarkCell(r, c, false);
                else if (pieceOnCell.Item2 != currentPieceColor) MarkCell(r, c, true);
            }
        }
    }

    private static void MarkBishop(GameObject currentCell)
    {
        int currentCellIndex = cells.IndexOf(currentCell);

        UnmarkAll(currentCellIndex);
        markedCells = new List<string>();

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
        for (int r = startRow, c = startCol; IsIndexValid(r, 0, 8) && IsIndexValid(c, 0, 8) && !stop; r += rowIncrement, c += colIncrement)
        {
            Tuple<Piece, PieceColor> pieceOnCell = GetPieceOnCell(r * 8 + c);

            if (pieceOnCell == nullPiece) MarkCell(r, c, false);
            else
            {
                if (pieceOnCell.Item2 != currentPieceColor) MarkCell(r, c, true);
                stop = true;
            }
        }
    }
    
    private static void MarkPawn(GameObject currentCell, bool invertDirection)
    {
        int currentCellIndex = cells.IndexOf(currentCell);

        UnmarkAll(currentCellIndex);
        markedCells = new List<string>();

        int row = currentCellIndex / 8;
        int colIndex = currentCellIndex % 8;
        int[] rowAdds = { -1, +1 };
        int columnDelta = (invertDirection) ? -1 : 1;

        // the cell in front
        Tuple<Piece, PieceColor> pieceOnCell = GetPieceOnCell(row * 8 + colIndex + columnDelta);
        if (pieceOnCell == nullPiece) MarkCell(row, colIndex + columnDelta, false);

        // the first move that the pawn makes, can skip one cell.
        if (currentPieceColor == PieceColor.White && currentCell.name.Contains("2"))
        {
            pieceOnCell = GetPieceOnCell(row * 8 + colIndex + 2);

            if (pieceOnCell == nullPiece) MarkCell(row, colIndex + 2, false);
            else if (pieceOnCell.Item2 != currentPieceColor) MarkCell(row, colIndex + 2, true);
        }
        else if (currentPieceColor == PieceColor.Black && currentCell.name.Contains("7"))
        {
            pieceOnCell = GetPieceOnCell(row * 8 + colIndex - 2);

            if (pieceOnCell == nullPiece) MarkCell(row, colIndex - 2, false);
            else if (pieceOnCell.Item2 != currentPieceColor) MarkCell(row, colIndex - 2, true);
        }

        // the cells on the diagonals
        for (int i = 0; i < rowAdds.Length; i++)
        if (row + rowAdds[i] >= 0 && row + rowAdds[i] < 8)
        {
            pieceOnCell = GetPieceOnCell((row + rowAdds[i]) * 8 + colIndex + columnDelta);
            if (pieceOnCell.Item2 != PieceColor.NULL && pieceOnCell.Item2 != currentPieceColor) MarkCell(row + rowAdds[i], colIndex + columnDelta, true);
        }
    }

    private static Tuple<Piece, PieceColor> GetPieceOnCell(int cellIndex)
    {
        if (cells[cellIndex].GetComponent<Cell_Script>().HasAPiece())
        {
            Piece p = Piece.NULL;
            PieceColor c = PieceColor.NULL;

            string pName = cells[cellIndex].transform.GetChild(0).name;

            string[] parts = pName.Split('_');

             c = (parts[1] == "B" ? PieceColor.Black : PieceColor.White);

            if (parts[0].Contains("Pawn")) p = Piece.Pawn;
            else if (parts[0].Contains("Bishop")) p = Piece.Bishop;
            else if (parts[0].Contains("Rook")) p = Piece.Rook;
            else if (parts[0].Contains("Knight")) p = Piece.Knight;
            else if (parts[0].Contains("Queen")) p = Piece.Queen;
            else if (parts[0].Contains("King")) p = Piece.King;

            return new Tuple<Piece, PieceColor>(p, c);
        }
        return nullPiece;
    }

    public static bool CheckMove(string endCell)
    {
        return markedCells.Contains(endCell);
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

    private static void MarkCell(int row, int col, bool isCapturing)
    {
        int index = row * 8 + col;
        if (index >= 0 && index < cells.Count)
        {
            if (!isCapturing) cells[index].GetComponent<Cell_Script>().MarkForLegalCells();
            else cells[index].GetComponent<Cell_Script>().MarkForCaptureCells();
            markedCells.Add(cells[index].name);
        }
    }

    public static bool IsPromotionalMove(string cellName)
    {
        // if the piece is white, and the row is 8
        // if the piece is black, and the row is 1
        if (currentPiece == Piece.Pawn)
        {
            if (currentPieceColor == PieceColor.White)
            {
                return cellName.Contains("8");
            }
            else if (currentPieceColor == PieceColor.Black)
            {
                return cellName.Contains("1");
            }
        }
        return false;
    }

    public static void ShowPromotionalPanel()
    {
        PromotionalPanel.transform.GetChild(0).gameObject.SetActive(true);
        updatePromotionalPieces_Function((currentPieceColor == PieceColor.White));
    }

    public static void DisablePromotionalPanel()
    {
        PromotionalPanel.transform.GetChild(0).gameObject.SetActive(false);
    }
}
