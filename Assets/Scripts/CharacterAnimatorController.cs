using UnityEngine;

public class CharacterAnimatorController : MonoBehaviour
{
    public Animator animator;

    public string standUp = "standUp";
    public string jump = "jump";
    public string dancing = "dancing";


    void Start()
    {
        EnsureAnimator();
    }

    public void EnsureAnimator()
    {
        if (animator == null)
            animator = GetComponent<Animator>();
        if (animator == null)
            animator = GetComponentInParent<Animator>();
        if (animator == null)
            animator = GetComponentInChildren<Animator>();
    }

    public void PlayStandUp()
    {
        EnsureAnimator();
        if (animator != null)
            animator.SetTrigger(standUp);
    }

    public void PlayJump()
    {
        EnsureAnimator();
        if (animator != null)
            animator.SetTrigger(jump);
    }

    public void PlayDancing()
    {
        EnsureAnimator();
        if (animator != null)
            animator.SetTrigger(dancing);
    }
}
