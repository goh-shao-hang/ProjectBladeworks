using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCells.Player
{
    public class PlayerMoveState : PlayerBaseState
    {
        private Vector3 movement;

        public PlayerMoveState(FiniteStateMachine context, Player player) : base(context, player)
        {
        }

        public override void Enter()
        {
            base.Enter();

            _player.Animator.SetBool(GameData.isMovingHash, true);
        }

        public override void Execute()
        {
            base.Execute();

            movement.x = _player.InputHandler.MovementInput.x;
            movement.z = _player.InputHandler.MovementInput.y;

            _player.SetMovement(_player.PlayerData.BaseSpeed * movement * Time.deltaTime);
            UpdateMoveAnimations(movement, GameData.moveAnimationDampTime);
        }

        public override void Exit()
        {
            base.Exit();

            _player.Animator.SetBool(GameData.isMovingHash, false);
        }

        private void UpdateMoveAnimations(Vector3 movement, float damping)
        {
            _player.Animator.SetFloat(GameData.xMovementHash, movement.x, damping, Time.deltaTime);
            _player.Animator.SetFloat(GameData.yMovementHash, movement.z, damping, Time.deltaTime);
        }


        public override void CheckSwitchState()
        {
            base.CheckSwitchState();

            if (_player.InputHandler.AttackInput)
            {
                _ctx.ChangeState(_player.PlayerStateFactory.Attack);
            }
            else if (_player.InputHandler.MovementInput == Vector2.zero)
            {
                _ctx.ChangeState(_player.PlayerStateFactory.Idle);
            }
        }
    }
}