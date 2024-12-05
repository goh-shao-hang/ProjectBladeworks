using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCells.Entities.Player.Weapons
{
    public abstract class WeaponComponent : MonoBehaviour
    {
        private Weapon _weapon;
        private WeaponAnimationEventHandler _weaponAnimationEventHandler;

        protected Weapon weapon => _weapon ??= GetComponentInParent<Weapon>();
        protected WeaponAnimationEventHandler weaponAnimationEventHandler => _weaponAnimationEventHandler ??= GetComponentInParent<WeaponAnimationEventHandler>();

        protected virtual void OnEnable()
        {
            weapon.OnWeaponActivate += Activate;
        }

        protected virtual void OnDisable()
        {
            weapon.OnWeaponDeactivate += Deactivate;
        }

        public virtual void Activate() { }

        public virtual void Deactivate() { }
    }

    public abstract class WeaponComponents<T> : WeaponComponent
    {
        protected override void OnEnable()
        {
            weapon.OnWeaponActivate += Activate;
        }

        protected override void OnDisable()
        {
            weapon.OnWeaponDeactivate += Deactivate;
        }

        public virtual void Activate(T param) { }

        public virtual void Deactivate(T param) { }
    }
}