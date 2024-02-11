using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombScript : MonoBehaviour
{
    [SerializeField] private AnimationClip Explode_Clip;
    private Animator anim;

    private void Start()
    {
        anim = transform.GetChild(0).GetComponent<Animator>();
        StartCoroutine(WaitForExplosion());
    }

    private IEnumerator WaitForExplosion()
    {
        anim.enabled = true;
        yield return new WaitForSeconds(Explode_Clip.length);
        Destroy(gameObject);
    }
}
