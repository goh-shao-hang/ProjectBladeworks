using System;
using System.Collections;
using UnityEngine;
using GameCells.Core;

namespace GameCells.Player.Weapons
{
    public class CombatManager : MonoBehaviour
    {
        [SerializeField] private SO_WeaponData _weaponData;
        [SerializeField] private Transform _weaponSocket;
        private RootMotionManager _rootMotionManager;
        private CharacterMovement _characterMovement;
        private PlayerWeaponAnimationEventTrigger _playerWeaponAnimationEventTrigger;

        private Coroutine _comboTimerCO;
        private int _currentComboCount;
        private bool _isComboFinished = false;
        private bool _isNextComboAllowed = false;

        public SO_WeaponData WeaponData => _weaponData;
        public RootMotionManager RootMotionManager => _rootMotionManager ??= GetComponentInChildren<RootMotionManager>();
        public CharacterMovement CharacterMovement => GetComponent<CharacterMovement>();
        public PlayerWeaponAnimationEventTrigger PlayerWeaponAnimationEventTrigger => _playerWeaponAnimationEventTrigger ??= GetComponentInChildren<PlayerWeaponAnimationEventTrigger>();
        public int CurrentComboCount => _currentComboCount;
        public bool IsComboFinished => _isComboFinished;
        public bool IsNextComboAllowed => _isNextComboAllowed;

        public event Action<RuntimeAnimatorController> OnWeaponEquip;

        private void Start()
        {
            InitializeWeapon();
        }

        private void Update()
        {
            if (UnityEngine.Input.GetKeyDown(KeyCode.F1))
            {
                InitializeWeapon();
            }
        }

        private void OnEnable()
        {
            PlayerWeaponAnimationEventTrigger.OnComboFinished += ComboFinished;
            PlayerWeaponAnimationEventTrigger.OnAllowNextCombo += AllowNextCombo;
        }

        private void OnDisable()
        {
            PlayerWeaponAnimationEventTrigger.OnComboFinished -= ComboFinished;
            PlayerWeaponAnimationEventTrigger.OnAllowNextCombo -= AllowNextCombo;
        }

        private void AttackMovement()
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
            _currentComboCount = 0;
            OnWeaponEquip?.Invoke(_weaponData.PlayerRuntimeAnimatorController);
        }

        public void ResetCombo()
        {
            _currentComboCount = 0;   
        }
        
        public void TriggerCombo()
        {
            _isComboFinished = false;
            _isNextComboAllowed = false;

            if (_comboTimerCO != null)
            {
                StopCoroutine(_comboTimerCO);
                _comboTimerCO = null;
            }

            if (_currentComboCount != 0)
                _comboTimerCO = StartCoroutine(StartComboTimerCO());
        }

        public void AllowNextCombo()
        {
            _currentComboCount = (_currentComboCount + 1) % _weaponData.comboCount;
            _isNextComboAllowed = true;
        }

        private void ComboFinished()
        {
            Debug.Log("Finished");
            _isComboFinished = true;
            _isNextComboAllowed = true;
        }


        //TODO: maybe use async instead
        private IEnumerator StartComboTimerCO()
        {
            yield return WaitHandler.GetWaitForSeconds(_weaponData.comboResetTime);

            _comboTimerCO = null;
            ResetCombo();
        }
    }
}