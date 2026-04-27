using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ObjectPooling;

public class TestEffectPlay : MonoBehaviour
{
    [SerializeField] private PoolingType _effectPoolingType;
    private float _effectDuration;
    private float _lastTime;
    private void Update()
    {
        _effectPoolingType = (PoolingType)Mathf.Clamp((int)_effectPoolingType, 4, 18);

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButton(0))
        {
            if (_lastTime + _effectDuration > Time.realtimeSinceStartup) return;

            if(_effectPoolingType == PoolingType.UpgradeSelectEffect)
            {
                UpgradeSelectEffect upgradeSelectEffect = 
                    PoolManager.Instance.Pop(PoolingType.UpgradeSelectEffect) as UpgradeSelectEffect;

                upgradeSelectEffect.PlayEffect(mousePos);

                _effectDuration = 0.7f;
            }
            else if(_effectPoolingType == PoolingType.Exp)
            {
                ExpEffect expEffect =
                    PoolManager.Instance.Pop(PoolingType.Exp) as ExpEffect;

                expEffect.PlayEffect(mousePos);

                _effectDuration = 0.5f;
            }
            else
            {
                EffectPlayer effect = PoolManager.Instance.Pop(_effectPoolingType) as EffectPlayer;
                effect.SetPositionAndPlay(mousePos);

                _effectDuration = effect._duration;
            }

            _lastTime = Time.realtimeSinceStartup;
        }
    }
}
