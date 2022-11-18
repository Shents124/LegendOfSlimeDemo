using UnityEngine;
using UnityEngine.Pool;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private int health = 10;
    
    public bool isDead;
    private ObjectPool<Enemy> _pool;
    public int Health => health;
    private event CombatEvent OnTakeDamage;
    
    private void OnEnable()
    {
        health = 10;
        isDead = false;
    }

    private void Awake()
    {
        OnTakeDamage += HandleOnTakeDamage;
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        transform.position += Vector3.left * (Time.deltaTime * speed);
    }
    
    public void EventTakeDamage(int damage, Ashe attacker, Enemy target)
    {
        OnTakeDamage?.Invoke(damage, attacker, target);
    }
    
    private void HandleOnTakeDamage(int damage, Ashe attacker, Enemy target)
    {
        if(isDead)
            return;
        health -= damage;
        if (health <= 0)
        {
            isDead = true;
            ReleaseToPool();
        }
    }

    public void SetPool(ObjectPool<Enemy> pool) => _pool = pool;

    private void ReleaseToPool()
    {
        _pool.Release(this);
    }
}