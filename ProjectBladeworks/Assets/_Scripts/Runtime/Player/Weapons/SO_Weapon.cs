using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameCells.Player.Weapons
{
    [CreateAssetMenu(fileName = "WeaponData", menuName = "Scriptable Objects/Data/Weapon Data")]
    public class SO_Weapon : ScriptableObject
    {
        public string WeaponName;
        public ERarity Rarity;
        public EWeaponType WeaponType;
        public GameObject WeaponMesh;
        public RuntimeAnimatorController PlayerRuntimeAnimatorController;

        public int comboCount = 3;
        public float comboResetTime = 1.5f;
        public float baseAttackPercentage = 1f;
        public float baseAttackSpeedPercentage = 1f;

        public ComboInstanceData[] comboData;

        private void OnValidate()
        {

#if UNITY_EDITOR

            int oldLength = comboData.Length;

            if (oldLength != comboCount)
                Array.Resize(ref comboData, comboCount);

            for (int i = 0; i < comboCount; i++)
            {
                comboData[i].name = $"Combo {i + 1}";
            }

#endif

        }
    }
}