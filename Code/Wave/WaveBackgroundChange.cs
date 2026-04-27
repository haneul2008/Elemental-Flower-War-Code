using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WaveBackgroundChange : MonoBehaviour
{
    public UnityEvent OnBackGroundChange;

    [SerializeField] private List<Sprite> backgroundImages;
    [SerializeField] private int currentWaveNum;
    [SerializeField] private float _changeTerm;

    /*[SerializeField] private Material effectMat;
    [SerializeField] private Material lightMat;*/

    private SpriteRenderer _spriter;

    private void Awake()
    {
        _spriter = GetComponent<SpriteRenderer>();

        WaveManager.Instance.OnWaveChanged += BgChange;
        
        _spriter.sprite = backgroundImages[currentWaveNum++];
    }

    private void BgChange()
    {
        DOVirtual.DelayedCall(_changeTerm, () =>
        {
            if (currentWaveNum > 3) return;

            _spriter.sprite = backgroundImages[currentWaveNum++];
            OnBackGroundChange?.Invoke();
        });
    }
}
