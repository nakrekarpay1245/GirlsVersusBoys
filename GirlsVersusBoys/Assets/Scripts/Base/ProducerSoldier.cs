using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProducerSoldier : Soldier
{
    [Header("HEALTH")]
    [SerializeField]
    private ParticleSystem getHitParticle;

    [SerializeField]
    private Image healthBarFill;

    [Header("PRODUCE")]
    [SerializeField]
    private float produceTime;
    [SerializeField]
    private float produceAmount;

    [SerializeField]
    private ParticleSystem produceParticle;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private Collider collider;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        collider = GetComponentInChildren<Collider>();
        produceParticle = GetComponentInChildren<ParticleSystem>();

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
        produceParticle.Stop();
    }

    public override void Deploye()
    {
        gameObject.layer = 10;
        produceParticle.Play();
        StartCoroutine(ProduceMoneyRouitne());
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
        if (other.gameObject.CompareTag("Projectile"))
        {
            Projectile projectile = other.GetComponent<Projectile>();
            if (projectile.isEnemy)
            {
                // Debug.Log("Some enemy throw a projectile to me");
                DecreaseHealth();
            }
        }
    }

    private IEnumerator ProduceMoneyRouitne()
    {
        while (true)
        {
            yield return new WaitForSeconds(produceTime);
            ProduceMoney();
        }
    }

    private void ProduceMoney()
    {
        Manager.instance.IncreaseMoney(produceAmount);
    }
}
