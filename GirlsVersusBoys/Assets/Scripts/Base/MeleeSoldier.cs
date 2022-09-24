using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MeleeSoldier : Soldier
{
    [Header("HEALTH")]
    [SerializeField]
    private string deadAnim;

    [SerializeField]
    private ParticleSystem getHitParticle;

    [SerializeField]
    private Image healthBarFill;

    [Header("ATTACK")]
    [SerializeField]
    private float attackTime;
    [SerializeField]
    private float nextAttackTime;

    [SerializeField]
    private string attackAnim;

    [SerializeField]
    private AttackArea attackArea;

    [SerializeField]
    private List<Unit> oppositeUnitsInAttackArea;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private Collider collider;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        collider = GetComponentInChildren<Collider>();
        attackArea = GetComponentInChildren<AttackArea>();
        health = maxHealth;
        DisplayHealth();
        ChangeState(UnitState.Idle);
        NotDeployed();
    }

    private void Update()
    {
        if (state == UnitState.Attack)
        {
            Attack();

            if (oppositeUnitsInAttackArea.Count > 0)
            {
                for (int i = 0; i < oppositeUnitsInAttackArea.Count; i++)
                {
                    if (oppositeUnitsInAttackArea[i].health <= 0)
                    {
                        RemoveOppositeUnit(oppositeUnitsInAttackArea[i]);
                    }
                }
            }
            else
            {
                ChangeState(UnitState.Idle);
            }
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            ChangeState(UnitState.Attack);
        }
    }

    public override void Dead()
    {
        PlayAnimation(deadAnim);
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

    public override void Attack()
    {
        if (Time.time > nextAttackTime)
        {
            nextAttackTime = Time.time + attackTime;
            PlayAnimation(attackAnim);
        }
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
                // Debug.Log("Soldier Idle State");
            }

            else if (state == UnitState.Attack)
            {
                // Debug.Log("Soldier Attack State");
            }

            else if (state == UnitState.Dead)
            {
                // Debug.Log("Soldier Dead State");
                Dead();
            }
        }
    }

    public override void NotDeployed()
    {
        attackArea.gameObject.SetActive(false);
        gameObject.layer = 2;
    }

    public override void Deploye()
    {
        attackArea.gameObject.SetActive(true);
        gameObject.layer = 10;
    }

    public override void AddOppositeUnit(Unit _unit)
    {
        oppositeUnitsInAttackArea.Add(_unit);
    }
    public override void RemoveOppositeUnit(Unit _unit)
    {
        oppositeUnitsInAttackArea.Remove(_unit);
    }

    private void PlayAnimation(string name)
    {
        animator.CrossFade(name, 0.2f);
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
                Destroy(projectile.gameObject);
            }
        }
    }
}
