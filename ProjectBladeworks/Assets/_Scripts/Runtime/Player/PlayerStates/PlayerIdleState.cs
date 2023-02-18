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

        public override void CheckSwitchState()
        {
            base.CheckSwitchState();

            if (_player.InputHandler.AttackInput)
            {
                _player.Animator.SetTrigger(GameData.attackHash);
            }
            else if (_player.InputHandler.MovementInput != Vector2.zero)
            {
                _ctx.ChangeState(_player.PlayerStateFactory.Move);
            }
        }
    }
}