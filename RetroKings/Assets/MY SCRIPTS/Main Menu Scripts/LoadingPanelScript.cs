using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingPanelScript : MonoBehaviour
{
    [SerializeField] private List<Image> Circles;
    [SerializeField] private float DelayBetweenFrame = 0.2f;
    private List<float> Alphas;
    private int initIndex;

    private void OnEnable()
    {
        GetComponent<Animator>().Play("Open");

        foreach (Image i in Circles)
            i.gameObject.SetActive(true);

        Alphas = new List<float>();
        initIndex = -1;
        for (int i = 0; i < Circles.Count; i++)
            Alphas.Add((1.0f * (Circles.Count - i)) / Circles.Count);
        // I am doubling it so that each value goes through each circle without the need of extra ifs and calculations.
        for (int i = 0; i < Circles.Count; i++)
            Alphas.Add((1.0f * (Circles.Count - i)) / Circles.Count);

        StartCoroutine(AnimateLoading());
    }

    private void OnDisable()
    {
        foreach(Image i in Circles)
            i.gameObject.SetActive(false);
        StopAllCoroutines();
    }

    private IEnumerator AnimateLoading()
    {
        initIndex++;
        if (initIndex >= Circles.Count) initIndex = 0;

        for (int i = 0; i < Circles.Count; i++)
        {
            Color c = Circles[i].color;
            Circles[i].color = new Color(c.r, c.g, c.b, Alphas[initIndex + i]);
        }

        yield return new WaitForSeconds(DelayBetweenFrame);
        StartCoroutine(AnimateLoading());
    }}
