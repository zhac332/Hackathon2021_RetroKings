using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellScript : MonoBehaviour
{
	private GameManagerScript GameManager;

	private void Start()
	{
		GameManager = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
	}

	private void OnMouseDown()
	{
		Debug.Log("cell clicked.");
	}
}
