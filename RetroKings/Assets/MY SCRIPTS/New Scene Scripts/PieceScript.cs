using System;
using Unity.Burst.CompilerServices;
using UnityEngine;

[Serializable]
public enum Piece
{
    NULL,
    Pawn,
    Bishop,
    Knight,
    Rook,
    Queen,
    King
}

[Serializable]
public enum PieceColor
{
    NULL,
    White,
    Black
}

public class PieceScript : MonoBehaviour
{
    [SerializeField] private Piece Name;
    [SerializeField] private PieceColor Color;
    [SerializeField] private Color SelectedColor;
    [SerializeField] private Color UnselectedColor;
    private string currentCell;
    private SpriteRenderer sr;

    private void Start()
    {
        if (name[name.Length - 1] == 'B') Color = PieceColor.Black;
        else Color = PieceColor.White;

        sr = GetComponent<SpriteRenderer>();
        currentCell = transform.parent.name;
        char firstChar = char.ToLower(currentCell[0]);
        currentCell = firstChar + currentCell[1..];
    }

    private void Select()
    {
        sr.color = SelectedColor;
    }

    private void Deselect()
    {
        sr.color = UnselectedColor;
    }

    private void Piece_OnClick()
    {
        // if there is a first point selected, then I will select that
        if (!Move.IsFirstCellSelected())
        {
            Select();
            Move.SelectPiece(currentCell, Name, Color);
        }
        else if (Move.IsFirstCellSelected() && !Move.IsSecondCellSelected())
        {
            Deselect();
            Move.SelectPiece(currentCell, Name, Color);
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 raycastPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(raycastPosition, Vector2.zero);

            if (hit.collider != null)
            {
                if (hit.collider.name == name)
                {
                    Piece_OnClick();
                }
            }
        }
    }
}
