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
    
}
