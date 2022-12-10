using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField] private EInteractType interactType;
    [SerializeField] private float interactRadius = 2f;
    

    void Awake()
    {
        gameObject.tag = "Interactable";
    }

    public EInteractType GetInteractType()
    {
        return interactType;
    }

    public float GetInteractRadius()
    {
        return interactRadius;
    }

    public void Interact()
    {
        Debug.Log($"Interacted with " + gameObject.name);
    }
}

public enum EInteractType
{
    Enemy,
    Item,
    NPC,
    Interactable
}
