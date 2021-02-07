using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{
    [SerializeField] private AudioClip Piece_Move;
    [SerializeField] private AudioClip Piece_Capture;
    [SerializeField] private AudioClip Piece_Check;
    [SerializeField] private AudioClip Piece_Checkmate;
    [SerializeField] private AudioClip Piece_Select;
    [SerializeField] private AudioClip Piece_Deselect;
    [SerializeField] private AudioClip Piece_Stuck;

    public void PieceMove() {
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.clip = Piece_Move;
        audioSource.Play();
    }

    public void PieceCapture() {
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.clip = Piece_Capture;
        audioSource.Play();
    }

    public void PieceCheck() {
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.clip = Piece_Check;
        audioSource.Play();
    }

    public void PieceCheckmate() {
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.clip = Piece_Checkmate;
        audioSource.Play();
    }

    public void PieceSelect()
	{
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.clip = Piece_Select;
        audioSource.Play();
    }

    public void PieceStuck()
	{
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.clip = Piece_Stuck;
        audioSource.Play();
    }
}
