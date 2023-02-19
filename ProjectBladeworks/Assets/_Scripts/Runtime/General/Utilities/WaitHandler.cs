using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitHandler
{
    private static readonly Dictionary<float, WaitForSeconds> waitForSecondsDict = new Dictionary<float, WaitForSeconds>();

    public static WaitForSeconds GetWaitForSeconds(float duration)
    {
        if (waitForSecondsDict.TryGetValue(duration, out WaitForSeconds wait)) return wait;

        waitForSecondsDict[duration] = new WaitForSeconds(duration);
        return waitForSecondsDict[duration];
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    private static void ResetWaitDictionaries()
    {
        waitForSecondsDict.Clear();
        Debug.Log("Dictionaries resetted.");
    }
}
