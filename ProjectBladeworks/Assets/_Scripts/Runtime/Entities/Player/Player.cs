using UnityEngine;
using GameCells.Player.Input;
using GameCells.Player.Weapons;

namespace GameCells.Player
{
    public class Player : Entity
    {
        private FiniteStateMachine _stateMachine = new FiniteStateMachine();
        private PlayerStateFactory _playerStateFactory;
        private IInputHandler _inputHandler;
        private PlayerWeaponAnimationEventTrigger _playerAnimationEventTrigger;
        private CombatManager _combatManager;
        private CharacterController _characterController;
        private Animator _animator;

        //Component Getters
        public PlayerStateFactory PlayerStateFactory => _playerStateFactory ??= new PlayerStateFactory(this._stateMachine, this);
        public IInputHandler InputHandler => _inputHandler ??= GetInputHandler();
        public PlayerWeaponAnimationEventTrigger PlayerAnimationEventTrigger => _playerAnimationEventTrigger ??= GetComponentInChildren<PlayerWeaponAnimationEventTrigger>();
        public CombatManager CombatManager => _combatManager ??= GetComponentInChildren<CombatManager>();
        public CharacterController CharacterController => _characterController ??= GetComponent<CharacterController>();
        public Animator Animator => _animator ??= GetComponentInChildren<Animator>();

        //Variables
        [SerializeField] private Transform _currentEnemyTransform;

        private Vector3 _lookPosition;

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
            _stateMachine.CurrentState.Execute();
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

        

        private void LookAtCurrentEnemy()
        {
            _lookPosition = _currentEnemyTransform.position - transform.position;
            _lookPosition.y = 0;
            transform.rotation = Quaternion.LookRotation(_lookPosition);
        }
    }
}