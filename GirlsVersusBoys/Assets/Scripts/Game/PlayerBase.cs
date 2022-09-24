using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerBase : MonoBehaviour
{
    [SerializeField]
    private float health;

    [SerializeField]
    private GameObject cross_1;
    [SerializeField]
    private GameObject cross_2;
    [SerializeField]
    private GameObject cross_3;

    private void TakeDamage()
    {
        health--;
        DisplayCrosses();
        if (health <= 0)
        {
            Dead();
        }
        else
        {
            Debug.Log("Get Hit Effect");
        }
    }

    public void DisplayCrosses()
    {
        switch (health)
        {
            case 0:
                cross_1.transform.DOScale(Vector3.one, 1);
                break;

            case 1:
                cross_2.transform.DOScale(Vector3.one, 1);
                break;

            case 2:
                cross_3.transform.DOScale(Vector3.one, 1);
                break;

            default:
                cross_1.transform.DOScale(Vector3.zero, 1);
                cross_2.transform.DOScale(Vector3.zero, 1);
                cross_3.transform.DOScale(Vector3.zero, 1);
                break;
        }
    }
    public void Dead()
    {
        // Debug.Log("Level Failed");
        Manager.instance.FinishLevel(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            // Debug.Log("Enemy entered base");
            Destroy(other.gameObject);
            TakeDamage();
        }
    }
}
