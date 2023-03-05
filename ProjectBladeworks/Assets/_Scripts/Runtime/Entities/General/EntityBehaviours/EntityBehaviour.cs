using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCells.Entities.Behaviour
{
    public abstract class EntityBehaviour : MonoBehaviour
    {
        private EntityBehaviourManager _entityBehaviourManager;
        private EntityBehaviourManager entityBehaviourManager => _entityBehaviourManager ??= GetComponentInParent<EntityBehaviourManager>();

        private void Awake()
        {
            entityBehaviourManager.AddBehaviour(this);
        }
    }
}