using GameCells.Entities.Behaviour;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GameCells.Entities
{
    public class Entity : MonoBehaviour
    {
        [SerializeField] 
        private SO_EntityData _entityData;
        private EntityBehaviourManager _entityBehaviourManager;

        public SO_EntityData EntityData => _entityData;
        public EntityBehaviourManager EntityBehaviourManager => _entityBehaviourManager ??= GetComponentInChildren<EntityBehaviourManager>();
    }
}