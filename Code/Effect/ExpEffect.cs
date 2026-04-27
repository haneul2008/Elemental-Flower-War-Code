using UnityEngine;
using DG.Tweening;
using ObjectPooling;
using UnityEngine.Events;

public class ExpEffect : PoolableMono
{
    public UnityEvent OnExpEvent;

    [SerializeField] private float _moveUpAmount;
    [SerializeField] private float _moveUpDelay;

    private Animator _anim;
    private SpriteRenderer _spriteRenderer;
    private void Awake()
    {
        _anim = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void PlayEffect(Vector2 pos)
    {
        transform.position = pos;

        float endY = transform.position.y + _moveUpAmount;

        transform.DOMoveY(endY, _moveUpDelay)
            .SetEase(Ease.OutQuad)
            .OnComplete(() =>
            {
                _anim.SetTrigger("Play");
            });
    }

    public override void ResetItem()
    {
    }

    public void InvokeOnExpEvent()
    {
        OnExpEvent?.Invoke();
    }
}
