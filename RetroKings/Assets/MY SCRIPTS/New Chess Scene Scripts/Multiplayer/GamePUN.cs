using Photon.Pun;
using System;
using UnityEngine;
using UnityEngine.UI;

public class GamePUN : MonoBehaviour
{
    [SerializeField] private HistoryOfMovesPUN historyOfMovesPUN;

    [SerializeField] private Text WhitePointsText;
    [SerializeField] private Text BlackPointsText;

    [SerializeField] private CapturedPiecesScript whitePiecesCaptured;
    [SerializeField] private CapturedPiecesScript blackPiecesCaptured;

    [SerializeField] private GameObject GameOverText;

    [SerializeField] private Text TurnText;

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
    private bool whiteSkip = false;
    private string blackImmuneString = "";
    private bool blackSkip = false;

    public void SetMyTurn(int c)
    {
        color = c; // 0 = white, 1 = black
        myTurn = (c == 0);
        TurnText.text = (myTurn ? "Your turn!" : "Waiting on your opponent.");
        whiteImmuneString = blackImmuneString = "";
        pv = GetComponent<PhotonView>();
        historyOfMovesPUN.Initialize();
    }

    public void SendRPC_SwitchTurn()
    {
        pv.RPC("SwitchTurn", RpcTarget.All);
    }

    [PunRPC]
    public void SwitchTurn()
    {
        myTurn = !myTurn;

        WhitePointsText.text = "White points: " + White_Points;
        BlackPointsText.text = "Black points: " + Black_Points;

        TurnText.text = (myTurn ? "Your turn!" : "Waiting on your opponent.");

        if (blackSkip)
        {
            blackSkip = false;
        }
        else blackImmuneString = "";

        if (whiteSkip)
        {
            whiteSkip = false;
        }
        else whiteImmuneString = "";

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

        WhitePointsText.text = "White points: " + White_Points;
        BlackPointsText.text = "Black points: " + Black_Points;
    }

    public void SendRPC_DestroyUsed(Tuple<Piece, PieceColor> piece)
    {
        pv.RPC("DestroyUsed", RpcTarget.Others, piece.Item1.ToString(), piece.Item2.ToString());
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

    /// A duplicate of the original DestroyUsed method simply because PunRPC doesn't like when putting a Tuple as parameter.
    [PunRPC]
    public void DestroyUsed(string pi, string pC)
    {
        Tuple<Piece, PieceColor> piece = new Tuple<Piece, PieceColor>(EnumParser.ParsePiece(pi), EnumParser.ParsePieceColor(pC));

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

    public void SendRPC_ImmunityUsed(string cell)
    {
        pv.RPC("ImmunityUsed", RpcTarget.Others, cell, true);
    }

    [PunRPC]
    public void ImmunityUsed(string cell, bool rpced)
    {
        // if it is rpced, that means I didn't use it.
        if (rpced)
        {
            // the opponent used it.
            if (color == 0)
            {
                Black_Points -= 4;
                blackImmuneString = cell;
                blackSkip = true;
            }
            else
            {
                White_Points -= 4;
                whiteImmuneString = cell;
                whiteSkip = true;
            }
        }
        else
        {
            // I used it.
            if (color == 0)
            {
                White_Points -= 4;
                whiteImmuneString = cell;
                whiteSkip = true;
            }
            else
            {
                Black_Points -= 4;
                blackImmuneString = cell;
                blackSkip = true;
            }
        }

        MoveCheckerPUN.ResetPowerupToggles();
    }

    public bool IsGameOver()
    {
        return gameOver;
    }

    public void PieceCaptured(string pieceName, bool addPoints)
    {
        PieceColor color = (pieceName.Contains("W") ? PieceColor.White : PieceColor.Black);
        Piece piece = Piece.NULL;

        if (pieceName.Contains("Pawn"))
        {
            piece = Piece.Pawn;
            if (addPoints) AddPoints(Pawn_Value, color);
        }
        else if (pieceName.Contains("Bishop"))
        {
            piece = Piece.Bishop;
            if (addPoints) AddPoints(Bishop_Value, color);
        }
        else if (pieceName.Contains("Rook"))
        {
            piece = Piece.Rook;
            if (addPoints) AddPoints(Rook_Value, color);
        }
        else if (pieceName.Contains("Knight"))
        {
            piece = Piece.Knight;
            if (addPoints) AddPoints(Knight_Value, color);
        }
        else if (pieceName.Contains("Queen"))
        {
            piece = Piece.Queen;
            if (addPoints) AddPoints(Queen_Value, color);
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

    public bool AmBlack()
    {
        return color == 1;
    }

    public bool AmWhite()
    {
        return color == 0;
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
