using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    [SerializeField]
    private string oppositeTag;

    private Unit unit;

    private void Awake()
    {
        unit = GetComponentInParent<Unit>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(oppositeTag))
        {
            Debug.Log("Opposite");
            unit.ChangeState(UnitState.Attack);
        }
    }
}
