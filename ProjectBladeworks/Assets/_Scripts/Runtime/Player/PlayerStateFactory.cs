using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCells.Player
{
    public class PlayerStateFactory : StateFactory
    {
        private Player _player;

        public PlayerStateFactory(FiniteStateMachine context, Player player) : base(context)
        {
            this._player = player;
        }

        public BaseState Idle => new PlayerIdleState(_context, this, _player);
        public BaseState Move => new PlayerMoveState(_context, this, _player);
    }
}