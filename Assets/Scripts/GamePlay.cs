using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class GamePlay : MonoBehaviour
{
    [SerializeField] private Ashe attacker;
    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private BoxCollider2D spawnArea;
    [SerializeField] private int amountOfWave;
    [SerializeField] private float spawnDelayTime = 1f;

    private readonly HashSet<Enemy> _listSpawn = new();

    private void Awake()
    {
        enemySpawner.onReturnEnemy += HandleOnEnemyDie;
        //attacker.OnAttackDone += HandleOnAttackDone;
    }

    // private void HandleOnAttackDone(Enemy target)
    // {
    //     if (_listSpawn.Count <= 1)
    //         attacker.RemoveTarget();
    //     else
    //     {
    //         foreach (var enemy in _listSpawn)
    //         {
    //             if (target != enemy)
    //             {
    //                 SetTargetToAttack(enemy);
    //                 return;
    //             }
    //         }
    //     }
    // }

    private void Start()
    {
        StartCoroutine(SpawnWave());
    }

    private void HandleOnEnemyDie(Enemy obj)
    {
        _listSpawn.Remove(obj);
        if (IsCanSpawn())
        {
            attacker.RemoveTarget();
            StartCoroutine(SpawnWave(spawnDelayTime));
        }
        else
        {
            var target = GetTarget();
            SetTargetToAttack(target);
        }
    }

    private Enemy GetTarget()
    {
        return _listSpawn.FirstOrDefault(enemy => enemy.isDead == false);
    }

    private bool IsCanSpawn()
    {
        return _listSpawn.Count == 0;
    }

    private IEnumerator SpawnWave(float timeDelay)
    {
        yield return new WaitForSeconds(timeDelay);
        StartCoroutine(SpawnWave());
    }

    private IEnumerator SpawnWave()
    {
        _listSpawn.Clear();
        for (var i = 0; i < amountOfWave; i++)
        {
            yield return null;
            var enemy = enemySpawner.GetEnemy();
            enemy.transform.position = GetRandomPos();
            _listSpawn.Add(enemy);
        }
        var target = GetTarget();
        SetTargetToAttack(target);
    }

    private void SetTargetToAttack(Enemy target)
    {
        attacker.SetTarget(target, GetAttackTime(attacker, target));
    }

    private static int GetAttackTime(Ashe ashe, Enemy target)
    {
        var timeAttack = target.Health / ashe.Damage;
        var remainder = target.Health % ashe.Damage;
        if (remainder > 0)
            timeAttack++;
        return timeAttack;
    }

    private Vector3 GetRandomPos()
    {
        var bounds = spawnArea.bounds;
        var posX = bounds.max.x;
        var randomY = Random.Range(bounds.min.y, bounds.max.y);
        var randomZ = Random.Range(-1f, 1f);
        return new Vector3(posX, randomY, randomZ);
    }
}