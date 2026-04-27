using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolveEffect : MonoBehaviour
{
    [SerializeField] private bool _dead;
    [SerializeField] private float _speed = 1;

    private Material _mat;
    private float _fade;
    private bool _isDissolve;

    private readonly int _fadeId = Shader.PropertyToID("_Fade");
    private void Awake()
    {
        _mat = TryGetComponent(out SpriteRenderer spriter)
            ? spriter.material : transform.Find("Visual").GetComponent<SpriteRenderer>().material;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !_isDissolve)
        {
            Dissolve(!_dead);
        }

        if (!_isDissolve) return;

        if(_dead)
            _fade -= Time.deltaTime * _speed;
        else
            _fade += Time.deltaTime * _speed;

        _mat.SetFloat(_fadeId, _fade);

        if (_dead && _fade < 0 || !_dead && _fade > 1)
            _isDissolve = false;
    }

    public void Dissolve(bool dead) //dead = true, appear = false
    {
        _isDissolve = true;
        _dead = dead;
        _fade = dead ? 1 : 0;
    }
}
