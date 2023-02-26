using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCells.Player.Input
{
    public class Joystick : MonoBehaviour
    {
        private RectTransform rectTransform;
        [SerializeField] private RectTransform knob;

        public RectTransform JoystickRectTransform => rectTransform ??= GetComponent<RectTransform>();
        public RectTransform Knob => knob;
    }
}