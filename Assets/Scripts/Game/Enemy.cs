using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour, IDamageable
{
    protected const float _destroyedTreshold = 0f;

    [SerializeField] protected float _health = 100f;
    [SerializeField] protected float _moveSpeed = 5f;
    [SerializeField] protected Transform _target;
    [SerializeField] protected int _scorePoints = 100;
    [SerializeField] protected Vector3 _currVelocity;
    [SerializeField] protected float _maxVelocity = 2f;
    [SerializeField] protected float _mass = 15f;
    [SerializeField] protected Animator _animator;
    [SerializeField] protected GameObject _destroyedShipDecoyPrefab;
    [SerializeField] protected float _destroyDelay = 1f;
    protected GameObject _destroyedShip = null;

    public event System.Action<Enemy> EnemyShipDestroyed;
    public float Health => _health;
    public int ScorePoints => _scorePoints;
    public Transform Target { get => _target; set => _target = value; }

    protected virtual void Update()
    {
        if (!_target)
        {
            return;
        }
        if (_health <= _destroyedTreshold)
        {
            OnShipDestroyed();
            return;
        }
    }
    protected void ApplySteeringVelocity()
    {
        if (!_target)
        {
            return;
        }

        var _desiredVelocity = _target.position - transform.position;
        _desiredVelocity.Normalize();

        _desiredVelocity *= _maxVelocity;

        var _steeringVelocity = _desiredVelocity - _currVelocity;
        _steeringVelocity /= _mass;

        _currVelocity += _steeringVelocity;
        _currVelocity = Vector3.ClampMagnitude(_currVelocity, _maxVelocity);
    }
    protected virtual void OnShipDestroyed()
    {
        _target = null;
        _currVelocity = Vector3.zero;
        EnemyShipDestroyed?.Invoke(this);
        if (!_destroyedShip)
        {
            _destroyedShip = Instantiate(_destroyedShipDecoyPrefab, transform.position, transform.rotation);
            Destroy(gameObject, _destroyDelay);
        }
    }

    public void TakeDamage(float damage)
    {
        _health -= damage;
    }
}
