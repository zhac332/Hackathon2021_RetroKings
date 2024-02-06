using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class GameControlsScript : MonoBehaviour
{
    [SerializeField] private PieceDisplayScript PieceDisplay;

    [Header("For turns")]
    [SerializeField] private Sprite KingWhite;
    [SerializeField] private Sprite KingBlack;
    [SerializeField] private GameObject GameOverText; 

    [Header("For the promotion buttons")]
    [SerializeField] private Image QueenButton_Image;
    [SerializeField] private Image RookButton_Image;
    [SerializeField] private Image BishopButton_Image;
    [SerializeField] private Image KnightButton_Image;
    [SerializeField] private List<Sprite> WhitePieces;
    [SerializeField] private List<Sprite> BlackPieces;

    [Header("Only if in multiplayer")]
    [SerializeField] private GamePUN GameP;   

    private void Start()
    {
        if (PhotonNetwork.IsConnected)
        {
            MoveCheckerPUN.SetUpdatePromotionalPiecesFunction((white) => UpdateImages(white));
            MoveCheckerPUN.AcquireAllCells();
        }
        else
        {
            MoveChecker.SetUpdatePromotionalPiecesFunction((white) => UpdateImages(white));
            MoveChecker.AcquireAllCells();
        }

        Game.SetGameOverTrigger(SetGameOver);
    }

    public void SetDisplay(bool white)
    {
        PieceDisplay.SetPerspective(white);
    }

    public void SetGameOver()
    {
        GameOverText.SetActive(true);
    }

    public void DestroyButton_OnClick()
    {
        if (PhotonNetwork.IsConnected) MoveCheckerPUN.MarkDestroyableCells(GameP.GetPoints(), GameP.IsMyTurn());
        else MoveChecker.MarkDestroyableCells(Game.GetPoints(), Game.IsMyTurn());
    }

    public void ImmunityButton_OnClick()
    {
        if (PhotonNetwork.IsConnected) MoveCheckerPUN.MarkShieldableCells(GameP.GetPoints(), GameP.IsMyTurn());
        else MoveChecker.MarkShieldableCells(Game.GetPoints(), Game.IsMyTurn());
    }

    public void UpdateImages(bool isWhite)
    {
        QueenButton_Image.sprite = (isWhite) ? WhitePieces[0] : BlackPieces[0];
        RookButton_Image.sprite = (isWhite) ? WhitePieces[1] : BlackPieces[1];
        BishopButton_Image.sprite = (isWhite) ? WhitePieces[2] : BlackPieces[2];
        KnightButton_Image.sprite = (isWhite) ? WhitePieces[3] : BlackPieces[3];
    }

    public void QueenButton_OnClick()
    {
        Move.PromotionSelected(Piece.Queen);
    }

    public void RookButton_OnClick()
    {
        Move.PromotionSelected(Piece.Rook);
    }

    public void BishopButton_OnClick()
    {
        Move.PromotionSelected(Piece.Bishop);
    }

    public void KnightButton_OnClick()
    {
        Move.PromotionSelected(Piece.Knight);
    }
}
