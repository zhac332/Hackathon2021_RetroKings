using System;
using UnityEngine;
using Photon.Pun;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class CellPUN_Script : MonoBehaviour
{
    [SerializeField] private SoundPlayerScript SoundPlayer;
    [SerializeField] private GamePUN GameP;

    [SerializeField] private HistoryOfMovesPUN ListOfMoves;

    [SerializeField] private PhotonView moveUpdater;

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
    [SerializeField] private Color DestroyableCell_Color;
    [SerializeField] private Color ShieldableCell_Color;
    [SerializeField] private Color White_ShieldedCell_Color;
    [SerializeField] private Color Black_ShieldedCell_Color;
    [SerializeField] private Color CapturingCell_Color;
    [SerializeField] private Color LegalCell_Color;
    [SerializeField] private Color Selected_Color;
    [SerializeField] private Color Default_Color;
    private SpriteRenderer sr;

    [Header("Prefabs")]
    [SerializeField] private GameObject BombPrefab;
    [SerializeField] private GameObject ShieldPrefab;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        MoveCheckerPUN.AcquireAllCells();
    }

    private void Cell_OnClick()
    {
        if (GameP.IsGameOver()) return;

        if (MoveCheckerPUN.IsDestroyPowerupOn())
        {
            if (MoveCheckerPUN.CheckMove(name))
            {
                ExecuteMove_DestroyFeature();
                return;
            }
        }

        if (MoveCheckerPUN.IsImmunityPowerupOn())
        {
            if (MoveCheckerPUN.CheckMove(name))
            {
                ExecuteMove_ImmunityFeature();
                return;
            }
        }

        if (!GameP.IsMyTurn())
        {
            SoundPlayer.PlayNoSound();
            Debug.LogError("not your turn!");
            return;
        }

        GameObject piece = null;
        if (!Move.IsFirstCellSelected())
        {
            if (!HasAPiece())
            {
                SoundPlayer.PlayNoSound();
                Debug.Log("Select a cell with a piece. If there is no piece, how do I know what move to make?");
                return;
            }

            if (!GameP.IsMyPiece(transform.GetChild(0).name))
            {
                SoundPlayer.PlayNoSound();
                Debug.LogError("Not your piece...");
                return;
            }

            // if (HasAPiece())....
            piece = transform.GetChild(0).gameObject;

            SelectCell();
            SoundPlayer.PlayPieceSelectSound();

            Move.SelectPiece(name, piece.name);
            MoveCheckerPUN.SetFirstPiece(piece.name);
            MoveCheckerPUN.MarkAvailableCells(gameObject);
            MoveCheckerPUN.ResetPowerupToggles();
        }
        else
        {
            if (Move.IsCellIdenticalWithFirst(name))
            {
                DeselectCell();
                SoundPlayer.PlayPieceDeselectSound();
                Move.SelectPiece();
                MoveCheckerPUN.UnmarkAll();
            }
            else if (!Move.IsSecondCellSelected())
            {
                if (MoveCheckerPUN.CheckMove(name))
                {
                    // this is where I should check if the end cell makes the pawn be promoted
                    if (MoveCheckerPUN.IsPromotionalMove(name))
                    {
                        MoveCheckerPUN.ShowPromotionalPanel();
                        Move.SelectCell_Promote(name, (cell1, cell2, piece, switchTurn) => ExecuteMove(cell1, cell2, piece, switchTurn));
                        SoundPlayer.PlayPromotionSound();
                    }
                    else
                    {
                        // just move like normal
                        piece = null;
                        if (transform.childCount != 0) piece = transform.GetChild(0).gameObject;

                        Move.SelectCell(name, (cell1, cell2, piece, switchTurn) => ExecuteMove(cell1, cell2, piece, switchTurn));
                    }
                }
                else Debug.LogWarning("Illegal move.");
            }
        }
    }

    public bool HasAPiece()
    {
        return transform.childCount != 0;
    }

    private void SelectCell()
    {
        sr.color = Selected_Color;
    }

    public void DeselectCell()
    {
        sr.color = Default_Color;
    }

    public void MarkForLegalCells()
    {
        sr.color = LegalCell_Color;
    }

    public void MarkForCaptureCells()
    {
        sr.color = CapturingCell_Color;
    }

    public void MarkForDestroyableCells()
    {
        sr.color = DestroyableCell_Color;
    }

    public void MarkForShieldableCells()
    {
        sr.color = ShieldableCell_Color;
    }

    public void MarkShieldedCell_White()
    {
        sr.color = White_ShieldedCell_Color;
        GameP.ImmunityUsed(name, false);
        GameP.SendRPC_ImmunityUsed(name);
    }

    public void MarkShieldedCell_Black()
    {
        sr.color = Black_ShieldedCell_Color;
        GameP.ImmunityUsed(name, false);
        GameP.SendRPC_ImmunityUsed(name);
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

    private bool IsLongCastling(string cell1, string cell2, Piece p)
    {
        if (p == Piece.King)
        {
            return ((cell1 == "E1" && cell2 == "C1") || (cell1 == "E8" && cell2 == "C8"));
        }
        return false;
    }

    private bool IsShortCastling(string cell1, string cell2, Piece p)
    {
        if (p == Piece.King)
        {
            return ((cell1 == "E1" && cell2 == "G1") || (cell1 == "E8" && cell2 == "G8"));
        }
        return false;
    }

    private void ExecuteMove(string cell1, string cell2, Tuple<Piece, PieceColor> piece, bool switchTurn)
    {
        // I need to hold the child piece from cell1,
        // I need to remove the child piece from cell2, if there is one
        // and put the piece from cell1 to cell2
        bool capture = false;
        bool isPromotional = MoveCheckerPUN.IsPromotionalMove(name);

        GameObject c1 = GameObject.Find(cell1);
        GameObject c2 = GameObject.Find(cell2);
        GameObject p = c1.transform.GetChild(0).gameObject;
        bool isCapture = false;

        UpdatePieceDisplay(p, piece);

        c1.GetComponent<CellPUN_Script>().DeselectCell();

        if (c2.transform.childCount != 0)
        {
            capture = true;
            GameObject go = c2.transform.GetChild(0).gameObject;
            GameP.PieceCaptured(go.name, true, transform, true);
            isCapture = true;
            Destroy(go);
        }

        if (isCapture) SoundPlayer.PlayPieceCaptureSound();
        else SoundPlayer.PlayPieceMoveSound();

        p.transform.SetParent(c2.transform);

        Debug.Log("Moved " + p.name + " from " + c1.name + " to " + c2.name);

        p.transform.localPosition = new Vector3(0f, 0f, 0f);

        Move.ResetMove();
        MoveCheckerPUN.UnmarkAll();
        if (switchTurn)
        {
            if (IsLongCastling(cell1, cell2, piece.Item1))
            {
                ListOfMoves.AddCastlesMove(cell1, cell2, true, false);
            }
            else if (IsShortCastling(cell1, cell2, piece.Item1))
            {
                ListOfMoves.AddCastlesMove(cell1, cell2, false, false);
            }
            else if (isPromotional)
            {
                ListOfMoves.AddPromotionMove(cell1, cell2, piece.Item1, false);
            }
            else
            {
                if (!capture) ListOfMoves.AddNormalMove(cell1, cell2, false);
                else ListOfMoves.AddCaptureMove(cell1, cell2, false);
            }

            GameP.SendRPC_SwitchTurn();
        }
        MoveCheckerPUN.UpdateCastlingPossibilities(piece, cell1);

        moveUpdater.RPC("ExecutedMove", RpcTarget.Others, cell1, cell2, piece.Item1.ToString(), piece.Item2.ToString());
    }

    private void ExecuteMove_DestroyFeature()
    {
        GameObject p = transform.GetChild(0).gameObject;
        Tuple<Piece, PieceColor> piece = PieceChecker.TransformGoInTuple(p);

        Instantiate(BombPrefab, transform.position, Quaternion.identity);

        UpdatePieceDisplay(p, piece);
        Destroy(p);

        ListOfMoves.AddDestroyUse(name, false);

        SoundPlayer.PlayDestroySound();

        GameP.PieceCaptured(p.name, false, transform, true);
        GameP.DestroyUsed(piece);
        GameP.SendRPC_DestroyUsed(piece);
        Move.ResetMove();
        GameP.SendRPC_SwitchTurn();
        MoveCheckerPUN.UpdateCastlingPossibilities(piece, name);

        moveUpdater.RPC("ExecutedMove_DestroyFeature", RpcTarget.Others, name);
    }

    private void ExecuteMove_ImmunityFeature()
    {
        Instantiate(ShieldPrefab, transform.position, Quaternion.identity);

        if (GameP.AmWhite()) MarkShieldedCell_White();
        else MarkShieldedCell_Black();

        ListOfMoves.AddImmunityMove(name, false);

        SoundPlayer.PlayShieldSound();

        Move.ResetMove();

        moveUpdater.RPC("ExecutedMove_ImmunityFeature", RpcTarget.Others, name);
        GameP.SendRPC_SwitchTurn();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Get all the raycast hits at the current mouse position
            PointerEventData pointerData = new PointerEventData(EventSystem.current);
            pointerData.position = Input.mousePosition;

            // Check if the first hit is a UI element
            List<RaycastResult> results = new List<RaycastResult>(1);
            EventSystem.current.RaycastAll(pointerData, results);

            if (results.Count > 0 && results[0].gameObject.layer == LayerMask.NameToLayer("UI"))
            {
                // The first hit is a UI element, do not proceed with Cell_OnClick()
                return;
            }

            // If not a UI element, perform your original raycast
            Vector2 raycastPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(raycastPosition, Vector2.zero);

            if (hit.collider != null)
            {
                if (hit.collider.name == name)
                {
                    Cell_OnClick();
                }
            }
        }
    }
}
