using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AndroidNativeRuntimeProfilerFactory : INativeRuntimeProfilerFactory
{
    INativeRuntimeProfilerReadable INativeRuntimeProfilerFactory.GetInstance()
    {
        return new AndroidNativeRuntimeProfiler();
    }
}
