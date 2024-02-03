using Photon.Pun;
using System;
using UnityEngine;
using UnityEngine.UI;

public class GamePUN : MonoBehaviour
{
    [SerializeField] private Text WhitePointsText;
    [SerializeField] private Text BlackPointsText;

    [SerializeField] private CapturedPiecesScript whitePiecesCaptured;
    [SerializeField] private CapturedPiecesScript blackPiecesCaptured;

    [SerializeField] private GameObject GameOverText;

    private bool myTurn = true;
    private int White_Points = 10;
    private int Black_Points = 10;
    private readonly int Pawn_Value = 1;
    private readonly int Bishop_Value = 3;
    private readonly int Knight_Value = 3;
    private readonly int Rook_Value = 5;
    private readonly int Queen_Value = 9;
    private bool gameOver = false;
    private PhotonView pv;
    private int color;

    private string whiteImmuneString = "";
    private string blackImmuneString = "";

    public void SetMyTurn(int c)
    {
        color = c; // 0 = white, 1 = black
        myTurn = (c == 0);
        whiteImmuneString = blackImmuneString = "";
        pv = GetComponent<PhotonView>();
    }

    public void SendRPC_SwitchTurn()
    {
        pv.RPC("SwitchTurn", RpcTarget.All);
    }

    public void SwitchTurn()
    {
        myTurn = !myTurn;

        WhitePointsText.text = "White points: " + White_Points;
        BlackPointsText.text = "Black points: " + Black_Points;

        if (!myTurn)
        {
            // white turns black
            blackImmuneString = "";
        }
        else
        {
            // black turns white
            whiteImmuneString = "";
        }

        MoveCheckerPUN.UnmarkAll();
    }

    public bool CanWhiteUseImmune()
    {
        return White_Points >= 4;
    }

    public bool CanBlackUseImmune()
    {
        return Black_Points >= 4;
    }

    public string GetWhiteImmuneCell()
    {
        return whiteImmuneString;
    }

    public string GetBlackImmuneCell()
    {
        return blackImmuneString;
    }

    private void AddPoints(int value, PieceColor color)
    {
        if (color == PieceColor.White) Black_Points += value;
        else White_Points += value;
    }

    public void DestroyUsed(Tuple<Piece, PieceColor> piece)
    {
        int value = 0;

        if (piece.Item1 == Piece.Pawn) value = Pawn_Value;
        if (piece.Item1 == Piece.Bishop) value = Bishop_Value;
        if (piece.Item1 == Piece.Knight) value = Knight_Value;
        if (piece.Item1 == Piece.Rook) value = Rook_Value;
        if (piece.Item1 == Piece.Queen) value = Queen_Value;

        if (myTurn) White_Points -= value;
        else Black_Points -= value;

        MoveCheckerPUN.ResetPowerupToggles();
    }

    public void ImmunityUsed(string cell)
    {
        if (myTurn)
        {
            White_Points -= 4;
            whiteImmuneString = cell;
        }
        else
        {
            Black_Points -= 4;
            blackImmuneString = cell;
        }

        MoveCheckerPUN.ResetPowerupToggles();
    }

    public bool IsGameOver()
    {
        return gameOver;
    }

    public void PieceCaptured(string pieceName)
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
            GameOverText.SetActive(true);
        }

        if (color == PieceColor.White) blackPiecesCaptured.AddPieceCaptured(piece);
        else if (color == PieceColor.Black) whitePiecesCaptured.AddPieceCaptured(piece);
    }

    public bool IsMyTurn()
    {
        return myTurn;
    }

    public bool IsMyPiece(string end)
    {
        return (end.Contains("_B") && color == 1) || (end.Contains("_W") && color == 0);
    }

    public int GetPoints()
    {
        if (myTurn) return White_Points;
        return Black_Points;
    }
}