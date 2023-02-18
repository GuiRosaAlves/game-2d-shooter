using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShip : MonoBehaviour, IDamageable
{
    private const string NormalAttackButton = "NormalAttack";
    private const string SpecialAttackButton = "SpecialAttack";
    private const string VerticalAxis = "Vertical";
    private const string HorizontalAxis = "Horizontal";
    private const float DestroyedHPTreshold = 0f;
    private const string AnimatorHealth = "health";
    private const string AnimatorNormalAttack = "normal-attack";
    private const string AnimatorSpecialAttack = "special-attack";

    [SerializeField] private float _health = 100f;
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _rotationSpeed = 2f;
    [SerializeField] private Projectile _ammo;
    [SerializeField] private float _normalAttackFireRate;
    [SerializeField] private float _specialAttackFireRate;
    [SerializeField] private Transform _normalAttackFirePoint;
    [SerializeField] private Transform[] _specialAttackFirePoints;
    [SerializeField] private Animator _animator;
    private float _normalAttackTimer;
    private float _specialAttackTimer;

    public float Health => _health;

    public event System.Action PlayerShipDestroyed;

    private void Update()
    {
        UpdateAnimatorParams();
        if (_health <= DestroyedHPTreshold)
        {
            PlayerShipDestroyed?.Invoke();
            return;
        }

        _normalAttackTimer += Time.deltaTime;
        _specialAttackTimer += Time.deltaTime;

        if (Input.GetButton(NormalAttackButton) && _normalAttackTimer >= _normalAttackFireRate)
        {
            NormalAttack();
        }
        if (Input.GetButtonDown(SpecialAttackButton) && _specialAttackTimer >= _specialAttackFireRate)
        {
            SpecialAttack();
        }

        var velocityY = Input.GetAxis(VerticalAxis);
        var velocityX = Input.GetAxis(HorizontalAxis);
        var dirVector = new Vector2(0, velocityY);

        var velocity = dirVector * _moveSpeed;
        var newPosition = velocity * Time.deltaTime;

        transform.Translate(newPosition, Space.Self);
        transform.Rotate(0f, 0f, -velocityX * _rotationSpeed);
    }

    private void NormalAttack()
    {
        Instantiate(_ammo, _normalAttackFirePoint.position, transform.rotation);
        _animator.SetTrigger(AnimatorNormalAttack);
        _normalAttackTimer = 0f;
    }
    private void SpecialAttack()
    {

        foreach (var firePoint in _specialAttackFirePoints)
        {
            Instantiate(_ammo, firePoint.position, firePoint.rotation);
        }
        _animator.SetTrigger(AnimatorSpecialAttack);
        _specialAttackTimer = 0f;
    }

    public void TakeDamage(float damage)
    {
        _health -= damage;
    }

    private void UpdateAnimatorParams()
    {
        _animator.SetFloat(AnimatorHealth, _health);
        if (_health <= 0f)
        {
            return;
        }
        var isShooting = Input.GetButton(NormalAttackButton);
        _animator.SetBool(AnimatorNormalAttack, isShooting);
    }
}
