namespace GameCells.Player
{
    public class PlayerStateFactory : StateFactory
    {
        private Player _player;

        public PlayerStateFactory(FiniteStateMachine context, Player player) : base(context)
        {
            this._player = player;
        }

        public BaseState Idle => new PlayerIdleState(_context, _player);
        public BaseState Move => new PlayerMoveState(_context, _player);
        public BaseState Attack => new PlayerAttackState(_context, _player);
    }
}