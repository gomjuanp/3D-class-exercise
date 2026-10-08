using UnityEngine;
using UnityEngine.InputSystem;

public class MainPlayerController : MonoBehaviour
{
    private CharacterAnimatorController anim;

    void Start()
    {
        InitializeComponents();
    }

    private void InitializeComponents()
    {
        if (anim == null)
        {
            anim = GetComponent<CharacterAnimatorController>();
            if (anim == null)
                anim = GetComponentInParent<CharacterAnimatorController>();
            if (anim == null)
                anim = GetComponentInChildren<CharacterAnimatorController>();

            if (anim == null)
            {
                Animator foundAnimator = GetComponentInParent<Animator>();
                if (foundAnimator == null)
                    foundAnimator = GetComponent<Animator>();
                if (foundAnimator == null)
                    foundAnimator = GetComponentInChildren<Animator>();

                if (foundAnimator != null)
                {
                    anim = foundAnimator.gameObject.GetComponent<CharacterAnimatorController>();
                    if (anim == null)
                        anim = foundAnimator.gameObject.AddComponent<CharacterAnimatorController>();
                    anim.animator = foundAnimator;
                }
                else
                {
                    anim = gameObject.AddComponent<CharacterAnimatorController>();
                }
            }
        }

        PlayerInput playerInput = GetComponent<PlayerInput>();
        if (playerInput == null)
            playerInput = GetComponentInParent<PlayerInput>();
        if (playerInput != null && playerInput.actions != null)
        {
            playerInput.actions.Enable();
        }
    }

    void Update()
    {
        if (Keyboard.current != null)
        {
            if (Keyboard.current.cKey.wasPressedThisFrame || Keyboard.current.spaceKey.wasPressedThisFrame)
                PlayJump();

            if (Keyboard.current.ctrlKey.wasPressedThisFrame ||
                Keyboard.current.leftCtrlKey.wasPressedThisFrame ||
                Keyboard.current.rightCtrlKey.wasPressedThisFrame)
            {
                PlayDancing();
            }

            if (Keyboard.current.shiftKey.wasPressedThisFrame ||
                Keyboard.current.leftShiftKey.wasPressedThisFrame ||
                Keyboard.current.rightShiftKey.wasPressedThisFrame)
            {
                PlayStandUp();
            }
        }
    }

    public void PlayJump()
    {
        if (anim != null) anim.PlayJump();
    }

    public void PlayDancing()
    {
        if (anim != null) anim.PlayDancing();
    }

    public void PlayStandUp()
    {
        if (anim != null) anim.PlayStandUp();
    }


    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed) PlayJump();
    }

    public void OnDance(InputAction.CallbackContext context)
    {
        if (context.performed) PlayDancing();
    }

    public void OnDancing(InputAction.CallbackContext context)
    {
        if (context.performed) PlayDancing();
    }

    public void OnStandUp(InputAction.CallbackContext context)
    {
        if (context.performed) PlayStandUp();
    }

    public void OnJump(InputValue value) => PlayJump();
    public void OnDance(InputValue value) => PlayDancing();
    public void OnDancing(InputValue value) => PlayDancing();
    public void OnStandUp(InputValue value) => PlayStandUp();

    public void OnJump() => PlayJump();
    public void OnDance() => PlayDancing();
    public void OnDancing() => PlayDancing();
    public void OnStandUp() => PlayStandUp();
}
