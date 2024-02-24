using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayMessageScript : MonoBehaviour
{
    [SerializeField] private GameObject CenterRectangle;
    [SerializeField] private Text Message;
    [SerializeField] private AnimationClip OpenMessage;
    [SerializeField] private Animator Image_Animator;

    public void ShowMessage(string message)
    {
        StartCoroutine(AnimateImage(message));
        GetComponent<Image>().raycastTarget = true;
        ChangeColorOfCurrentPanel(0.15f);
    }

    private void ChangeColorOfCurrentPanel(float alpha)
    {
        Color c = GetComponent<Image>().color;
        GetComponent<Image>().color = new Color(c.r, c.g, c.b, alpha);
    }

    private IEnumerator AnimateImage(string message)
    {
        CenterRectangle.SetActive(true);
        Image_Animator.Play(OpenMessage.name);
        Message.text = message;

        yield return new WaitForSeconds(OpenMessage.length);

        Message.gameObject.SetActive(true);
    }

    public void OnClick_Detected()
    {
        CenterRectangle.SetActive(false);
        Message.gameObject.SetActive(false);
        GetComponent<Image>().raycastTarget = false;
        ChangeColorOfCurrentPanel(0.0f);
    }
}
