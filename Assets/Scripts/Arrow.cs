using UnityEngine;
using UnityEngine.Pool;

public class Arrow : MonoBehaviour
{
    public Rigidbody2D rigid2D;
    private Ashe _attacker;
    private Enemy _target;
    private int _damage;

    private ObjectPool<Arrow> _pool;

    private void Awake()
    {
        rigid2D = GetComponent<Rigidbody2D>();
    }

    public void SetPool(ObjectPool<Arrow> pool)
    {
        _pool = pool;
    }

    public void Init(Ashe attacker, Enemy target, int damage)
    {
        _attacker = attacker;
        _target = target;
        _damage = damage;
    }

    private void Update()
    {
        var velocity = rigid2D.velocity;
        var angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        var enemy = col.GetComponent<Enemy>();
        if (enemy)
        {
            enemy.EventTakeDamage(_damage, _attacker, _target);
        }

        _pool.Release(this);
    }
}