using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceDisplayScript : MonoBehaviour
{
    [SerializeField] private List<GameObject> cells;

    public List<GameObject> GetCells()
    {
        return cells;
    }

    public void SetPerspective(bool white)
    {
        transform.rotation = Quaternion.Euler(0f, 0f, (white ? 0f : 180f));
        if (!white)
        {
            SetPiecesRotation(0f);
        }
    }

    private void SetPiecesRotation(float zRot)
    {
        GameObject[] pieces = GameObject.FindGameObjectsWithTag("Piece");

        for (int i = 0; i < pieces.Length; i++)
            pieces[i].transform.rotation = Quaternion.Euler(0f, 0f, zRot);
    }
}
