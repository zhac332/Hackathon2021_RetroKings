using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CapturedPiecesScript : MonoBehaviour
{
    [SerializeField] private Sprite Rook_Sprite;
    [SerializeField] private Sprite Bishop_Sprite;
    [SerializeField] private Sprite Knight_Sprite;
    [SerializeField] private Sprite King_Sprite;
    [SerializeField] private Sprite Queen_Sprite;
    [SerializeField] private Sprite Pawn_Sprite;

    private List<Image> PiecesCaptured;
    private int index;

    private void Start()
    {
        if (name.Contains("White")) Game.SetWhitePiecesCaptured(this);
        else if (name.Contains("Black")) Game.SetBlackPiecesCaptured(this);

        PiecesCaptured = new List<Image>();
        index = 0;

        for (int i = 0; i < transform.childCount; i++)
        {
            Transform t = transform.GetChild(i);
            PiecesCaptured.Add(t.GetComponent<Image>());
        }

        for (int i = 0; i < PiecesCaptured.Count; i++)
        {
            PiecesCaptured[i].sprite = null;
            PiecesCaptured[i].gameObject.SetActive(false);
        }
    }

    public void AddPieceCaptured(Piece p)
    {
        PiecesCaptured[index].gameObject.SetActive(true);

        if (p == Piece.Pawn) PiecesCaptured[index].sprite = Pawn_Sprite;
        if (p == Piece.Bishop) PiecesCaptured[index].sprite = Bishop_Sprite;
        if (p == Piece.Rook) PiecesCaptured[index].sprite = Rook_Sprite;
        if (p == Piece.Knight) PiecesCaptured[index].sprite = Knight_Sprite;
        if (p == Piece.King) PiecesCaptured[index].sprite = King_Sprite;
        if (p == Piece.Queen) PiecesCaptured[index].sprite = Queen_Sprite;

        index++;
    }
}
