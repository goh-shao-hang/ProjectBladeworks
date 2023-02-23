using UnityEngine;
using GameCells.Player.Input;
using GameCells.Player.Weapons;

namespace GameCells.Player
{
    public class Player : MonoBehaviour
    {
        //Components
        [SerializeField] private SO_Entity playerData;

        private FiniteStateMachine _stateMachine = new FiniteStateMachine();
        private PlayerStateFactory _playerStateFactory;
        private IInputHandler _inputHandler;
        private PlayerWeaponAnimationEventTrigger _playerAnimationEventTrigger;
        private CombatManager _combatManager;
        private CharacterController _characterController;
        private Animator _animator;
        private GroundChecker _playerGroundChecker;

        //Component Getters
        public SO_Entity PlayerData => playerData;
        public PlayerStateFactory PlayerStateFactory => _playerStateFactory ??= new PlayerStateFactory(this._stateMachine, this);
        public IInputHandler InputHandler => _inputHandler ??= GetInputHandler();
        public PlayerWeaponAnimationEventTrigger PlayerAnimationEventTrigger => _playerAnimationEventTrigger ??= GetComponentInChildren<PlayerWeaponAnimationEventTrigger>();
        public CombatManager CombatManager => _combatManager ??= GetComponentInChildren<CombatManager>();
        public CharacterController CharacterController => _characterController ??= GetComponent<CharacterController>();
        public Animator Animator => _animator ??= GetComponentInChildren<Animator>();
        public GroundChecker PlayerGroundChecker => _playerGroundChecker ??= GetComponent<GroundChecker>();

        //Variables
        [SerializeField] private Transform _currentEnemyTransform;
        [SerializeField] private bool _gravityEnabled;

        private Vector3 _lookPosition;
        private Vector3 _appliedMovement;

        private void OnEnable()
        {
            CombatManager.OnWeaponEquip += SetRuntimeAnimatorController;
        }

        private void OnDisable()
        {
            CombatManager.OnWeaponEquip -= SetRuntimeAnimatorController;
        }

        private void Awake()
        {
            _stateMachine.Initialize(PlayerStateFactory.Movement);
        }

        private void Update()
        {
            //Debug.Log(_stateMachine.CurrentState);
            _stateMachine.CurrentState.Execute();
            ApplyMovement();
        }

        private void LateUpdate()
        {
            LookAtCurrentEnemy();
        }

        private IInputHandler GetInputHandler()
        {
            if (TryGetComponent(out _inputHandler))
            {
                return _inputHandler;
            }
            else
            {
                Debug.LogError("There is no input handler on this player!");
                return null;
            }
        }

        public void SetRuntimeAnimatorController(RuntimeAnimatorController runtimeAnimatorController)
        {
            Animator.runtimeAnimatorController = runtimeAnimatorController;
        }

        private void ApplyMovement()
        {
            if (_gravityEnabled)
            {
                if (!PlayerGroundChecker.CollisionDetected)
                {
                    _appliedMovement.y += GameData.Gravity * Time.deltaTime;
                }
            }

            CharacterController.Move(transform.TransformDirection(_appliedMovement));
        }

        public void SetMovementX(float movement)
        {
            _appliedMovement.x = movement;   
        }

        public void SetMovementY(float movement)
        {
            _appliedMovement.y = movement;
        }

        public void SetMovementZ(float movement)
        {
            _appliedMovement.z = movement;
        }

        private void LookAtCurrentEnemy()
        {
            _lookPosition = _currentEnemyTransform.position - transform.position;
            _lookPosition.y = 0;
            transform.rotation = Quaternion.LookRotation(_lookPosition);
        }
    }
}