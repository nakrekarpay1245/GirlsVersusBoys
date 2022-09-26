using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    private float speed = 1;

    private void Awake()
    {
        // Debug.Log("projectile generated");
        Destroy(gameObject, 10);
    }

    private void Update()
    {
        transform.Translate(transform.right * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Debug.Log("enter ");
        if (other.gameObject.CompareTag("Enemy"))
        {
            // Debug.Log("enter enemy");
            Destroy(gameObject);
        }
        Debug.Log("enter ");
        if (other.gameObject.CompareTag("Soldier"))
        {
            // Debug.Log("enter enemy");
            Destroy(gameObject);
        }
    }
}
