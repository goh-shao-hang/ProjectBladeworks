using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCells.Player.Input
{
    public interface IInputHandler
    {
        public Vector2 Movement { get; }
        public bool Tap { get; }
    }
}