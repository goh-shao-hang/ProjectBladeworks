using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AnimationEventsExtension
{
    /// <summary>
    /// Adds a animation event to the specified animationClip.
    /// </summary>
    /// <param name="animationClip"></param>
    /// <param name="normalizedTime"></param>
    /// <param name="functionName"></param>
    public static void AddAnimationEvent(this AnimationClip animationClip, float time, string functionName)
    {
        if (time < 0f || time > animationClip.length)
        {
            Debug.LogError($"Invalid event time for added animation clip {animationClip.name}!");
        }

        AnimationEvent animationEvent = new AnimationEvent()
        {
            time = time,
            functionName = functionName
        };

        animationClip.AddEvent(animationEvent);
    }

    /// <summary>
    /// Adds a animation event to the specified animationClip in the normalized time.
    /// </summary>
    /// <param name="animationClip"></param>
    /// <param name="normalizedTime"></param>
    /// <param name="functionName"></param>
    public static void AddAnimationEventNormalizedTime(this AnimationClip animationClip, float normalizedTime, string functionName)
    {
        float eventTime = animationClip.length * normalizedTime;
        if (eventTime < 0f || eventTime > 1f)
        {
            Debug.LogError($"Invalid event time for added animation clip {animationClip.name}!");
        }

        AnimationEvent animationEvent = new AnimationEvent
        {
            time = animationClip.length * normalizedTime,
            functionName = functionName
        };

        animationClip.AddEvent(animationEvent);
    }
}
