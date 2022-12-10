using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Movement : MonoBehaviour
{
    [SerializeField] private new Camera camera;

    //Movement
    private NavMeshAgent agent;
    private string groundTag = "Ground";
    private RaycastHit hit;


    //Animation
    [SerializeField] private Animator animator;
    int isWalkingHash;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        isWalkingHash = Animator.StringToHash("isWalking");
    }

    void Update()
    {
        SetAnimation();

        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if (hit.collider.CompareTag(groundTag))
                {
                    agent.SetDestination(hit.point);
                }
            }
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
}
