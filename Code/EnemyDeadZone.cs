using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeadZone : MonoBehaviour
{
    [SerializeField] private float _speed;

    private bool _isWaveChanged;
    private CircleCollider2D _collider;
    private float _defaultRadius;

    private void Awake()
    {
        WaveManager.Instance.OnWaveChanged += StartFindEnemies;
        _collider = GetComponent<CircleCollider2D>();
        _defaultRadius = _collider.radius;
        _collider.enabled = false;
    }

    private void Update()
    {
        if(!_isWaveChanged) return;

        _collider.radius += Time.deltaTime * _speed;

        if(_collider.radius > 11)
        {
            _collider.enabled = false;
            _isWaveChanged = false;
        }
    }

    private void StartFindEnemies()
    {
        _collider.radius = _defaultRadius;
        _collider.enabled = true;

        _isWaveChanged = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.TryGetComponent(out Enemy enemy))
        {
            enemy.DeadZone();
            enemy.HealthCompo.TakeDamage(Mathf.CeilToInt(enemy.HealthCompo.CurrentHealth), 0);
        }
    }
}
