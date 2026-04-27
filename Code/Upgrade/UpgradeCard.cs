using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;


public class UpgradeCard : MonoBehaviour
{
    public UpgradeCast UpgradeCast;

    public UpgradeEnum UpgradeEnum;
    public void GetUpgrade(Upgrade upgrade)
    {
        UpgradeEnum = upgrade.upgradeEnum;

        //UI - Desc연동
    }

    //Ui클릭했을 때 실행
    public void UpgradeChoice() //업그레이드 골랐을 때
    {
        UpgradeCast.SetUpgrade(UpgradeEnum);

        EffectPlayer effect =
            PoolManager.Instance.Pop(ObjectPooling.PoolingType.UpgradeSelectEffect) as EffectPlayer;

        effect.SetPositionAndPlay(Input.mousePosition);
    }
}
