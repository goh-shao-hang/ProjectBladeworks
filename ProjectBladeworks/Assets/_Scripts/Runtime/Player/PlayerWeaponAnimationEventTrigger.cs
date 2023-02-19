using System;
using UnityEngine;

namespace GameCells.Player
{
    public class PlayerWeaponAnimationEventTrigger : MonoBehaviour
    {
        public event Action OnAttackFinished;
        public event Action OnAllowNextAttack;

        private void AttackFinished()
        {
            OnAttackFinished?.Invoke();
        }

        private void AllowNextCombo()
        {
            OnAllowNextAttack?.Invoke();
        }
    }
}