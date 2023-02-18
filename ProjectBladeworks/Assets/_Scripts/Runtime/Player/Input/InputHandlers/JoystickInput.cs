using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using Etouch = UnityEngine.InputSystem.EnhancedTouch;

namespace GameCells.Player.Input
{
    public class JoystickInput : MonoBehaviour, IInputHandler
    {
        [SerializeField] private Vector2 joystickSize = new Vector2(300, 300);
        [SerializeField] private Joystick joystick;

        private Vector2 movement;
        public Vector2 Movement => movement;

        [Header("Settings")]
        [SerializeField] private float minTapToSwipeDistance = 7; //If distance exceeds this value when touch is released, consider this a swipe instead of a tap.

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

        #region Animator Hash

        private static readonly int xMovementHash = Animator.StringToHash("xMovement");
        private static readonly int yMovementHash = Animator.StringToHash("yMovement");
        private static readonly int attackHash = Animator.StringToHash("attack");

        #endregion Animator Hash

        private void Awake()
        {
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
            playerControls.Gameplay.Tap.performed += ctx => OnTap(ctx);
            playerControls.Gameplay.TouchStart.performed += ctx => OnTouchBegin(ctx);
            playerControls.Gameplay.TouchMove.performed += ctx => OnTouchMoved(ctx);
            playerControls.Gameplay.TouchEnd.performed += ctx => OnTouchEnded(ctx);
            //playerControls.Gameplay.StartDrag.performed += ctx => OnStartDrag(ctx);
            //playerControls.Gameplay.EndDrag.performed += ctx => OnEndDrag(ctx);
        }

        #region Input Events

        private void OnTap(InputAction.CallbackContext ctx)
        {
            if (touchPosition.y >= Screen.height * 0.5f) return;

            tap = true;
            Debug.Log("TAP");
        }

        private void OnTouchBegin(InputAction.CallbackContext ctx)
        {
            if (touchDetected) return; //If this is not null, a finger is already using the joystick so ignore the new one

            if (touchPosition.y >= Screen.height * 0.5f) return; //Ignore if the finger is higher than half the screen

            //Debug.Log("touched");
            touchDetected = true;
            movement = Vector2.zero;
            joystick.gameObject.SetActive(true);
            joystick.JoystickRectTransform.sizeDelta = joystickSize;
            joystick.JoystickRectTransform.anchoredPosition = ClampJoystickStartPositionOnScreen(touchPosition);
        }

        private void OnTouchMoved(InputAction.CallbackContext ctx)
        {
            //if (!touchDetected) return;

            Debug.Log("move");
            Vector2 knobPosition;
            float maxMovement = joystickSize.x / 2f;

            touchPosition = ctx.ReadValue<Vector2>();

            if (!joystick.gameObject.activeInHierarchy) return;

            if (Vector2.Distance(touchPosition, joystick.JoystickRectTransform.anchoredPosition) > maxMovement)
            {
                knobPosition = (touchPosition - joystick.JoystickRectTransform.anchoredPosition).normalized * maxMovement;
            }
            else
            {
                knobPosition = touchPosition - joystick.JoystickRectTransform.anchoredPosition;
            }

            joystick.Knob.anchoredPosition = knobPosition;
            movement = knobPosition / maxMovement; //returns a Vector2 with values between -1 and 1
        }

        private void OnTouchEnded(InputAction.CallbackContext ctx)
        {   
            //if (touchDetected == false) return;

            //Debug.Log("up");
            touchDetected = false;
            joystick.Knob.anchoredPosition = Vector2.zero;
            joystick.gameObject.SetActive(false);
            movement = Vector2.zero;
        }

        private void OnStartDrag(InputAction.CallbackContext ctx)
        {
            dragStartPosition = touchPosition;
        }

        private void OnEndDrag(InputAction.CallbackContext ctx)
        {
            Vector2 touchDelta = touchPosition - dragStartPosition;
            float sqrDistance = touchDelta.sqrMagnitude;

            if (sqrDistance > (minTapToSwipeDistance * minTapToSwipeDistance))
            {
                swipeDirection = touchDelta;
            }

            dragStartPosition = Vector2.zero;
        }

        #endregion Input Events

        private Vector2 ClampJoystickStartPositionOnScreen(Vector2 startPosition)
        {
            if (startPosition.x < joystickSize.x * 0.5f)
            {
                startPosition.x = joystickSize.x * 0.5f;
            }
            else if (startPosition.x > Screen.width - joystickSize.x * 0.5f)
            {
                startPosition.x = Screen.width - joystickSize.x * 0.5f;
            }

            if (startPosition.y < joystickSize.y * 0.5f)
            {
                startPosition.y = joystickSize.y * 0.5f;
            }

            return startPosition;
        }
    }
}