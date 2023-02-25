using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData
{
    #region Common Data

    public const float Gravity = -9.8f;
    public const float GroundCheckDistance = 0.5f;
    public const float MoveAnimationDampTime = 0.15f;

    #endregion Common Data

    #region Layer Masks

    public static readonly LayerMask GroundLayer = LayerMask.NameToLayer("Ground");

    #endregion Layer Masks

    #region Animator Hashes

    public static readonly int XMovementHash = Animator.StringToHash("xMovement");
    public static readonly int YMovementHash = Animator.StringToHash("yMovement");
    public static readonly int AttackHash = Animator.StringToHash("attack");
    public static readonly int CurrentComboHash = Animator.StringToHash("currentCombo");
    public static readonly int TriggerComboHash = Animator.StringToHash("triggerCombo");

    #endregion Animator Hashes

}
