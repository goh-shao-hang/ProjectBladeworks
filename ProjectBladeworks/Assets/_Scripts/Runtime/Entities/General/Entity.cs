using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Entity : MonoBehaviour
{
    //Entity Components
    private CharacterMovement _characterMovement;

    //Component Getters
    public CharacterMovement CharacterMovement => _characterMovement ??= GetComponent<CharacterMovement>();
}
