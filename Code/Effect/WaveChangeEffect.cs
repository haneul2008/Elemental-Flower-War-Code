using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveChangeEffect : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private List<Vector2> _ringPos = new List<Vector2>();

    private readonly int _distanceId = Shader.PropertyToID("_WaveDistanceFromCenter");
    private readonly int _spawnPosId = Shader.PropertyToID("_RingSpawnPos");

    private float _distance;
    private bool _isPlaying;
    private Material _mat;
    private int _index;
    private void Awake()
    {
        _mat = GetComponent<SpriteRenderer>().material;
        WaveManager.Instance.OnWaveChanged += PlayEffect;
        WaveManager.Instance.OnAllWaveEnded += UnSubcribe;
    }

    private void UnSubcribe()
    {
        WaveManager.Instance.OnWaveChanged -= PlayEffect;
        WaveManager.Instance.OnAllWaveEnded -= UnSubcribe;
    }

    private void Update()
    {
        if (!_isPlaying) return;

        _distance += Time.deltaTime * _speed;

        _mat.SetFloat(_distanceId, _distance);

        if (_distance > 1)
            _isPlaying = false;
    }

    private void PlayEffect()
    {
        _mat.SetVector(_spawnPosId, _ringPos[++_index]);

        _distance = -0.1f;

        _isPlaying = true;
    }
}
