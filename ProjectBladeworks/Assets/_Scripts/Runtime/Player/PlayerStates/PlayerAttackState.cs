using UnityEngine;

namespace GameCells.Player
{
    public class PlayerAttackState : PlayerBaseState
    {
        public PlayerAttackState(FiniteStateMachine context, Player player) : base(context, player)
        {
        }

        public override void Enter()
        {
            base.Enter();

            TriggerCombo();
        }

        private void AttackFinished()
        {
            _player.Animator.SetBool(GameData.isAttackingHash, false);
            Debug.Log("Finished");
        }

        /*private void AllowNextAttack()
        {
            
            _player.Animator.SetBool(GameData.isAttackingHash, false);
        }*/

        private void TriggerCombo()
        {
            _player.CombatManager.TriggerCombo();
            
            _player.Animator.speed = _player.CombatManager.WeaponData.baseAttackSpeedPercentage;
            _player.Animator.SetBool(GameData.isAttackingHash, true);
            _player.Animator.SetTrigger(GameData.triggerComboHash);
            _player.Animator.SetInteger(GameData.currentComboHash, _player.CombatManager.CurrentComboCount);
            //_player.Animator.SetBool(GameData.isMovingHash, false);
        }

        public override void CheckSwitchState()
        {
            base.CheckSwitchState();

            if (_player.CombatManager.IsNextComboAllowed && _player.InputHandler.AttackInput)
            {
                TriggerCombo();
            }
            else if (_player.CombatManager.IsComboFinished == true)
            {
                AttackFinished();
                _player.Animator.SetInteger(GameData.currentComboHash, _player.CombatManager.CurrentComboCount);
                _ctx.ChangeState(_player.PlayerStateFactory.Idle);
            }
        }
    }
}