using System;
using UnityEngine;
using GameCells.Core;

namespace GameCells.Player
{
    public class PlayerWeaponAnimationEventTrigger : MonoBehaviour
    {
        //Animation events
        public event Action OnComboFinished;
        public event Action OnAllowNextCombo;
        public event Action OnPlayerHitboxActivate;
        public event Action OnPlayerHitboxDeactivate;

        //TODO: resolve attack movement
        //public event Action OnPlayerAttackMovementStart;
        //public event Action OnPlayerAttackMovementEnd;

        private void AttackFinished()
        {
            OnComboFinished?.Invoke();
        }

        private void AllowNextCombo()
        {
            OnAllowNextCombo?.Invoke();
        }

        private void ActivateHitbox()
        {
            OnPlayerHitboxActivate?.Invoke();
        }

        private void DeactivateHitbox()
        {
            OnPlayerHitboxDeactivate?.Invoke();
        }

        /*private void StartAttackMovement()
        {
            OnPlayerAttackMovementStart?.Invoke();
        }

        private void StopAttackMovement()
        {
            OnPlayerAttackMovementEnd?.Invoke();
        }*/
    }
}