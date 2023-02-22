using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCells.Core
{
    public class GameEvent
    {
        private event Action gameEvent;

        public void Invoke()
        {
            gameEvent.Invoke();
        }

        public void AddSubscriber(Action subscriber)
        {
            gameEvent += subscriber;
        }

        public void RemoveSubscriber(Action subscriber)
        {
            gameEvent -= subscriber;
        }
    }
}