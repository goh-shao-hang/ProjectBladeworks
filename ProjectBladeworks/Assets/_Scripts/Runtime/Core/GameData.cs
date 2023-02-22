using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData
{
    public const float moveAnimationDampTime = 0.15f;

    #region Animator Hashes

    public static readonly int isMovingHash = Animator.StringToHash("isMoving");
    public static readonly int xMovementHash = Animator.StringToHash("xMovement");
    public static readonly int yMovementHash = Animator.StringToHash("yMovement");
    public static readonly int attackHash = Animator.StringToHash("attack");
    public static readonly int isAttackingHash = Animator.StringToHash("isAttacking");
    public static readonly int currentComboHash = Animator.StringToHash("currentCombo");
    public static readonly int triggerComboHash = Animator.StringToHash("triggerCombo");

    #endregion Animator Hashes
}
