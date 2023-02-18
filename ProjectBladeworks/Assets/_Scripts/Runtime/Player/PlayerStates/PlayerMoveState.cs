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
        }

        public override void Execute()
        {
            base.Execute();

            movement.x = _player.InputHandler.MovementInput.x;
            movement.z = _player.InputHandler.MovementInput.y;

            _player.CharacterController.Move(_player.PlayerData.BaseSpeed * movement * Time.deltaTime);
            UpdateMoveAnimations(movement);
        }

        public override void Exit()
        {
            base.Exit();

            UpdateMoveAnimations(Vector3.zero);
        }

        private void UpdateMoveAnimations(Vector3 movement)
        {
            _player.Animator.SetFloat(GameData.xMovementHash, movement.x, GameData.moveAnimationDampTime, Time.deltaTime);
            _player.Animator.SetFloat(GameData.yMovementHash, movement.z, GameData.moveAnimationDampTime, Time.deltaTime);
        }


        public override void CheckSwitchState()
        {
            base.CheckSwitchState();

            if (_player.InputHandler.AttackInput)
            {
                _player.Animator.SetTrigger(GameData.attackHash);
            }
            else if (movement == Vector3.zero)
            {
                _ctx.ChangeState(_player.PlayerStateFactory.Idle);
            }
        }
    }
}