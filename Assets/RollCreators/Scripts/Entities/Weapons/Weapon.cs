﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct Weapon
{
    public string name;
    public float damage;
    public float rateOfFire;
    public float spread;
    public float speed;
    public bool isExplodable;
    public float fireDamagePercent;
    public float fireDamageRate;
    public float bounceDistance;
}
