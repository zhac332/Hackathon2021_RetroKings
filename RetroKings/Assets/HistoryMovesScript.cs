using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class MoveString
{
    public Text Nr;
    public Text Move1;
    public Text Move2;
}

public class HistoryMovesScript : MonoBehaviour
{
    [SerializeField] private List<MoveString> Moves;
    private List<string> moves;

    private void Start()
    {
        moves = new List<string>();

        for (int i = 0; i < Moves.Count; i++)
        {
            Moves[i].Nr.text = (i + 1).ToString();
            Moves[i].Move1.text = Moves[i].Move2.text = "";
        }
    }

    public void AddCaptureMove(string cell1, string cell2)
    {
        cell1 = char.ToLower(cell1[0]) + cell1[1..];
        cell2 = char.ToLower(cell2[0]) + cell2[1..];
        moves.Add(cell1 + "x" + cell2);
        UpdateStrings();
    }

    public void AddNormalMove(string cell1, string cell2)
    {
        cell1 = char.ToLower(cell1[0]) + cell1[1..];
        cell2 = char.ToLower(cell2[0]) + cell2[1..];
        moves.Add(cell1 + "-" + cell2);
        UpdateStrings();
    }
    
    public void AddCastlesMove(string cell1, string cell2, bool isLong)
    {
        cell1 = char.ToLower(cell1[0]) + cell1[1..];
        cell2 = char.ToLower(cell2[0]) + cell2[1..];
        string symbol = (isLong ? "-OO-" : "-O-");
        moves.Add(cell1 + symbol + cell2);
        UpdateStrings();
    }

    public void AddPromotionMove(string cell1, string cell2, Piece p)
    {
        cell1 = char.ToLower(cell1[0]) + cell1[1..];
        cell2 = char.ToLower(cell2[0]) + cell2[1..];
        string pieceIcon = "";

        if (p == Piece.Knight) pieceIcon = "N";
        if (p == Piece.Bishop) pieceIcon = "B";
        if (p == Piece.Rook) pieceIcon = "R";
        if (p == Piece.Queen) pieceIcon = "Q";

        moves.Add(cell1 + "-" + cell2 + "^" + pieceIcon);
        UpdateStrings();
    }

    public void AddDestroyUse(string cell)
    {
        cell = char.ToLower(cell[0]) + cell[1..];
        moves.Add("D-" + cell);
        UpdateStrings();
    }

    public void AddImmunityMove(string cell)
    {
        cell = char.ToLower(cell[0]) + cell[1..];
        moves.Add("S-" + cell);
        UpdateStrings();
    }

    private void UpdateStrings()
    {
        int textIndex = -1;
        for (int i = 0; i < moves.Count; i ++)
            if (i % 2 == 0)
            {
                textIndex++;
                Moves[textIndex].Nr.text = textIndex.ToString();
                Moves[textIndex].Move1.text = moves[i];
            }
            else
            {
                Moves[textIndex].Move2.text = moves[i];
            }
    }
}
