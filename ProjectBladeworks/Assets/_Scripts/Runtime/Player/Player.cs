using UnityEngine;
using GameCells.Player.Input;
using GameCells.Player.Weapons;

namespace GameCells.Player
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private SO_Entity playerData;

        public SO_Entity PlayerData => playerData;

        private FiniteStateMachine _stateMachine = new FiniteStateMachine();

        private PlayerStateFactory _playerStateFactory;
        public PlayerStateFactory PlayerStateFactory => _playerStateFactory ??= new PlayerStateFactory(this._stateMachine, this);

        private IInputHandler _inputHandler;
        public IInputHandler InputHandler => _inputHandler ??= GetInputHandler();

        private PlayerWeaponAnimationEventTrigger playerAnimationEventTrigger;
        public PlayerWeaponAnimationEventTrigger PlayerAnimationEventTrigger => playerAnimationEventTrigger ??= GetComponentInChildren<PlayerWeaponAnimationEventTrigger>();

        private CombatManager combatManager;
        public CombatManager CombatManager => combatManager ??= GetComponentInChildren<CombatManager>();

        [SerializeField] private Transform currentEnemyTransform;

        //Components
        private CharacterController characterController;
        public CharacterController CharacterController => characterController ??= GetComponent<CharacterController>();
        private Animator animator;
        public Animator Animator => animator ??= GetComponentInChildren<Animator>();

        private Vector3 lookPosition;

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
            _stateMachine.Initialize(PlayerStateFactory.Idle);
        }

        private void Update()
        {
            //Debug.Log(_stateMachine.CurrentState);
            _stateMachine.CurrentState.Execute();
        }

        private void LateUpdate()
        {
            lookPosition = currentEnemyTransform.position - transform.position;
            lookPosition.y = 0;
            transform.rotation = Quaternion.LookRotation(lookPosition);
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

        public void SetMovement(Vector3 movement)
        {
            CharacterController.Move(transform.TransformDirection(movement));
        }
    }
}