using System;
using UnityEngine;
using UnityEngine.Pool;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Enemy prefab;
    public Action<Enemy> onReturnEnemy;
    private ObjectPool<Enemy> _pool;

    private void Awake()
    {
        onReturnEnemy += OnReturnEnemy;
    }

    // Start is called before the first frame update
    void Start()
    {
        _pool = new ObjectPool<Enemy>(CreateEnemy, OnTakeEnemy, onReturnEnemy);
    }

    public Enemy GetEnemy()
    {
        return _pool.Get();
    }
    
    private void OnReturnEnemy(Enemy obj)
    {
        obj.gameObject.SetActive(false);
    }

    private void OnTakeEnemy(Enemy obj)
    {
        obj.gameObject.SetActive(true);
    }

    private Enemy CreateEnemy()
    {
        var enemy = Instantiate(prefab, transform, true);
        enemy.SetPool(_pool);
        return enemy;
    }
}
