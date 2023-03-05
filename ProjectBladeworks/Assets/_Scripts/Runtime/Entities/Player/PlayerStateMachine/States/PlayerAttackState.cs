using UnityEngine;

namespace GameCells.Entities.Player
{
    public class PlayerAttackState : PlayerBaseState
    {
        public PlayerAttackState(FiniteStateMachine context, Player player) : base(context, player)
        {
        }

        public override void Enter()
        {
            base.Enter();

            _player.CombatManager.PlayerRootMotionManager.AllowAnimationMovement(true);
            TriggerNextCombo();
        }

        public override void Execute()
        {
            base.Execute();
        }

        public override void Exit()
        {
            base.Exit();

            _player.CombatManager.PlayerRootMotionManager.AllowAnimationMovement(false);
        }

        private void TriggerNextCombo()
        {
            _player.Animator.SetTrigger(GameData.TriggerComboHash);
            _player.Animator.SetInteger(GameData.CurrentComboHash, _player.CombatManager.CurrentComboCount);
            _player.Animator.SetFloat(GameData.AttackSpeedHash, _player.CombatManager.WeaponData.comboData[_player.CombatManager.CurrentComboCount].baseAttackSpeedPercentage);

            _player.CombatManager.TriggerCombo();
        }

        public override void CheckSwitchState()
        {
            base.CheckSwitchState();

            if (_player.CombatManager.IsNextComboAllowed && _player.InputHandler.AttackInput) //Consider this as switching to itself
            {
                TriggerNextCombo();
            }
            else if (_player.CombatManager.IsComboFinished)
            {
                _player.Animator.SetFloat(GameData.AttackSpeedHash, 1f);
                _ctx.ChangeState(_player.PlayerStateFactory.Movement);
            }
        }
    }
}