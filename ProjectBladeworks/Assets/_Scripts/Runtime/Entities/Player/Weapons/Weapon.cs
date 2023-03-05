using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public event Action OnWeaponHit;

    [field: SerializeField] public BoxCollider WeaponHitbox { get; private set; }
    [SerializeField] private LayerMask damageableLayer;

    private bool isActive = false;
    private List<Collider> hitTargets = new List<Collider>();
    private Collider[] detectedTargets = new Collider[10];
    private Vector3 _hitboxSize;

    private void FixedUpdate()
    {
        if (isActive)
        {
            Vector3 hitboxCenter = transform.TransformPoint(WeaponHitbox.center);

            int detectedCount = Physics.OverlapBoxNonAlloc(hitboxCenter, _hitboxSize * 0.5f, detectedTargets, transform.rotation, damageableLayer);
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
            WeaponHitbox.size.x * transform.lossyScale.x,
            WeaponHitbox.size.y * transform.lossyScale.y,
            WeaponHitbox.size.z * transform.lossyScale.z
            );
    }

    public void Deactivate()
    {
        isActive = false;
        hitTargets.Clear();
    }
}
