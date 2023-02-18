using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameCells.Player.Input;

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


        //Components
        private CharacterController characterController;
        public CharacterController CharacterController => characterController ??= GetComponent<CharacterController>();
        private Animator animator;
        public Animator Animator => animator ??= GetComponentInChildren<Animator>();

        private void Awake()
        {
            _stateMachine.Initialize(PlayerStateFactory.Idle);
        }

        private void Update()
        {
            Debug.Log(_stateMachine.CurrentState);
            _stateMachine.CurrentState.Execute();
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
    }
}