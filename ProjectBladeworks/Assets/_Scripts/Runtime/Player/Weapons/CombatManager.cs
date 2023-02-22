using System;
using System.Collections;
using UnityEngine;
using GameCells.Core;

namespace GameCells.Player.Weapons
{
    public class CombatManager : MonoBehaviour
    {
        [SerializeField] private SO_Weapon _weaponData;
        [SerializeField] private Transform _weaponSocket;

        public event Action<RuntimeAnimatorController> OnWeaponEquip;

        private Coroutine comboTimerCO;
        private int currentComboCount;
        private bool isComboFinished = false;
        private bool isNextComboAllowed = false;

        public SO_Weapon WeaponData => _weaponData;
        public int CurrentComboCount => currentComboCount;
        public bool IsComboFinished => isComboFinished;
        public bool IsNextComboAllowed => isNextComboAllowed;

        private void Start()
        {
            InitializeWeapon();
        }

        private void OnEnable()
        {
            InGameEventsManager.GetInstance().OnComboFinished.AddSubscriber(ComboFinished);
            InGameEventsManager.GetInstance().OnAllowNextCombo.AddSubscriber(AllowNextCombo);
        }

        private void OnDisable()
        {
            
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
            isNextComboAllowed = true;
            currentComboCount = 0;
        }
        
        public void TriggerCombo()
        {
            isComboFinished = false;
            isNextComboAllowed = false;
            currentComboCount = (currentComboCount + 1) % _weaponData.comboCount;

            if (comboTimerCO != null)
            {
                StopCoroutine(comboTimerCO);
                comboTimerCO = null;
            }

            if (currentComboCount != 0)
                comboTimerCO = StartCoroutine(StartComboTimerCO());
        }

        public void AllowNextCombo()
        {
            isNextComboAllowed = true;
        }

        private void ComboFinished()
        {
            isComboFinished = true;
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