using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCells.Entities.Player.Weapons
{
    public class WeaponEffects : WeaponComponent
    {
        [SerializeField] private ParticleSystem _effect;
        [SerializeField] private bool _stopUponDeactivation;
        [SerializeField] private float _duration;

        private Coroutine _effectStopCO;

        public override void Activate()
        {
            _effect.Play();

            if (!_stopUponDeactivation)
            {
                if (_effectStopCO != null)
                {
                    StopCoroutine(_effectStopCO);
                    _effectStopCO = null;
                }

                _effectStopCO = StartCoroutine(StopEffectAfterDuration());
            }
        }

        public override void Deactivate()
        {
            if (_stopUponDeactivation)
            {
                _effect.Stop();
            }
        }

        private IEnumerator StopEffectAfterDuration()
        {
            yield return WaitHandler.GetWaitForSeconds(_duration);
            _effect.Stop();
        }
    }
}