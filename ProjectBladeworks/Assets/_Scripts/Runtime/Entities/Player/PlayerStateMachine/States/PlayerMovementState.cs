using GameCells.Entities.Behaviour;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCells.Entities.Player
{
    public class PlayerMovementState : PlayerBaseState
    {
        private EB_CharacterMovement _characterMovement;
        private EB_CharacterMovement characterMovement => _characterMovement ??= _player.EntityBehaviourManager.GetEntityBehaviour<EB_CharacterMovement>();

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

            characterMovement.SetMovementX(_player.EntityData.BaseSpeed * movement.x);
            characterMovement.SetMovementZ(_player.EntityData.BaseSpeed * movement.z);
            _player.Animator.SetFloat(GameData.MoveSpeedHash, _player.EntityData.BaseSpeed);
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
        }
    }
}