using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCells.Entities.Behaviour
{
    public class EB_CharacterMovement : EntityBehaviour
    {
        [SerializeField] private bool _gravityEnabled;

        private CharacterController _characterController;
        public CharacterController CharacterController => _characterController ??= GetComponentInParent<CharacterController>();

        private bool _overrideYMovement = false;
        private Vector3 _appliedMovement;
        public Vector3 AppliedMovement => _appliedMovement;

        private void Update()
        {
            ApplyMovement();
        }

        public void OverrideYMovement(bool overrideYMovement)
        {
            _overrideYMovement = overrideYMovement;
        }

        private void ApplyMovement()
        {
            if (_gravityEnabled)//&& !_overrideYMovement)
            {
                if (!CharacterController.isGrounded)
                {
                    _appliedMovement.y += GameData.Gravity * Time.deltaTime;
                }
                else if (!_overrideYMovement)
                {
                    _appliedMovement.y = -0.2f;
                }
            }

            CharacterController.Move(transform.TransformDirection(_appliedMovement * Time.deltaTime));
        }

        public void SetMovement(Vector3 movement)
        {
            _appliedMovement = movement;
        }

        public void SetMovementX(float xMovement)
        {
            _appliedMovement.x = xMovement;
        }

        public void SetMovementY(float yMovement)
        {
            _appliedMovement.y = yMovement;
        }

        public void SetMovementZ(float zMovement)
        {
            _appliedMovement.z = zMovement;
        }
    }
}