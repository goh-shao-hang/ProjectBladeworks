using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCells.Core
{
    public class InGameEventsManager : Singleton<InGameEventsManager>
    {
        #region Player Events

        //Weapon Events
        public readonly GameEvent OnComboFinished = new GameEvent();
        public readonly GameEvent OnAllowNextCombo = new GameEvent();

    #endregion Player Events
    }
}