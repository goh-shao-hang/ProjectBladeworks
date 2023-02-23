using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCells.Player
{
    public class GroundChecker : MonoBehaviour, ICollisionDetector
    {
        [SerializeField] private Transform _groundCheck;

        public bool CollisionDetected => Physics.Raycast(this._groundCheck.position, -transform.up, GameData.GroundCheckDistance, GameData.GroundLayer);
    }
}