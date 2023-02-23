using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ComboInstanceData
{
    [HideInInspector] public string name;
    public float baseAttackPercentage = 1f;
    public float baseAttackSpeedPercentage = 1f;
}
