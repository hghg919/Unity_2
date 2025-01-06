using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Debugger 
{
    public static void Log(object message)
    {
        if (!IsDebugMode()) return;

        Debug.Log(message);
    }

    public static bool IsDebugMode()
    {
#if UNITY_EDITOR
        return true;
#else
        return false;
#endif
    }
}
