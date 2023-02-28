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

            _player.CombatManager.RootMotionManager.AllowAnimationMovement(true);
            TriggerNextCombo();
        }

        public override void Execute()
        {
            base.Execute();
        }

        public override void Exit()
        {
            base.Exit();

            _player.CombatManager.RootMotionManager.AllowAnimationMovement(false);
        }

        private void TriggerNextCombo()
        {
            _player.Animator.SetTrigger(GameData.TriggerComboHash);
            _player.Animator.SetInteger(GameData.CurrentComboHash, _player.CombatManager.CurrentComboCount);
            _player.Animator.speed = _player.CombatManager.WeaponData.comboData[_player.CombatManager.CurrentComboCount].baseAttackSpeedPercentage;

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
                _ctx.ChangeState(_player.PlayerStateFactory.Movement);
            }
        }
    }
}