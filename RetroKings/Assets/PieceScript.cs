using System;
using Unity.Burst.CompilerServices;
using UnityEngine;

[Serializable]
public enum Piece
{
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
    White,
    Black
}

public class PieceScript : MonoBehaviour
{
    [SerializeField] private Piece Name;
    [SerializeField] private PieceColor Color;
    private string startCell;

    private void Start()
    {
        startCell = transform.parent.name;
        char firstChar = char.ToLower(startCell[0]);
        startCell = firstChar + startCell[1..];
    }

    private void Piece_OnClick(string str)
    {
        Debug.Log("Clicked on a non-UI object: " + str);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 raycastPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(raycastPosition, Vector2.zero);

            if (hit.collider != null)
            {
                Piece_OnClick(hit.collider.gameObject.name);
            }
        }
    }
}
