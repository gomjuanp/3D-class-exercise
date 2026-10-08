using UnityEngine;
using System.Collections;

public class PanelAnimatorController : MonoBehaviour
{
    public Animator animator;
    public string triggerName = "SlideTrigger";

    void Start()
    {
        if (animator == null)
            animator = GetComponent<Animator>();

        StartCoroutine(LoopAnimation());
    }

    IEnumerator LoopAnimation()
    {
        while (true)
        {
            yield return new WaitForSeconds(5f);

            animator.SetTrigger(triggerName);
        }
    }
}
