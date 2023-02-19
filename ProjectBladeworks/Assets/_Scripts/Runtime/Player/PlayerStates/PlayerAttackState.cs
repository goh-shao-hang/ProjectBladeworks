using UnityEngine;

namespace GameCells.Player
{
    public class PlayerAttackState : PlayerBaseState
    {
        private bool attackFinished;
        private bool nextComboAllowed;

        public PlayerAttackState(FiniteStateMachine context, Player player) : base(context, player)
        {
        }

        public override void Enter()
        {
            base.Enter();

            attackFinished = false;
            nextComboAllowed = false;
            _player.PlayerAnimationEventTrigger.OnAttackFinished += AttackFinished;
            _player.PlayerAnimationEventTrigger.OnAllowNextAttack += AllowNextAttack;
            TriggerCombo();
        }

        public override void Exit()
        {
            base.Exit();

            _player.PlayerAnimationEventTrigger.OnAttackFinished -= AttackFinished;
        }

        private void AttackFinished()
        {
            attackFinished = true;
            _player.Animator.SetBool(GameData.isAttackingHash, false);
            Debug.Log("Finished");
        }

        private void AllowNextAttack()
        {
            nextComboAllowed = true;
        }

        private void TriggerCombo()
        {
            _player.WeaponManager.TriggerCombo();
            
            _player.Animator.speed = _player.WeaponManager.WeaponData.baseAttackSpeedPercentage;
            _player.Animator.SetBool(GameData.isAttackingHash, true);
            _player.Animator.SetInteger(GameData.currentComboHash, _player.WeaponManager.CurrentComboCount);
            _player.Animator.SetBool(GameData.isMovingHash, false);
        }

        public override void CheckSwitchState()
        {
            base.CheckSwitchState();

            if (attackFinished == false && nextComboAllowed && _player.InputHandler.AttackInput)
            {
                TriggerCombo();
            }
            else if (attackFinished == true)
            {
                _ctx.ChangeState(_player.PlayerStateFactory.Idle);
            }
        }
    }
}