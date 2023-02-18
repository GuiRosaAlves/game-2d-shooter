using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chaser : Enemy
{
    private const string AnimatorHealth = "health";
    public float colisionDOT = 5f;
    protected override void Update()
    {
        UpdateAnimatorParameters();
        base.Update();
        ApplySteeringVelocity();
        transform.position += (_currVelocity * Time.deltaTime);
        var angle = Utils.LookAtTarget2D(transform, transform.position + _currVelocity);
        transform.Rotate(0, 0, angle);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        var playerShip = collision.gameObject.GetComponent<PlayerShip>();
        var finalDOT = colisionDOT * Time.deltaTime;
        playerShip?.TakeDamage(finalDOT);
    }

    private void UpdateAnimatorParameters()
    {
        _animator.SetFloat(AnimatorHealth, _health);
    }
}
