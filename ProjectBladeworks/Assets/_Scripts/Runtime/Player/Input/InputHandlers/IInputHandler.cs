using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCells.Player.Input
{
    public interface IInputHandler
    {
        public Vector2 MovementInput { get; }
        public bool AttackInput { get; }
        public bool DodgeInput { get; }
    }
}