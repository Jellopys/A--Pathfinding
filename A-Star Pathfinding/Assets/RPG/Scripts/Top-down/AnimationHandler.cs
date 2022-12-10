using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    private Animator animator;
    private int isWalkingHash;
    private int isAttackingHash;
    [SerializeField] AnimationClip spinningAnim;
    private float spinAnimTime;

    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        isWalkingHash = Animator.StringToHash("isWalking");
        isAttackingHash = Animator.StringToHash("isAttacking");
        spinAnimTime = spinningAnim.length;

    }

    /// TODO: Set movement animation here
    /// TODO: Set movement animation here
    /// TODO: Set movement animation here
    /// TODO: Set movement animation here
    /// TODO: Set movement animation here
    /// TODO: Set movement animation here

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetAttacking(bool isAttacking)
    {
        animator.SetBool(isAttackingHash, isAttacking);
    }

    public float GetSpinAnimationTime()
    {
        return spinAnimTime;
    }

    public void SetTrigger(string triggerName)
    {
        animator.SetTrigger(triggerName);
    }

    public float GetAnimationTime()
    {
        var currentClipInfo = animator.GetCurrentAnimatorClipInfo(0);
        float animLength = currentClipInfo[0].clip.length;
        return animLength;
    }
}
