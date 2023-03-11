using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCells.Entities.Player.Weapons
{
    [RequireComponent(typeof(Weapon))]
    public abstract class WeaponComponents : MonoBehaviour
    {
        private Weapon _weapon;
        private Weapon Weapon => _weapon ??= GetComponentInParent<Weapon>();

        protected virtual void OnEnable()
        {
            _weapon.OnWeaponActivate += Activate;
        }

        protected virtual void OnDisable()
        {
            _weapon.OnWeaponDeactivate += Deactivate;
        }

        public abstract void Activate();

        public abstract void Deactivate();
    }
}