using System.Collections;
using UnityEngine;

public delegate void CombatEvent(float damage, Ashe attacker,
    Enemy target);

public class Ashe : MonoBehaviour
{
    [SerializeField] private DetectEnemy detectEnemy;
    [SerializeField] private float attackSpeed;
    [SerializeField] private float dame;

    private Enemy _target;
    private Animator _animator;
    public event CombatEvent OnAttack;
    public event CombatEvent OnEnemyDie;

    private bool _isAttack;
    private static readonly int AttackParaAnim = Animator.StringToHash("attack");
    private WaitForSeconds _waitForSeconds;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        detectEnemy.OnDetectEnemy += HandleOnDetectEnemy;
        _waitForSeconds = new WaitForSeconds(1 / attackSpeed);
    }

    private void HandleOnDetectEnemy()
    {
        if (_isAttack)
            return;
        if (detectEnemy.GetEnemy(out var enemy) == false)
            return;

        _target = enemy;
        StartCoroutine(Attack());
    }

    private IEnumerator Attack()
    {
        _isAttack = true;
        var timeDelay = 1 / attackSpeed;
        while (_target.isDead == false)
        {
            yield return CoroutineAttack();
        }

        if (detectEnemy.GetEnemy(out var enemy) == false)
        {
            _isAttack = false;
            yield break;
        }

        _target = enemy;
    }

    private IEnumerator CoroutineAttack()
    {
        _animator.SetFloat(AttackParaAnim, attackSpeed);
        yield return _waitForSeconds;
    }

    private void SpawnArrow()
    {
        Debug.Log("Attack");
    }
}