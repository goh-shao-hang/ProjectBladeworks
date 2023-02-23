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

            TriggerNextCombo();
        }

        public override void Execute()
        {
            base.Execute();

            if (_player.CombatManager.IsNextComboAllowed && _player.InputHandler.AttackInput)
            {
                TriggerNextCombo();
            }
        }

        private void TriggerNextCombo()
        {
            _player.Animator.SetTrigger(GameData.TriggerComboHash);
            _player.Animator.SetInteger(GameData.CurrentComboHash, _player.CombatManager.CurrentComboCount);
            _player.Animator.speed = _player.CombatManager.WeaponData.baseAttackSpeedPercentage;

            _player.CombatManager.TriggerCombo();
            
            //_player.Animator.SetBool(GameData.isAttackingHash, true);
            //_player.Animator.SetBool(GameData.isMovingHash, false);
        }

        public override void CheckSwitchState()
        {
            base.CheckSwitchState();
            
            if (_player.CombatManager.IsComboFinished == true)
            {
                _ctx.ChangeState(_player.PlayerStateFactory.Movement);
            }
        }
    }
}