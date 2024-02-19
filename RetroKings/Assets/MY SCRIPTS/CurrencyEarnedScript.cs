using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// Script that dictates how a currency object behaves in the game view 
/// once the player has destroyed an enemy object.
/// </summary>
public class CurrencyEarnedScript : MonoBehaviour
{
    [SerializeField] private float FadeDuration = 1.0f; // Set this value through the Inspector
    private float currentTime = 0f;

    [Header("Digits")]
    [SerializeField] private Sprite Plus;
    [SerializeField] private Sprite Zero;
    [SerializeField] private Sprite One;
    [SerializeField] private Sprite Two;
    [SerializeField] private Sprite Three;
    [SerializeField] private Sprite Four;
    [SerializeField] private Sprite Five;
    [SerializeField] private Sprite Six;
    [SerializeField] private Sprite Seven;
    [SerializeField] private Sprite Eight;
    [SerializeField] private Sprite Nine;

    /// <summary>
    /// Sprite Renderers which exist in the object that shows how much currency the player earned.
    /// </summary>
    [Header("Sprite Renderers")]
    [SerializeField] private SpriteRenderer[] C;

    /// <summary>
    /// Method that assigns a digit sprite to the specified sprite renderer.
    /// </summary>
    /// <param name="digit"> Digit that determines which sprite to use. </param>
    /// <param name="sr"> The sprite renderer to assign the sprite to. </param>
    private void ReplaceDigit(int digit, SpriteRenderer sr)
	{
        if (digit == -2) sr.sprite = Plus;
        if (digit == 0) sr.sprite = Zero;
        if (digit == 1) sr.sprite = One;
        if (digit == 2) sr.sprite = Two;
        if (digit == 3) sr.sprite = Three;
        if (digit == 4) sr.sprite = Four;
        if (digit == 5) sr.sprite = Five;
        if (digit == 6) sr.sprite = Six;
        if (digit == 7) sr.sprite = Seven;
        if (digit == 8) sr.sprite = Eight;
        if (digit == 9) sr.sprite = Nine;
	}

    /// <summary>
    /// Determines what sprites the script should use based on the digits
    /// of the currency value that the player has earned.
    /// </summary>
    /// <param name="value"> The amount of currency the player has earned. </param>
    public void WhatCurrency(int value)
	{
        int index = 0;

        while (value != 0)
		{
            ReplaceDigit(value % 10, C[index]);
            index++;
            value /= 10;
		}

        ReplaceDigit(-2, C[index]);
        currentTime = 0f;
        StartCoroutine(Fade());
	}

    private IEnumerator Fade()
    {
        float ratio = currentTime / FadeDuration;

        foreach (SpriteRenderer sr in C)
        {
            Color startColor = sr.color;
            sr.color = new Color(startColor.r, startColor.g, startColor.b, 1 - ratio);
        }

        yield return new WaitForSeconds(Time.deltaTime);
        currentTime += Time.deltaTime;

        if (currentTime < FadeDuration)
        {
            StartCoroutine(Fade());
        }
        else Destroy(gameObject);
    }

    /// <summary>
    /// To be called as a keyframe in the animation
    /// </summary>
    public void Disable()
	{
        Destroy(gameObject);
	}
}
