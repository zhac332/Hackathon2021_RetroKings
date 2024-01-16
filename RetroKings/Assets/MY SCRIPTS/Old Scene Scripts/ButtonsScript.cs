using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonsScript : MonoBehaviour
{
    private GameManagerScript GameManager;
	private int WhitePoints;
	private int BlackPoints;
	private string turn;

	private void Start()
	{
		GameManager = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
	}

	public void ExtraTurnButton_Click()
	{
		if (turn == "White" && WhitePoints >= 40)
		{
			GameManager.ExtraTurn();
			WhitePoints -= 40;
			GameManager.SetWhitePoints(WhitePoints);
		}
		else if (turn == "Black" && BlackPoints >= 40)
		{
			GameManager.ExtraTurn();
			BlackPoints -= 40;
			GameManager.SetBlackPoints(BlackPoints);
		}
	}

	public void ImmunityButton_Click()
	{
		if (turn == "White" && WhitePoints >= 40)
		{
			GameManager.Immunity();
			WhitePoints -= 40;
			GameManager.SetWhitePoints(WhitePoints);
		}
		else if (turn == "Black" && BlackPoints >= 40)
		{
			GameManager.Immunity();
			BlackPoints -= 40;
			GameManager.SetBlackPoints(BlackPoints);
		}
	}

	public void KillButton_Click()
	{
		if (turn == "White" && WhitePoints >= 40)
		{
			GameManager.Kill();
			WhitePoints -= 40;
			GameManager.SetWhitePoints(WhitePoints);
		}
		else if (turn == "Black" && BlackPoints >= 40)
		{
			GameManager.Kill();
			BlackPoints -= 40;
			GameManager.SetBlackPoints(BlackPoints);
		}
	}

	public void RestartButton_Click()
	{
		SceneManager.LoadScene("MainScene");
	}

	private void FixedUpdate()
	{
		WhitePoints = GameManager.GetWhitePoints();
		BlackPoints = GameManager.GetBlackPoints();
		turn = GameManager.GetTurn();
	}
}
