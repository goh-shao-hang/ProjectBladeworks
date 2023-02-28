using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootMotionManager : MonoBehaviour
{
    [SerializeField] private Transform rootTransform;
    [SerializeField] private Animator _animator;
    [SerializeField] private float animationMovementExaggerationMultiplier = 1f;
    
    private bool _allowAnimationMovement = false;

    public void AllowAnimationMovement(bool allow)
    {
        _allowAnimationMovement = allow;
    }

    private void OnAnimatorMove()
    {
        if (_allowAnimationMovement)
        {
            rootTransform.position += _animator.deltaPosition * animationMovementExaggerationMultiplier;
        }
    }
}
