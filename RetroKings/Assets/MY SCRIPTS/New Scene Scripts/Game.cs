using System;
using UnityEngine;
using UnityEngine.UI;

public static class Game
{
    private static bool myTurn = true;
    private static Action displayTurn_Function;
    private static CapturedPiecesScript whitePiecesCaptured;
    private static CapturedPiecesScript blackPiecesCaptured;
    private static int White_Points = 10;
    private static int Black_Points = 10;
    private static readonly int Pawn_Value = 1;
    private static readonly int Bishop_Value = 3;
    private static readonly int Knight_Value = 3;
    private static readonly int Rook_Value = 5;
    private static readonly int Queen_Value = 9;
    private static Text PointsText;
    private static Action setGameOverTextOn;
    private static bool gameOver = false;

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

    public static void SetGameOverTrigger(Action t)
    {
        setGameOverTextOn = t;
    }

    public static void SwitchTurn()
    {
        myTurn = !myTurn;
        
        PointsText = GameObject.Find("MAIN CANVAS/Turn and Powerups Panel/PointsText").GetComponent<Text>();
        
        if (myTurn) PointsText.text = "Points: " + White_Points;
        else PointsText.text = "Points: " + Black_Points;
        
        displayTurn_Function();
    }

    private static void AddPoints(int value, PieceColor color)
    {
        if (color == PieceColor.White) Black_Points += value;
        else White_Points += value;
    }

    public static bool IsGameOver()
    {
        return gameOver;
    }

    public static void PieceCaptured(string pieceName)
    {
        PieceColor color = (pieceName.Contains("W") ? PieceColor.White : PieceColor.Black);
        Piece piece = Piece.NULL;

        if (pieceName.Contains("Pawn"))
        {
            piece = Piece.Pawn;
            AddPoints(Pawn_Value, color);
        }
        else if (pieceName.Contains("Bishop"))
        {
            piece = Piece.Bishop;
            AddPoints(Bishop_Value, color);
        }
        else if (pieceName.Contains("Rook"))
        {
            piece = Piece.Rook;
            AddPoints(Rook_Value, color);
        }
        else if (pieceName.Contains("Knight"))
        {
            piece = Piece.Knight;
            AddPoints(Knight_Value, color);
        }
        else if (pieceName.Contains("Queen"))
        {
            piece = Piece.Queen;
            AddPoints(Queen_Value, color);
        }
        else if (pieceName.Contains("King"))
        {
            piece = Piece.King;
            gameOver = true;
            setGameOverTextOn();
        }

        if (color == PieceColor.White) blackPiecesCaptured.AddPieceCaptured(piece);
        else if (color == PieceColor.Black) whitePiecesCaptured.AddPieceCaptured(piece);
    }

    public static bool IsMyTurn()
    {
        return myTurn;
    }
    
    public static int GetPoints()
    {
        if (myTurn) return White_Points;
        return Black_Points;
    }
}
