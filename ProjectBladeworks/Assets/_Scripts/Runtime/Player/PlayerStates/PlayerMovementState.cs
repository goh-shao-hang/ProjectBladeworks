using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCells.Player
{
    public class PlayerMovementState : PlayerBaseState
    {
        private Vector3 movement;

        public PlayerMovementState(FiniteStateMachine context, Player player) : base(context, player)
        {
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void Execute()
        {
            base.Execute();

            movement.x = _player.InputHandler.MovementInput.x;
            movement.z = _player.InputHandler.MovementInput.y;

            _player.SetMovementX(_player.PlayerData.BaseSpeed * movement.x * Time.deltaTime);
            _player.SetMovementZ(_player.PlayerData.BaseSpeed * movement.z * Time.deltaTime);
            UpdateMoveAnimations(movement, GameData.MoveAnimationDampTime);
        }

        public override void Exit()
        {
            base.Exit();
        }

        private void UpdateMoveAnimations(Vector3 movement, float damping)
        {
            _player.Animator.SetFloat(GameData.XMovementHash, movement.x, damping, Time.deltaTime);
            _player.Animator.SetFloat(GameData.YMovementHash, movement.z, damping, Time.deltaTime);
        }


        public override void CheckSwitchState()
        {
            base.CheckSwitchState();

            if (_player.InputHandler.AttackInput)
            {
                _ctx.ChangeState(_player.PlayerStateFactory.Attack);
            }
            /*else if (_player.InputHandler.MovementInput == Vector2.zero)
            {
                _ctx.ChangeState(_player.PlayerStateFactory.Idle);
            }*/
        }
    }
}