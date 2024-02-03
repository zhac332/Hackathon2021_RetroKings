using System;
using UnityEngine;

public class CellPUN_Script : MonoBehaviour
{
    [SerializeField] private GamePUN GameP;

    [SerializeField] private HistoryMovesScript ListOfMoves;

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

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        MoveChecker.AcquireAllCells();
    }

    private void Cell_OnClick()
    {
        if (GameP.IsGameOver()) return;

        if (MoveChecker.IsDestroyPowerupOn())
        {
            if (MoveChecker.CheckMove(name))
            {
                ExecuteMove_DestroyFeature();
                return;
            }
        }

        if (MoveChecker.IsImmunityPowerupOn())
        {
            if (MoveChecker.CheckMove(name))
            {
                ExecuteMove_ImmunityFeature();
                return;
            }
        }

        if (!GameP.IsMyTurn())
        {
            Debug.LogError("not your turn!");
            return;
        }

        GameObject piece = null;
        if (!Move.IsFirstCellSelected())
        {
            if (!HasAPiece())
            {
                Debug.Log("Select a cell with a piece. If there is no piece, how do I know what move to make?");
                return;
            }

            if (!GameP.IsMyPiece(transform.GetChild(0).name))
            {
                Debug.LogError("Not your piece...");
                return;
            }

            // if (HasAPiece())....
            piece = transform.GetChild(0).gameObject;

            SelectCell();

            Move.SelectPiece(name, piece.name);
            MoveChecker.SetFirstPiece(piece.name);
            MoveChecker.MarkAvailableCells(gameObject);
            MoveChecker.ResetPowerupToggles();
        }
        else
        {
            if (Move.IsCellIdenticalWithFirst(name))
            {
                DeselectCell();
                Move.SelectPiece();
                MoveChecker.UnmarkAll();
            }
            else if (!Move.IsSecondCellSelected())
            {
                if (MoveChecker.CheckMove(name))
                {
                    // this is where I should check if the end cell makes the pawn be promoted
                    if (MoveChecker.IsPromotionalMove(name))
                    {
                        MoveChecker.ShowPromotionalPanel();
                        Move.SelectCell_Promote(name, (cell1, cell2, piece, switchTurn) => ExecuteMove(cell1, cell2, piece, switchTurn));
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
    }

    public void MarkShieldedCell_Black()
    {
        sr.color = Black_ShieldedCell_Color;
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
        bool isPromotional = MoveChecker.IsPromotionalMove(name);

        GameObject c1 = GameObject.Find(cell1);
        GameObject c2 = GameObject.Find(cell2);
        GameObject p = c1.transform.GetChild(0).gameObject;

        UpdatePieceDisplay(p, piece);

        c1.GetComponent<Cell_Script>().DeselectCell();

        if (c2.transform.childCount != 0)
        {
            capture = true;
            GameObject go = c2.transform.GetChild(0).gameObject;
            GameP.PieceCaptured(go.name);
            Destroy(go);
        }

        p.transform.SetParent(c2.transform);

        Debug.Log("Moved " + p.name + " from " + c1.name + " to " + c2.name);

        p.transform.localPosition = new Vector3(0f, 0f, 0f);

        Move.ResetMove();
        MoveChecker.UnmarkAll();
        if (switchTurn)
        {

            if (IsLongCastling(cell1, cell2, piece.Item1))
            {
                ListOfMoves.AddCastlesMove(cell1, cell2, true);
            }
            else if (IsShortCastling(cell1, cell2, piece.Item1))
            {
                ListOfMoves.AddCastlesMove(cell1, cell2, false);
            }
            else if (isPromotional)
            {
                ListOfMoves.AddPromotionMove(cell1, cell2, piece.Item1);
            }
            else
            {
                if (!capture) ListOfMoves.AddNormalMove(cell1, cell2);
                else ListOfMoves.AddCaptureMove(cell1, cell2);
            }

            GameP.SwitchTurn();
        }
        MoveChecker.UpdateCastlingPossibilities(piece, cell1);

    }

    private void ExecuteMove_DestroyFeature()
    {
        GameObject p = transform.GetChild(0).gameObject;
        Tuple<Piece, PieceColor> piece = PieceChecker.TransformGoInTuple(p);

        UpdatePieceDisplay(p, piece);
        Destroy(p);

        ListOfMoves.AddDestroyUse(name);

        GameP.PieceCaptured(p.name);
        GameP.DestroyUsed(piece);
        Move.ResetMove();
        MoveChecker.UnmarkAll();
        GameP.SwitchTurn();
        MoveChecker.UpdateCastlingPossibilities(piece, name);
    }

    private void ExecuteMove_ImmunityFeature()
    {
        if (GameP.IsMyTurn()) MarkShieldedCell_White();
        else MarkShieldedCell_Black();

        ListOfMoves.AddImmunityMove(name);

        GameP.ImmunityUsed(name);

        Move.ResetMove();
        MoveChecker.UnmarkAll();
        GameP.SwitchTurn();
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
                    Cell_OnClick();
                }
            }
        }
    }
}
