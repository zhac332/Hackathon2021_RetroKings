using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AreYouSurePanel : MonoBehaviour
{
    [SerializeField] private Text QuestionText;
    [SerializeField] private AnimationClip ZoomIn;
    [SerializeField] private AnimationClip ZoomOut;
    private Animator anim;
    private Action Yes_Trigger;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void NewQuestion(string question, Action Yes_T)
    {
        QuestionText.text = question;
        Yes_Trigger = Yes_T;
        anim.enabled = true;
        anim.Play(ZoomIn.name);
        GetComponent<Image>().raycastTarget = true;
    }

    public void YesButton_OnClick()
    {
        StartCoroutine(AnimateZoomOut(true));
    }

    public void NoButton_OnClick()
    {
        StartCoroutine(AnimateZoomOut(false));
    }

    private IEnumerator AnimateZoomOut(bool yes)
    {
        anim.Play(ZoomOut.name);
        yield return new WaitForSeconds(ZoomOut.length);
        anim.enabled = false;
        GetComponent<Image>().raycastTarget = false;
        if (yes) Yes_Trigger.Invoke();
    }
}
