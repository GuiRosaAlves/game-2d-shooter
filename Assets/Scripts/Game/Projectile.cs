using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _damage = 5f;
    private float _lifeTime = 5f;

    private void Start()
    {
        Destroy(gameObject, _lifeTime);
    }
    void Update()
    {
        var velocity = _moveSpeed * Time.deltaTime * transform.up;
        transform.position += velocity;
        Debug.DrawRay(transform.position, transform.up * _moveSpeed, Color.green);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.name + "NAMMEEE!!!");
        var damageableObj = collision.gameObject.GetComponent<IDamageable>();
        if (damageableObj != null)
        {
            damageableObj?.TakeDamage(_damage);
        }

        Destroy(gameObject);
    }
}
