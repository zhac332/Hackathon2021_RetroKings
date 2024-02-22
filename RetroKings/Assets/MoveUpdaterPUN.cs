using UnityEngine;
using Photon.Pun;
using System;

public class MoveUpdaterPUN : MonoBehaviour
{
    [SerializeField] private GameObject BombPrefab;
    [SerializeField] private GameObject ShieldPrefab;

    [SerializeField] private SoundPlayerScript SoundPlayer;
    [SerializeField] private GamePUN GameP;

    [Header("Sprites for pieces")]
    [SerializeField] private Sprite Queen_White;
    [SerializeField] private Sprite Rook_White;
    [SerializeField] private Sprite Bishop_White;
    [SerializeField] private Sprite Knight_White;
    [SerializeField] private Sprite Queen_Black;
    [SerializeField] private Sprite Rook_Black;
    [SerializeField] private Sprite Bishop_Black;
    [SerializeField] private Sprite Knight_Black;

    [Header("Colors")]
    [SerializeField] private Color White_ShieldedCell_Color;
    [SerializeField] private Color Black_ShieldedCell_Color;

    private PhotonView pv;

    private void Start()
    {
        pv = GetComponent<PhotonView>();
    }

    private void UpdatePieceDisplay(GameObject piece, Tuple<Piece, PieceColor> pieceData)
    {
        SpriteRenderer sr = piece.GetComponent<SpriteRenderer>();

        if (pieceData.Item2 == PieceColor.White)
        {
            if (pieceData.Item1 == Piece.Queen)
            {
                sr.sprite = Queen_White;
                sr.gameObject.name = "Queen_W";
            }
            if (pieceData.Item1 == Piece.Rook)
            {
                sr.sprite = Rook_White;
                sr.gameObject.name = "Rook_W";
            }
            if (pieceData.Item1 == Piece.Bishop)
            {
                sr.sprite = Bishop_White;
                sr.gameObject.name = "Bishop_W";
            }
            if (pieceData.Item1 == Piece.Knight)
            {
                sr.sprite = Knight_White;
                sr.gameObject.name = "Knight_W";
            }
        }
        else if (pieceData.Item2 == PieceColor.Black)
        {
            if (pieceData.Item1 == Piece.Queen)
            {
                sr.sprite = Queen_Black;
                sr.gameObject.name = "Queen_B";
            }
            if (pieceData.Item1 == Piece.Rook)
            {
                sr.sprite = Rook_Black;
                sr.gameObject.name = "Rook_B";
            }
            if (pieceData.Item1 == Piece.Bishop)
            {
                sr.sprite = Bishop_Black;
                sr.gameObject.name = "Bishop_B";
            }
            if (pieceData.Item1 == Piece.Knight)
            {
                sr.sprite = Knight_Black;
                sr.gameObject.name = "Knight_B";
            }
        }
    }

    [PunRPC]
    public void ExecutedMove(string cell1, string cell2, string pi, string pC)
    {
        // I need to hold the child piece from cell1,
        // I need to remove the child piece from cell2, if there is one
        // and put the piece from cell1 to cell2
        Tuple<Piece, PieceColor> piece = new Tuple<Piece, PieceColor>(EnumParser.ParsePiece(pi), EnumParser.ParsePieceColor(pC));
        GameObject c1 = GameObject.Find(cell1);
        GameObject c2 = GameObject.Find(cell2);
        GameObject p = c1.transform.GetChild(0).gameObject;
        bool isCaptured = false;

        UpdatePieceDisplay(p, piece);

        c1.GetComponent<CellPUN_Script>().DeselectCell();

        if (c2.transform.childCount != 0)
        {
            GameObject go = c2.transform.GetChild(0).gameObject;
            GameP.PieceCaptured(go.name, true, null, false);
            Destroy(go);
            isCaptured = true;
        }

        if (isCaptured) SoundPlayer.PlayPieceCaptureSound();
        else SoundPlayer.PlayPieceMoveSound();

        p.transform.SetParent(c2.transform);

        Debug.Log("Moved " + p.name + " from " + c1.name + " to " + c2.name);

        p.transform.localPosition = new Vector3(0f, 0f, 0f);

        Move.ResetMove();
        MoveCheckerPUN.UnmarkAll();
        MoveCheckerPUN.UpdateCastlingPossibilities(piece, cell1);
    }

    [PunRPC]
    public void ExecutedMove_DestroyFeature(string cell)
    {
        GameObject c = GameObject.Find(cell);
        GameObject p = c.transform.GetChild(0).gameObject;
        Tuple<Piece, PieceColor> piece = PieceChecker.TransformGoInTuple(p);

        Instantiate(BombPrefab, p.transform.position, Quaternion.identity);

        UpdatePieceDisplay(p, piece);
        Destroy(p);

        SoundPlayer.PlayDestroySound();

        GameP.PieceCaptured(p.name, false, null, false);
        Move.ResetMove();
        MoveCheckerPUN.UnmarkAll();
        MoveCheckerPUN.UpdateCastlingPossibilities(piece, name);
    }

    [PunRPC]
    public void ExecutedMove_ImmunityFeature(string cell)
    {
        GameObject go = GameObject.Find(cell);
        if (GameP.AmWhite()) MarkShieldedCell_White(go.GetComponent<SpriteRenderer>());
        else MarkShieldedCell_Black(go.GetComponent<SpriteRenderer>());

        Instantiate(ShieldPrefab, go.transform.position, Quaternion.identity);
        SoundPlayer.PlayShieldSound();

        Move.ResetMove();
    }

    private void MarkShieldedCell_White(SpriteRenderer sr)
    {
        sr.color = White_ShieldedCell_Color;
    }

    private void MarkShieldedCell_Black(SpriteRenderer sr)
    {
        sr.color = Black_ShieldedCell_Color;
    }
}
