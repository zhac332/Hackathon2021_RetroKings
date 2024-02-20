using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayerScript : MonoBehaviour
{
    [SerializeField] private AudioClip Intro_Sound;
    [SerializeField] private AudioClip Promotion_Sound;
    [SerializeField] private AudioClip PieceCapture_Sound;
    [SerializeField] private AudioClip PieceSelect_Sound;
    [SerializeField] private AudioClip PieceDeselect_Sound;
    [SerializeField] private AudioClip PieceMove_Sound;
    [SerializeField] private AudioClip No_Sound;
    [SerializeField] private AudioClip Destroy_Sound;
    [SerializeField] private AudioClip Shield_Sound;

    private AudioSource AS;

    private void Start()
    {
        AS = GetComponent<AudioSource>();
    }

    public void PlayIntroSound()
    {
        AS.clip = Intro_Sound;
        AS.Play();
    }

    public void PlayPromotionSound()
    {
        AS.clip = Promotion_Sound;
        AS.Play();
    }

    public void PlayPieceCaptureSound()
    {
        AS.clip = PieceCapture_Sound;
        AS.Play();
    }

    public void PlayPieceSelectSound()
    {
        AS.clip = PieceSelect_Sound;
        AS.Play();
    }

    public void PlayPieceDeselectSound()
    {
        AS.clip = PieceDeselect_Sound;
        AS.Play();
    }

    public void PlayPieceMoveSound()
    {
        AS.clip = PieceMove_Sound;
        AS.Play();
    }

    public void PlayNoSound()
    {
        AS.clip = No_Sound;
        AS.Play();
    }

    public void PlayDestroySound()
    {
        AS.clip = Destroy_Sound;
        AS.Play();
    }

    public void PlayShieldSound()
    {
        AS.clip = Shield_Sound;
        AS.Play();
    }
}
