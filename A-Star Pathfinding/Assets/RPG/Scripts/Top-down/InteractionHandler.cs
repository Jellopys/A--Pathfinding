using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class InteractionHandler : MonoBehaviour
{
    private bool isInteracting;
    private AnimationHandler animationHandler;
    private Interactable currentInteractable;
    private Player player;
    private NavMeshAgent agent;
    private EInteractType interactType;
    private bool isAttacking;

    void Awake()
    {
        animationHandler = GetComponent<AnimationHandler>();
        player = GetComponent<Player>();
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        MoveToInteraction();
        FaceTarget();
    }

    public void HandleInteraction(Interactable interactable)
    {
        if (isInteracting) return;

        // TODO: Check if player can even reach the interactable
        currentInteractable = interactable;
    }

    public void Interact()
    {
        EInteractType interactType = currentInteractable.GetInteractType();
        isInteracting = true;

        switch (interactType)
        {
            case EInteractType.Enemy:
                EnemyInteraction();
                break;

            case EInteractType.Interactable:
                currentInteractable.Interact();
                currentInteractable = null;
                isInteracting = false;
                break;
        }
    }

    public void ObjectInteract()
    {
        // TODO: Play object interaction animation
    }

    public bool IsInteracting()
    {
        return isInteracting;
    }

    public void StopInteracting()
    {
        currentInteractable = null;
        isInteracting = false;

        //Stop attacking
        animationHandler.SetAttacking(false);
        StopCoroutine(BasicAttack());
    }

    public void MoveToInteraction()
    {
        if (currentInteractable == null) return;
        if (IsInteracting()) return;

        if (IsInInteractionRange())
        {
            agent.isStopped = true;
            agent.SetDestination(gameObject.transform.position);
            Interact();
        }
        else
        {
            isInteracting = false;
            agent.isStopped = false;
            StopCoroutine(BasicAttack());
            agent.SetDestination(currentInteractable.gameObject.transform.position);
        }
    }

    public bool IsInInteractionRange()
    {
        if (currentInteractable == null) return false;

        float interactRadius = currentInteractable.GetInteractRadius();
        float distance = Vector3.Distance(transform.position, currentInteractable.gameObject.transform.position);
        isInteracting = distance < interactRadius;
        return distance < interactRadius;
    }

    private void EnemyInteraction()
    {
        // StartCoroutine(BasicAttack());
        StartAttacking();
    }

    public void StartAttacking()
    {
        if (isAttacking) return;
        isAttacking = true;
        animationHandler.SetAttacking(true);
        animationHandler.SetTrigger("atkTrigger");

        float atkSpeed = player.GetAtkSpeed();

        Invoke("BasicAtkCooldown", atkSpeed);
    }

    private void BasicAtkCooldown()
    {
        isAttacking = false;
        if (IsInInteractionRange()) // TODO: Check if target is still alive and if object exists
        {
            StartAttacking();
        }
        else // Not in range
        {
            animationHandler.SetAttacking(false);
        }
    }

    public IEnumerator BasicAttack()
    {
        // TODO: manipulate the speed of the animation
        // TODO: Cooldown
        animationHandler.SetAttacking(true);
        float animLength = animationHandler.GetAnimationTime();
        yield return new WaitForSeconds(animLength);

        if (IsInInteractionRange()) // TODO: Check if target is still alive and if object exists
        {
            StartCoroutine(BasicAttack());
        }
        else // Not in range
        {
            isInteracting = false;
            animationHandler.SetAttacking(false);
            StopCoroutine(BasicAttack());
        }
    }

    private void FaceTarget()
    {

        if (!isInteracting || !IsInInteractionRange()) return;

        Vector3 targetPos = currentInteractable.gameObject.transform.position;
        Vector3 direction = (targetPos - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5);
    }

    private void DealDamage()
    {
        Debug.Log($"dealt damage");
    }
}
