using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class Character : MonoBehaviour
{
    // [SerializeField] protected Animator animator;
    // protected NavMeshAgent agent;
    // int isWalkingHash;
    
    // protected virtual void Start()
    // {
    //     agent = GetComponent<NavMeshAgent>();
    //     isWalkingHash = Animator.StringToHash("isWalking");
    // }

    
    // protected virtual void Update()
    // {
    //     SetAnimation();
    //     MoveCharacter();
    // }

    // void MoveCharacter()
    // {

    // }

    // private void SetAnimation()
    // {
    //     if (Mathf.Abs(agent.velocity.x) > 0.01f || Mathf.Abs(agent.velocity.z) > 0.01f)
    //     {
    //         animator.SetBool(isWalkingHash, true);
    //     }
    //     else
    //     {
    //         animator.SetBool(isWalkingHash, false);
    //     }
    // }
}
