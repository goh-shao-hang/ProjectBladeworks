using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCells.Entities.Player.Weapons
{
    public class WeaponHitbox : MonoBehaviour
    {
        public event Action OnWeaponHit;

        [SerializeField] private float _weaponLength;

        private LayerMask damageableLayer;
        private RaycastHit _raycastHit;
        private List<GameObject> _objectsHitInCurrentAttack = new List<GameObject>();

        private bool _activated = false;

        private void Update()
        {
            if (_activated)
            {
                if (Physics.Raycast(transform.position, -transform.up, out _raycastHit, _weaponLength, damageableLayer))
                {
                    GameObject hitTarget = _raycastHit.transform.gameObject;

                    if (!_objectsHitInCurrentAttack.Contains(hitTarget))
                    {
                        print($"{_raycastHit.transform.gameObject.name} damaged!");
                        _objectsHitInCurrentAttack.Add(hitTarget);
                        OnWeaponHit.Invoke();
                    }
                }
            }
        }

        public void Activate(LayerMask damageableLayer)
        {
            this.damageableLayer = damageableLayer;
            _objectsHitInCurrentAttack.Clear();

            _activated = true;
        }

        public void Deactivate()
        {
            _activated = false;
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.position - transform.up * _weaponLength);

            if (_raycastHit.point != null)
            {
                Gizmos.DrawSphere(_raycastHit.point, .1f);
            }
        }
#endif
    }
}