using System;
using UnityEngine;

public class Cell_Script : MonoBehaviour
{
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
        GameObject piece = null;
        if (!Move.IsFirstCellSelected())
        {
            if (HasAPiece())
            {
                piece = transform.GetChild(0).gameObject;

                SelectCell();

                Move.SelectPiece(name, piece.name);
                MoveChecker.SetFirstPiece(piece.name);
                MoveChecker.MarkAvailableCells(gameObject);
            }
            else Debug.Log("Select a cell with a piece. If there is no piece, how do I know what move to make?");
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
                    piece = null;
                    if (transform.childCount != 0) piece = transform.GetChild(0).gameObject;

                    if (piece == null) Move.SelectCell(name, (cell1, cell2, piece) => ExecuteMove(cell1, cell2, piece));
                    else Move.SelectCell(name, piece.name, (cell1, cell2, piece) => ExecuteMove(cell1, cell2, piece));
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

    private void ExecuteMove(string cell1, string cell2, Tuple<Piece, PieceColor> piece)
    {
        // I need to hold the child piece from cell1,
        // I need to remove the child piece from cell2, if there is one
        // and put the piece from cell1 to cell2
        GameObject c1 = GameObject.Find(cell1);
        GameObject c2 = GameObject.Find(cell2);
        GameObject p = c1.transform.GetChild(0).gameObject;

        c1.GetComponent<Cell_Script>().DeselectCell();

        if (c2.transform.childCount != 0)
            Destroy(c2.transform.GetChild(0).gameObject);

        p.transform.SetParent(c2.transform);

        Debug.Log("Moved " + p.name + " from " + c1.name + " to " + c2.name);

        p.transform.localPosition = new Vector3(0f, 0f, 0f);

        Move.ResetMove();
        MoveChecker.UnmarkAll();
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
