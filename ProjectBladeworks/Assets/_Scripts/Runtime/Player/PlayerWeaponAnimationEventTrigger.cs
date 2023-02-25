using System;
using UnityEngine;
using GameCells.Core;

namespace GameCells.Player
{
    public class PlayerWeaponAnimationEventTrigger : MonoBehaviour
    {
        private void AttackFinished()
        {
            InGameEventsManager.GetInstance().OnComboFinished?.Invoke();
        }

        private void AllowNextCombo()
        {
            InGameEventsManager.GetInstance().OnAllowNextCombo?.Invoke();
        }

        private void ActivateHitbox()
        {

        }

        private void DeactivateHitbox()
        {

        }

        private void ComboStep()
        {

        }
    }
}