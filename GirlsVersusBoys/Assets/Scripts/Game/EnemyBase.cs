using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    [SerializeField]
    private List<Enemy> enemyList;

    [SerializeField]
    private List<Transform> generateTransformList;

    [SerializeField]
    private float enemyGenerateTime;
    private float nextGenerateTime;

    private bool generate;

    public static EnemyBase instance;
    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
    }

    private void Update()
    {
        GenerateEnemies();
    }

    public void GenerateEnemies()
    {
        if (Time.time > nextGenerateTime && generate)
        {
            nextGenerateTime = Time.time + enemyGenerateTime;

            Enemy currentEnemy = Instantiate(enemyList[Random.Range(0, enemyList.Count)],
               generateTransformList[Random.Range(0, generateTransformList.Count)].position,
               Quaternion.LookRotation(-transform.forward));
        }
    }

    public void Start()
    {
        generate = true;
    }

    public void Stop()
    {
        generate = false;
    }
}
