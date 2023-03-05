using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCells.Entities.Player.Input
{
    public interface IInputHandler
    {
        public Vector2 MovementInput { get; }
        public bool AttackInput { get; }
        public bool DodgeInput { get; }
    }
}