using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCells.Entities.Behaviour
{
    [RequireComponent(typeof(CinemachineImpulseSource))]
    public class EB_CameraShakeSource : EntityBehaviour
    {
        private CinemachineImpulseSource _cinemachineImpulseSource;
        private CinemachineImpulseSource cinemachineImpulseSource => _cinemachineImpulseSource ??= GetComponent<CinemachineImpulseSource>();

        public void CameraShake()
        {
            cinemachineImpulseSource.GenerateImpulse();
        }
    }
}