using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GameCells.Entities.Behaviour
{
    public class EntityBehaviourManager : MonoBehaviour
    {
        private readonly List<EntityBehaviour> _entityBehaviours = new List<EntityBehaviour>();

        public void AddBehaviour(EntityBehaviour behaviour)
        {
            if (!_entityBehaviours.Contains(behaviour))
            {
                _entityBehaviours.Add(behaviour);
            }
        }

        public T GetEntityBehaviour<T>() where T: EntityBehaviour
        {
            var behaviour = _entityBehaviours.OfType<T>().FirstOrDefault();
            if (behaviour == null)
            {
                Debug.LogWarning($"{typeof(T)} not found on {transform.parent.name}");
            }

            return behaviour;
        }
    }
}