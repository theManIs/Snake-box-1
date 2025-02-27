﻿using System;
using UnityEngine;

namespace Snake_box
{
    [Serializable]
    public class TurretPreferences
    {
        [Range(1, 15)]
        public int StartLevel = 1;
        public float BaseLevelModifier = 2;
        [Range(0, 100)]
        public float Range = 20;
        [Range(5, 1000)]
        public float Cooldown = 250;
        public float UpdatePrice = 134;
        public float BuyPrice = 134;
        public GameObject ThumbnailPrefab;
        public EnemyType PreferableEnemy;
        public TurretProjectileAbs TurretShell;
        public GameObject TurretPrefab;
        public ArmorTypes ArmorPiercing;
        public string FirePointHierarchy = "TurretShift/FirePoint";
        public ProjectilePreferences ProjectilePreferences;
    }
}