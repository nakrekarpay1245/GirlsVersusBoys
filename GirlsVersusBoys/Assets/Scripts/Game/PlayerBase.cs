using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : MonoBehaviour
{
    [SerializeField]
    private float health;

    private void TakeDamage()
    {
        health--;
        if (health <= 0)
        {
            Dead();
        }
        else
        {
            Debug.Log("Get Hit Effect");
        }
    }

    public void Dead()
    {
        Debug.Log("Level Failed");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
            TakeDamage();
        }
    }
}
