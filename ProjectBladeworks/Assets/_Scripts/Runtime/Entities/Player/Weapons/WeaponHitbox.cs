using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHitbox : MonoBehaviour
{
    [field: SerializeField] public BoxCollider Hitbox { get; private set; }
    [SerializeField] private float _boxcastThickness;
    [SerializeField] private LayerMask layerMask;

    public bool isActive = false;
    private List<Collider> hitTargets = new List<Collider>();
    private Collider[] detectedTargets = new Collider[10];
    private Vector3 _hitboxSize;
    //private Vector3 _halfExtents;

    private void FixedUpdate()
    {
        if (isActive)
        {
            Vector3 center = transform.TransformPoint(Hitbox.center);

            /*for (int i = 0; i < detectedTargets.Length; i++)
            {
                detectedTargets[i] = null;
            }*/


            //detectedTargets = Physics.OverlapBox(center, _hitboxSize * 0.5f, transform.rotation, layerMask);
            //Array.Clear(detectedTargets, 0, detectedTargets.Length);
            int newFound = Physics.OverlapBoxNonAlloc(center, _hitboxSize * 0.5f, detectedTargets, transform.rotation, layerMask);
            for (int i = 0; i < newFound; i++)
            {
                if (detectedTargets[i] == null) continue;
                if (hitTargets.Contains(detectedTargets[i])) continue;
                hitTargets.Add(detectedTargets[i]);
                Debug.Log(detectedTargets[i] + "hit!");
            }
            /*foreach (Collider target in detectedTargets)
            {
                //Debug.Log(target);
                if (target == null) continue;
                if (hitTargets.Contains(target)) continue;
                hitTargets.Add(target);
                Debug.Log(target + "hit!");
            }*/
        }
    }

    public void Activate()
    {
        isActive = true;
        hitTargets.Clear();
        _hitboxSize.Set(
            Hitbox.size.x * transform.lossyScale.x,
            Hitbox.size.y * transform.lossyScale.y,
            Hitbox.size.z * transform.lossyScale.z
            );
    }

    public void Deactivate()
    {
        isActive = false;
        hitTargets.Clear();
    }

    /*private void CheckHit()
    {
        _hitboxSize.Set(
            Hitbox.size.x * transform.lossyScale.x,
            Hitbox.size.y * transform.lossyScale.y,
            Hitbox.size.z * transform.lossyScale.z
            );

        //Perform a boxcast along the inner of the hitbox. This gives a hit normal.
        float distance = _hitboxSize.y - _boxcastThickness;
        Vector3 direction = transform.up;
        Vector3 center = transform.TransformPoint(Hitbox.center);
        Vector3 start = center - direction * (distance * 0.5f);
        _halfExtents.Set(_hitboxSize.x * 0.5f, _boxcastThickness * 0.5f, _hitboxSize.z * 0.5f);
        Quaternion orientation = transform.rotation;

        Hits = Physics.BoxCastAll(center, _halfExtents, direction, orientation, distance, layerMask);

        foreach (var hit in Hits)
        {
            if (hit.point != Vector3.zero)
            {
                Debug.DrawLine(hit.point, hit.point + hit.normal * 5, Color.red, 1000);
            }
        }
    }*/
}
