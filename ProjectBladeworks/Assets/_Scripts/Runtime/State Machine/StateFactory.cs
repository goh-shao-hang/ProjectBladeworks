using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateFactory
{
    protected FiniteStateMachine _context;

    public StateFactory(FiniteStateMachine context)
    {
        this._context = context;
    }
}
