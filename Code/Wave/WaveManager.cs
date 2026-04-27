using System;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoSingleton<WaveManager>
{
    public event Action OnWaveChanged;
    public event Action OnAllWaveEnded;

    public WaveDataSO NowWave { get; private set; }

    [SerializeField] private List<WaveDataSO> _waveList;
    [SerializeField] private Core _core;

    private float _currentTime;
    private int _count;
    private void Awake()
    {
        NowWave = _waveList[0];
    }
    private void Update()
    {
        _currentTime += Time.deltaTime;

        if (_currentTime > NowWave.waveTime && _count < _waveList.Count)
        {
            _count++;
            ChangeWave(_count);
        }
    }

    private void ChangeWave(int index)
    {
        if (index >= _waveList.Count)
        {
            OnAllWaveEnded?.Invoke();
            print("AllWaveEnded");
            return;
        }

        NowWave = _waveList[index];
        _currentTime = 0;

        OnWaveChanged?.Invoke();
        //_core.IncreaseHealth();
    }
}
