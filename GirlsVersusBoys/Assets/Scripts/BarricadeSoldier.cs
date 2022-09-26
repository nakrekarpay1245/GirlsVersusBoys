using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarricadeSoldier : Soldier
{
    [Header("HEALTH")]
    [SerializeField]
    private ParticleSystem getHitParticle;

    [SerializeField]
    private Image healthBarFill;

    private Collider collider;

    private void Awake()
    {
        collider = GetComponentInChildren<Collider>();

        health = maxHealth;
        ChangeState(UnitState.Idle);

        NotDeployed();
    }

    public override void Dead()
    {
        collider.enabled = false;
        Destroy(gameObject, 5);
        gridBox.UnDeployed();
    }

    public override void GetHit()
    {
        // Debug.Log("I am getting hit");

        DisplayHealth();

        ParticleSystem currentGetHitParticle =
            Instantiate(getHitParticle, transform.position, Quaternion.identity);
    }

    public void DisplayHealth()
    {
        if (health <= 1)
        {
            healthBarFill.color = Color.black;
        }

        healthBarFill.fillAmount = health / maxHealth;
    }

    public override void NotDeployed()
    {
        gameObject.layer = 2;
    }

    public override void Deploye()
    {
        gameObject.layer = 10;
    }

    public override void ChangeState(UnitState _state)
    {
        if (state != _state)
        {
            if (state != UnitState.Dead)
            {
                state = _state;
            }

            if (state == UnitState.Idle)
            {
                // Debug.Log("Soldier Idle");
            }

            else if (state == UnitState.Dead)
            {
                // Debug.Log("Soldier Dead State");
                Dead();
            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("EnemyAttackBox"))
        {
            // Debug.Log("Some enemy hit me");
            DecreaseHealth();
        }
        if (other.gameObject.CompareTag("EnemyProjectile"))
        {
            // Debug.Log("Some enemy throw a projectile to me");
            DecreaseHealth();
        }
    }
}
