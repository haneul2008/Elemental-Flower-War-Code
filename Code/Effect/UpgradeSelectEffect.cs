using ObjectPooling;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UpgradeSelectEffect : PoolableMono
{
    [SerializeField] private List<Sprite> _effectFrames = new List<Sprite>();
    [SerializeField] private float _term;

    private WaitForSecondsRealtime _ws;
    private int _cnt;
    private Coroutine _coroutine;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _ws = new WaitForSecondsRealtime(_term);
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.material.renderQueue = 2000;
    }

    public void PlayEffect(Vector2 position)
    {
        transform.position = position;

        _coroutine = StartCoroutine(EffectPlayCoroutine());
    }

    private IEnumerator EffectPlayCoroutine()
    {
        yield return _ws;

        _spriteRenderer.sprite = _effectFrames[_cnt];
        _cnt++;

        if (_cnt == _effectFrames.Count)
        {
            PoolManager.Instance.Push(this);

            yield break;
        }

        _coroutine = StartCoroutine(EffectPlayCoroutine());
    }

    public override void ResetItem()
    {
        if (_coroutine != null) 
            StopCoroutine(_coroutine);

        _cnt = 0;
    }
}
