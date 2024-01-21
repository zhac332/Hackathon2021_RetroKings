using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameControlsScript : MonoBehaviour
{
    [Header("For turns")]
    [SerializeField] private Sprite KingWhite;
    [SerializeField] private Sprite KingBlack;
    [SerializeField] private Image TurnImage;
    [SerializeField] private GameObject GameOverText; 

    [Header("For the promotion buttons")]
    [SerializeField] private Image QueenButton_Image;
    [SerializeField] private Image RookButton_Image;
    [SerializeField] private Image BishopButton_Image;
    [SerializeField] private Image KnightButton_Image;
    [SerializeField] private List<Sprite> WhitePieces;
    [SerializeField] private List<Sprite> BlackPieces;
    private bool destroyButton_Toggle = false;
    private bool immunityButton_Toggle = false;

    private void Start()
    {
        MoveChecker.SetUpdatePromotionalPiecesFunction((white) => UpdateImages(white));
        Game.SetDisplayTurnFunction(DisplayTurn);
        Game.SetGameOverTrigger(SetGameOver);
        DisplayTurn();
    }

    public void SetGameOver()
    {
        GameOverText.SetActive(true);
    }

    public void DestroyButton_OnClick()
    {
        destroyButton_Toggle = !destroyButton_Toggle;
        if (destroyButton_Toggle)
        {
            immunityButton_Toggle = false;
            MoveChecker.MarkDestroyableCells(Game.GetPoints(), Game.IsMyTurn());
        }
        else MoveChecker.UnmarkAll();
    }

    public void ImmunityButton_OnClick()
    {
        immunityButton_Toggle = !immunityButton_Toggle;
        if (immunityButton_Toggle)
        {
            destroyButton_Toggle = false;
            MoveChecker.MarkShieldableCells(Game.GetPoints(), Game.IsMyTurn());
        }
        else MoveChecker.UnmarkAll();
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

    public void DisplayTurn()
    {
        TurnImage.sprite = (Game.IsMyTurn()) ? KingWhite : KingBlack;
    }
}
