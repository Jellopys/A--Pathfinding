using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Ability : MonoBehaviour
{
    protected float manaCost;
    protected float cooldown;
    protected AbilityType abilityType;
    protected GameObject abilityPrefab;
    

    protected float GetManaCost() { return manaCost; }
    protected GameObject GetAbilityPrefab() { return abilityPrefab; }

    protected AbilityType GetAbilityType()
    {
        return abilityType;
    }

    public virtual void CastAbility()
    {

    }
}

public enum AbilityType
{
    GroundTarget,
    Target,
    NoTarget,
    Friendly    
}
