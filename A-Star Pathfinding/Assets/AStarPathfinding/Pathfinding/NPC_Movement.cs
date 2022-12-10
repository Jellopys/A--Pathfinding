using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Movement : MonoBehaviour
{
    private int currentPathIndex;
    public float speed = 50f;
    private List<Vector3> pathVectorList;
    Vector3 cachedPos;
    float animTime = 0;
    float moveTime = 1;
    public float a = 1;
    public float b = 1;

    void Start() {
        gameObject.transform.position = new Vector3(5,5,0);
        cachedPos = transform.position;
    }

    void Update() {
        HandleMovement();
    }

    private void HandleMovement() 
    {
        if (pathVectorList == null) return;

        Vector3 targetPosition = pathVectorList[currentPathIndex];

        animTime += Time.deltaTime;
        float tTime = Mathf.Clamp01( animTime / moveTime);

        float tValue = EaseOutBack(tTime);

        transform.position = Vector3.LerpUnclamped(cachedPos, targetPosition, tValue);
        if (tTime >= 1f ) 
        {
            currentPathIndex++;
            cachedPos = transform.position;
            animTime = 0;

            if (currentPathIndex >= pathVectorList.Count) 
            {
                StopMoving();
            }
        }       

        // if (Vector3.Distance(transform.position, targetPosition) > 1f) 
        // {
        //     Vector3 moveDir = (targetPosition - transform.position).normalized;

        //     // transform.position = transform.position + moveDir * speed * Time.deltaTime;

            

        //     transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * speed);
        // } 
        // else 
        // {
        //     currentPathIndex++;
        //     cachedPos = transform.position;
        //     if (currentPathIndex >= pathVectorList.Count) 
        //     {
        //         StopMoving();
        //     }
        // }
    }

    float EaseOutBack(float t ) => CustomEase(a, b, t );

    float CustomEase(float a, float b, float t) {
        float c3 = (a + b - 2);
        float c2 = (3-2*a-b);
        float t2 = t * t;
        float t3 = t2 * t;
        return c3 * t3 + c2 * t2 + a * t;
    }

    private void StopMoving() {
        pathVectorList = null;
    }

    public Vector3 GetCurrentPosition() {
        return transform.position;
    }

    public void SetTargetPosition(Vector3 targetPosition) {
        currentPathIndex = 0;
        pathVectorList = Pathfinding.Instance.FindPath(GetCurrentPosition(), targetPosition);

        if (pathVectorList != null && pathVectorList.Count > 1) {
            pathVectorList.RemoveAt(0);
        }
    }
}
