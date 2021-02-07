using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonsScript : MonoBehaviour
{
    private GameManagerScript GameManager;

	private void Start()
	{
		GameManager = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
	}

	public void ExtraTurnButton_Click()
	{

	}

	public void ImmunityButton_Click()
	{

	}

	public void KillButton_Click()
	{

	}

	public void RestartButton_Click()
	{
		SceneManager.LoadScene("MainScene");
	}
}
