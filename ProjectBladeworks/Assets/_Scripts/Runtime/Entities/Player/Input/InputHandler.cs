using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GameCells.Entities.Player.Input
{
    public class InputHandler : Singleton<InputHandler>
    {
        [Header("Settings")]
        [SerializeField] private float minSwipeDistance = 7;

        //References
        private PlayerControls playerControls;

        //Fields and properties
        private Vector2 touchPosition;
        private Vector2 dragStartPosition;
        private bool tap;
        private bool touchDetected;
        private Vector2 swipeDirection;

        public Vector2 TouchPosition => touchPosition;
        public Vector2 DragStartPosition => dragStartPosition;
        public bool Tap => tap;
        public bool TouchDetected => touchDetected;

        private void Awake()
        { 
            SetDontDestroyOnLoad();
            SetupControls();
        }

        private void LateUpdate()
        {
            tap = false;
            swipeDirection = Vector2.zero;
        }

        private void OnEnable()
        {
            playerControls.Enable();
        }

        private void OnDisable()
        {
            playerControls.Disable();
        }

        private void SetupControls()
        {
            playerControls = new PlayerControls();

            //Register different actions as events
            /*playerControls.Gameplay.Tap.performed += ctx => OnTap(ctx);
            playerControls.Gameplay.TouchPosition.performed += ctx => OnTouchMoved(ctx);
            playerControls.Gameplay.StartDrag.performed += ctx => OnStartDrag(ctx);
            playerControls.Gameplay.EndDrag.performed += ctx => OnEndDrag(ctx);*/
        }

        #region Input Events

        private void OnTap(InputAction.CallbackContext ctx)
        {
            tap = true;
        }

        private void OnTouchBegin(InputAction.CallbackContext ctx)
        {
            touchPosition = ctx.ReadValue<Vector2>();
            touchDetected = true;
        }

        private void OnTouchMoved(InputAction.CallbackContext ctx)
        {
            touchPosition = ctx.ReadValue<Vector2>();
        }

        private void OnTouchEnded(InputAction.CallbackContext ctx)
        {
            touchPosition = Vector2.zero;
            touchDetected = false;
        }

        private void OnStartDrag(InputAction.CallbackContext ctx)
        {
            dragStartPosition = touchPosition;
        }

        private void OnEndDrag(InputAction.CallbackContext ctx)
        {
            Vector2 touchDelta = touchPosition - dragStartPosition;
            float sqrDistance = touchDelta.sqrMagnitude;

            if (sqrDistance > (minSwipeDistance * minSwipeDistance))
            {
                swipeDirection = touchDelta;
            }

            dragStartPosition = Vector2.zero;
        }

        #endregion Input Events

    }
}