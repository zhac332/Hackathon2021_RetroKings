using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{
    [SerializeField] private AudioClip IntroClip;

    [SerializeField] private AudioClip Piece_Move;
    [SerializeField] private AudioClip Piece_Capture;
    [SerializeField] private AudioClip Piece_Check;
    [SerializeField] private AudioClip Piece_Checkmate;
    [SerializeField] private AudioClip Piece_Select;
    [SerializeField] private AudioClip Piece_Deselect;
    [SerializeField] private AudioClip Piece_Stuck;

    private AudioSource AS;

	private void Start()
	{
        AS = GetComponent<AudioSource>();
        AS.clip = IntroClip;
        AS.Play();
	}

	public void PieceMove() {

        AS.clip = Piece_Move;
        AS.Play();
    }

    public void PieceCapture() {

        AS.clip = Piece_Capture;
        AS.Play();
    }

    public void PieceCheck() {

        AS.clip = Piece_Check;
        AS.Play();
    }

    public void PieceCheckmate() {

        AS.clip = Piece_Checkmate;
        AS.Play();
    }

    public void PieceSelect()
	{
        AS.clip = Piece_Select;
        AS.Play();
    }

    public void PieceStuck()
	{
        AS.clip = Piece_Stuck;
        AS.Play();
    }
}
