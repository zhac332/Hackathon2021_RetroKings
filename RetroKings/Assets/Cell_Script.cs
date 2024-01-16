using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell_Script : MonoBehaviour
{
    private void Cell_OnClick()
    {
        if (Move.IsFirstCellSelected() && !Move.IsSecondCellSelected())
        {

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
                Cell_OnClick();
            }
        }
    }
}
