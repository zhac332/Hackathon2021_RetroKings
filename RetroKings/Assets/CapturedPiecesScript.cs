using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CapturedPiecesScript : MonoBehaviour
{
    [Tooltip("0 - King, 1 - Queen, 2 - Rook, 4 - Bishop, 6 - Knight, 8 - Pawn")]
    [SerializeField] private List<Image> PiecesCaptured;
    private int rookIndex = 2;
    private int bishopIndex = 4;
    private int knightIndex = 6;
    private int pawnIndex = 8;

    private void Start()
    {
        if (name.Contains("White")) Game.SetWhitePiecesCaptured(this);
        else if (name.Contains("Black")) Game.SetBlackPiecesCaptured(this);

        for (int i = 0; i < PiecesCaptured.Count; i++)
            PiecesCaptured[i].gameObject.SetActive(false);
    }

    public void AddPieceCaptured(Piece p)
    {
        int index = 0;

        if (p == Piece.Pawn)
        {
            index = pawnIndex;
            pawnIndex++;
        }
        else if (p == Piece.Rook)
        {
            index = rookIndex;
            rookIndex++;
        }
        else if (p == Piece.Bishop)
        {
            index = bishopIndex;
            bishopIndex++;
        }
        else if (p == Piece.Knight)
        {
            index = knightIndex;
            knightIndex++;
        }
        else if (p == Piece.Queen) index = 1;
        else if (p == Piece.King) index = 0;

        PiecesCaptured[index].gameObject.SetActive(true);
    }
}
