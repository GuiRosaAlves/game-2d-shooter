using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private IDamageable _owner;
    [SerializeField] private Vector3 _offset;
    [SerializeField] private Slider _healthSlider;

    public IDamageable Owner { get => _owner; set => _owner = value; }

    private void Update()
    {
        if (_owner != null)
        {
            return;
        }
        transform.position = _owner.transform.position + _offset;
    }
}
