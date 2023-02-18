using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameCells.Player.Input;

namespace GameCells.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        private CharacterController characterController;
        private CharacterController CharacterController => characterController ??= GetComponent<CharacterController>();
        private IInputHandler inputHandler;
        private IInputHandler InputHandler => inputHandler ??= GetComponent<IInputHandler>();
        private Animator animator;
        private Animator Animator => animator ??= GetComponentInChildren<Animator>();

        [Header("Gameplay")]
        [SerializeField] private float moveSpeed = 5f;

        [Header("Animations")]
        [SerializeField] private float moveAnimationDampTime = 0.1f;

        private Vector3 appliedMovement;

        #region Animator Hash

        private static readonly int xMovementHash = Animator.StringToHash("xMovement");
        private static readonly int yMovementHash = Animator.StringToHash("yMovement");
        private static readonly int attackHash = Animator.StringToHash("attack");

        #endregion Animator Hash

        private void Update()
        {
            appliedMovement.x = InputHandler.Movement.x;
            appliedMovement.z = InputHandler.Movement.y;

            CharacterController.Move(moveSpeed * appliedMovement * Time.deltaTime);
            UpdateAnimations();

            if (inputHandler.Tap)
            {
                animator.SetTrigger(attackHash);
            }
        }

        private void UpdateAnimations()
        {
            Animator.SetFloat(xMovementHash, appliedMovement.x, moveAnimationDampTime, Time.deltaTime);
            Animator.SetFloat(yMovementHash, appliedMovement.z, moveAnimationDampTime, Time.deltaTime);
        }
    }
}