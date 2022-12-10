using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Player player;


    void Update()
    {
        // Debug.Log(LayerMask.GetMask("Clickable"));
    }

    public void ClickTarget()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // RaycastHit hit Physics.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector3.zero, Mathf.Infinity, );
        }
    }
}
