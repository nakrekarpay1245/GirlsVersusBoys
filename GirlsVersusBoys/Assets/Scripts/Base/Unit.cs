using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [Header("HEALTH")]
    public float maxHealth;
    public float health;

    public UnitState state;

    public virtual void DecreaseHealth()
    {
        health = health - 1;
        // Debug.Log("health: " + health.ToString());
        if (health <= 0)
        {
            GetHit();
            ChangeState(UnitState.Dead);
        }
        else
        {
            GetHit();
        }
    }

    public virtual void ChangeState(UnitState _state)
    {
        state = _state;
    }

    public virtual void GetHit()
    {
        Debug.Log(gameObject.name + " is getting hit");
    }

    public virtual void Dead()
    {
        Debug.Log("I am sorry but I am a little died");
    }
    public virtual void Attack()
    {
        Debug.Log("I am attacking");
    }

    public virtual void AddOppositeUnit(Unit _unit)
    {

    }

    public virtual void RemoveOppositeUnit(Unit _unit)
    {

    }
}
