using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData
{
    public const float moveAnimationDampTime = 0.15f;

    #region Animator Hashes

    public static readonly int xMovementHash = Animator.StringToHash("xMovement");
    public static readonly int yMovementHash = Animator.StringToHash("yMovement");
    public static readonly int attackHash = Animator.StringToHash("attack");

    #endregion Animator Hashes
}
