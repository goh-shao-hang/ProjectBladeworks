using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameCells.Player.Input;

namespace GameCells.Player
{
    public class Player : MonoBehaviour
    {
        private FiniteStateMachine _stateMachine = new FiniteStateMachine();
        private PlayerStateFactory _playerStateFactory;
        public PlayerStateFactory playerStateFactory => _playerStateFactory ??= new PlayerStateFactory(this._stateMachine, this);
        private IInputHandler _inputHandler;
        public IInputHandler inputHandler => _inputHandler ??= GetInputHandler();

        private void Awake()
        {
            _stateMachine.Initialize(playerStateFactory.Idle);
        }

        private void Update()
        {
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