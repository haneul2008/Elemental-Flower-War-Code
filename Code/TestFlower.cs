using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestFlower : MonoBehaviour
{
    [SerializeField] private PlantFlower _plantFlower;
    [SerializeField] private FlowerElementType _type;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            _plantFlower.ReadyToPlant(_type);
        }
    }
}
