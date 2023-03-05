using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField] private SO_EntityData _entityData;
    private EB_CharacterMovement _characterMovement;

    public SO_EntityData EntityData => _entityData;
    public EB_CharacterMovement CharacterMovement => _characterMovement ??= GetComponent<EB_CharacterMovement>();
}
