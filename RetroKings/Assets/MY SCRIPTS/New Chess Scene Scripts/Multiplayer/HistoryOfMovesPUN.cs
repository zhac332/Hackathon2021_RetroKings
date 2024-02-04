using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using System.Runtime.CompilerServices;

public class HistoryOfMovesPUN : MonoBehaviour
{
    [SerializeField] private GameObject LinePrefab;
    [SerializeField] private Vector3 StartPos;
    [SerializeField] private ScrollRect scroll;
    [SerializeField] private RectTransform contentRectTransform;
    [SerializeField] private PhotonView pv;

    private List<Tuple<string, string>> moves;

    private void Start()
    {
        moves = new List<Tuple<string, string>>();
    }

    public void Initialize()
    {
        moves = new List<Tuple<string, string>>
        {
            new Tuple<string, string>("", "")
        };
        AddNewLine(1);
    }

    [PunRPC]
    public void AddCaptureMove(string cell1, string cell2, bool rpced)
    {
        cell1 = char.ToLower(cell1[0]) + cell1[1..];
        cell2 = char.ToLower(cell2[0]) + cell2[1..];
        AddNewData(cell1 + "x" + cell2);
        if (!rpced) pv.RPC("AddCaptureMove", RpcTarget.Others, cell1, cell2, true);
    }

    [PunRPC]
    public void AddNormalMove(string cell1, string cell2, bool rpced)
    {
        cell1 = char.ToLower(cell1[0]) + cell1[1..];
        cell2 = char.ToLower(cell2[0]) + cell2[1..];
        AddNewData(cell1 + "-" + cell2);
        if (!rpced) pv.RPC("AddNormalMove", RpcTarget.Others, cell1, cell2, true);
    }

    [PunRPC]
    public void AddCastlesMove(string cell1, string cell2, bool isLong, bool rpced)
    {
        cell1 = char.ToLower(cell1[0]) + cell1[1..];
        cell2 = char.ToLower(cell2[0]) + cell2[1..];
        string symbol = (isLong ? "-OO-" : "-O-");
        AddNewData(cell1 + symbol + cell2);
        if (!rpced) pv.RPC("AddCastlesMove", RpcTarget.Others, cell1, cell2, isLong, true);
    }

    [PunRPC]
    public void AddPromotionMove(string cell1, string cell2, Piece p, bool rpced)
    {
        cell1 = char.ToLower(cell1[0]) + cell1[1..];
        cell2 = char.ToLower(cell2[0]) + cell2[1..];
        string pieceIcon = "";

        if (p == Piece.Knight) pieceIcon = "N";
        if (p == Piece.Bishop) pieceIcon = "B";
        if (p == Piece.Rook) pieceIcon = "R";
        if (p == Piece.Queen) pieceIcon = "Q";

        AddNewData(cell1 + "-" + cell2 + "^" + pieceIcon);
        if (!rpced) pv.RPC("AddPromotionMove", RpcTarget.Others, cell1, cell2, p, true);
    }

    [PunRPC]
    public void AddDestroyUse(string cell, bool rpced)
    {
        cell = char.ToLower(cell[0]) + cell[1..];
        AddNewData("D-" + cell);
        if (!rpced) pv.RPC("AddDestroyUse", RpcTarget.Others, cell, true);
    }

    [PunRPC]
    public void AddImmunityMove(string cell, bool rpced)
    {
        cell = char.ToLower(cell[0]) + cell[1..];
        AddNewData("S-" + cell);
        if (!rpced) pv.RPC("AddImmunityMove", RpcTarget.Others, cell, true);
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

    private void Shift(float height)
    {
        for (int i = 0; i < scroll.content.transform.childCount; i++)
        {
            var tr = scroll.content.transform.GetChild(i).GetComponent<RectTransform>();
            var pos = tr.anchoredPosition;
            pos.y += (height / 2);
            tr.anchoredPosition = pos;
        }
        StartPos = new Vector3(StartPos.x, StartPos.y + 15f, 0f);
    }

    private void AddNewLine(int count)
    {
        var go = Instantiate(LinePrefab, scroll.content.transform);
        RectTransform rt = go.GetComponent<RectTransform>();

        // Adjust the Y position based on the number of existing lines
        float yOffset = StartPos.y - (count - 1) * rt.rect.height;
        rt.anchoredPosition = new Vector2(0f, yOffset);

        if (moves.Count >= 8)
        {
            // Update the size of the content rectangle
            float newHeight = contentRectTransform.sizeDelta.y + rt.rect.height;
            contentRectTransform.sizeDelta = new Vector2(contentRectTransform.sizeDelta.x, newHeight);

            Vector3 pos = contentRectTransform.position;
            pos.y += rt.rect.height / 2;
            contentRectTransform.position = pos;

            Shift(rt.rect.height);


            Canvas.ForceUpdateCanvases();
            scroll.verticalNormalizedPosition = 0f;
        }

        go.GetComponent<LineScript>().SetNr(count);
        go.GetComponent<LineScript>().SetMove1("");
        go.GetComponent<LineScript>().SetMove2("");
    }
}
