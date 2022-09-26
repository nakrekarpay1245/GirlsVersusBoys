using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThrowerEnemy : Enemy
{
    [Header("HEALTH")]
    [SerializeField]
    private string deadAnim;

    [SerializeField]
    private ParticleSystem getHitParticle;

    [SerializeField]
    private Image healthBarFill;

    [SerializeField]
    private float produceAmount = 10;

    [Header("MOVEMENT")]
    [SerializeField]
    private float moveSpeed;
    private float speed;

    [Header("ATTACK")]
    [SerializeField]
    private float attackTime;
    [SerializeField]
    private float nextAttackTime;

    [SerializeField]
    private string attackAnim;

    public List<Unit> oppositeUnitsInAttackArea;

    private Animator animator;

    private Collider collider;

    private void Start()
    {
        Manager.instance.AddEnemy(this);
        animator = GetComponentInChildren<Animator>();
        collider = GetComponentInChildren<Collider>();
        health = maxHealth;
        DisplayHealth();
        ChangeState(UnitState.Move);
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

        else if (state == UnitState.Move)
        {
            Move();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            ChangeState(UnitState.Attack);
        }
    }

    public void Move()
    {
        // Debug.Log("I am moving");
        transform.Translate(-transform.right * speed * Time.deltaTime);
    }

    public override void Dead()
    {
        Manager.instance.RemoveEnemy(this);
        PlayAnimation(deadAnim);
        collider.enabled = false;
        ProduceMoney();
        Destroy(gameObject, 5);
    }

    private void ProduceMoney()
    {
        Manager.instance.IncreaseMoney(produceAmount);
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
                // Debug.Log("Enemy Idle State");
                speed = 0;
                animator.SetFloat("speed", speed);
                ChangeState(UnitState.Move);
            }

            else if (state == UnitState.Attack)
            {
                // Debug.Log("Enemy Attack State");
                speed = 0;
                animator.SetFloat("speed", speed);
            }

            else if (state == UnitState.Move)
            {
                // Debug.Log("Enemy Move State");
                speed = moveSpeed;
                animator.SetFloat("speed", speed);
            }

            else if (state == UnitState.Dead)
            {
                // Debug.Log("Enemy Dead State");
                Dead();
            }
        }
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
        if (other.gameObject.CompareTag("SoldierAttackBox"))
        {
            // Debug.Log("Some enemy hit me");
            DecreaseHealth();
        }

        if (other.gameObject.CompareTag("SoldierProjectile"))
        {
            // Debug.Log("Some enemy throw a projectile to me");
            DecreaseHealth();
        }
    }
}
