using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [Header("HEALTH")]
    [SerializeField]
    private float maxHealth;
    [SerializeField]
    private float health;

    public UnitState state;

    private void DecreaseHealth()
    {
        health = health - 1;
        if (health <= 0)
        {
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
}
