using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private Animator animator;
    private NavMeshAgent agent;
    private int isWalkingHash;
    private int isAttackingHash;
    private int canAttackHash;
    [SerializeField] private SphereCollider aggroCollider;
    private bool hasAggro = false;
    private Vector3 aggroPos;
    private GameObject aggroTarget;
    [SerializeField] private float leashRange = 10f;
    [SerializeField] private float attackRange = 1f;
    [SerializeField] private float attackSpeed = 3f;

    private bool canAttack = true;
    private bool isAttacking;
    private bool isReturning;

    [SerializeField] private float healthValue = 100f;
    [SerializeField] private float manaValue = 100f;
    private float maxHealthValue = 100f;
    private float maxManaValue = 100f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        isWalkingHash = Animator.StringToHash("isWalking");
        isAttackingHash = Animator.StringToHash("isAttacking");
        canAttackHash = Animator.StringToHash("canAttack");
        aggroPos = transform.position;
    }

    void Update()
    {
        SetAnimation();
        CheckLeashRange();
        MoveToTarget();
        CheckReturnRange();
        CheckCanAttack();
        // TODO: Rotate to target
    }

    private void CheckCanAttack()
    {
        if (canAttack) return;

        float atkCooldown = attackSpeed;
        atkCooldown -= Time.deltaTime;
        if (atkCooldown <= 0) canAttack = true;
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

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player") && !hasAggro)
        {
            hasAggro = true;
            aggroTarget = collider.gameObject;
        }
    }

    private void MoveToTarget()
    {
        if (aggroTarget == null) return;
        if (!hasAggro || isReturning) return;

        agent.SetDestination(aggroTarget.transform.position);

        float dist = Vector3.Distance(transform.position, aggroTarget.transform.position);

        if (dist < attackRange)
        {
            StartAttacking();
            agent.ResetPath();
        }
    }

    private void CheckLeashRange()
    {
        if (!hasAggro) return;

        float dist = Vector3.Distance(transform.position, aggroPos);
        if (dist > leashRange)
        {
            ReturnToLeashPos();
        }
    }

    public void StartAttacking()
    {
        if (isAttacking) return;
        isAttacking = true;
        animator.SetBool(isAttackingHash, true);
        animator.SetTrigger("atkTrigger");
        // animator.SetBool(canAttackHash, true);

        Invoke("BasicAtkCooldown", attackSpeed);
    }

    private void BasicAtkCooldown()
    {
        isAttacking = false;
        if (IsInAttackRange()) // TODO: Check if target is still alive and if object exists
        {
            StartAttacking();
        }
        else // Not in range
        {
            animator.SetBool(isAttackingHash, false);
        }
    }

    public bool IsInAttackRange()
    {
        if (aggroTarget == null) return false;

        float dist = Vector3.Distance(transform.position, aggroTarget.transform.position);
        return dist < attackRange;
    }

    private void CheckReturnRange()
    {
        if (!isReturning) return;

        float dist = Vector3.Distance(transform.position, aggroPos);

        if (dist < 0.5f)
        {
            hasAggro = false;
            isReturning = false;
        }
    }

    private void ReturnToLeashPos()
    {      
        agent.ResetPath();
        agent.SetDestination(aggroPos);
        isReturning = true;
        aggroTarget = null;
    }

    private void DealDamage()
    {
        Debug.Log(gameObject + " dealt damage");
    }

    private void SetCanAttack()
    {
        canAttack = false;
        animator.SetBool(canAttackHash, false);
    }
}
