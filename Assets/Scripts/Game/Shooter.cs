using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : Enemy
{
    private const string AnimatorHealth = "health";
    private const string AnimatorFired = "fired";
    [SerializeField] private float _distanceToFollow = 15f;
    [SerializeField] private float _fireRate = 3f;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private Projectile _ammo;

    private float _timer = 0f;

    protected override void Update()
    {
        UpdateAnimatorParameters();
        base.Update();
        if (!_target)
        {
            return;
        }
        _timer += Time.deltaTime;

        var distanceFromPlayer = transform.position - _target.position;

        if (distanceFromPlayer.magnitude < _distanceToFollow)
        {
            if (_timer >= _fireRate)
            {
                Shoot();
            }

            ApplySteeringVelocity();
            var lookDir = transform.position + _currVelocity;
            var angle = Utils.LookAtTarget2D(transform, lookDir);
            transform.Rotate(0, 0, angle);
            return;
        }

        transform.position += _currVelocity * Time.deltaTime;
    }


    private void Shoot()
    {
        Instantiate(_ammo, _firePoint.position, transform.rotation);
        _animator.SetTrigger(AnimatorFired);
        _timer = 0f;
    }

    private void UpdateAnimatorParameters()
    {
        _animator.SetFloat(AnimatorHealth, _health);
    }
}
