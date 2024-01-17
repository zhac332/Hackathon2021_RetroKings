using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameControlsScript : MonoBehaviour
{
    [SerializeField] private Image QueenButton_Image;
    [SerializeField] private Image RookButton_Image;
    [SerializeField] private Image BishopButton_Image;
    [SerializeField] private Image KnightButton_Image;
    [SerializeField] private List<Sprite> WhitePieces;
    [SerializeField] private List<Sprite> BlackPieces;

    private void Start()
    {
        MoveChecker.SetUpdatePromotionalPiecesFunction((white) => UpdateImages(white));
    }

    public void UpdateImages(bool isWhite)
    {
        QueenButton_Image.sprite = (isWhite) ? WhitePieces[0] : BlackPieces[0];
        RookButton_Image.sprite = (isWhite) ? WhitePieces[1] : BlackPieces[1];
        BishopButton_Image.sprite = (isWhite) ? WhitePieces[2] : BlackPieces[2];
        KnightButton_Image.sprite = (isWhite) ? WhitePieces[3] : BlackPieces[3];
    }
}
