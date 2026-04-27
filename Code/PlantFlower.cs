using UnityEngine;
using ObjectPooling;
using System;
using UnityEngine.Events;

public class PlantFlower : MonoBehaviour
{
    public UnityEvent OnPlantEvent;

    [SerializeField] private ContactFilter2D _contactFilter;
    [SerializeField] private UpgradeUI _upgradeUI;
    [SerializeField] private float _range;
    [SerializeField] private GameObject _rangeObj;

    private Flower _flower;
    private bool _isPlanting;
    private Collider2D[] _colliders;

    //테스트용
    private void Awake()
    {
        _colliders = new Collider2D[2];
        _upgradeUI.OnTryPlantFlower += ReadyToPlant;
    }

    private void Start()
    {
        float scale = _range * 2;

        _rangeObj.transform.localScale = new Vector3(scale, scale, 1);
        _rangeObj.SetActive(false);
    }

    private void Update()
    {
        if (!_isPlanting) return;

        FollowToMouse();
    }

    private void FollowToMouse()
    {
        if (_flower == null) return;

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Vector2.Distance(new Vector2(0, 0.3f), mousePos) < _range - 0.3f)
        {
            _flower.transform.position = mousePos;
        }

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 flowerPos = 
                new Vector3(_flower.transform.position.x, _flower.transform.position.y - 0.2f);
            Vector2 size = new Vector2(0.15f, 0.15f);

            int cnt = Physics2D.OverlapBox
                (flowerPos, size, 0, _contactFilter, _colliders);

            if (cnt > 1) return;

            PoolingType poolType = PoolingType.FirePlantEffect;

            switch (_flower.flowerType)
            {
                case FlowerElementType.Fire:
                    poolType = PoolingType.FirePlantEffect;
                    break;

                case FlowerElementType.Ice:
                    poolType = PoolingType.IcePlantEffect;
                    break;

                case FlowerElementType.Water:
                    poolType = PoolingType.WaterPlantEffect;
                    break;

                case FlowerElementType.Electric:
                    poolType = PoolingType.LightningPlantEffect;
                    break;
            }

            EffectPlayer effect = PoolManager.Instance.Pop(poolType) as EffectPlayer;
            effect.SetPositionAndPlay(_flower.transform.position);

            _isPlanting = false;
            _flower.isCanFire = true;
            //_flower.ShootingLiner.SetLine();
            _flower = null;
            Time.timeScale = 1;

            _rangeObj.SetActive(false);

            OnPlantEvent?.Invoke();
        }
    }

    public void ReadyToPlant(FlowerElementType type)
    {
        _flower = PoolManager.Instance.Pop(PoolingType.Flower) as Flower;
        _flower.SetUpFlower(type);
        _rangeObj.SetActive(true);
        _isPlanting = true;
    }
}
