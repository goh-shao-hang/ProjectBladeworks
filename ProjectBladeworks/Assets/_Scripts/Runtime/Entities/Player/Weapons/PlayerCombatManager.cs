using System;
using System.Collections;
using UnityEngine;
using GameCells.Entities.Behaviour;

namespace GameCells.Entities.Player.Weapons
{
    public class PlayerCombatManager : MonoBehaviour
    {
        //TODO: REMOVE
        [SerializeField] private GameObject testHitSparks;

        [SerializeField] private SO_WeaponData _weaponData;
        [SerializeField] private Transform _weaponSocket;
        //private Weapon _currentWeapon;
        private SO_EntityData _playerData;
        private WeaponHitbox _currentWeaponHitbox;
        private PlayerRootMotionManager _playerRootMotionManager;
        private PlayerWeaponAnimationEventTrigger _playerWeaponAnimationEventTrigger;
        private EB_CameraShakeSource _cameraShakeSource;

        private Coroutine _comboTimerCO;
        private int _currentComboCount;
        private bool _isComboFinished = false;
        private bool _isNextComboAllowed = false;

        public SO_WeaponData WeaponData => _weaponData;
        //public Player Player => _player ??= GetComponentInParent<Player>();
        public PlayerRootMotionManager PlayerRootMotionManager => _playerRootMotionManager ??= GetComponentInChildren<PlayerRootMotionManager>();
        public PlayerWeaponAnimationEventTrigger PlayerWeaponAnimationEventTrigger => _playerWeaponAnimationEventTrigger ??= GetComponentInChildren<PlayerWeaponAnimationEventTrigger>();
        public EB_CameraShakeSource cameraShakeSource => _cameraShakeSource ??= GetComponentInChildren<EB_CameraShakeSource>();
        public int CurrentComboCount => _currentComboCount;
        public bool IsComboFinished => _isComboFinished;
        public bool IsNextComboAllowed => _isNextComboAllowed;

        public event Action<RuntimeAnimatorController> OnWeaponEquip;

        #region CallBacks

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
            PlayerWeaponAnimationEventTrigger.OnPlayerHitboxActivate += ActivateWeaponHitbox;
            PlayerWeaponAnimationEventTrigger.OnPlayerHitboxDeactivate += DeactivateWeaponHitbox;

            if (_currentWeaponHitbox != null)
            {
                _currentWeaponHitbox.OnWeaponHit += HandleWeaponHit;
            }
        }

        private void OnDisable()
        {
            PlayerWeaponAnimationEventTrigger.OnComboFinished -= ComboFinished;
            PlayerWeaponAnimationEventTrigger.OnAllowNextCombo -= AllowNextCombo;
            PlayerWeaponAnimationEventTrigger.OnPlayerHitboxActivate -= ActivateWeaponHitbox;
            PlayerWeaponAnimationEventTrigger.OnPlayerHitboxDeactivate -= DeactivateWeaponHitbox;

            if (_currentWeaponHitbox != null)
            {
                _currentWeaponHitbox.OnWeaponHit -= HandleWeaponHit;
            }
        }

        #endregion

        public void Init(SO_EntityData playerData)
        {
            this._playerData = playerData;
        }

        [ContextMenu("Initialize Weapon")]
        private void InitializeWeapon()
        {
            if (_currentWeaponHitbox != null)
            {
                _currentWeaponHitbox.OnWeaponHit -= HandleWeaponHit;
            }

            int count = _weaponSocket.childCount;
            for (int i = 0; i < _weaponSocket.childCount; i++)
            {
                Destroy(_weaponSocket.GetChild(i).gameObject);
            }

            _currentWeaponHitbox = Instantiate(_weaponData.WeaponPrefab, _weaponSocket).GetComponentInChildren<WeaponHitbox>();
            OnWeaponEquip?.Invoke(_weaponData.PlayerRuntimeAnimatorController);
            _currentWeaponHitbox.OnWeaponHit += HandleWeaponHit;

            _currentComboCount = 0;
        }

        private void HandleWeaponHit()
        {
            //TODO: this should be handled by the victim, not the attacker. fix this soon
            cameraShakeSource?.CameraShake();

            //TODO: REMOVE
            var sparks = Instantiate(testHitSparks, _currentWeaponHitbox.transform);
            sparks.transform.SetParent(null);
            Destroy(sparks, 1f);
        }

        #region Combo

        public void TriggerCombo()
        {
            _isComboFinished = false;
            _isNextComboAllowed = false;

            _currentComboCount = (_currentComboCount + 1) % _weaponData.comboCount;
            Debug.Log(CurrentComboCount);

            if (_currentComboCount != 0)
            {
                if (_comboTimerCO != null)
                {
                    StopCoroutine(_comboTimerCO);
                    _comboTimerCO = null;
                }
                _comboTimerCO = StartCoroutine(StartComboTimerCO());
            }
        }

        public void AllowNextCombo()
        {
            _isNextComboAllowed = true;
        }

        private void ComboFinished()
        {
            _isComboFinished = true;
            _isNextComboAllowed = true;
        }

        public void ResetCombo()
        {
            _currentComboCount = 0;
        }

        private IEnumerator StartComboTimerCO()
        {
            yield return WaitHandler.GetWaitForSeconds(_weaponData.comboResetTime);

            _comboTimerCO = null;
            ResetCombo();
        }

        #endregion

        #region Hitbox

        public void ActivateWeaponHitbox()
        {
            _currentWeaponHitbox.Activate(_playerData.DamageableLayers);
        }

        public void DeactivateWeaponHitbox()
        {
            _currentWeaponHitbox.Deactivate();
        }

        #endregion
    }
}