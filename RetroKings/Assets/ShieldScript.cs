using System.Collections;
using UnityEngine;

public class ShieldScript : MonoBehaviour
{
    [SerializeField] private AnimationClip clip;
    private Animator anim;

    private void Start()
    {
        anim = transform.GetChild(0).GetComponent<Animator>();
        StartCoroutine(AnimateShield());
    }

    private IEnumerator AnimateShield()
    {
        anim.enabled = true;
        yield return new WaitForSeconds(clip.length);
        Destroy(gameObject);
    }
}
