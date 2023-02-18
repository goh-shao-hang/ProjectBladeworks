using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Etouch = UnityEngine.InputSystem.EnhancedTouch;

namespace GameCells.Player.Input
{
    public class TouchInputJoystick : MonoBehaviour, IInputHandler
    {
        [SerializeField] private Vector2 joystickSize = new Vector2(300, 300);
        [SerializeField] private Joystick joystick;

        private Finger movementFinger;
        private Vector2 movement;
        public Vector2 Movement => movement;
        private bool tap;
        public bool Tap => tap;

        private void OnEnable()
        {
            EnhancedTouchSupport.Enable();
            Etouch.Touch.onFingerDown += OnFingerDown;
            Etouch.Touch.onFingerUp += OnFingerUp;
            Etouch.Touch.onFingerMove += OnFingerMove;
        }

        private void OnDisable()
        {
            Etouch.Touch.onFingerDown -= OnFingerDown;
            Etouch.Touch.onFingerUp -= OnFingerUp;
            Etouch.Touch.onFingerMove -= OnFingerMove;
            EnhancedTouchSupport.Disable();
        }

        private void OnFingerDown(Finger newFinger)
        {
            if (movementFinger != null) return; //If this is not null, a finger is already using the joystick so ignore the new one

            if (newFinger.screenPosition.y >= Screen.height * 0.5f) return; //Ignore if the finger is higher than half the screen

            //Debug.Log("down");
            movementFinger = newFinger;
            movement = Vector2.zero;
            joystick.gameObject.SetActive(true);
            joystick.JoystickRectTransform.sizeDelta = joystickSize;
            joystick.JoystickRectTransform.anchoredPosition = ClampJoystickStartPositionOnScreen(newFinger.screenPosition);
        }

        private void OnFingerUp(Finger lostFinger)
        {
            if (lostFinger == movementFinger)
            {
                //Debug.Log("up");
                movementFinger = null;
                joystick.Knob.anchoredPosition = Vector2.zero;
                joystick.gameObject.SetActive(false);
                movement = Vector2.zero;
            }
        }

        private void OnFingerMove(Finger movingFinger)
        {
            if (movingFinger == movementFinger)
            {
                //Debug.Log("move");
                Vector2 knobPosition;
                float maxMovement = joystickSize.x / 2f;

                if (Vector2.Distance(movementFinger.currentTouch.screenPosition, joystick.JoystickRectTransform.anchoredPosition) > maxMovement)
                {
                    knobPosition = (movementFinger.currentTouch.screenPosition - joystick.JoystickRectTransform.anchoredPosition).normalized * maxMovement;
                }
                else
                {
                    knobPosition = movementFinger.currentTouch.screenPosition - joystick.JoystickRectTransform.anchoredPosition;
                }

                joystick.Knob.anchoredPosition = knobPosition;
                movement = knobPosition / maxMovement; //returns a Vector2 with values between -1 and 1
            }
        }

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