using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCells.Player
{
    public class PlayerIdleState : PlayerBaseState
    {
        public PlayerIdleState(FiniteStateMachine context, Player player) : base(context, player)
        {
        }

        public override void Execute()
        {
            base.Execute();

            /*if (_player.Animator.GetFloat(GameData.xMovementHash) >= 0.01f || (_player.Animator.GetFloat(GameData.xMovementHash) >= 0.01f))
            {
                UpdateMoveAnimations(Vector3.zero, GameData.moveAnimationDampTime);
            }*/
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
            else if (_player.InputHandler.MovementInput != Vector2.zero)
            {
                _ctx.ChangeState(_player.PlayerStateFactory.Move);
            }
        }
    }
}