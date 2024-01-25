using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class HistoryMovesScript : MonoBehaviour
{
    [SerializeField] private GameObject LinePrefab;
    private ScrollRect scroll;
    private List<Tuple<string, string>> moves;
    private int blockIndex = 0;

    private void Start()
    {
        scroll = GetComponent<ScrollRect>();
        moves = new List<Tuple<string, string>>
        {
            new Tuple<string, string>("", "")
        };
        AddNewLine(1);
    }

    public void AddCaptureMove(string cell1, string cell2)
    {
        cell1 = char.ToLower(cell1[0]) + cell1[1..];
        cell2 = char.ToLower(cell2[0]) + cell2[1..];
        AddNewData(cell1 + "x" + cell2);
    }

    public void AddNormalMove(string cell1, string cell2)
    {
        cell1 = char.ToLower(cell1[0]) + cell1[1..];
        cell2 = char.ToLower(cell2[0]) + cell2[1..];
        AddNewData(cell1 + "-" + cell2);
    }
    
    public void AddCastlesMove(string cell1, string cell2, bool isLong)
    {
        cell1 = char.ToLower(cell1[0]) + cell1[1..];
        cell2 = char.ToLower(cell2[0]) + cell2[1..];
        string symbol = (isLong ? "-OO-" : "-O-");
        AddNewData(cell1 + symbol + cell2);
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
    }

    public void AddDestroyUse(string cell)
    {
        cell = char.ToLower(cell[0]) + cell[1..];
        AddNewData("D-" + cell);
    }

    public void AddImmunityMove(string cell)
    {
        cell = char.ToLower(cell[0]) + cell[1..];
        AddNewData("S-" + cell);
    }

    private void AddNewData(string data)
    {
        if (moves[moves.Count - 1].Item1 == "")
        {
            moves[moves.Count - 1] = new Tuple<string, string>(data, "");
            UpdateLastLine(moves.Count, data, "");
        }
        else
        {
            moves[moves.Count - 1] = new Tuple<string, string>(moves[moves.Count - 1].Item1, data);
            UpdateLastLine(data);
            moves.Add(new Tuple<string, string>("", ""));
            AddNewLine(moves.Count);
        }
    }

    private void UpdateLastLine(int count, string move1, string move2)
    {
        var go = scroll.content.transform.GetChild(moves.Count - 1);
        go.GetComponent<LineScript>().SetNr(count);
        go.GetComponent<LineScript>().SetMove1(move1);
        go.GetComponent<LineScript>().SetMove2(move2);
    }

    private void UpdateLastLine(string move2)
    {
        var go = scroll.content.transform.GetChild(moves.Count - 1);
        go.GetComponent<LineScript>().SetMove2(move2);
    }

    private void AddNewLine(int count)
    {
        var go = Instantiate(LinePrefab, scroll.content.transform);
        go.GetComponent<LineScript>().SetNr(count);
        go.GetComponent<LineScript>().SetMove1("");
        go.GetComponent<LineScript>().SetMove2("");
    }
}
