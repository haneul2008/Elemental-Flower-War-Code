using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.Rendering.DebugUI;

public class UpgradeCast : MonoBehaviour
{
    public UnityEvent OnUpgradeEvent;

    private NotifyValue<Collider2D> FlowerCollider = new NotifyValue<Collider2D>();

    [SerializeField] private ContactFilter2D _contactFilter;
    [SerializeField] private float _castRadius;
    
    private bool _casting;
    private Collider2D[] _collider;
    private UpgradeEnum _upgradeEnum;
    private Flower _flower;

    private readonly int _blinkID = Shader.PropertyToID("_IsSelect");
    private readonly int _blinkColorID = Shader.PropertyToID("_HitColor");

    private void Awake()
    {
        _collider = new Collider2D[1];
        FlowerCollider.OnvalueChanged += HandleFlowerChanged;
    }

    private void OnDestroy()
    {
        FlowerCollider.OnvalueChanged -= HandleFlowerChanged;
    }

    private void HandleFlowerChanged(Collider2D prev, Collider2D next)
    { 
        if(_flower != null)
            _flower.FlowerMat.SetInt(_blinkID, 0);

        if (next == null) return;

        _flower = next.GetComponent<Flower>();
        _flower.FlowerMat.SetInt(_blinkID, 1);
    }

    private void Update()
    {
        if (!_casting) return;

        FlowerCollider.Value = DetectPlayer();
        FlowerChoice();
    }

    private Collider2D DetectPlayer()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        int cnt = Physics2D.OverlapCircle(mousePos, _castRadius, _contactFilter, _collider);

        return cnt > 0? _collider[0] : null;
    }

    public void SetUpgrade(UpgradeEnum upgradeEnum)
    {
        _casting = true;
        _upgradeEnum = upgradeEnum;
    }

    private void FlowerChoice()
    {
        if (Input.GetMouseButtonDown(0) && _flower != null)
        {
            if (!_flower.FlowerUpgrade.CheckUpgrade(_upgradeEnum, true))
            {
                _flower.FlowerMat.SetColor(_blinkColorID, new Color(1 * 1.8f, 0, 0));

                DOVirtual.DelayedCall(0.3f, () =>
                {
                    _flower.FlowerMat.SetColor(_blinkColorID, new Color(1, 1, 1) * 1.8f);
                });
                return;
            }

            Time.timeScale = 1;
            _casting = false;

            _flower.UpgradeFlower(_upgradeEnum);

            EffectPlayer effect =
                PoolManager.Instance.Pop(ObjectPooling.PoolingType.StarEffect) as EffectPlayer;

            effect.SetPositionAndPlay(_flower.transform.position);

            _flower.FlowerMat.SetInt(_blinkID, 0);
            _flower = null;

            OnUpgradeEvent?.Invoke();
        }
    }
}
