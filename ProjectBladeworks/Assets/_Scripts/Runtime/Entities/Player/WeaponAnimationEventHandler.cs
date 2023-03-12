using System;
using UnityEngine;
using GameCells.Core;

namespace GameCells.Entities.Player
{
    public class WeaponAnimationEventHandler : MonoBehaviour
    {
        //Animation events, considered points of interest to insert special events such as activating hitbox
        public event Action OnAttackFinished; //When the attack animation ends
        public event Action OnAllowNextCombo; //When next combo is allowed
        public event Action OnWeaponBeginAction; //When the weapon begins its action
        public event Action OnWeaponEndAction; //When the weapon ends its action

        private void AttackFinished() => OnAttackFinished?.Invoke();

        private void AllowNextCombo() => OnAllowNextCombo?.Invoke();

        private void ActivateHitbox() => OnWeaponBeginAction?.Invoke();

        private void DeactivateHitbox() => OnWeaponEndAction?.Invoke();
    }
}