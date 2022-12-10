using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    [SerializeField] private new Camera camera;
    [SerializeField] private Animator animator;
    private InteractionHandler interactionHandler;
    private AnimationHandler animationHandler;

    [SerializeField] private Stat health;
    [SerializeField] private Stat mana;

    private string groundTag = "Ground";
    private string interactableTag = "Interactable";
    private RaycastHit hit;
    private NavMeshAgent agent;
    private Interactable currentInteractable;
    private int isWalkingHash;
    private int isAttackingHash;
    private int isSpinningHash;
    private bool isInteracting;
    private bool isAiming;
    private bool isAimingGroundSpell;

    private float castSpeed = 3f;

    [SerializeField] private float healthValue = 100f;
    [SerializeField] private float manaValue = 100f;
    private float maxHealthValue = 100f;
    private float maxManaValue = 100f;
    private float attackSpeed = 2f; // placeholder attackspeed

    void Start()
    {
        health.Initialize(maxHealthValue, maxHealthValue);
        mana.Initialize(maxManaValue, maxManaValue);
        agent = GetComponent<NavMeshAgent>();
        interactionHandler = GetComponent<InteractionHandler>();
        animationHandler = GetComponent<AnimationHandler>();
        isWalkingHash = Animator.StringToHash("isWalking");
    }

    void Update()
    {
        SetAnimation();
        HandleInput();
        DebugHealth();
    }

    void DebugHealth()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            mana.MyCurrentValue += 10f;
            health.MyCurrentValue += 10f;
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            mana.MyCurrentValue -= 10f;
            health.MyCurrentValue -= 10f;
        }
    }

    private void SetAnimation()
    {
        if (Mathf.Abs(agent.velocity.x) > 0.01f || Mathf.Abs(agent.velocity.z) > 0.01f)
        {
            animator.SetBool(isWalkingHash, true);
        }
        else
        {
            animator.SetBool(isWalkingHash, false);
        }
    }

    public float GetAtkSpeed() 
    {
        return attackSpeed;
    } 

    void HandleInput()
    {

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);

            if (isAimingGroundSpell)
            {
                if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                {
                    // TODO: Cast aoe spell on hit.point
                }
            }
            
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                // TODO: cast Aoe spell
                if (hit.collider.CompareTag(interactableTag) && isAiming)
                {
                    // TODO: Cast spell on target if isAiming is active
                }
                else if (hit.collider.CompareTag(interactableTag))
                {
                    // TODO: Target the unit
                }
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if (hit.collider.CompareTag(groundTag))
                {
                    interactionHandler.StopInteracting();
                    agent.isStopped = false;
                    agent.SetDestination(hit.point);
                }
                
                if (hit.collider.CompareTag(interactableTag))
                {
                    currentInteractable = hit.collider.gameObject.GetComponent<Interactable>();
                    interactionHandler.HandleInteraction(currentInteractable);
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            // TODO: Enable Targeting cursor
            // TODO: Hold current spell that will be cast on next LMB click
            
            RequestAbilityCast();
        }
    }

    public void RequestAbilityCast()
    {
        StartCoroutine(SpinAttack());
        // TODO: Get Spell
        // TODO: Check cooldown
        // TODO: Check WHICH spell you're casting
        // TODO: Check if you have mana
        // TODO: Check if you're casting an aoe or target spell
        // TODO: Check if you even need a target
        isAiming = true;
    }

    public IEnumerator SpinAttack()
    {
        agent.isStopped = true;
        animationHandler.SetTrigger("spinTrigger");
        // TODO: DO DAMAGE
        // TODO: Spawn VFX
        float animTime = animationHandler.GetSpinAnimationTime() * 0.8f;
        Debug.Log(animTime);
        yield return new WaitForSeconds(animTime);
        agent.isStopped = false; // This is faulty, it overrides other the isStopped from basic attack. Refactor
    }

    public IEnumerator CastSpell()
    {
        yield return new WaitForSeconds(castSpeed);
    }
}
