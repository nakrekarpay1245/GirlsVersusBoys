using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public bool isSoldier;
    public bool isEnemy;

    [SerializeField]
    private float speed = 1;

    private void Awake()
    {
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
    }
}
