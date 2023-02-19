using System;
using System.Collections;
using UnityEngine;

namespace GameCells.Player.Weapons
{
    public class WeaponManager : MonoBehaviour
    {
        [SerializeField] private SO_Weapon _weaponData;
        [SerializeField] private Transform _weaponSocket;

        public event Action<RuntimeAnimatorController> OnWeaponEquip;

        private Coroutine comboTimerCO;
        private int currentComboCount;

        public SO_Weapon WeaponData => _weaponData;
        public int CurrentComboCount => currentComboCount;

        private void Start()
        {
            InitializeWeapon();
        }

        [ContextMenu("Initialize Weapon")]
        private void InitializeWeapon()
        {
            int count = _weaponSocket.childCount;
            for (int i = 0; i < _weaponSocket.childCount; i++)
            {
                Destroy(_weaponSocket.GetChild(0).gameObject);
            }

            GameObject newWeapon = Instantiate(_weaponData.WeaponMesh, _weaponSocket);
            currentComboCount = 0;
            OnWeaponEquip?.Invoke(_weaponData.PlayerRuntimeAnimatorController);
        }

        public void ResetCombo()
        {
            currentComboCount = 0;
        }
        
        public void TriggerCombo()
        {
            currentComboCount = (currentComboCount + 1) % _weaponData.comboCount;

            if (comboTimerCO != null)
            {
                StopCoroutine(comboTimerCO);
                comboTimerCO = null;
            }

            if (currentComboCount != 0)
                comboTimerCO = StartCoroutine(StartComboTimerCO());
        }

        //TODO: maybe use async instead
        private IEnumerator StartComboTimerCO()
        {
            yield return WaitHandler.GetWaitForSeconds(_weaponData.comboResetTime);

            comboTimerCO = null;
            ResetCombo();
        }
    }
}