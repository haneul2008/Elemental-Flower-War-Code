using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Era
{
    Ancient,
    Joongse,
    Present,
    Future
}
[CreateAssetMenu(menuName = "SO/WaveData")]
public class WaveDataSO : ScriptableObject
{
    public float waveTime;
    public Era waveType;
    public GameObject tilemap;
}
