using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCells.Entities.Player.Weapons
{
    public class Weapon : MonoBehaviour
    {
        public event Action OnWeaponActivate;
        public event Action OnWeaponDeactivate;

        public void Activate()
        {
            OnWeaponActivate?.Invoke();
        }

        public void Deactivate()
        {
            OnWeaponActivate?.Invoke();
        }

        /*public event Action OnWeaponHit;

        [Header("Hitbox")]
        [SerializeField] private BoxCollider _weaponHitbox;
        [SerializeField] private LayerMask _damageableLayer;

        [Header("Trail Rendering")]
        [SerializeField] private bool _emitTrail;
        [SerializeField] private TrailRenderer _trailRenderer;

        private bool isActive = false;

        public BoxCollider WeaponHitbox => _weaponHitbox;
        public TrailRenderer TrailRenderer => _trailRenderer;

        //Hitbox
        private List<Collider> hitTargets = new List<Collider>();
        private Collider[] detectedTargets = new Collider[10];
        private Vector3 _hitboxSize;

        //Trail VFX
        private Coroutine _stopTrailCO = null;

        private void FixedUpdate()
        {
            if (isActive)
            {
                Vector3 hitboxCenter = transform.TransformPoint(_weaponHitbox.center);

                int detectedCount = Physics.OverlapBoxNonAlloc(hitboxCenter, _hitboxSize * 0.5f, detectedTargets, transform.rotation, _damageableLayer);
                for (int i = 0; i < detectedCount; i++)
                {
                    if (detectedTargets[i] == null) continue;
                    if (hitTargets.Contains(detectedTargets[i])) continue;
                    hitTargets.Add(detectedTargets[i]);
                    Debug.Log(detectedTargets[i] + "hit!");
                    OnWeaponHit?.Invoke();
                }
            }
        }

        public void Activate()
        {
            isActive = true;
            hitTargets.Clear();

            _hitboxSize.Set(
                _weaponHitbox.size.x * transform.lossyScale.x,
                _weaponHitbox.size.y * transform.lossyScale.y,
                _weaponHitbox.size.z * transform.lossyScale.z
                );

            StartEmittingTrail();
        }

        public void Deactivate()
        {
            isActive = false;
            hitTargets.Clear();

            StopEmittingTrail();            
        }

        private void StartEmittingTrail()
        {
            if (_emitTrail && _trailRenderer != null)
            {
                if (_stopTrailCO != null)
                {
                    StopCoroutine(_stopTrailCO);
                    _stopTrailCO = null;
                }
                _trailRenderer.enabled = true;
            }
        }

        private void StopEmittingTrail()
        {
            if (_emitTrail && _trailRenderer != null)
            {
                if (_stopTrailCO != null)
                {
                    StopCoroutine(_stopTrailCO);
                    _stopTrailCO = null;
                }

                _stopTrailCO = StartCoroutine(StopTrailCO());
            }
        }

        private IEnumerator StopTrailCO()
        {
            yield return WaitHandler.GetWaitForSeconds(_trailRenderer.time);
            _trailRenderer.enabled = false;
        }*/
    }
}