using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    [SerializeField] private List<GameObject> row1 = new List<GameObject>();
    [SerializeField] private List<GameObject> row2 = new List<GameObject>();
    [SerializeField] private List<GameObject> row3 = new List<GameObject>();
    [SerializeField] private List<GameObject> row4 = new List<GameObject>();
    [SerializeField] private List<GameObject> row5 = new List<GameObject>();
    [SerializeField] private List<GameObject> row6 = new List<GameObject>();
    [SerializeField] private List<GameObject> row7 = new List<GameObject>();
    [SerializeField] private List<GameObject> row8 = new List<GameObject>();
    [Header("White pieces")]
    [SerializeField] private GameObject Pawn_White;
    [SerializeField] private GameObject King_White;
    [SerializeField] private GameObject Queen_White;
    [SerializeField] private GameObject Bishop_White;
    [SerializeField] private GameObject Knight_White;
    [SerializeField] private GameObject Rook_White;
    [Header("Black pieces")]
    [SerializeField] private GameObject Pawn_Black;
    [SerializeField] private GameObject King_Black;
    [SerializeField] private GameObject Queen_Black;
    [SerializeField] private GameObject Bishop_Black;
    [SerializeField] private GameObject Knight_Black;
    [SerializeField] private GameObject Rook_Black;


    private GameObject[,] board_cells = new GameObject[8, 8];

    // Start is called before the first frame update
    void Start()
    {
        InitBoard();
        ShowWhitePerspective();
        //ShowBlackPerspective();
    }

    private void InitBoard()
	{
        for (int i = 0; i < row1.Count; i++)
            board_cells[0, i] = row1[i];
        for (int i = 0; i < row2.Count; i++)
            board_cells[1, i] = row2[i];
        for (int i = 0; i < row3.Count; i++)
            board_cells[2, i] = row3[i];
        for (int i = 0; i < row4.Count; i++)
            board_cells[3, i] = row4[i];
        for (int i = 0; i < row5.Count; i++)
            board_cells[4, i] = row5[i];
        for (int i = 0; i < row6.Count; i++)
            board_cells[5, i] = row6[i];
        for (int i = 0; i < row7.Count; i++)
            board_cells[6, i] = row7[i];
        for (int i = 0; i < row8.Count; i++)
            board_cells[7, i] = row8[i];
    }

    private void ShowWhitePerspective()
	{
        /// -----------------------------------------WHITE PIECES-----------------------------------------
        
        // the white pawns will be on row 7
        // the other white pieces will be on row 8

        Vector3 globalPosition = new Vector3();

        // setting up the white pawns
        foreach (GameObject cell in row7)
		{
            globalPosition = cell.transform.position;
            globalPosition.y += 0.1f;
            Instantiate(Pawn_White, globalPosition, Quaternion.identity);
		}

        globalPosition = row8[0].transform.position;
        Instantiate(Rook_White, globalPosition, Quaternion.identity);
        globalPosition = row8[7].transform.position;
        Instantiate(Rook_White, globalPosition, Quaternion.identity);

        globalPosition = row8[1].transform.position;
        Instantiate(Knight_White, globalPosition, Quaternion.identity);
        globalPosition = row8[6].transform.position;
        Instantiate(Knight_White, globalPosition, Quaternion.identity);

        globalPosition = row8[2].transform.position;
        Instantiate(Bishop_White, globalPosition, Quaternion.identity);
        globalPosition = row8[5].transform.position;
        Instantiate(Bishop_White, globalPosition, Quaternion.identity);

        globalPosition = row8[3].transform.position;
        Instantiate(Queen_White, globalPosition, Quaternion.identity);

        globalPosition = row8[4].transform.position;
        Instantiate(King_White, globalPosition, Quaternion.identity);

        /// -----------------------------------------BLACK PIECES-----------------------------------------

        // setting up the white pawns
        foreach (GameObject cell in row2)
        {
            globalPosition = cell.transform.position;
            globalPosition.y += 0.1f;
            Instantiate(Pawn_Black, globalPosition, Quaternion.identity);
        }

        globalPosition = row1[0].transform.position;
        Instantiate(Rook_Black, globalPosition, Quaternion.identity);
        globalPosition = row1[7].transform.position;
        Instantiate(Rook_Black, globalPosition, Quaternion.identity);

        globalPosition = row1[1].transform.position;
        Instantiate(Knight_Black, globalPosition, Quaternion.identity);
        globalPosition = row1[6].transform.position;
        Instantiate(Knight_Black, globalPosition, Quaternion.identity);

        globalPosition = row1[2].transform.position;
        Instantiate(Bishop_Black, globalPosition, Quaternion.identity);
        globalPosition = row1[5].transform.position;
        Instantiate(Bishop_Black, globalPosition, Quaternion.identity);

        globalPosition = row1[3].transform.position;
        Instantiate(Queen_Black, globalPosition, Quaternion.identity);

        globalPosition = row1[4].transform.position;
        Instantiate(King_Black, globalPosition, Quaternion.identity);
    }

    private void ShowBlackPerspective()
	{
        /// -----------------------------------------WHITE PIECES-----------------------------------------

        Vector3 globalPosition = new Vector3();

        // setting up the white pawns
        foreach (GameObject cell in row2)
        {
            globalPosition = cell.transform.position;
            globalPosition.y += 0.1f;
            Instantiate(Pawn_White, globalPosition, Quaternion.identity);
        }

        globalPosition = row1[0].transform.position;
        Instantiate(Rook_White, globalPosition, Quaternion.identity);
        globalPosition = row1[7].transform.position;
        Instantiate(Rook_White, globalPosition, Quaternion.identity);

        globalPosition = row1[1].transform.position;
        Instantiate(Knight_White, globalPosition, Quaternion.identity);
        globalPosition = row1[6].transform.position;
        Instantiate(Knight_White, globalPosition, Quaternion.identity);

        globalPosition = row1[2].transform.position;
        Instantiate(Bishop_White, globalPosition, Quaternion.identity);
        globalPosition = row1[5].transform.position;
        Instantiate(Bishop_White, globalPosition, Quaternion.identity);

        globalPosition = row1[3].transform.position;
        Instantiate(Queen_White, globalPosition, Quaternion.identity);

        globalPosition = row1[4].transform.position;
        Instantiate(King_White, globalPosition, Quaternion.identity);


        /// -----------------------------------------BLACK PIECES-----------------------------------------

        // setting up the white pawns
        foreach (GameObject cell in row7)
        {
            globalPosition = cell.transform.position;
            globalPosition.y += 0.1f;
            Instantiate(Pawn_Black, globalPosition, Quaternion.identity);
        }

		globalPosition = row8[0].transform.position;
		Instantiate(Rook_Black, globalPosition, Quaternion.identity);
		globalPosition = row8[7].transform.position;
		Instantiate(Rook_Black, globalPosition, Quaternion.identity);

		globalPosition = row8[1].transform.position;
		Instantiate(Knight_Black, globalPosition, Quaternion.identity);
		globalPosition = row8[6].transform.position;
		Instantiate(Knight_Black, globalPosition, Quaternion.identity);

		globalPosition = row8[2].transform.position;
		Instantiate(Bishop_Black, globalPosition, Quaternion.identity);
		globalPosition = row8[5].transform.position;
		Instantiate(Bishop_Black, globalPosition, Quaternion.identity);

		globalPosition = row8[3].transform.position;
		Instantiate(Queen_Black, globalPosition, Quaternion.identity);

		globalPosition = row8[4].transform.position;
		Instantiate(King_Black, globalPosition, Quaternion.identity);
	}
}
