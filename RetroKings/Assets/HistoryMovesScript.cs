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

[Serializable]
public class MoveString_Text
{
    public string Move1;
    public string Move2;

    public MoveString_Text(string a, string b)
    {
        Move1 = a;
        Move2 = b;
    }
}

public class HistoryMovesScript : MonoBehaviour
{
    [SerializeField] private List<MoveString> Moves;
    [SerializeField] private int NrLinesDisplayable = 19;
    private List<MoveString_Text> moves;
    private int blockIndex = 0;

    private void Start()
    {
        moves = new List<MoveString_Text>();
        moves.Add(new MoveString_Text("", ""));
        blockIndex = 0;

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
        AddNewData(cell1 + "x" + cell2);
        UpdateBlockIndex();
        UpdateStrings();
    }

    public void AddNormalMove(string cell1, string cell2)
    {
        cell1 = char.ToLower(cell1[0]) + cell1[1..];
        cell2 = char.ToLower(cell2[0]) + cell2[1..];
        AddNewData(cell1 + "-" + cell2);
        UpdateBlockIndex();
        UpdateStrings();
    }
    
    public void AddCastlesMove(string cell1, string cell2, bool isLong)
    {
        cell1 = char.ToLower(cell1[0]) + cell1[1..];
        cell2 = char.ToLower(cell2[0]) + cell2[1..];
        string symbol = (isLong ? "-OO-" : "-O-");
        AddNewData(cell1 + symbol + cell2);
        UpdateBlockIndex();
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

        AddNewData(cell1 + "-" + cell2 + "^" + pieceIcon);
        UpdateBlockIndex();
        UpdateStrings();
    }

    public void AddDestroyUse(string cell)
    {
        cell = char.ToLower(cell[0]) + cell[1..];
        AddNewData("D-" + cell);
        UpdateBlockIndex();
        UpdateStrings();
    }

    public void AddImmunityMove(string cell)
    {
        cell = char.ToLower(cell[0]) + cell[1..];
        AddNewData("S-" + cell);
        UpdateBlockIndex();
        UpdateStrings();
    }

    private void AddNewData(string data)
    {
        if (moves[moves.Count - 1].Move1 == "") moves[moves.Count - 1].Move1 = data;
        else
        {
            moves[moves.Count - 1].Move2 = data;
            moves.Add(new MoveString_Text("", ""));
        }
    }

    private void UpdateBlockIndex()
    {
        blockIndex = (moves.Count - 1) / NrLinesDisplayable;
    }

    public void UpButton_OnClick()
    {
        Debug.Log("up");
        blockIndex--;
        if (blockIndex < 0) blockIndex = 0;
        UpdateStrings();
    }

    public void DownButton_OnClick()
    {
        Debug.Log("down");
        blockIndex++;
        if (blockIndex * NrLinesDisplayable >= moves.Count) blockIndex--;
        UpdateStrings();
    }

    private void UpdateStrings()
    {
        for (int i = 0; i < Moves.Count; i++)
        {
            int index = blockIndex * NrLinesDisplayable + i;

            Moves[i].Nr.text = (index + 1).ToString();

            if (index < moves.Count)
            {
                Moves[i].Move1.text = moves[index].Move1;
                Moves[i].Move2.text = moves[index].Move2;
            }
            else
            {
                Moves[i].Move1.text = "";
                Moves[i].Move2.text = "";
            }
        }

    }
}
