using System;
using UnityEngine;

public delegate void CombatEvent(int damage, Ashe attacker, Enemy target);

public class Ashe : MonoBehaviour
{
    [SerializeField] private float attackSpeed;
    [SerializeField] private int damage;
    [SerializeField] private GameObject launchPosition;
    [SerializeField] private BulletSpawner bulletSpawner;

    public int Damage => damage;
    public event CombatEvent OnAttack;
    public Action<Enemy> OnAttackDone;
    public bool hasTarget;
    
    private Enemy _target;
    private Animator _animator;
   
    private static readonly int AttackParamAnim = Animator.StringToHash("attack");
   
    private int _attackTime;
    private int _currentAttackTime;
    private float _attackDelay;
    private float _attackCooldownLeft;
    
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        OnAttack += HandleOnAttack;
    }
    
    private void Start()
    {
        _attackDelay = 1 / attackSpeed;
    }

    private void Update()
    {
        _attackCooldownLeft -= Time.deltaTime;
        if (hasTarget == false || _attackCooldownLeft > 0) 
            return;

        _currentAttackTime++;
        _animator.SetFloat(AttackParamAnim, attackSpeed);
        _attackCooldownLeft = _attackDelay;
        // if(_currentAttackTime >= _attackTime)
        //     OnAttackDone?.Invoke(_target);
    }

    public void EventOnAttack(int damage, Ashe attacker, Enemy target)
    {
        OnAttack?.Invoke(damage, attacker, target);
    }
    
    private void HandleOnAttack(int damage, Ashe attacker, Enemy target)
    {
        throw new System.NotImplementedException();
    }

    public void SetTarget(Enemy target, int attackTime)
    {
        _target = target;
        _attackTime = attackTime;
        hasTarget = true;
    }

    public void RemoveTarget()
    {
        hasTarget = false;
        _animator.SetFloat(AttackParamAnim, 1);
    }
    
    private void SpawnArrow()
    {
        if(hasTarget == false)
            return;
        var arrow = bulletSpawner.GetBullet();
        arrow.Init(this, _target, damage);
        arrow.transform.position = launchPosition.transform.position;
        ProjectileLauncher.Launch(arrow.rigid2D, launchPosition, _target.gameObject);
    }
}