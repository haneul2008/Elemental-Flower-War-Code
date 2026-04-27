using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public struct Upgrade
{
    public UpgradeEnum upgradeEnum;
}
public class UpgradeCore : MonoBehaviour
{
    [SerializeField] private List<Upgrade> _upgrades = new List<Upgrade>();
    [SerializeField] private List<UpgradeCard> _upgradeCards = new List<UpgradeCard>();

    public void SetUpgradeCard()
    {
        foreach(UpgradeCard card in _upgradeCards)
        {
            Upgrade randUpgrade = _upgrades[Random.Range(0, _upgrades.Count)];
            card.GetUpgrade(randUpgrade);
        }
    }
}
